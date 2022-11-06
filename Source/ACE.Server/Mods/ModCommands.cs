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
using ACE.Server.WorldObjects;
using HarmonyLib;
using System.Text;
using System.ComponentModel;

namespace ACE.Server.Command.Handlers
{
    public static class ModCommands
    {
        enum ModCommand
        {
            List, L = List,
            Enable, E = Enable,
            Disable, D = Disable,
            Toggle, T = Toggle,
            Restart, R = Restart,
            Method, M = Method,
            Find, F = Find,
        }
        //static string USAGE = $"/mod {String.Join('|', Enum.GetNames(typeof(ModCommand)))}";

        [CommandHandler("mod", AccessLevel.Developer, CommandHandlerFlag.None, -1,
            "Lazy mod control")]
        public static void HandleListMods(Session session, params string[] parameters)
        {
            if (parameters.Length < 1 || !Enum.TryParse(typeof(ModCommand), parameters[0], true, out var verb)) return;

            ModContainer match = null;
            if (parameters.Length > 1)
                match = ModManager.GetModContainerByName(parameters[1]);

            switch (verb)
            {
                case ModCommand.Enable:
                    if (match is not null)
                        EnableMod(session, match);
                    return;
                case ModCommand.Disable:
                    if (match is not null)
                        DisableMod(session, match);
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
                            EnableMod(session, match);
                        else
                            DisableMod(session, match);
                    }
                    return;

                //List mod status
                case ModCommand.List:
                    ModManager.ListMods(session?.Player);
                    return;

                //Prints out some information about a method and its params/types
                case ModCommand.Method:
                    if (parameters.Length < 3)
                        return;

                    var type = AccessTools.TypeByName(parameters[1]);
                    var method = parameters[2];
                    if (type is null || method is null)
                        return;
                    var mcMethod = AccessTools.FirstMethod(type, m => m.Name.Contains(method));

                    const int spacing = -40;
                    var sb = new StringBuilder($"Method {mcMethod.Name} found:");

                    foreach (var param in mcMethod.GetParameters())
                    {
                        sb.AppendLine($"Name: {param.Name,spacing}\r\nType: {param.ParameterType,spacing}\r\nDflt: {param.DefaultValue,spacing}\r\n");
                    }
                    Log(sb.ToString(), session);

                    return;

                //Full reload
                case ModCommand.Find:
                    ModManager.FindMods();
                    return;
            }

            ModManager.ListMods();
        }

        private static void DisableMod(Session session, ModContainer match)
        {
            Log($"Disabling {match.ModMetadata.Name}", session);
            match.Shutdown();
            match.ModMetadata.Enabled = false;
            match.SaveMetadata();
        }

        private static void EnableMod(Session session, ModContainer match)
        {
            Log($"Enabling {match.ModMetadata.Name}", session);
            match.Enable();
            match.ModMetadata.Enabled = true;
            match.SaveMetadata();
        }

        private static void Log(string message, Session session)
        {
            if (session?.Player is not null)
                session.Player.SendMessage(message);
            Console.WriteLine(message);
        }
    }
}
