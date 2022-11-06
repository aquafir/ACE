using ACE.Entity.Enum;
using ACE.Server.Command;
using ACE.Server.Managers;
using ACE.Server.Mod;
using ACE.Server.Network;
using ACE.Server.WorldObjects;
using HarmonyLib;
using log4net;
using log4net.Core;
using McMaster.NETCore.Plugins;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Mono.Cecil.Cil;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
        private static List<ModContainer> Mods { get; set; } = new();

        #region Init / Shutdown
        public static void Initialize()
        {
            FindMods();
            EnableAllMods(Mods);
        }

        internal static void Shutdown()
        {
            //Todo: consider 
            UnpatchAllMods();
        }
        #endregion

        #region Load
        /// <summary>
        /// Finds all valid mods in the mod directory and attempts to load them.
        /// </summary>
        public static void FindMods()
        {
            Mods = LoadModEntries(ModDirectory);
            Mods = Mods.OrderByDescending(x => x.ModMetadata.Priority).ToList();

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
            foreach (var entry in entries)
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
        }

        /// <summary>
        /// Loads all valid metadata from folders in a given directory as ModContainer
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static List<ModContainer> LoadAllMetadata(string directory)
        {
            var loadedMods = new List<ModContainer>();
            var directories = Directory.GetDirectories(directory);

            //Check for missing and shut them down
            //foreach (var mod in Mods.Where(x => !directories.Contains(x.FolderPath))) {
            //    log.Info($"Shutting down mod {mod.ModMetadata.Name} with missing folder:\r\n\t{mod.FolderPath}");
            //    mod.Shutdown();
            //}
            //Mods.RemoveAll(x => !directories.Contains(x.FolderPath));

            //Check already loaded?
            //Structure is /<ModDir>/<AssemblyName>/<AssemblyName.dll> and Meta.json
            foreach (var modDir in directories)
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
            foreach (var mod in Mods.Where(x => x.ModMetadata.Name.Contains(modName)))
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
            foreach (var mod in Mods)
            {
                UnpatchMod(mod);
            }
        }

        public static void UnpatchModByName(string modName)
        {
            foreach (var mod in Mods.Where(x => x.ModMetadata.Name.Contains(modName)))
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
        public static void ListMods(Player player = null)
        {
            var sb = new StringBuilder();
            if (Mods.Count < 1)
                sb.AppendLine("No mods to display.");
            else
            {
                sb.AppendLine($"Displaying mods ({Mods.Count}):");
                foreach (var mod in Mods)
                {
                    var meta = mod.ModMetadata;
                    sb.AppendLine($"{meta.Name} is {(meta.Enabled ? "Enabled" : "Disabled")}");
                    sb.AppendLine($"\tSource: {mod.FolderPath}");
                    sb.AppendLine($"\tStatus: {mod.Status}");
                }
            }

            log.Info(sb);
            player?.SendMessage(sb.ToString());
        }

        public static string GetFolder(IHarmonyMod mod)
        {
            var match = Mods.Where(x => x.Instance == mod).FirstOrDefault();
            return match is null ? "" : match.FolderPath;
        }


        public enum LogLevel
        {
            Debug,
            Error,
            Fatal,
            Info,
            Warn
        }

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    log.Debug(message);
                    break;
                case LogLevel.Error:
                    log.Error(message);
                    break;
                case LogLevel.Fatal:
                    log.Fatal(message);
                    break;
                case LogLevel.Info:
                    log.Info(message);
                    break;
                case LogLevel.Warn:
                    log.Warn(message);
                    break;
                default:
                    log.Info(message);
                    break;
            };
        }

        public static void Message(string name, string message)
        {
            var player = PlayerManager.FindByName(name, out bool online);
            if (online)
            {
                ((Player)player).SendMessage(message);
            }
        }

        public static ModContainer GetModContainerByName(string name, bool allowPartial = true) => allowPartial ?
            Mods.Where(x => x.ModMetadata.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() :
        Mods.Where(x => x.ModMetadata.Name == name).FirstOrDefault();

        public static ModContainer GetModContainerByPath(string path) =>
            Mods.Where(x => x.FolderPath == path).FirstOrDefault();
        #endregion
    }
}
