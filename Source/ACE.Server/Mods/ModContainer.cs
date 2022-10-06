using ACE.Adapter.GDLE.Models;
using ACE.Mod;
using ACE.Server.Command;
using log4net;
using McMaster.NETCore.Plugins;
using System;

using System.IO;
using System.Linq;
using System.Reflection;

namespace ACE.Server.Mod
{
    public class ModContainer
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly TimeSpan RELOAD_TIMEOUT = TimeSpan.FromSeconds(3);

        public ModMetadata ModMetadata { get; set; }
        public ModStatus Status = ModStatus.Unloaded;

        public Assembly ModAssembly { get; set; }   //Todo: decide what actually makes sense to keep
        public Type ModType { get; set; }
        public IHarmonyMod Instance { get; set; }


        // C:\ACE\Mods\SomeMod
        public string FolderPath { get; set; }
        // SomeMod
        public string FolderName //{ get; private set; }      
                => new DirectoryInfo(FolderPath).Name;
        // C:\ACE\Mods\SomeMod\Somedll
        public string DllPath =>
                Path.Combine(FolderPath, FolderName + ".dll");
        // C:\ACE\Mods\SomeMod\Meta.json
        public string MetadataPath =>
                Path.Combine(FolderPath, "Meta.json");
        // MyModNamespace.Mod
        public string TypeName =>
            ModAssembly.ManifestModule.ScopeName.Replace(".dll", "." + ModMetadata.TYPENAME);

        public PluginLoader Loader { get; private set; }
        private FileSystemWatcher _dllWatcher;
        private DateTime _lastChange = DateTime.Now;

        /// <summary>
        /// Sets up mod watchers for a valid mod Meta.json
        /// </summary>
        public void Initialize()
        {
            //Todo: checks n all that jazz

            _dllWatcher = new FileSystemWatcher()
            {
                Path = FolderPath,
                //Filter = DllPath,
                Filter = $"{FolderName}.dll",
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite  //?
            };
            //_dllWatcher.Changed += ModDll_Changed;
            //_dllWatcher.Created += ModDll_Created;
            //_dllWatcher.Renamed += ModDll_Renamed;
            //_dllWatcher.Deleted += ModDll_Changed;
            //_dllWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;

            Loader = PluginLoader.CreateFromAssemblyFile(
                assemblyFile: DllPath,
                isUnloadable: true,
                sharedTypes: new[] { typeof(IHarmonyMod) },
                configure: config =>
                {
                    config.EnableHotReload = true;
                    config.IsLazyLoaded = false;     //?
                }
            );
            Loader.Reloaded += Reload;

            log.Info($"Set up {FolderName}");

            Start();
        }

        private void Start()
        {
            CreateModInstance();
            EnableMod();
        }

        private void Shutdown()
        {
            log.Info($"{FolderName} shutting down @ {DateTime.Now}");
            Instance?.Dispose();
            Instance = null;
            Status = ModStatus.Unloaded;
        }

        private void CreateModInstance()
        {
            if (!File.Exists(DllPath))
            {
                log.Warn($"Missing mod: {DllPath}");
                return;
            }

            //Todo: decide if assemblies should be loaded if mods are inactive.  Currently using Unloaded
            if (!ModMetadata.Enabled)
            {
                log.Info($"Instance not created for disabled mod: {FolderName}");
                return;
            }

            try
            {
                ModAssembly = Loader.LoadDefaultAssembly();

                //Safer to use the dll to get the type than using convention
                ModType = ModAssembly.GetType(TypeName);

                //Non-explicit version with possibility of IHarmonyMod types
                //var types = ModAssembly.GetTypes().Where(t => typeof(IHarmonyMod).IsAssignableFrom(t) && !t.IsAbstract).ToList();

                if (ModType is null)
                {
                    Status = ModStatus.LoadFailure;
                    log.Warn($"Missing IHarmonyMod Type {TypeName} from {ModAssembly}");
                }
                else
                {
                    Status = ModStatus.Inactive;
                }
            }
            catch (Exception e)
            {
                Status = ModStatus.LoadFailure;
                log.Error($"Failed to load mod file `{DllPath}`: {e}");
            }
        }

        private void EnableMod()
        {
            try
            {
                if (Status != ModStatus.Inactive)
                {
                    log.Info($"Skipping already active mod: {ModMetadata.Name}");
                    return;
                }

                if (Instance is null)
                {
                    Instance = Activator.CreateInstance(ModType) as IHarmonyMod;
                    log.Info($"Created instance of {ModType.Name}");
                }

                Instance?.Initialize();
                Status = ModStatus.Active;

                log.Info($"Activated mod `{ModMetadata.Name} (v{ModMetadata.Version})`.");
            }
            catch (Exception ex)
            {
                log.Error($"Error patching {ModMetadata.Name}: {ex}");
            }

        }



        #region Events
        //If Loader has hot reload enabled this triggers after the assembly is loaded again (after GC)
        private void Reload(object sender, PluginReloadedEventArgs eventArgs)
        {
            var lapsed = DateTime.Now - _lastChange;
            if (lapsed < RELOAD_TIMEOUT)
            {
                //log.Info($"Not reloading {FolderName}: {lapsed.TotalSeconds}/{RELOAD_TIMEOUT.TotalSeconds}");
                //return;
                Shutdown();
            }

            Start();
            log.Info($"Reloaded {FolderName} @ {DateTime.Now} after {lapsed.TotalSeconds}/{RELOAD_TIMEOUT.TotalSeconds} seconds");

            //log.Info($"Reloaded {FolderPath}");
        }


        private void ModDll_Changed(object sender, FileSystemEventArgs e)
        {
            //Todo: Rethink reload in progress?
            var lapsed = DateTime.Now - _lastChange;
            if (lapsed < RELOAD_TIMEOUT)
            {
                //log.Info($"Not reloading {FolderName}: {lapsed.TotalSeconds}/{RELOAD_TIMEOUT.TotalSeconds}");
                return;
            }

            log.Info($"{FolderName} changed @ {DateTime.Now} after {lapsed.TotalMilliseconds}ms");
            _lastChange = DateTime.Now;

            Shutdown();
        }
        #endregion
    }

    public enum ModStatus
    {
        Unloaded,	        //Assembly not loaded
        Active,		        //Loaded and active
        Inactive,           //Loaded and activatable
        LoadFailure,        //Failed to load assembly
        //NameConflict,       //Mod loaded but a higher priority mod has the same name
        //MissingDependency,  //Keeping it simple for now
        //Conflict,           //Loaded and conflict detected
    }
}
