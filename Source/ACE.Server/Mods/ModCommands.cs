using System;
using System.Net;

using log4net;

using ACE.Database;
using ACE.Database.Models.Auth;
using ACE.Entity.Enum;
using ACE.Server.Network;
using ACE.Server.Mod;

namespace ACE.Server.Command.Handlers
{
    public static class ModCommands
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [CommandHandler("listmods", AccessLevel.Developer, CommandHandlerFlag.None, 0,
            "Lists available mods and their status.")]
        public static void HandleListMods(Session session, params string[] parameters)
        {
            ModManager.ListMods();
        }

        [CommandHandler("findmods", AccessLevel.Developer, CommandHandlerFlag.None, 0,
            "Finds mods with valid metadata in the mod folder.")]
        public static void HandleFindMods(Session session, params string[] parameters)
        {
            ModManager.FindMods();
        }

        [CommandHandler("enablemod", AccessLevel.Developer, CommandHandlerFlag.None, 1,
            "Loads mods from the mod folder and enables active ones.")]
        [CommandHandler("em", AccessLevel.Developer, CommandHandlerFlag.None, 1,
            "Loads mods from the mod folder and enables active ones.")]
        public static void HandleEnableMod(Session session, params string[] parameters)
        {
            ModManager.EnableModByName(parameters[0]);
        }

        [CommandHandler("disablemod", AccessLevel.Developer, CommandHandlerFlag.None, 1,
            "Loads mods from the mod folder and enables active ones.")]
        [CommandHandler("dm", AccessLevel.Developer, CommandHandlerFlag.None, 1,
            "Loads mods from the mod folder and enables active ones.")]
        public static void HandleDisableMod(Session session, params string[] parameters)
        {
            ModManager.UnpatchModByName(parameters[0]);
        }

        [CommandHandler("test", AccessLevel.Developer, CommandHandlerFlag.None, 0,
            "Loads mods from the mod folder and enables active ones.")]
        [CommandHandler("t", AccessLevel.Developer, CommandHandlerFlag.None, 0,
            "Loads mods from the mod folder and enables active ones.")]
        public static void HandleTest(Session session, params string[] parameters)
        {
            ModManager.Test(3);
            ModManager.Test();
        }
    }
}
