using HarmonyLib;
using log4net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mono.Cecil.Cil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ACE.Server.Mod
{
    public static class ModManager
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static string ModDirectory { get; } = @"C:\ACE\Mods\";

        /// <summary>
        /// Mods with at least metadata loaded
        /// </summary>
        private static List<ModEntry> _mods = new();

        public static void Initialize() { }

        public static void ListMods()
        {
            foreach (var mod in _mods)
            {
                var meta = mod.ModMetadata;
                Console.WriteLine($"{meta.Name} is {(meta.Enabled ? "Enabled" : "Disabled")}\r\n\tSource: {mod.Source}\r\n\tStatus: {mod.Status}");
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

            CheckDuplicateNames(_mods);

            ListMods();
            //EnableAllMods(ModManager._mods);
        }

        /// <summary>
        /// Loads all mods that have a valid Meta.json and correctly named DLL
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static List<ModEntry> LoadModEntries(string directory, bool unpatch = true)
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

            //var loadedMods = new List<ModEntry>();

            ////Structure is /modDir/<AssemblyName>/<AssemblyName.dll> and Meta.json
            //foreach (var modDir in Directory.GetDirectories(directory))
            //{
            //    //var metadataPath = Path.Combine(modDir, ModMetadata.FILENAME);
            //    var modName = new DirectoryInfo(modDir).Name;
            //    var dllPath = Path.Combine(modDir, modName + ".dll");

            //    if (!File.Exists(dllPath))
            //    {
            //        //Log missing mod
            //        continue;
            //    }

            //    //Todo: decide if assemblies should be loaded if mods are inactive.  Currently using Unloaded
            //    if (!metadata.Enabled)
            //    {
            //        loadedMods.Add(entry);
            //        continue;
            //    }

            //    //Load mod and get the type
            //    try
            //    {
            //        var modDll = Assembly.UnsafeLoadFrom(dllPath);

            //        //var types = modDll.GetTypes();
            //        //modTypes = modDll.GetTypes().Where(t => typeof(IHarmonyMod).IsAssignableFrom(t)).ToList();    //Non-explicit version with possibility of multiple types
            //        entry.ModType = modDll.GetType($"{modName}.{ModMetadata.TYPENAME}");

            //        if (entry.ModType is null)
            //        {
            //            entry.Status = ModStatus.LoadFailure;
            //            log.Warn($"Failed to load Type {modName}.{ModMetadata.TYPENAME} from {modDll}");
            //        }
            //        else
            //        {
            //            entry.Status = ModStatus.Inactive;
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        //Logger.Log($"Failed to load mod file `{dllPath}`: {e}");
            //        entry.Status = ModStatus.LoadFailure;
            //    }

            //    loadedMods.Add(entry);
            //}

            //return loadedMods;
        }

        /// <summary>
        /// Loads IHarmonyMod type for mod entry if available
        /// </summary>
        /// <param name="entry"></param>
        private static void LoadMod(ModEntry entry)
        {
            var folderName = new DirectoryInfo(entry.Source).Name;
            var dllPath = Path.Combine(entry.Source, folderName + ".dll");

            if (!File.Exists(dllPath))
            {
                log.Warn($"Nothing loaded for missing mod: {dllPath}");
                return;
            }

            //Todo: decide if assemblies should be loaded if mods are inactive.  Currently using Unloaded
            if (!entry.ModMetadata.Enabled)
            {
                log.Info($"Nothing loaded for disabled mod: {dllPath}");
                return;
            }

            try
            {
                //Todo: do this without locking dll for hot reload
                var modDll = Assembly.UnsafeLoadFrom(dllPath);

                //Non-explicit version with possibility of IHarmonyMod types
                //var types = modDll.GetTypes();
                //modTypes = modDll.GetTypes().Where(t => typeof(IHarmonyMod).IsAssignableFrom(t)).ToList();

                //Safer to use the dll to get the type than using convention
                var typeName = modDll.ManifestModule.ScopeName.Replace(".dll", "." + ModMetadata.TYPENAME);
                entry.ModType = modDll.GetType(typeName);

                if (entry.ModType is null)
                {
                    entry.Status = ModStatus.LoadFailure;
                    log.Warn($"Missing IHarmonyMod Type {typeName} from {modDll}");
                }
                else
                {
                    entry.Status = ModStatus.Inactive;
                }
            }
            catch (Exception e)
            {
                entry.Status = ModStatus.LoadFailure;
                log.Error($"Failed to load mod file `{dllPath}`: {e}");
            }
        }

        /// <summary>
        /// Loads all valid metadata from folders in a given directory as ModEntry
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static List<ModEntry> LoadAllMetadata(string directory)
        {
            var loadedMods = new List<ModEntry>();

            //Structure is /modDir/<AssemblyName>/<AssemblyName.dll> and Meta.json
            foreach (var modDir in Directory.GetDirectories(directory))
            {
                var metadataPath = Path.Combine(modDir, ModMetadata.FILENAME);

                if (!TryLoadModEntry(metadataPath, out var entry))
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
        private static bool TryLoadModEntry(string metadataPath, out ModEntry entry)
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

                entry = new ModEntry()
                {
                    ModMetadata = metadata,
                    Source = Path.GetDirectoryName(metadataPath),    //Todo: would dll/metadata path make more sense?
                };

                return true;
            }
            catch (Exception ex)
            {
                log.Error($"Unable to deserialize mod metadata from: {metadataPath}");
                return false;
            }
        }

        private static void CheckDuplicateNames(List<ModEntry> mods)
        {
            foreach (var group in mods.OrderByDescending(x => x.ModMetadata.Priority).GroupBy(m => m.ModMetadata.Name))
            {
                //First is highest priority mod with a name, flag any others
                foreach (var mod in group.Skip(1))
                {
                    log.Error($"Duplicate mod found: {mod.ModMetadata.Name}");
                    mod.Status = ModStatus.NameConflict;
                }
            }
        }
        #endregion

        #region Patch
        private static void EnableAllMods(List<ModEntry> mods)
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

        public static void EnableMod(ModEntry mod)
        {
            try
            {
                if (mod.Status != ModStatus.Inactive)
                {
                    log.Info($"Skipping already active mod: {mod.ModMetadata.Name}");
                    return;
                }

                if (mod.ModInstance is null)
                {
                    mod.ModInstance = Activator.CreateInstance(mod.ModType) as IHarmonyMod;
                    log.Info($"Created instance of {mod.ModType.Name}");
                }

                mod.ModInstance?.Initialize();
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

        private static void UnpatchMod(ModEntry mod)
        {
            try
            {
                if (mod.Status != ModStatus.Active)
                {
                    //log.Info($"Skipping {mod.ModMetadata.Name}");
                    return;
                }

                mod.ModInstance?.Shutdown();
                mod.Status = ModStatus.Inactive;
                log.Info($"Unpatching {mod.ModMetadata.Name}");
            }
            catch (Exception ex)
            {
                log.Error($"Error unpatching {mod.ModMetadata.Name}: {ex}");
            }
        }
        #endregion

        #region Hot Reload
        private static readonly bool HotReloadEnabled = true;
        public static FileSystemWatcher PluginWatcher = null;
        private static bool needsReload;
        private static DateTime lastFileChange;
        //System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

        //Version = ServerBuildInfo.GetVersionInfo()

        private static void PluginWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (!HotReloadEnabled)
                    return;
                if (needsReload == false)
                {
                    //Start reload
                    //Core.RenderFrame += Core_RenderFrame;
                }
                needsReload = true;
                //Gate by time?
                lastFileChange = DateTime.UtcNow;
            }
            catch (Exception ex) { }
        }

        private static void LoadPluginAssembly()
        {
            try
            {
                if (HotReloadEnabled && PluginWatcher == null)
                {
                    PluginWatcher = new FileSystemWatcher();
                    PluginWatcher.Path = ModDirectory;
                    PluginWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;
                    PluginWatcher.Filter = "*.dll"; //Todo: rethink this
                    PluginWatcher.Changed += PluginWatcher_Changed;
                    PluginWatcher.EnableRaisingEvents = true;
                }
                //Check for whether mod is loaded?
                //if (PluginInstance != null)
                //{
                //    LogError("************* Attempt to LoadPluginAssembly() when PluginInstance != null! ***************");
                //    UnloadPluginAssembly();
                //}

                //var assemblyPath = System.IO.File.ReadAllBytes(PluginAssemblyPath);
                //var pdbPath = System.IO.File.ReadAllBytes(PluginAssemblyPath.Replace(".dll", ".pdb"));
                //CurrentAssembly = Assembly.Load(assemblyPath, pdbPath);
                //PluginType = CurrentAssembly.GetType(PluginAssemblyNamespace);
                //MethodInfo startupMethod = PluginType.GetMethod("Startup");
                //PluginInstance = Activator.CreateInstance(PluginType);
                //startupMethod.Invoke(PluginInstance, new object[] {
                //    PluginAssemblyPath,
                //    Global.PluginStorageDirectory.Value,
                //    Global.DatabaseFile.Value,
                //    Host,
                //    Core
                //});

                //hasLoaded = true;
            }
            catch (Exception ex) { }
        }

        private static void UnloadPluginAssembly()
        {
            try
            {
                //if (PluginInstance != null && PluginType != null)
                //{
                //    MethodInfo shutdownMethod = PluginType.GetMethod("Shutdown");
                //    shutdownMethod.Invoke(PluginInstance, null);
                //    PluginInstance = null;
                //    CurrentAssembly = null;
                //    PluginType = null;
                //}
            }
            catch (Exception ex) { }
        }
        #endregion

        //Shutdown
        //ServerManager.ShutdownServer

        public static void Log(string message)
        {
            log.Info(message);
        }

        public static void Test()
        {
            Console.WriteLine("Test");
        }
    }
}
