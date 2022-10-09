using System;
using System.Net;

//using log4net;

using ACE.Database;
using ACE.Database.Models.Auth;
using ACE.Entity.Enum;
using ACE.Server.Network;
using ACE.Server.Mod;
using ACE.Adapter.GDLE.Models;
using System.Linq;

namespace ACE.Server.Command.Handlers
{
    public static class ModCommands
    {
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        enum ModCommand
        {
            List, L = List,
            Enable, E = Enable,
            Disable, D = Disable,
            Toggle, T = Toggle,
            Restart, R = Restart,
        }
        [CommandHandler("mod", AccessLevel.Developer, CommandHandlerFlag.None, -1,
            "Manage mods the lazy way")]
        public static void HandleListMods(Session session, params string[] parameters)
        {
            if (parameters.Length < 1 || !Enum.TryParse(typeof(ModCommand), parameters[0], true, out var verb)) return;

            var player = session.Player;

            ModContainer match = null;
            if (parameters.Length > 1)
                match = GetModByName(parameters[1]);

            switch (verb)
            {
                case ModCommand.Enable:
                    if (match is not null)
                    {
                        Log($"Enabling {match.ModMetadata.Name}", session);
                        match.Enable();
                    }
                    return;
                case ModCommand.Disable:
                    if (match is not null)
                    {
                        Log($"Disabling {match.ModMetadata.Name}", session);
                        match.Shutdown();
                    }
                    return;
                case ModCommand.Restart:
                    if (match is not null)
                    {
                        Log($"Restarting {match.ModMetadata.Name}", session);
                        match.Restart();
                    }
                    return;
                case ModCommand.Toggle:
                    if (match is not null)
                    {
                        if (match.Status == ModStatus.Inactive)
                        {
                            Log($"Enabling {match.ModMetadata.Name}", session);
                            match.Enable();
                        }
                        else
                        {
                            Log($"Disabling {match.ModMetadata.Name}", session);
                            match.Shutdown();
                        }
                    }
                    return;
                case ModCommand.List:
                    if (ModManager.Mods.Count > 0)
                        Log($"Displaying mods ({ModManager.Mods.Count})\n", session);
                    foreach (var mod in ModManager.Mods)
                    {
                        var meta = mod.ModMetadata;
                        Log($"{meta.Name} is {(meta.Enabled ? "Enabled" : "Disabled")}\n\tSource: {mod.FolderPath}\n\tStatus: {mod.Status}", session);
                    }
                    return;
            }

            ModManager.ListMods();
        }

        private static ModContainer GetModByName(string name) => ModManager.Mods.Where(x =>
        x.ModMetadata.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase)
        || x.TypeName.Contains(name, StringComparison.InvariantCultureIgnoreCase)
        ).FirstOrDefault();


        private static void Log(string message, Session session)
        {
            if (session.Player is not null)
                session.Player.SendMessage(message);
            Console.WriteLine(message);
        }


        [CommandHandler("findmods", AccessLevel.Developer, CommandHandlerFlag.None, 0,
            "Finds mods with valid metadata in the mod folder.")]
        public static void HandleFindMods(Session session, params string[] parameters)
        {
            ModManager.FindMods();
        }

        //[CommandHandler("listmods", AccessLevel.Developer, CommandHandlerFlag.None, 0,
        //    "Lists available mods and their status.")]
        //public static void HandleListMods(Session session, params string[] parameters)
        //{

        //    ModManager.ListMods();
        //}

        //[CommandHandler("enablemod", AccessLevel.Developer, CommandHandlerFlag.None, 1,
        //    "Loads mods from the mod folder and enables active ones.")]
        //[CommandHandler("em", AccessLevel.Developer, CommandHandlerFlag.None, 1,
        //    "Loads mods from the mod folder and enables active ones.")]
        //public static void HandleEnableMod(Session session, params string[] parameters)
        //{
        //    ModManager.EnableModByName(parameters[0]);
        //}

        //[CommandHandler("disablemod", AccessLevel.Developer, CommandHandlerFlag.None, 1,
        //    "Loads mods from the mod folder and enables active ones.")]
        //[CommandHandler("dm", AccessLevel.Developer, CommandHandlerFlag.None, 1,
        //    "Loads mods from the mod folder and enables active ones.")]
        //public static void HandleDisableMod(Session session, params string[] parameters)
        //{
        //    ModManager.UnpatchModByName(parameters[0]);
        //}
    }
}
