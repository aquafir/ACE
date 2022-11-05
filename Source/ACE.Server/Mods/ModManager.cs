using ACE.Entity.Enum;
using ACE.Mod;
using ACE.Server.Command;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.WorldObjects;
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
        public static List<ModContainer> Mods => _mods;

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

        public static string GetFolder(IHarmonyMod mod)
        {
            var match = Mods.Where(x => x.Instance == mod).FirstOrDefault();
            return match is null ? "" : match.FolderPath;
        }

        public static void Log(string message)
        {
            log.Info(message);
        }

        public static void Message(string name, string message)
        {
            var player = PlayerManager.FindByName(name, out bool online);
            if(online)
            {
                ((Player)player).SendMessage(message);
            }
        }
        #endregion

        #region Helpers
        private static Dictionary<CommandHandlerInfo, Action<Session, string[]>> commands = new();
        public static bool TryCreateCommand(MethodInfo method, string command)
        {
            var handler = (CommandHandler)Delegate.CreateDelegate(typeof(CommandHandler), method);
            
            var info = new CommandHandlerInfo()
            {
                Attribute = new CommandHandlerAttribute(command, AccessLevel.Player, CommandHandlerFlag.None),//, access, flags, description, usage),
                Handler = handler
            };

            CommandManager.TryAddCommand(info);
            return true;
        }
                                            
        public static bool TryCreateCommand(Action<Session, string[]> handler,
                                            string command,
                                            //AccessLevel access,
                                            //CommandHandlerFlag flags = CommandHandlerFlag.None,
                                            //string description = "",
                                            //string usage = "",
                                            bool overrides = true)
        {
            //if(commands.ContainsKey(command) && !overrides)
            //{
            //        log.Warn($"Command already exists: {command} ");
            //        return false;
            //}
            var commandHandler = (CommandHandler)Delegate.CreateDelegate(typeof(CommandHandler), handler.Method);
            var info = new CommandHandlerInfo()
            {
                Attribute = new CommandHandlerAttribute(command, AccessLevel.Player, CommandHandlerFlag.None),//, access, flags, description, usage),
                Handler = commandHandler
            };
            
            if(CommandManager.TryAddCommand(info, overrides))
            {
                log.Info($"Created command: {command}");
                return true;
            }
            else
            {
                log.Warn($"Command already exists: {command} ");
                return false;
            }
        }
        public static bool TryRemoveCommand(string command)
        {
            //Todo: decide about using something like handler.Method.GetParameters to allow overloading commands
            return true;
        }

        public static CommandHandlerAttribute CreateHandlerAttribute(string command, AccessLevel accessLevel, CommandHandlerFlag flags = CommandHandlerFlag.None, string description = "", string usage = "")
        {
            return new CommandHandlerAttribute(command, accessLevel, flags, description, usage);
        }

        public static CommandHandlerInfo CreateHandlerInfo(CommandHandler handler)
        {
            return null;
        }

        public static ModContainer GetModContainerByName(string name) =>
            Mods.Where(x => x.ModMetadata.Name == name).FirstOrDefault();

        public static ModContainer GetModContainerByPath(string path) =>
            Mods.Where(x => x.FolderPath == path).FirstOrDefault();

        //Shutdown
        //ServerManager.ShutdownServer

        public static void Log(string message)
        {
            log.Info(message);
        }
    }
}
