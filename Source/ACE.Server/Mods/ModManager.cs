using ACE.Mod;
using ACE.Server.Command;
using HarmonyLib;
using log4net;
using McMaster.NETCore.Plugins;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mono.Cecil.Cil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace ACE.Server.Mod
{
    public static class ModManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string ModDirectory { get; } = @"C:\ACE\Mods\";

        /// <summary>
        /// Mods with at least metadata loaded
        /// </summary>
        private static List<ModContainer> _mods = new();

        public static void Initialize() {
            FindMods();
            EnableAllMods(_mods);
        }
        internal static bool Test(int i = 1)
        {
            log.Info($"Test {i}");
            return true;
        }
        

        public static void ListMods()
        {
            foreach (var mod in _mods)
            {
                var meta = mod.ModMetadata;
                Console.WriteLine($"{meta.Name} is {(meta.Enabled ? "Enabled" : "Disabled")}\r\n\tSource: {mod.FolderPath}\r\n\tStatus: {mod.Status}");
            }
        }

        #region Load
        /// <summary>
        /// Finds all valid mods in the mod directory and attempts to load them.
        /// </summary>
        public static void FindMods()
        {
            _mods = LoadModEntries(ModDirectory);
            _mods = _mods.OrderByDescending(x => x.ModMetadata.Priority).ToList();

            //Todo: Filter out bad mods here or when loading entries?
            //CheckDuplicateNames(_mods);

            ListMods();
            //EnableAllMods(ModManager._mods);
        }

        /// <summary>
        /// Loads all mods that have a valid Meta.json and correctly named DLL
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static List<ModContainer> LoadModEntries(string directory, bool unpatch = true)
        {
            //Todo: decide if this should always be done?
            if (unpatch)
            {
                UnpatchAllMods();
            }

            var entries = LoadAllMetadata(directory);
            foreach(var entry in entries)
            {
                LoadMod(entry);
            }
            return entries;
        }

        /// <summary>
        /// Sets up PluginLoader and IHarmonyMod type for mod entry if possible
        /// </summary>
        /// <param name="entry"></param>
        private static void LoadMod(ModContainer entry)
        {
            var folderName = new DirectoryInfo(entry.FolderPath).Name;
            var dllPath = Path.Combine(entry.FolderPath, folderName + ".dll");


            //entry.
            entry.Initialize();
            return;

            //if (!File.Exists(dllPath))
            //{
            //    log.Warn($"Nothing loaded for missing mod: {dllPath}");
            //    return;
            //}

            ////Todo: decide if assemblies should be loaded if mods are inactive.  Currently using Unloaded
            //if (!entry.ModMetadata.Enabled)
            //{
            //    log.Info($"Nothing loaded for disabled mod: {dllPath}");
            //    return;
            //}

            //try
            //{
            //    //Todo: do this without locking dll for hot reload
            //    var modDll = Assembly.UnsafeLoadFrom(dllPath);

            //    //Non-explicit version with possibility of IHarmonyMod types
            //    //var types = modDll.GetTypes();
            //    //modTypes = modDll.GetTypes().Where(t => typeof(IHarmonyMod).IsAssignableFrom(t)).ToList();

            //    //Safer to use the dll to get the type than using convention
            //    var typeName = modDll.ManifestModule.ScopeName.Replace(".dll", "." + ModMetadata.TYPENAME);
            //    entry.ModType = modDll.GetType(typeName);

            //    if (entry.ModType is null)
            //    {
            //        entry.Status = ModStatus.LoadFailure;
            //        log.Warn($"Missing IHarmonyMod Type {typeName} from {modDll}");
            //    }
            //    else
            //    {
            //        entry.Status = ModStatus.Inactive;
            //    }
            //}
            //catch (Exception e)
            //{
            //    entry.Status = ModStatus.LoadFailure;
            //    log.Error($"Failed to load mod file `{dllPath}`: {e}");
            //}
        }

        /// <summary>
        /// Loads all valid metadata from folders in a given directory as ModContainer
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static List<ModContainer> LoadAllMetadata(string directory)
        {
            var loadedMods = new List<ModContainer>();

            //Structure is /modDir/<AssemblyName>/<AssemblyName.dll> and Meta.json
            foreach (var modDir in Directory.GetDirectories(directory))
            {
                var metadataPath = Path.Combine(modDir, ModMetadata.FILENAME);

                if (!TryLoadModContainer(metadataPath, out var entry))
                {
                    continue;
                }

                loadedMods.Add(entry);
            }

            return loadedMods;
        }

        /// <summary>
        /// Loads metadata from specified ..\Meta.json file.  Fails if missing or invalid.
        /// </summary>
        /// <param name="metadataPath"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        private static bool TryLoadModContainer(string metadataPath, out ModContainer entry)
        {
            entry = null;

            if (!File.Exists(metadataPath))
            {
                //Log missing metadata
                log.Warn($"Metadata not found at: {metadataPath}");
                return false;
            }

            try
            {
                var metadata = JsonConvert.DeserializeObject<ModMetadata>(File.ReadAllText(metadataPath));

                entry = new ModContainer()
                {
                    ModMetadata = metadata,
                    FolderPath = Path.GetDirectoryName(metadataPath),    //Todo: would dll/metadata path make more sense?
                };

                return true;
            }
            catch (Exception ex)
            {
                log.Error($"Unable to deserialize mod metadata from: {metadataPath}");
                return false;
            }
        }

        //private static void CheckDuplicateNames(List<ModContainer> mods)
        //{
        //    foreach (var group in mods.OrderByDescending(x => x.ModMetadata.Priority).GroupBy(m => m.ModMetadata.Name))
        //    {
        //        //First is highest priority mod with a name, flag any others
        //        foreach (var mod in group.Skip(1))
        //        {
        //            log.Error($"Duplicate mod found: {mod.ModMetadata.Name}");
        //            mod.Status = ModStatus.NameConflict;
        //        }
        //    }
        //}
        #endregion

        #region Patch
        private static void EnableAllMods(List<ModContainer> mods)
        {
            foreach (var mod in mods.Where(m => m.Status == ModStatus.Inactive && m.ModMetadata.Enabled))
            {
                EnableMod(mod);
            }
        }

        public static void EnableModByName(string modName)
        {
            foreach (var mod in _mods.Where(x => x.ModMetadata.Name.Contains(modName)))
            {
                EnableMod(mod);
            }
        }

        public static void EnableMod(ModContainer mod)
        {
            try
            {
                if (mod.Status != ModStatus.Inactive)
                {
                    log.Info($"Skipping already active mod: {mod.ModMetadata.Name}");
                    return;
                }

                if (mod.Instance is null)
                {
                    mod.Instance = Activator.CreateInstance(mod.ModType) as IHarmonyMod;
                    log.Info($"Created instance of {mod.ModType.Name}");
                }

                mod.Instance?.Initialize();
                mod.Status = ModStatus.Active;

                log.Info($"Activated mod `{mod.ModMetadata.Name} (v{mod.ModMetadata.Version})`.");
            }
            catch (Exception ex)
            {
                log.Error($"Error patching {mod.ModMetadata.Name}: {ex}");
            }
        }
        #endregion

        #region Unpatch
        public static void UnpatchAllMods()
        {
            foreach (var mod in _mods)
            {
                UnpatchMod(mod);
            }
        }

        public static void UnpatchModByName(string modName)
        {
            foreach (var mod in _mods.Where(x => x.ModMetadata.Name.Contains(modName)))
            {
                UnpatchMod(mod);
            }
        }

        private static async void UnpatchMod(ModContainer mod)
        {
            try
            {
                if (mod.Status != ModStatus.Active)
                {
                    //log.Info($"Skipping {mod.ModMetadata.Name}");
                    return;
                }

                //mod.Instance?.Shutdown();
                mod.Instance?.Dispose();
                mod.Status = ModStatus.Inactive;
                log.Info($"Unpatching {mod.ModMetadata.Name}");
            }
            catch (Exception ex)
            {
                log.Error($"Error unpatching {mod.ModMetadata.Name}: {ex}");
            }
        }
        #endregion

        #region Helpers

        #endregion

        #region Hot Reload
        private static readonly bool HotReloadEnabled = true;
        public static FileSystemWatcher PluginWatcher = null;
        private static bool needsReload;
        private static DateTime lastFileChange;
        //System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

        //Version = ServerBuildInfo.GetVersionInfo()

        public static void Start()
        {
            var loader = PluginLoader.CreateFromAssemblyFile(ModDirectory,
                config => config.EnableHotReload = true);

            loader.Reloaded += ShowPluginInfo;

            var cts = new CancellationTokenSource();
        }

        static void ShowPluginInfo(object sender, PluginReloadedEventArgs eventArgs)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("HotReloadApp: ");
            Console.ResetColor();
            Console.WriteLine("plugin was reloaded");
            InvokePlugin(eventArgs.Loader);
        }

        static void InvokePlugin(PluginLoader loader)
        {
            var assembly = loader.LoadDefaultAssembly();
            assembly
                .GetType("TimestampedPlugin.InfoDisplayer", throwOnError: true)
                !.GetMethod("Print")
                !.Invoke(null, null);
        }

        #endregion

        //Shutdown
        //ServerManager.ShutdownServer

        public static void Log(string message)
        {
            log.Info(message);
        }
    }
}
