using System;
using ACE.Entity.Enum;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.Factories;
using ACE.Server.WorldObjects;
using ACE.Entity.Enum.Properties;
using ACE.Server.ValheelMods;
using ACE.Server.WorldObjects.Entity;
using ACE.Common;
using ACE.Server.Entity.Actions;
using System.Collections.Generic;
using ACE.Server.Entity;
using System.Text;
using System.Linq;
using ACE.Entity.Models;

namespace ACE.Server.Command.Handlers
{
    public static class ValheelPlayerCommands
    {
        [CommandHandler("va", AccessLevel.Player, CommandHandlerFlag.None, "This is the ValHeel ability command Handler.")]

        public static void ValheelAbilityHandler(Session session, params string[] parameters)
        {
            var player = session.Player;

            if (parameters.Length == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"These are the current abilities: Health Over Time /va hot, Stamina Over Time /va sot", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"These are the current abilities: Power Attack /va pa, Bastion /va ba, Brutalize /va br, Life Well /va lw", ChatMessageType.Broadcast));
            }

            if (parameters[0] == "hot")
            {
                var fellows = player.GetFellowshipTargets();
                double currentUnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                if (fellows != null)
                {
                    foreach (var fellow in fellows)
                    {
                        if (player.GetDistance(fellow) < 30.0f)
                        {
                            if (parameters[1] == "8")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot8, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "7")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot7, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "6")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot6, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "5")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot5, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "4")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot4, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "3")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot3, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "2")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot2, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "1")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot1, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                        }
                    }
                }
            }
            if (parameters[0]== "sot")
            {
                // This sets the HoT flag on the target
                var fellows = player.GetFellowshipTargets();
                double currentUnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

                if (fellows != null)
                {
                    foreach (var fellow in fellows)
                    {
                        if (player.GetDistance(fellow) < 30.0f)
                        {
                            if (parameters[1] == "8")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot8, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "7")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot7, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "6")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot6, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "5")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot5, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "4")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot4, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "3")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot3, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "2")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot2, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                            else if (parameters[1] == "1")
                            {
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot1, true);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, fellow.MaxHoTDuration);
                                fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, fellow.MaxHoTTicks);
                            }
                        }
                    }
                }
            }
            if (parameters[0] == "ba")
                player.IsTankBuffed = true;
            if (parameters[0] == "pa")
                player.IsDamageBuffed = true;
            if (parameters[0] == "br")
                player.Brutalize = true;
            if (parameters[0] == "lw")
                player.LifeWell = true;
            if (parameters[0] == "st")
                player.Stealth = true;
            if (parameters[0] == "th")
                player.IsTaunting = true;
            else
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Could not find ability {parameters[0]}", ChatMessageType.Broadcast));
            }
        }

        [CommandHandler("vex", AccessLevel.Admin, CommandHandlerFlag.None, "This is the ValHeel Currency Exchange.")]

        public static void HandleVex(Session session, params string[] parameters)
        {
            var player = session.Player;

            if (parameters.Length == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"{ValHeelCurrencyMarket.MarketDataGenerator()}", ChatMessageType.System));
            }

            if (parameters.Length > 0)
            {
                // This is the buy command
                if (parameters[0].Equals("buy", StringComparison.OrdinalIgnoreCase))
                {
                    if (parameters[2] != null)
                    {
                        Int64.TryParse(parameters[2], out long amount);

                        if (parameters[1].Equals("ac", StringComparison.OrdinalIgnoreCase))
                        {
                            ValHeelCurrencyMarket.BuyAshCoins(player, amount);
                        }
                        if (parameters[1].Equals("ct", StringComparison.OrdinalIgnoreCase))
                        {
                            ValHeelCurrencyMarket.BuyCarnageTokens(player, amount);
                        }
                    }
                }

                // This is the sell command
                if (parameters[0].Equals("sell", StringComparison.OrdinalIgnoreCase))
                {
                    if (parameters[2] != null)
                    {
                        Int64.TryParse(parameters[2], out long amount);

                        if (parameters[1].Equals("ac", StringComparison.OrdinalIgnoreCase))
                        {
                            if (parameters[2] != null)
                            {
                                ValHeelCurrencyMarket.SellAshCoins(player, amount);
                            }
                        }
                        if (parameters[1].Equals("ct", StringComparison.OrdinalIgnoreCase))
                        {
                            if (parameters[2] != null)
                            {
                                ValHeelCurrencyMarket.SellCarnageTokens(player, amount);
                            }
                        }
                    }
                }

                if (parameters[0].Equals("exchange", StringComparison.OrdinalIgnoreCase))
                {
                    if (parameters[2] != null)
                    {
                        Int64.TryParse(parameters[2], out long amount);

                        if (parameters[1].Equals("acforct", StringComparison.OrdinalIgnoreCase))
                        {
                            if (parameters[2] != null)
                            {
                                ValHeelCurrencyMarket.ExchangeAcForCt(player, amount);
                            }
                        }

                        if (parameters[1].Equals("ctforac", StringComparison.OrdinalIgnoreCase))
                        {
                            if (parameters[2] != null)
                            {
                                ValHeelCurrencyMarket.ExchangeCtForAc(player, amount);
                            }
                        }
                    }
                }
            }
        }

        [CommandHandler("achlist", AccessLevel.Player, CommandHandlerFlag.None, "This command displays your currently earned achievements.")]

        public static void HandleAchievements(Session session, params string[] parameters)
        {
            var player = session.Player;

            var prestigious = $"Prestigious: {player.PrestigeMilestones} ";
            var monsterSlayer = $"Monster Slayer: {player.MonsterKillsMilestones} ";
            var wealthyBanker = $"Wealthy Banker: {player.HcPyrealsWonMilestones} ";
            var levelingUp = $"Leveling Up: {player.LevelMilestones} ";
            var hardcoreMaster = $"Hardcore Master: {player.HcScoreMilestones} ";

            StringBuilder result = new StringBuilder();

            result.AppendLine($"---------- Your Current Achievements ---------- ");
            result.AppendLine(prestigious);
            result.AppendLine(monsterSlayer);
            result.AppendLine(wealthyBanker);
            result.AppendLine(levelingUp);
            result.AppendLine(hardcoreMaster);

            var finalResult = result.ToString();

            session.Network.EnqueueSend(new GameMessageSystemChat($"{finalResult}", ChatMessageType.x1B));
        }

        [CommandHandler("hcleaders", AccessLevel.Player, CommandHandlerFlag.None, "This command displays the current top ten highest level hardcore players.")]

        public static void HandleHcLeaders(Session session, params string[] parameters)
        {
            List<Player> onlineHcPlayers = new List<Player>();
            List<OfflinePlayer> offlineHcPlayers = new List<OfflinePlayer>();

            foreach (var p in PlayerManager.GetAllOnline())
            {
                if (p.Hardcore == true)
                    onlineHcPlayers.Add(p);
            }

            foreach (var i in PlayerManager.GetAllOffline())
            {
                if (i.Hardcore == true)
                    offlineHcPlayers.Add(i);
            }

            List<IPlayer> allHcPlayers = new List<IPlayer>();
            allHcPlayers.AddRange(onlineHcPlayers);
            allHcPlayers.AddRange(offlineHcPlayers);

            List<IPlayer> playersToRemove = new List<IPlayer>();

            foreach (var p in allHcPlayers)
            {
                if (CalculateHcPlayerAge(p, GetCurrentUnixTime()) >= 5_184_000 && p.Level >= 10000)
                {
                    playersToRemove.Add(p);
                }
            }

            foreach (var p in playersToRemove)
            {
                allHcPlayers.Remove(p);
            }

            List<IPlayer> top10Players = allHcPlayers.OrderByDescending(p => p.HcScore).Take(10).ToList();

            StringBuilder result = new StringBuilder();
            int rank = 1;

            result.AppendLine("---------------------------- Hardcore Top Ten! ---------------------------- ");
            result.AppendLine("");
            result.AppendLine("Rank - Name - Level - Age - Creature Kills - Pyreals Won - Score ");

            foreach (var player in top10Players)
            {
                result.AppendLine($"{rank} - {player.Name} - {player.Level} - {player.HcAge} - {player.CreatureKills} - {player.HcPyrealsWon} - {player.HcScore} ");
                rank++;
            }

            string finalResult = result.ToString();

            if (!string.IsNullOrEmpty(finalResult))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"{finalResult}", ChatMessageType.x1B));
            }
        }

        public static long CalculateHcPlayerAge(IPlayer player, double currentUnxiTime)
        {
            var dob = player.GetProperty(PropertyString.DateOfBirth);

            DateTime dateFromString = DateTime.Parse(dob);
            long originalUnixTimestamp = ((DateTimeOffset)dateFromString).ToUnixTimeSeconds();

            DateTime currentDate = DateTime.Now.ToUniversalTime();
            long currentUnixTimestamp = ((DateTimeOffset)currentDate).ToUnixTimeSeconds();

            long ageInSeconds = currentUnixTimestamp - originalUnixTimestamp;

            player.HcAgeTimestamp = ageInSeconds;

            return ageInSeconds;
        }

        public static double GetCurrentUnixTime()
        {
            DateTime currentDate = DateTime.Now.ToUniversalTime();
            double currentUnixTimestamp = ((DateTimeOffset)currentDate).ToUnixTimeSeconds();

            return currentUnixTimestamp;
        }

        

        [CommandHandler("hardcore", AccessLevel.Admin, CommandHandlerFlag.None, "Handles Hardcore mode.", "")]

        public static void HandleHardcore(Session session, params string[] parameters)
        {

            if (parameters.Length <= 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"Use this command to enable Hardcore mode for your character.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"Hardcore enabled characters gain a permanent 50% xp bonus.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"This means that if your character dies, you will automatically be logged out and your character will be permanently deleted!", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"To enable Hardcore mode enter /hardcore on, in chat.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"WARNING! THIS IS PERMANENT AND CANNOT BE UNDONE!!!", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
            }
            switch (parameters?[0].ToLower())
            {
                case "1":
                case "on":
                    session.Player.Hardcore = true;
                    session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Warning! Hardcore mode has been enabled!", ChatMessageType.x1B));
                    session.Network.EnqueueSend(new GameMessageSystemChat($"If this chracter dies, you will be logged out, and the character permanently deleted!", ChatMessageType.x1B));
                    session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                    break;
            }
        }

        [CommandHandler("pets", AccessLevel.Player, CommandHandlerFlag.None, "Handles combat pet commands.", "")]

        public static void HandlePets(Session session, params string[] parameters)
        {
            var target = CommandHandlerHelper.GetLastAppraisedObject(session);

            if (parameters.Length == 0 || parameters[0] == null)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"Use this for pet commands.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                return;
            }
            else
            {
                if (parameters[0].Equals("reset", StringComparison.OrdinalIgnoreCase))
                {
                    List<Creature> creatures = new List<Creature>();
                    var landlbockCreatures = session.Player.CurrentLandblock.GetAllWorldObjectsForDiagnostics().OfType<Creature>().ToList();

                    foreach (var creature in session.Player.PhysicsObj.ObjMaint.GetVisibleObjectsValuesOfTypeCreature())
                    {
                        if (creature.IsCombatPet)
                        {
                            if (creature.PetOwner == session.Player.Guid.Full)
                            {
                                creatures.Add(creature);
                            }
                        }
                    }

                    foreach(var creature in creatures)
                    {
                        if (creature.IsCombatPet)
                        {
                            if (creature.PetOwner == session.Player.Guid.Full)
                            {
                                creature.Destroy();
                                session.Player.NumberOfPets--;
                                if (session.Player.NumberOfPets < 0)
                                {
                                    session.Player.NumberOfPets = 0;
                                }
                            }
                        }
                    }

                    if (landlbockCreatures.Count(c => c.PetOwner == session.Player.Guid.Full) == 0)
                    {
                        session.Player.NumberOfPets = 0;
                    }

                    return;
                }
                if (parameters[0].Equals("kill", StringComparison.OrdinalIgnoreCase))
                {
                    if (target.PetOwner != null && session.Player.Guid.Full == target.PetOwner)
                    {
                        target.Destroy();
                        session.Player.NumberOfPets--;
                        if (session.Player.NumberOfPets < 0)
                        {
                            session.Player.NumberOfPets = 0;
                        }
                    }
                    return;
                }
            }
        }

        [CommandHandler("convertlumtosc", AccessLevel.Player, CommandHandlerFlag.None,"Handles converting banked luminance to skill credits.","")]

        public static void HandleConvertLumToSC(Session session, params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"Use this command to convert luminace into skill credits.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                return;
            }
            else
            {
                long baseCost = 1000000000;
                int.TryParse(parameters[0], out int amount);
                long cost = baseCost * amount;

                if (amount <= 0 || parameters == null)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                    return;
                }
                if (session.Player.BankedLuminance < cost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough banked luminance.", ChatMessageType.Help));
                    return;
                }
                else
                {
                    session.Player.BankedLuminance -= cost;
                    var newScAmount = session.Player.AvailableSkillCredits += amount;

                    session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(session.Player, PropertyInt.AvailableSkillCredits, (int)newScAmount));
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have converted {cost} luminance into {amount} skill credits.", ChatMessageType.x1B));
                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your New Account Balance: {session.Player.BankedLuminance:N0} Luminanace", ChatMessageType.x1B));
                    return;
                }
            }
        }

        [CommandHandler("spendsc", AccessLevel.Admin, CommandHandlerFlag.None, "Handles all skill credit spending.", "")]        
        public static void HandleSpendSC(Session session, params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"Use this command to convert skill credits into skill points for a specified skill. 1 skill point costs 2 skill credits.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"/spendsc meleed, missiled, arcane, magicd, itemtink, assesp, decep, heal, jump, lock, run, assesc, weaptink, armortink...", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"/spendsc magictink, creature, itemench, war, lead, loyal, fletch, alch, cook, salv, twohand, void, heavy, light, fines...", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"/spendsc missilew, shield, dual, reck, sneak, dirty, summon.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.x1B));
            }
            else
            {
                int skillCreditCost = 2;
                int.TryParse(parameters[1], out int amount);

                if (amount >= 2 && amount * 2 > session.Player.AvailableSkillCredits)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough skil credits.", ChatMessageType.Help));
                    return;
                }
                if (parameters[0].Equals("meleed"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.MeleeDefense);
                    
                    if (session.Player.AvailableSkillCredits < skillCreditCost)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must have at least 2 available skill credits.", ChatMessageType.Help));
                        return;
                    }
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (parameters.Length < 2 && !int.TryParse(parameters[1], out amount) || amount == 1)
                    {                        
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Melee Defense 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;                                               
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Melee Defense {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("missiled"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.MissileDefense);

                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Missile Defense 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Missile Defense {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("arcane"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.ArcaneLore);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Missile Defense 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Missile Defense {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("magicd"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.MagicDefense);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Magic Defense 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Magic Defense {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("manac"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.ManaConversion);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Mana Conversion 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Mana Conversion {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("itemtink"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.ItemTinkering);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Item Tinkering 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Item Tinkering {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("assesp"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.AssessPerson);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Asses Person 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Asses Person {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("decep"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Deception);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Deception 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Deception {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("heal"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Healing);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Healing 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Healing {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("jump"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Jump);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Jump 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Jump {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("lock"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Lockpick);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Lock Pick 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Lock Pick {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("run"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Run);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Run 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Run {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("assesc"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.AssessCreature);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Asses Creature 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Asses Creature {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("weaptink"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.WeaponTinkering);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Weapon Tinkering 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Weapon Tinkering {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("armortink"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.ArmorTinkering);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Armor Tinkering 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Armor Tinkering {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("magictink"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.MagicItemTinkering);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Magic Item Tinkering 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Magic Item Tinkering {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("creature"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.CreatureEnchantment);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Creature Enchantment 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Creature Enchantment {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("itemench"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.ItemEnchantment);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Item Enchantment 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Item Enchantment {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("life"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.LifeMagic);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Life Magic 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Life Magic {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("war"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.WarMagic);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised War Magic 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised War Magic {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("lead"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Leadership);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Leadership 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Leadership {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("loyal"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Loyalty);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Loyalty 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Loyalty {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("fletch"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Fletching);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Fletching 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Fletching {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("alch"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Alchemy);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Alchemy 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Alchemy {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("cook"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Cooking);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Cooking 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Cooking {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("salv"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Salvaging);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Salvaging 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Salvaging {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("twohand"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.TwoHandedCombat);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Two Handed Combat 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Two Handed Combat {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("void"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.VoidMagic);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Void Magic 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Void Magic {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("heavy"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.HeavyWeapons);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Heavy Weapons 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Heavy Weapons {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("light"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.LightWeapons);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Light Weapons 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Light Weapons {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("fines"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.FinesseWeapons);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Finesse Weapons 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Finesse Weapons {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("missilew"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.MissileWeapons);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Missile Weapons 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Missile Weapons {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("shield"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Shield);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Shield 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Shield {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("dual"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.DualWield);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Dual Wield 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Dual Wield {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("reck"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Recklessness);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Recklessness 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Recklessness {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("sneak"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.SneakAttack);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Sneak Attack 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Sneak Attack {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("dirty"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.DirtyFighting);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Dirty Fighting 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Dirty Fighting {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                if (parameters[0].Equals("summon"))
                {
                    var player = session.Player;
                    var numOfCredits = session.Player.AvailableSkillCredits;
                    var skill = session.Player.GetCreatureSkill(Skill.Summoning);
                    
                    if (!skill.IsMaxRank)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Skill must be at max rank before you can use this command.", ChatMessageType.Help));
                        return;
                    }
                    if (amount < 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You must use a valid integer.", ChatMessageType.Help));
                        return;
                    }
                    if (amount == 1)
                    {
                        skill.InitLevel += 1;
                        session.Player.AvailableSkillCredits -= 2;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Summoning 1 point.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    else
                    {
                        skillCreditCost = amount * 2;
                        skill.InitLevel += (uint)amount;
                        session.Player.AvailableSkillCredits -= skillCreditCost;
                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have raised Summoning {amount} points.", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdateSkill(session.Player, skill));
                        session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.AvailableSkillCredits, (int)numOfCredits - skillCreditCost));
                    }
                    return;
                }
                else
                    return;
            }
        }

        /*[CommandHandler("besttimes", AccessLevel.Player, CommandHandlerFlag.None, 0, "Show current world population", "")]
        public static void HandleBestTimes(Session session, params string[] parameters)
        {
            ShardDatabase shardDatabase = new ShardDatabase();
            var times = shardDatabase.GetListofBestTimes();
            var timestring = string.Join(" , ", times);
            CommandHandlerHelper.WriteOutputInfo(session, $"{timestring}", ChatMessageType.Broadcast);
        }*/

        [CommandHandler("bank", AccessLevel.Player, CommandHandlerFlag.None,
            "Handles all Bank operations.",
            "")]
        public static void HandleBank(Session session, params string[] parameters)
        {
            long interestPeriod = PropertyManager.GetLong("interest_period").Item;
            long withdrawPeriod = 86400 * interestPeriod;
            int withdrawPeriodInDays = (int)PropertyManager.GetLong("interest_period").Item;

            if (parameters.Length == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] To use The Bank of ValHeel you must input one of the commands listed below into the chatbox. When you first use any command correctly, you will receive a bank account number.", ChatMessageType.System));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You may access your account number at any time to give to others so that they may send you pyreals or AshCoin.", ChatMessageType.System));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] How to use The Bank of ValHeel!", ChatMessageType.System));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank account - Shows your account number and account balances.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank send ACCOUNT# pyreals ### - Attempts to send an amount of pyreals to another account number.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank send ACCOUNT# ashcoin ### - Attempts to send an amount of AshCoin to another account number.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank send ACCOUNT# luminance ### - Attempts to send an amount of luminance to another account number.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit all - Attempts to deposit all pyreals, MMD's(converts to pyreals), AC Notes, and AshCoin from your character into your bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit pyreals ### - Attempts to deposit the specified amount of pyreals into your pyreal bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit ashcoin ### - Attempts to deposit the specified amount of AshCoin into your pyreal bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit luminance ### - Attempts to deposit the specified amount of luminance into your luminance bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank withdraw luminance ### - Attempts to withdraw the specified amount of luminance from your bank to your character.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank withdraw pyreals ### - Attempts to withdraw the specified amount of pyreals from your bank to your inventory. ", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank withdraw pyrealssavings ### - Attempts to withdraw the specified amount of pyreals from your savings to your inventory. ", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank withdraw ashcoin ### - Attempts to withdraw the specified amount of Ashcoin from your bank to your inventory. ", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
            }
            else
            {
                if (session.Player.BankAccountNumber == null)
                {
                    session.Player.BankedLuminance = 0;
                    session.Player.BankedPyreals = 0;
                    if (session.Player.Hardcore && session.Player.HcPyrealsWon > 0)
                        session.Player.BankedPyreals += (long)session.Player.HcPyrealsWon;
                    
                    session.Player.BankedAshcoin = 0;

                    if (session.Player.WithdrawTimer == null)
                    {
                        session.Player.WithdrawTimer = (Time.GetUnixTime() + withdrawPeriod);
                    }
                    if (session.Player.InterestTimer == null)
                    session.Player.InterestTimer = Time.GetFutureUnixTime(2592000);
                    else session.Player.InterestTimer = Time.GetFutureUnixTime(2592000);

                    var bankAccountCreation = new ActionChain();
                    bankAccountCreation.AddDelaySeconds(2);

                    bankAccountCreation.AddAction(WorldManager.ActionQueue, () =>
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Looks like you don't have an account, don't worry here in Dereth we give everyone a free checking account for all your needs!", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Creating your personal bank account number...", ChatMessageType.Broadcast));
                        Player_Bank.GenerateAccountNumber(session.Player);
                    });

                    bankAccountCreation.EnqueueChain();
                }
                else
                {
                    if (parameters[0].Equals("account", StringComparison.OrdinalIgnoreCase))
                    {
                        if (session.Player.BankedPyreals == null)                        
                            session.Player.BankedPyreals = 0;

                        if (session.Player.BankedLuminance == null)                        
                            session.Player.BankedLuminance = 0;
                        
                        if (session.Player.BankedAshcoin == null)                       
                            session.Player.BankedAshcoin = 0;
                        
                        if (session.Player.PyrealSavings == null)
                            session.Player.PyrealSavings = 0;

                        if (session.Player.BankedCarnageTokens == null)
                            session.Player.BankedCarnageTokens = 0;

                        // Moved interest to run automatically in Player_Tick
                        /*if (session.Player.InterestTimer== null)
                            session.Player.InterestTimer = 0;

                        if (session.Player.InterestTimer.HasValue)
                        {                           
                            if (Time.GetUnixTime() >= session.Player.InterestTimer)
                            {
                                session.Player.PyrealSavings += (long)(session.Player.BankedPyreals * 0.01);
                                session.Player.RemoveProperty(PropertyFloat.InterestTimer);
                                session.Player.SetProperty(PropertyFloat.InterestTimer, Time.GetFutureUnixTime(86400000)); //24 hours: 86400000
                            }
                            
                        }
                        if (!session.Player.InterestTimer.HasValue)
                        {
                            session.Player.SetProperty(PropertyFloat.InterestTimer, Time.GetFutureUnixTime(86400000)); //24 hours: 86400000
                        }*/
                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Account Number: {session.Player.BankAccountNumber}", ChatMessageType.x1B));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance || {session.Player.BankedAshcoin:N0} AshCoin || {session.Player.PyrealSavings:N0} Pyreal Savings || {session.Player.BankedCarnageTokens:N0} Carnage Tokens", ChatMessageType.x1B));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("send", StringComparison.OrdinalIgnoreCase))
                    {
                        bool accountFound = false;
                        long amountSent = 0;
                        int.TryParse(parameters[1], out int account);
                        Int64.TryParse(parameters[3], out long amt);

                        if (parameters.Length < 3)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Expected more parameters. Please make sure you have all the required fields for the /bank send command.", ChatMessageType.Help));
                            return;
                        }

                        if (parameters[2].Equals("pyreals", StringComparison.OrdinalIgnoreCase))
                        {

                            var players = PlayerManager.GetAllPlayers();

                            foreach (var player in players)
                            {
                                if (account == session.Player.BankAccountNumber)
                                    continue;

                                if (account == player.BankAccountNumber)
                                {
                                    if (amt > session.Player.BankedPyreals)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals in your bank to send {player.Name} that amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    if (amt <= 0)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Invalid amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    else
                                    {
                                        amountSent += amt;
                                        accountFound = true;
                                        session.Player.BankedPyreals -= amt;
                                        player.BankedPyreals += amt;
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You sent {player.Name} {amt:N0} Pyreals.", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your New Account Balance: {session.Player.BankedPyreals:N0} Pyreals", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

                                        var isOnline = PlayerManager.GetOnlinePlayer(player.Guid.Full);

                                        if (isOnline != null)
                                            isOnline.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK TRANSACTION] {session.Player.Name} sent you {amountSent:N0} Pyreals", ChatMessageType.x1B));

                                        break;
                                    }
                                }
                                else
                                    accountFound = false;
                            }

                            if (!accountFound)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Account number {account} does not exist.", ChatMessageType.Help));
                                return;
                            }
                        }
                        if (parameters[2].Equals("ashcoin", StringComparison.OrdinalIgnoreCase))
                        {

                            var players = PlayerManager.GetAllPlayers();

                            foreach (var player in players)
                            {
                                var sender = session.Player;

                                if (account == session.Player.BankAccountNumber)
                                    continue;

                                if (account == player.BankAccountNumber)
                                {
                                    if (amt > session.Player.BankedAshcoin)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough AshCoin in your bank to send {player.Name} that amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    if (amt <= 0)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Invalid amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    else
                                    {
                                        amountSent += amt;
                                        accountFound = true;
                                        session.Player.BankedAshcoin -= amt;
                                        player.BankedAshcoin += amt;
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You sent {player.Name} {amt:N0} AshCoin.", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your New Account Balance: {session.Player.BankedAshcoin:N0} AshCoin", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

                                        var isOnline = PlayerManager.GetOnlinePlayer(player.Guid.Full);

                                        if (isOnline != null)
                                            isOnline.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK TRANSACTION] {session.Player.Name} sent you {amountSent:N0} AshCoin", ChatMessageType.x1B));

                                        break;
                                    }
                                }
                                else
                                    accountFound = false;
                            }

                            if (!accountFound)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Account number {account} does not exist.", ChatMessageType.Help));
                                return;
                            }
                        }
                        else if (parameters[2].Equals("luminance", StringComparison.OrdinalIgnoreCase))
                        {
                            var players = PlayerManager.GetAllPlayers();

                            foreach (var player in players)
                            {
                                var sender = session.Player;

                                if (account == session.Player.BankAccountNumber)
                                    continue;

                                if (account == player.BankAccountNumber)
                                {
                                    if (amt > session.Player.BankedLuminance)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough luminance in your bank to send {player.Name} that amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    if (amt <= 0)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Invalid amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    else
                                    {
                                        amountSent += amt;
                                        accountFound = true;
                                        session.Player.BankedLuminance -= amt;
                                        player.BankedLuminance += amt;
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You sent {player.Name} {amt:N0} Luminance.", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your New Account Balance: {session.Player.BankedLuminance:N0} Luminanace", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

                                        var isOnline = PlayerManager.GetOnlinePlayer(player.Guid.Full);

                                        if (isOnline != null)
                                            isOnline.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK TRANSACTION] {session.Player.Name} sent you {amountSent:N0} Luminance", ChatMessageType.x1B));

                                        break;
                                    }
                                }
                                else
                                    accountFound = false;
                            }

                            if (!accountFound)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Account Number {account} does not exist.", ChatMessageType.Help));
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }
                    }


                    if (parameters[0].Equals("deposit", StringComparison.OrdinalIgnoreCase))
                    {
                        if (session.Player.BankedPyreals == null)
                        {
                            session.Player.BankedPyreals = 0;
                        }

                        if (session.Player.BankedLuminance == null)
                        {
                            session.Player.BankedLuminance = 0;
                        }

                        if (session.Player.BankedAshcoin == null)
                        {
                            session.Player.BankedAshcoin = 0;
                        }

                        if (session.Player.BankedCarnageTokens == null)
                        {
                            session.Player.BankedCarnageTokens = 0;
                        }

                        if (parameters[1].Equals("all", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit your Pyreals, Luminance, AshCoin, and Carnage Tokens into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, 0, true, false, false, false, false, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }

                        if (parameters[1].Equals("pyreals", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var pyreals = session.Player.GetInventoryItemsOfWCID(273);
                            long availablePyreals = 0;

                            foreach (var item in pyreals)
                                availablePyreals += (long)item.StackSize;

                            if (amt > availablePyreals)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals in your inventory to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Pyreals into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false, true, false, false, false, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }
                        if (parameters[1].Equals("pyrealsavings", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var pyreals = session.Player.GetInventoryItemsOfWCID(273);
                            long availablePyreals = 0;

                            foreach (var item in pyreals)
                                availablePyreals += (long)item.StackSize;

                            if (amt > availablePyreals)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals in your inventory to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Pyreals into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false, false, false, false, true, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }

                        if (parameters[1].Equals("ashcoin", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var ashcoin = session.Player.GetInventoryItemsOfWCID(801690);
                            long availableAshCoin = 0;

                            foreach (var item in ashcoin)
                                availableAshCoin += (long)item.StackSize;

                            if (amt > availableAshCoin)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough AshCoin in your inventory to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Ashcoin into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false, false, true, false, false, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }

                        if (parameters[1].Equals("ctokens", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var cToken = session.Player.GetInventoryItemsOfWCID(800112);
                            long availableCToken = 0;

                            foreach (var item in cToken)
                                availableCToken += (long)item.StackSize;

                            if (amt > availableCToken)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough Carnage Tokens in your inventory to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Carnage Tokens into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false, false, false, false, false, true);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }

                        if (parameters[1].Equals("luminance", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var available = session.Player.AvailableLuminance ?? 0;
                            var maximum = session.Player.MaximumLuminance ?? 0;
                            var remaining = maximum - available;

                            if (amt > available)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough Luminance to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Luminance into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false, false, false, true, false, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }
                    }

                    if (parameters[0].Equals("withdraw", StringComparison.OrdinalIgnoreCase) && (parameters[1].Equals("luminance", StringComparison.OrdinalIgnoreCase) || parameters[1].Equals("pyreals", StringComparison.OrdinalIgnoreCase) || parameters[1].Equals("pyrealsavings", StringComparison.OrdinalIgnoreCase) || parameters[1].Equals("ashcoin", StringComparison.OrdinalIgnoreCase) || parameters[1].Equals("ctokens", StringComparison.OrdinalIgnoreCase)))
                    {
                        if (session.Player.BankCommandTimer.HasValue)
                        {
                            if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                            {
                                session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                            }
                            else
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                return;
                            }
                        }

                        Int64.TryParse(parameters[2], out long amt);

                        if (amt <= 0)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to withdraw.", ChatMessageType.Broadcast));
                            return;
                        }

                        if (parameters[1].Equals("pyreals", StringComparison.OrdinalIgnoreCase))
                        {
                            if (amt > session.Player.BankedPyreals)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals to withdraw that amount from your bank. You have {session.Player.BankedPyreals:N0} Pyreals in your bank.", ChatMessageType.Help));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt:N0}.", ChatMessageType.Broadcast));
                                return;
                            }
                            else
                            {
                                session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                                long amountWithdrawn = 0;

                                if (amt > 2000000000)
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You can only withdraw a maximum of 2,000,000,000 pyreals at a time.", ChatMessageType.Broadcast));
                                    return;
                                }

                                if (amt >= 250000)
                                {
                                    var mmd = WorldObjectFactory.CreateNewWorldObject(20630);
                                    var mmds = amt / 250000f;
                                    mmd.SetStackSize((int)mmds);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(mmd))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    amt -= (long)mmds * 250000;
                                    amountWithdrawn += (long)mmds * 250000;
                                    session.Player.TryCreateInInventoryWithNetworking(mmd);
                                    session.Player.BankedPyreals -= (long)mmds * 250000;
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} MMDS.", ChatMessageType.Broadcast));
                                }

                                for (var i = amt; i >= 25000; i -= 25000)
                                {
                                    var pyreals = WorldObjectFactory.CreateNewWorldObject(273);
                                    pyreals.SetStackSize(25000);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(pyreals))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        break;
                                    }

                                    amt -= 25000;


                                    session.Player.TryCreateInInventoryWithNetworking(pyreals);

                                    session.Player.BankedPyreals -= pyreals.StackSize;
                                    amountWithdrawn += 25000;
                                }

                                if (amt < 25000 && amt > 0)
                                {
                                    var pyreals = WorldObjectFactory.CreateNewWorldObject(273);
                                    pyreals.SetStackSize((int)amt);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(pyreals))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    session.Player.TryCreateInInventoryWithNetworking(pyreals);

                                    session.Player.BankedPyreals -= amt;
                                    amountWithdrawn += amt;
                                }

                                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some pyreals from your bank account. (-{amountWithdrawn:N0})", ChatMessageType.x1B));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance ||  {session.Player.BankedAshcoin:N0}  AshCoin ||  {session.Player.PyrealSavings:N0}  Pyreal Savings ||  {session.Player.BankedCarnageTokens:N0}  Banked Carnage Tokens", ChatMessageType.x1B));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                            }
                        }
                        if (parameters[1].Equals("pyrealsavings", StringComparison.OrdinalIgnoreCase))
                        {
                            if (Time.GetUnixTime() >= session.Player.WithdrawTimer)
                            {
                                if (amt > session.Player.PyrealSavings)
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals to withdraw that amount from your bank. You have {session.Player.BankedPyreals:N0} Pyreals in your bank.", ChatMessageType.Help));
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt:N0}.", ChatMessageType.Broadcast));
                                    return;
                                }
                                if (amt > 2000000000)
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You can only withdraw a maximum of 2,000,000,000 pyreals at a time.", ChatMessageType.Broadcast));
                                    return;
                                }
                                else
                                {
                                    session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                                    long amountWithdrawn = 0;
                                  
                                    if (amt >= 250000)
                                    {
                                        var mmd = WorldObjectFactory.CreateNewWorldObject(20630);
                                        var mmds = amt / 250000f;
                                        mmd.SetStackSize((int)mmds);

                                        if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(mmd))
                                        {
                                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                            return;
                                        }

                                        amt -= (long)mmds * 250000;
                                        amountWithdrawn += (long)mmds * 250000;
                                        session.Player.TryCreateInInventoryWithNetworking(mmd);
                                        session.Player.PyrealSavings -= (long)mmds * 250000;
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} MMDS.", ChatMessageType.Broadcast));
                                    }

                                    for (var i = amt; i >= 25000; i -= 25000)
                                    {
                                        var pyreals = WorldObjectFactory.CreateNewWorldObject(273);
                                        pyreals.SetStackSize(25000);

                                        if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(pyreals))
                                        {
                                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                            break;
                                        }

                                        amt -= 25000;


                                        session.Player.TryCreateInInventoryWithNetworking(pyreals);

                                        session.Player.PyrealSavings -= pyreals.StackSize;
                                        amountWithdrawn += 25000;
                                    }

                                    if (amt < 25000 && amt > 0)
                                    {
                                        var pyreals = WorldObjectFactory.CreateNewWorldObject(273);
                                        pyreals.SetStackSize((int)amt);

                                        if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(pyreals))
                                        {
                                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                            return;
                                        }

                                        session.Player.TryCreateInInventoryWithNetworking(pyreals);

                                        session.Player.PyrealSavings -= amt;
                                        amountWithdrawn += amt;
                                    }

                                    session.Player.WithdrawTimer = (Time.GetUnixTime() + withdrawPeriod);
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some pyreals from your savings account. (-{amountWithdrawn:N0})", ChatMessageType.x1B));
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance ||  {session.Player.BankedAshcoin:N0}  AshCoin ||  {session.Player.PyrealSavings:N0}  Pyreal Savings ||  {session.Player.BankedCarnageTokens:N0}  Banked Carnage Tokens", ChatMessageType.x1B));
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                    return;
                                }
                            }
                            else
                                if (session.Player.WithdrawTimer == null)
                            {
                                session.Player.WithdrawTimer = (Time.GetUnixTime() + withdrawPeriod);
                            }
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You can only withdraw from your savings account once every {withdrawPeriodInDays:N0} days.", ChatMessageType.Help));
                            return;
                        }
                        if (parameters[1].Equals("ashcoin", StringComparison.OrdinalIgnoreCase))
                        {
                            if (amt > session.Player.BankedAshcoin)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough AshCoin to withdraw that amount from your bank. You have {session.Player.BankedAshcoin:N0} AshCoin in your bank.", ChatMessageType.Help));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt:N0}.", ChatMessageType.Broadcast));
                                return;
                            }
                            else
                            {
                                session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                                long amountWithdrawn = 0;

                                if (amt > 12500000)
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You can only withdraw a maximum of 12,500,000 AshCoin at a time.", ChatMessageType.Broadcast));
                                    return;
                                }

                                if (amt >= 50000)
                                {
                                    var fiftykAC = WorldObjectFactory.CreateNewWorldObject(801910);
                                    var mmds = amt / 50000f;
                                    fiftykAC.SetStackSize((int)mmds);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(fiftykAC))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    amt -= (long)mmds * 50000;
                                    amountWithdrawn += (long)mmds * 50000;
                                    session.Player.TryCreateInInventoryWithNetworking(fiftykAC);
                                    session.Player.BankedAshcoin -= (long)mmds * 50000;
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} 50k AC Notes.", ChatMessageType.Broadcast));
                                }

                                if (amt >= 10000 && amt < 50000)
                                {
                                    var tenkAC = WorldObjectFactory.CreateNewWorldObject(801909);
                                    var mmds = amt / 10000f;
                                    tenkAC.SetStackSize((int)mmds);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(tenkAC))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    amt -= (long)mmds * 10000;
                                    amountWithdrawn += (long)mmds * 10000;
                                    session.Player.TryCreateInInventoryWithNetworking(tenkAC);
                                    session.Player.BankedAshcoin -= (long)mmds * 10000;
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} 10k AC Notes.", ChatMessageType.Broadcast));
                                }

                                if (amt >= 5000 && amt < 10000)
                                {
                                    var fivekAC = WorldObjectFactory.CreateNewWorldObject(801908);
                                    var mmds = amt / 5000f;
                                    fivekAC.SetStackSize((int)mmds);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(fivekAC))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    amt -= (long)mmds * 5000;
                                    amountWithdrawn += (long)mmds * 5000;
                                    session.Player.TryCreateInInventoryWithNetworking(fivekAC);
                                    session.Player.BankedAshcoin -= (long)mmds * 5000;
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} 5k AC Notes.", ChatMessageType.Broadcast));
                                }

                                if (amt >= 1000 && amt < 5000)
                                {
                                    var onekAC = WorldObjectFactory.CreateNewWorldObject(801907);
                                    var mmds = amt / 1000f;
                                    onekAC.SetStackSize((int)mmds);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(onekAC))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    amt -= (long)mmds * 1000;
                                    amountWithdrawn += (long)mmds * 1000;
                                    session.Player.TryCreateInInventoryWithNetworking(onekAC);
                                    session.Player.BankedAshcoin -= (long)mmds * 1000;
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} 1k AC Notes.", ChatMessageType.Broadcast));
                                }

                                for (var i = amt; i >= 50000; i -= 50000)
                                {
                                    var ashcoin = WorldObjectFactory.CreateNewWorldObject(801690);
                                    ashcoin.SetStackSize(50000);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(ashcoin))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        break;
                                    }

                                    amt -= 50000;


                                    session.Player.TryCreateInInventoryWithNetworking(ashcoin);

                                    session.Player.BankedAshcoin -= ashcoin.StackSize;
                                    amountWithdrawn += 50000;
                                }

                                if (amt < 50000 && amt > 0)
                                {
                                    var ashcoin = WorldObjectFactory.CreateNewWorldObject(801690);
                                    ashcoin.SetStackSize((int)amt);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(ashcoin))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    session.Player.TryCreateInInventoryWithNetworking(ashcoin);

                                    session.Player.BankedAshcoin -= amt;
                                    amountWithdrawn += amt;
                                }

                                ValHeelCurrencyMarket.RemoveACFromCirculation(amt);
                                ValHeelCurrencyMarket.QueryACCirculation();
                                ValHeelCurrencyMarket.CalculateACValue(ValHeelCurrencyMarket.ACInCirculation);

                                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some AshCoin from your bank account. (-{amountWithdrawn:N0})", ChatMessageType.x1B));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance ||  {session.Player.BankedAshcoin:N0}  AshCoin ||  {session.Player.PyrealSavings:N0}  Pyreal Savings ||  {session.Player.BankedCarnageTokens:N0}  Banked Carnage Tokens", ChatMessageType.x1B));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                            }
                        }

                        if (parameters[1].Equals("luminance", StringComparison.OrdinalIgnoreCase))
                        {                            
                            Int64.TryParse(parameters[2], out long amt2);

                            if (amt2 <= 0)
                                return;

                            if (amt2 > session.Player.BankedLuminance)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough Luminance to withdraw that amount from your bank. You have {session.Player.BankedLuminance:N0} Luminance in your bank.", ChatMessageType.Help));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt2:N0}.", ChatMessageType.Broadcast));
                                return;
                            }

                            var available = session.Player.AvailableLuminance ?? 0;
                            var maximum = session.Player.MaximumLuminance ?? 0;
                            var remaining = maximum - available;

                            if (available == maximum)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You cannot withdraw that much Luminance because you cannot hold that much.", ChatMessageType.Help));
                                return;
                            }

                            if (amt2 > remaining)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You cannot withdraw that much Luminance because you cannot hold that much.", ChatMessageType.Help));
                                return;
                            }
                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Player.GrantLuminance(amt2, XpType.Admin, ShareType.None);
                            session.Player.BankedLuminance -= amt2;
                            session.Player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableLuminance, session.Player.AvailableLuminance ?? 0));

                            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some Luminance from your bank account. (-{amt2:N0})", ChatMessageType.x1B));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance ||  {session.Player.BankedAshcoin:N0}  AshCoin ||  {session.Player.PyrealSavings:N0}  Pyreal Savings ||  {session.Player.BankedCarnageTokens:N0}  Banked Carnage Tokens", ChatMessageType.x1B));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        }

                        if (parameters[1].Equals("CTokens", StringComparison.OrdinalIgnoreCase))
                        {
                            Int64.TryParse(parameters[2], out long amt2);

                            if (session.Player.BankedCarnageTokens == null)
                                session.Player.BankedCarnageTokens = 0;

                            var available = session.Player.BankedCarnageTokens ?? 0;

                            if (amt2 <= 0)
                                return;

                            if (amt2 > available)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough Carnage Tokens to withdraw that amount from your bank. You have {session.Player.BankedCarnageTokens:N0} Luminance in your bank.", ChatMessageType.Help));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt2:N0}.", ChatMessageType.Broadcast));
                                return;
                            }

                            int remainingAmount = (int)amt2;

                            while (remainingAmount > 0)
                            {
                                int stackSize = Math.Min(remainingAmount, 100);
                                var cToken = WorldObjectFactory.CreateNewWorldObject(800112);
                                cToken.SetStackSize(stackSize);
                                session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                                session.Player.TryCreateInInventoryWithNetworking(cToken);
                                remainingAmount -= stackSize;
                            }

                            // Adjust the banked Carnage Tokens after withdrawing
                            session.Player.BankedCarnageTokens -= amt2;

                            // Update the player's banked Carnage Tokens property
                            session.Player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.BankedCarnageTokens, session.Player.BankedCarnageTokens ?? 0));

                            ValHeelCurrencyMarket.RemoveCTFromCirculation(amt2);
                            ValHeelCurrencyMarket.QueryCTCirculation();
                            ValHeelCurrencyMarket.CalculateCTValue(ValHeelCurrencyMarket.CTInCirculation);

                            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some Carnage Tokens from your bank account. (-{amt2:N0})", ChatMessageType.x1B));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance || {session.Player.BankedAshcoin:N0} AshCoin || {session.Player.PyrealSavings:N0} Pyreal Savings || {session.Player.BankedCarnageTokens:N0} Banked Carnage Tokens", ChatMessageType.x1B));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        }
                    }
                }
            }
        }

        [CommandHandler("nextlevel", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0)]
        public static void HandleCheckXp(Session session, params string[] parameters)
        {
            if (session.Player.Level >= 275)
            {
                var currentxp = session.Player.TotalXpBeyond;
                var currentremaining = currentxp - session.Player.TotalExperience;
                session.Network.EnqueueSend(new GameMessageSystemChat($"{currentremaining:N0} exp to reach level {session.Player.Level + 1}.", ChatMessageType.Broadcast));
            }
            return;
        }       

        [CommandHandler("reputation", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "Refunds costs associated with /raise.")]
        public static void Reputation(Session session, params string[] parameters)
        {
            Player player = session.Player;

            if (!player.QuestManager.HasQuestCompletes("Reputation"))
            {
                return;
            }

            var numberOfSolves = player.QuestManager.GetCurrentSolves("Reputation");

            ChatPacket.SendServerMessage(session, $"You currently have {numberOfSolves} reputation.", ChatMessageType.Broadcast);

        }

        [CommandHandler("prestige", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0, "Refunds costs associated with /raise.")]
        public static void Prestige(Session session, params string[] parameters)
        {
            Player player = session.Player;

            if (!player.QuestManager.HasQuest("Prestige") || !player.QuestManager.HasQuest("Reputation"))
            {
                return;
            }

            var prestige = player.QuestManager.GetCurrentSolves("Prestige");
            var reputation = player.QuestManager.GetCurrentSolves("Reputation");

            if (prestige >= 1 && prestige <= 9)
                player.QuestManager.Stamp("PrestigeComplete");
            if (prestige >= 10 && prestige <= 24)
                player.QuestManager.Stamp("PrestigeComplete10");
            if (prestige >= 25 && prestige <= 49)
                player.QuestManager.Stamp("PrestigeComplete25");
            if (prestige >= 50 && prestige <= 74)
                player.QuestManager.Stamp("PrestigeComplete50");
            if (prestige >= 75 && prestige <= 99)
                player.QuestManager.Stamp("PrestigeComplete75");
            if (prestige >= 100 && prestige <= 149)
                player.QuestManager.Stamp("PrestigeComplete100");
            if (prestige >= 150)
                player.QuestManager.Stamp("PrestigeComplete150");

            ChatPacket.SendServerMessage(session, string.Format("You are currently Prestige level {0}.", prestige), ChatMessageType.Broadcast);
            return;                     
        }

        [CommandHandler("raiserefund", AccessLevel.Admin, CommandHandlerFlag.RequiresWorld, 0, "Refunds costs associated with /raise.")]
        public static void HandleRaiseRefund(Session session, params string[] parameters)
        {
            ValheelRaise.RaiseRefundToPlayer(session.Player);
        }

        [CommandHandler("raise", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Allows you to raise attributes past maximum at the cost of 20 billion XP. Allows you to raise Luminance Augmentation for Damage Rating (Destruction), Damage Reduction (Invulnerability), Critical Damage (Glory) and Critical Damage Reduction (Temperance) and Max Health, Stamina, and Mana (Vitality) for the cost of 10,000,000 Luminance.", "")]
        public static void HandleRaise(Session session, params string[] parameters)
        {
            Player player = session.Player;
            if (parameters.Length < 1)
            {
                ChatPacket.SendServerMessage(session, "Usage: /raise <str/end/coord/quick/focus/self/hp/stam/mp/mana> (for 20 billion exp) <destruction/invulnerability/glory/temperance/vitality> (for 10,000,000 luminance) [number of points to purchase (default: 1)]", ChatMessageType.Broadcast);
                return;
            }
            int result = 1;
            if (parameters.Length > 1 && !int.TryParse(parameters[1], out result))
            {
                ChatPacket.SendServerMessage(session, "Invalid value, values must be valid integers", ChatMessageType.Broadcast);
                return;
            }
            if (result <= 0)
            {
                ChatPacket.SendServerMessage(session, "Invalid value, values must be valid integers", ChatMessageType.Broadcast);
                return;
            }
            parameters[0] = parameters[0].ToLower();
            if (parameters[0].Equals("hp"))
            {
                parameters[0] = "health";
            }
            if (parameters[0].Equals("health"))
            {
                /*HandleRaiseHealth(player, session, result);*/

                for (int i = 0; i < result; i++)
                {
                    if (20000000000L > player.AvailableExperience)
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                        ChatPacket.SendServerMessage(session, string.Format("Your Maximum Health has been increased by {0}.", i), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                        return;
                    }
                    if (!player.SpendXP(20000000000L))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                        ChatPacket.SendServerMessage(session, string.Format("Your Maximum Health has been increased by {0}.", i), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                        return;
                    }
                    CreatureVital creatureVital = new CreatureVital(player, PropertyAttribute2nd.MaxHealth);
                    creatureVital.Ranks = Math.Clamp(creatureVital.Ranks + 1, 1u, uint.MaxValue);
                    player.UpdateVital(creatureVital, creatureVital.MaxValue);
                    CreatureVital creatureVital2 = new CreatureVital(player, PropertyAttribute2nd.Health);
                    creatureVital2.Ranks = Math.Clamp(creatureVital2.Ranks + 1, 1u, uint.MaxValue);
                    player.UpdateVital(creatureVital2, creatureVital2.MaxValue);
                    player.RaisedHealth++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                ChatPacket.SendServerMessage(session, string.Format("Your Maximum Health has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }
            if (parameters[0].Equals("pres"))
            {
                parameters[0] = "prestige";
            }
            if (parameters[0].Equals("prestige"))
            {
                var prestige = player.QuestManager.GetCurrentSolves("Prestige");
                var reputation = player.QuestManager.GetCurrentSolves("Reputation");
                var lumAugDamageRating = player.GetProperty(PropertyInt.LumAugDamageRating);
                var lumAugDamageReductionRating = player.GetProperty(PropertyInt.LumAugDamageReductionRating);
                var lumAugCritDamageRating = player.GetProperty(PropertyInt.LumAugCritDamageRating);
                var lumAugCritReductionRating = player.GetProperty(PropertyInt.LumAugCritReductionRating);

                if (prestige <= 9)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 5000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 5000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 5000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 5000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 5000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 1;
                        player.LumAugDamageReductionRating += 1;
                        player.LumAugCritDamageRating += 1;
                        player.LumAugCritReductionRating += 1;
                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                if (prestige >= 10 && prestige <= 24)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 10000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 10000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 10000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 10000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 10000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 2;
                        player.LumAugDamageReductionRating += 2;
                        player.LumAugCritDamageRating += 2;
                        player.LumAugCritReductionRating += 2;

                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete10");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                if (prestige >= 25 && prestige <= 49)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 25000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 25000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 25000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 25000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 25000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 3;
                        player.LumAugDamageReductionRating += 3;
                        player.LumAugCritDamageRating += 3;
                        player.LumAugCritReductionRating += 3;
                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete25");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                if (prestige >= 50 && prestige <= 74)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 35000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 35000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 35000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 35000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 35000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 4;
                        player.LumAugDamageReductionRating += 4;
                        player.LumAugCritDamageRating += 4;
                        player.LumAugCritReductionRating += 4;
                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete50");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                if (prestige >= 75 && prestige <= 99)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 50000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 50000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 50000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 50000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 50000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 5;
                        player.LumAugDamageReductionRating += 5;
                        player.LumAugCritDamageRating += 5;
                        player.LumAugCritReductionRating += 5;
                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete75");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                if (prestige >= 100 && prestige <= 149)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 100000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 100000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 100000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 100000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 100000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 5;
                        player.LumAugDamageReductionRating += 5;
                        player.LumAugCritDamageRating += 5;
                        player.LumAugCritReductionRating += 5;
                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete100");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                if (prestige >= 150)
                {
                    for (int i = 0; i < result; i++)
                    {
                        if (reputation < 250000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 250,000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (player.QuestManager.GetCurrentSolves("Reputation") < 250000)
                        {
                            ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}.", i), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough reputation, 250,000 reputation to increase prestige.", ChatMessageType.Broadcast);
                            return;
                        }
                        player.QuestManager.Decrement("Reputation", 250000);
                        player.QuestManager.Increment("Prestige", 1);
                        player.LumAugDamageRating += 5;
                        player.LumAugDamageReductionRating += 5;
                        player.LumAugCritDamageRating += 5;
                        player.LumAugCritReductionRating += 5;
                    }
                    var newreputation = player.QuestManager.GetCurrentSolves("Reputation");
                    var newprestige = player.QuestManager.GetCurrentSolves("Prestige");
                    player.QuestManager.Stamp("PrestigeComplete150");
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                    ChatPacket.SendServerMessage(session, string.Format("Your Prestige has been increased by {0}. And is now {1}. You have {2} Reputation remaining.", result, newprestige, newreputation), ChatMessageType.Broadcast);
                    player.PlayParticleEffect(PlayScript.TransUpWhite, player.Guid);
                    player.PlayParticleEffect(PlayScript.AetheriaLevelUp, player.Guid);
                    player.PlayParticleEffect(PlayScript.VitaeUpWhite, player.Guid);
                    return;
                }
                return;
            }
            if (parameters[0].Equals("stam"))
            {
                parameters[0] = "stamina";
            }
            if (parameters[0].Equals("stamina"))
            {
                for (int j = 0; j < result; j++)
                {
                    if (20000000000L > player.AvailableExperience)
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                        ChatPacket.SendServerMessage(session, string.Format("Your Maximum Stamina has been increased by {0}.", j), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                        return;
                    }
                    if (!player.SpendXP(20000000000L))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                        ChatPacket.SendServerMessage(session, string.Format("Your Maximum Stamina has been increased by {0}.", j), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                        return;
                    }
                    CreatureVital creatureVital3 = new CreatureVital(player, PropertyAttribute2nd.MaxStamina);
                    creatureVital3.Ranks = Math.Clamp(creatureVital3.Ranks + 1, 1u, uint.MaxValue);
                    player.UpdateVital(creatureVital3, creatureVital3.MaxValue);
                    CreatureVital creatureVital4 = new CreatureVital(player, PropertyAttribute2nd.Stamina);
                    creatureVital4.Ranks = Math.Clamp(creatureVital4.Ranks + 1, 1u, uint.MaxValue);
                    player.UpdateVital(creatureVital4, creatureVital4.MaxValue);
                    player.RaisedStamina++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                ChatPacket.SendServerMessage(session, string.Format("Your Maximum Stamina has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }

            int _luminanceRating = 0;


            // allows spending of luminance to increase luminance damage rating
            if (parameters[0].ToLowerInvariant().Equals("destruction"))
            {
                
                _luminanceRating = player.LumAugDamageRating;
                if (_luminanceRating == 0)
                    player.LumAugDamageRating = 1;
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, _luminanceRating));

                for (int j = 0; j < result; j++)
                {
                    _luminanceRating = player.LumAugDamageRating;

                    var destruction = player.GetProperty(PropertyInt.LumAugDamageRating);
                    var destructioncost = Math.Round((double)(10000000 * (1 + (destruction * 0.149))));

                    // while looping through the number of increases requested - if the total available is not enough to keep looping
                    // break out of the loop and inform the player of how many increases they received
                    if (destructioncost > player.AvailableLuminance || !player.SpendLuminance((long)destructioncost))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, _luminanceRating));
                        ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Damage Rating has been increased by {0}.", j), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, $"Not enough Luminance for remaining points, you require {destructioncost} luminance.", ChatMessageType.Broadcast);
                        return;
                    }
                    player.LumAugDamageRating++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageRating, player.LumAugDamageRating));
                ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Damage Rating has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }


            // allows spending of luminance to increase luminance damage reduction rating (Invulnerability)
            if (parameters[0].ToLowerInvariant().Equals("invulnerability"))
            {
                for (int j = 0; j < result; j++)
                {
                    _luminanceRating = player.LumAugDamageReductionRating;
                    if (_luminanceRating == 0)
                        player.LumAugDamageReductionRating = 1;
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, _luminanceRating));

                    var invulnerability = player.GetProperty(PropertyInt.LumAugDamageReductionRating);
                    var invulnerabilitycost = Math.Round((double)(10000000 * (1 + (invulnerability * 0.149))));

                    // while looping through the number of increases requested - if the total available is not enough to keep looping
                    // break out of the loop and inform the player of how many increases they received
                    if (invulnerabilitycost > player.AvailableLuminance || !player.SpendLuminance((long)invulnerabilitycost))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, _luminanceRating));
                        ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Damage Reduction Rating has been increased by {0}.", j), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, $"Not enough Luminance for remaining points, you require {invulnerabilitycost} luminance.", ChatMessageType.Broadcast);
                        return;
                    }
                    player.LumAugDamageReductionRating++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugDamageReductionRating, player.LumAugDamageReductionRating));
                ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Damage Reduction Rating has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }

            
            // allows spending of luminance to increase luminance critical damage rating (Glory)
            if (parameters[0].ToLowerInvariant().Equals("glory"))
            {
                for (int j = 0; j < result; j++)
                {
                    _luminanceRating = player.LumAugCritDamageRating;
                    if (_luminanceRating == 0)
                        player.LumAugCritDamageRating = 1;
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, _luminanceRating));

                    var glory = player.GetProperty(PropertyInt.LumAugCritDamageRating);
                    var glorycost = Math.Round((double)(10000000 * (1 + (glory * 0.149))));

                    // while looping through the number of increases requested - if the total available is not enough to keep looping
                    // break out of the loop and inform the player of how many increases they received
                    if (glorycost > player.AvailableLuminance || !player.SpendLuminance((long)glorycost))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, _luminanceRating));
                        ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Critical Damage Rating has been increased by {0}.", j), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, $"Not enough Luminance for remaining points, you require {glorycost} luminance.", ChatMessageType.Broadcast);
                        return;
                    }
                    player.LumAugCritDamageRating++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritDamageRating, player.LumAugCritDamageRating));
                ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Critical Damage Rating has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }

            
            // allows spending of luminance to increase luminance critical damage reduction rating (Temperance)
            if (parameters[0].ToLowerInvariant().Equals("temperance"))
            {
                for (int j = 0; j < result; j++)
                {
                    _luminanceRating = player.LumAugCritReductionRating;
                    if (_luminanceRating == 0)
                        player.LumAugCritReductionRating = 1;
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, _luminanceRating));

                    var temperance = player.GetProperty(PropertyInt.LumAugCritReductionRating);
                    var temperancecost = Math.Round((double)(10000000 * (1 + (temperance * 0.149))));

                    // while looping through the number of increases requested - if the total available is not enough to keep looping
                    // break out of the loop and inform the player of how many increases they received
                    if (temperancecost > player.AvailableLuminance || !player.SpendLuminance((long)temperancecost))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, _luminanceRating));
                        ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Critical Damage Reduction Rating has been increased by {0}.", j), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, $"Not enough Luminance for remaining points, you require {temperancecost} luminance.", ChatMessageType.Broadcast);
                        return;
                    }
                    player.LumAugCritReductionRating++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt(player, PropertyInt.LumAugCritReductionRating, player.LumAugCritReductionRating));
                ChatPacket.SendServerMessage(session, string.Format("Your Luminance Augmentation Critical Damage Reduction Rating has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }

            CreatureVital maxHealth = new CreatureVital(player, PropertyAttribute2nd.MaxHealth);

            if (parameters[0].Equals("vitality"))
            {
                if (maxHealth.Ranks < 5000)
                {
                    for (int j = 0; j < result; j++)
                    {
                        if (10000000L > player.AvailableLuminance)
                        {
                            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxMana]));
                            ChatPacket.SendServerMessage(session, string.Format("Your Vitality has been increased by {0}.", j), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough Luminance for remaining points, you require 10 million (10,000,000) Luminance per point.", ChatMessageType.Broadcast);
                            return;
                        }
                        if (!player.SpendLuminance(10000000L))
                        {
                            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxMana]));
                            ChatPacket.SendServerMessage(session, string.Format("Your Vitality has been increased by {0}.", j), ChatMessageType.Broadcast);
                            ChatPacket.SendServerMessage(session, "Not enough Luminance for remaining points, you require 10 million (10,000,000) Luminance per point.", ChatMessageType.Broadcast);
                            return;
                        }
                        CreatureVital creatureVital1 = new CreatureVital(player, PropertyAttribute2nd.MaxHealth);
                        creatureVital1.Ranks = Math.Clamp(creatureVital1.Ranks + 1, 1u, uint.MaxValue);
                        player.UpdateVital(creatureVital1, creatureVital1.MaxValue);
                        CreatureVital creatureVital2 = new CreatureVital(player, PropertyAttribute2nd.Health);
                        creatureVital2.Ranks = Math.Clamp(creatureVital2.Ranks + 1, 1u, uint.MaxValue);
                        player.UpdateVital(creatureVital2, creatureVital2.MaxValue);
                        CreatureVital creatureVital3 = new CreatureVital(player, PropertyAttribute2nd.MaxStamina);
                        creatureVital3.Ranks = Math.Clamp(creatureVital3.Ranks + 1, 1u, uint.MaxValue);
                        player.UpdateVital(creatureVital3, creatureVital3.MaxValue);
                        CreatureVital creatureVital4 = new CreatureVital(player, PropertyAttribute2nd.Stamina);
                        creatureVital4.Ranks = Math.Clamp(creatureVital4.Ranks + 1, 1u, uint.MaxValue);
                        player.UpdateVital(creatureVital4, creatureVital4.MaxValue);
                        CreatureVital creatureVital5 = new CreatureVital(player, PropertyAttribute2nd.MaxMana);
                        creatureVital5.Ranks = Math.Clamp(creatureVital5.Ranks + 1, 1u, uint.MaxValue);
                        player.UpdateVital(creatureVital5, creatureVital5.MaxValue);
                        CreatureVital creatureVital6 = new CreatureVital(player, PropertyAttribute2nd.Mana);
                        creatureVital6.Ranks = Math.Clamp(creatureVital6.Ranks + 1, 1u, uint.MaxValue);
                        player.UpdateVital(creatureVital6, creatureVital6.MaxValue);
                    }
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxMana]));
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxStamina]));
                    return;
                }                                

                if (maxHealth.Ranks >= 5000)
                {
                    ChatPacket.SendServerMessage(session, "You have reached Maximum Vitality", ChatMessageType.Broadcast);
                    return;                    
                }

            }
                        
            if (parameters[0].Equals("mana"))
            {
                for (int k = 0; k < result; k++)
                {
                    if (20000000000L > player.AvailableExperience)
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxMana]));
                        ChatPacket.SendServerMessage(session, string.Format("Your Maximum Mana has been increased by {0}.", k), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                        return;
                    }
                    if (!player.SpendXP(20000000000L))
                    {
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxMana]));
                        ChatPacket.SendServerMessage(session, string.Format("Your Maximum Mana has been increased by {0}.", k), ChatMessageType.Broadcast);
                        ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                        return;
                    }
                    CreatureVital creatureVital5 = new CreatureVital(player, PropertyAttribute2nd.MaxMana);
                    creatureVital5.Ranks = Math.Clamp(creatureVital5.Ranks + 1, 1u, uint.MaxValue);
                    player.UpdateVital(creatureVital5, creatureVital5.MaxValue);
                    CreatureVital creatureVital6 = new CreatureVital(player, PropertyAttribute2nd.Mana);
                    creatureVital6.Ranks = Math.Clamp(creatureVital6.Ranks + 1, 1u, uint.MaxValue);
                    player.UpdateVital(creatureVital6, creatureVital6.MaxValue);
                    player.RaisedMana++;
                }
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxMana]));
                ChatPacket.SendServerMessage(session, string.Format("Your Maximum Mana has been increased by {0}.", result), ChatMessageType.Broadcast);
                return;
            }
            if (parameters[0].Equals("str") || parameters[0].Equals("strength"))
            {
                parameters[0] = "Strength";
            }
            else if (parameters[0].Equals("end") || parameters[0].Equals("endurance"))
            {
                parameters[0] = "Endurance";
            }
            else if (parameters[0].Equals("coord") || parameters[0].Equals("coordination"))
            {
                parameters[0] = "Coordination";
            }
            else if (parameters[0].Equals("quick") || parameters[0].Equals("quickness"))
            {
                parameters[0] = "Quickness";
            }
            else if (parameters[0].Equals("focus") || parameters[0].Equals("focus"))
            {
                parameters[0] = "Focus";
            }
            else if (parameters[0].Equals("self") || parameters[0].Equals("self"))
            {
                parameters[0] = "Self";
            }
            PropertyAttribute result2;
            if (!System.Enum.TryParse<PropertyAttribute>(parameters[0], out result2))
            {
                ChatPacket.SendServerMessage(session, "Invalid Attribute, valid values are: Strength,Endurance,Coordination,Quickness,Focus,Self,Health,Stamina,Mana", ChatMessageType.Broadcast);
                return;
            }
            for (int l = 0; l < result; l++)
            {
                if (20000000000L > player.AvailableExperience)
                {
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(player, player.Attributes[result2]));
                    ChatPacket.SendServerMessage(session, string.Format("Your {0} has been increased by {1}.", result2.ToString(), l), ChatMessageType.Broadcast);
                    ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                    return;
                }
                if (!player.SpendXP(20000000000L))
                {
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(player, player.Attributes[result2]));
                    ChatPacket.SendServerMessage(session, string.Format("Your {0} has been increased by {1}.", result2.ToString(), l), ChatMessageType.Broadcast);
                    ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);                    
                    return;
                }
                player.Attributes[result2].StartingValue++;

            }
            player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(player, player.Attributes[result2]));
            ChatPacket.SendServerMessage(session, string.Format("Your {0} has been increased by {1}.", result2.ToString(), result), ChatMessageType.Broadcast);

            for (int l = 0; l < result; l++)
            {
                if (parameters[0].Equals("Strength"))
                {
                    player.RaisedStr++;
                }
                else if (parameters[0].Equals("Endurance"))
                {
                    player.RaisedEnd++;
                }
                else if (parameters[0].Equals("Coordination"))
                {
                    player.RaisedCoord++;
                }
                else if (parameters[0].Equals("Quickness"))
                {
                    player.RaisedQuick++;
                }
                else if (parameters[0].Equals("Focus"))
                {
                    player.RaisedFocus++;
                }
                else if (parameters[0].Equals("Self"))
                {
                    player.RaisedSelf++;
                }
            }
        }

        private static void HandleRaiseHealth(Player player, Session session, int result)
        {
            if (result <= 0)
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                ChatPacket.SendServerMessage(session, string.Format("Your Maximum Health has been increased by {0}.", player.RaisedHealth), ChatMessageType.Broadcast);
                return;
            }
            if (20000000000L > player.AvailableExperience)
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                ChatPacket.SendServerMessage(session, string.Format("Your Maximum Health has been increased by {0}.", player.RaisedHealth), ChatMessageType.Broadcast);
                ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                return;
            }
            if (!player.SpendXP(20000000000L))
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[PropertyAttribute2nd.MaxHealth]));
                ChatPacket.SendServerMessage(session, string.Format("Your Maximum Health has been increased by {0}.", player.RaisedHealth), ChatMessageType.Broadcast);
                ChatPacket.SendServerMessage(session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                return;
            }
            CreatureVital creatureVital = new CreatureVital(player, PropertyAttribute2nd.MaxHealth);
            creatureVital.Ranks = Math.Clamp(creatureVital.Ranks + 1, 1u, uint.MaxValue);
            player.UpdateVital(creatureVital, creatureVital.MaxValue);
            CreatureVital creatureVital2 = new CreatureVital(player, PropertyAttribute2nd.Health);
            creatureVital2.Ranks = Math.Clamp(creatureVital2.Ranks + 1, 1u, uint.MaxValue);
            player.UpdateVital(creatureVital2, creatureVital2.MaxValue);
            player.RaisedHealth++;
            HandleRaiseHealth(player, session, result - 1);
        }

        // This command is used to raise attributes and vitals
        /*[CommandHandler("raise", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Raises attributes and vitals.")]
        public static void HandleRaiseCommand(Player player, Session session, params string[] parameters)
        {
            if (parameters.Length < 2)
            {
                ChatPacket.SendServerMessage(session, "Usage: /raise <attribute> <amount>", ChatMessageType.Broadcast);
                return;
            }
            int result;
            if (!int.TryParse(parameters[1], out result))
            {
                ChatPacket.SendServerMessage(session, "Invalid amount, must be a number.", ChatMessageType.Broadcast);
                return;
            }
            if (result <= 0)
            {
                ChatPacket.SendServerMessage(session, "Invalid amount, must be greater than 0.", ChatMessageType.Broadcast);
                return;
            }
            if (result > 100)
            {
                ChatPacket.SendServerMessage(session, "Invalid amount, must be less than 100.", ChatMessageType.Broadcast);
                return;
            }
            if (parameters[0].Equals("Strength"))
            {
                HandleRaiseAttribute(player, session, result, PropertyAttribute.Strength);
            }
            else if (parameters[0].Equals("Endurance"))
            {
                HandleRaiseAttribute(player, session, result, PropertyAttribute.Endurance);
            }
            else if (parameters[0].Equals("Coordination"))
            {
                HandleRaiseAttribute(player, session, result, PropertyAttribute.Coordination);
            }
            else if (parameters[0].Equals("Quickness"))
            {
                HandleRaiseAttribute(player, session, result, PropertyAttribute.Quickness);
            }
            else if (parameters[0].Equals("Focus"))
            {
                HandleRaiseAttribute(player, session, result, PropertyAttribute.Focus);
            }
            else if (parameters[0].Equals("Self"))
            {
                HandleRaiseAttribute(player, session, result, PropertyAttribute.Self);
            }
            else if (parameters[0].Equals("Health"))
            {
                HandleRaiseVital(player, session, result, PropertyAttribute2nd.MaxHealth, PropertyAttribute2nd.Health);
            }
            else if (parameters[0].Equals("Stamina"))
            {
                HandleRaiseVital(player, session, result, PropertyAttribute2nd.MaxStamina, PropertyAttribute2nd.Stamina);
            }
            else if (parameters[0].Equals("Mana"))
            {
                HandleRaiseVital(player, session, result, PropertyAttribute2nd.MaxMana, PropertyAttribute2nd.Mana);
            }
            else
            {
                ChatPacket.SendServerMessage(session, "Invalid Attribute, valid values are: Strength,Endurance,Coordination,Quickness,Focus,Self,Health,Stamina,Mana", ChatMessageType.Broadcast);
            }
        }

        // This is the HandleRaiseVital method
        public static void HandleRaiseVital(Player player, Session session, int amount, PropertyAttribute2nd maxVital, PropertyAttribute2nd vital)
        {
            if (amount <= 0)
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[maxVital]));
                ChatPacket.SendServerMessage(player.Session, string.Format("Your Maximum {0} has been increased by {1}.", maxVital, player.RaisedHealth), ChatMessageType.Broadcast);
                return;
            }
            if (20000000000L > player.AvailableExperience)
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateVital(player, player.Vitals[maxVital]));
                ChatPacket.SendServerMessage(player.Session, string.Format("Your Maximum {0} has been increased by {1}.", maxVital, player.RaisedHealth), ChatMessageType.Broadcast);
                ChatPacket.SendServerMessage(player.Session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                return;
            }
        }

        // This is the HandleRaiseAttribute method
        public static void HandleRaiseAttribute(Player player, Session session, int amount, PropertyAttribute attribute)
        {
            // This is the HandleRaiseAttribute method
            if (amount <= 0)
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(player, player.Attributes[attribute]));
                ChatPacket.SendServerMessage(player.Session, string.Format("Your {0} has been increased by {1}.", attribute, player.RaisedHealth), ChatMessageType.Broadcast);
                return;
            }
            if (20000000000L > player.AvailableExperience)
            {
                player.Session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(player, player.Attributes[attribute]));
                ChatPacket.SendServerMessage(player.Session, string.Format("Your {0} has been increased by {1}.", attribute, player.RaisedHealth), ChatMessageType.Broadcast);
                ChatPacket.SendServerMessage(player.Session, "Not enough experience for remaining points, you require 20 billion(20,000,000,000) XP per point.", ChatMessageType.Broadcast);
                return;
            }
        }*/

        [CommandHandler("vassalxp", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows full experience from vassals.")]
        public static void HandleShowVassalXp(Session session, params string[] parameters)
        {
            CommandHandlerHelper.WriteOutputInfo(session, "Experience from vassals:", ChatMessageType.Broadcast);
            foreach (var vassalNode in AllegianceManager.GetAllegianceNode(session?.Player).Vassals.Values)
            {
                var vassal = vassalNode.Player;
                CommandHandlerHelper.WriteOutputInfo(session, $"{vassal.Name,-30}{vassal.AllegianceXPGenerated,-20:N0}", ChatMessageType.Broadcast);
            }
        }
    }
}
