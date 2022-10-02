using HarmonyLib;
using log4net;
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

        //Version = ServerBuildInfo.GetVersionInfo()

        public static string ModsDirectory { get; } = @"C:\ACE\Mods\";

        /// <summary>
        /// Mods with at least metadata loaded
        /// </summary>
        private static List<ModEntry> _mods = new ();

        public static void Initialize() { }

        public static void ListMods()
        {
            foreach(var mod in _mods)
            {
                var meta = mod.ModMetadata;
                Console.WriteLine($"{meta.Name} is {(meta.Enabled ? "Enabled" : "Disabled")}\r\n\tSource: {mod.Source}\r\n\tStatus: {mod.Status}");
            }
        }

        #region Enable
        private static void EnableAllMods(List<ModEntry> mods)
        {
            foreach (var mod in mods.Where(m => m.Status == ModStatus.Inactive && m.ModMetadata.Enabled).OrderByDescending(x => x.ModMetadata.Priority))
            {
                try
                {
                    mod.ModInstance = Activator.CreateInstance(mod.ModType) as IHarmonyMod;
                    mod.ModInstance.Initialize();

                    Console.WriteLine($"Activated mod `{mod.ModMetadata.Name} (v{mod.ModMetadata.Version})`.");
                }
                catch (Exception ex)
                {
                    //Todo: Log
                }
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
                    //log.Info($"Skipping {mod.ModMetadata.Name}");
                    return;
                }

                log.Info($"Patching {mod.ModMetadata.Name}");
                mod.ModInstance?.Shutdown();
            }
            catch (Exception ex)
            {
                log.Error($"Error patching {mod.ModMetadata.Name}: {ex}");
            }
        }
        #endregion

        #region Load
        public static void LoadMods()
        {
            _mods = LoadFromDirectory(ModsDirectory);
            _mods = _mods.OrderByDescending(x => x.ModMetadata.Priority).ToList();

            CheckDuplicateNames(_mods);

            ListMods();
            EnableAllMods(ModManager._mods);
        }

        /// <summary>
        /// Loads all mods that have a valid Meta.json and correctly named DLL
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static List<ModEntry> LoadFromDirectory(string directory, bool unpatch = true)
        {
            if (unpatch)
                UnpatchAllMods();

                var loadedMods = new List<ModEntry>();

            //Structure is /modDir/<AssemblyName>/<AssemblyName.dll> and Meta.json
            foreach (var modDir in Directory.GetDirectories(directory))
            {
                var modName = new DirectoryInfo(modDir).Name;
                var dllPath = Path.Combine(modDir, modName + ".dll");
                var metadataPath = Path.Combine(modDir, ModMetadata.FILENAME);

                if (!File.Exists(metadataPath))
                {
                    //Log missing metadata
                    continue;
                }
                if (!File.Exists(dllPath))
                {
                    //Log missing mod
                    continue;
                }

                //Get metadata for mod
                if (!TryLoadMetadata(metadataPath, out var metadata))
                {
                    //Have to at least have valid metadata to be added to list of mods
                    continue;
                }

                //Skip loading if inactive?
                var entry = new ModEntry()
                {
                    ModMetadata = metadata,
                    Source = modDir,    //Todo: would dll/metadata path make more sense?
                };

                //Todo: decide if assemblies should be loaded if mods are inactive.  Currently using Unloaded
                if (!metadata.Enabled)
                {
                    loadedMods.Add(entry);
                    continue;
                }

                //Load mod and get the type
                try
                {
                    var modDll = Assembly.UnsafeLoadFrom(dllPath);

                    var types = modDll.GetTypes();
                    entry.ModType = modDll.GetType($"{modName}.{ModMetadata.TYPENAME}");
                    entry.Status = entry.ModType is null ? ModStatus.LoadFailure : ModStatus.Inactive;
                    //modTypes = modDll.GetTypes().Where(t => typeof(IHarmonyMod).IsAssignableFrom(t)).ToList();    //Non-explicit version with possibility of multiple types
                }
                catch (Exception e)
                {
                    //Logger.Log($"Failed to load mod file `{dllPath}`: {e}");
                    entry.Status = ModStatus.LoadFailure;
                }

                loadedMods.Add(entry);
            }

            return loadedMods;
        }

        private static bool TryLoadMetadata(string metadataPath, out ModMetadata metadata)
        {
            metadata = null;
            try
            {
                metadata = JsonConvert.DeserializeObject<ModMetadata>(File.ReadAllText(metadataPath));
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

                log.Info($"Unpatching {mod.ModMetadata.Name}");
                    mod.ModInstance?.Shutdown();
            }
            catch (Exception ex)
            {
                log.Error($"Error unpatching {mod.ModMetadata.Name}: {ex}");
            }
        }
        #endregion

        //Shutdown
        //ServerManager.ShutdownServer
    }
}
