using ACE.Server.Entity.Actions;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;
using ACE.Server.WorldObjects.Entity;
using ACE.Entity.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity;

#region Player
namespace ACE.Server.WorldObjects
{
    partial class Player
    {
        public void UnSneak()
        {
            if (CloakStatus == CloakStatus.Off)
                return;

            var actionChain = new ActionChain();

            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageDeleteObject(this));
            });
            actionChain.AddAction(this, () =>
            {
                NoDraw = true;
                EnqueueBroadcastPhysicsState();
                Visibility = false;
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageCreateObject(this));
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                Cloaked = false;
                Ethereal = false;
                NoDraw = false;
                ReportCollisions = true;
                EnqueueBroadcastPhysicsState();
            });

            actionChain.EnqueueChain();
        }

        public void HandleSneak()
        {
            if (CloakStatus == CloakStatus.On)
                return;

            var actionChain = new ActionChain();

            actionChain.AddAction(this, () =>
            {
                Cloaked = true;
                Ethereal = false;
                NoDraw = true;
                ReportCollisions = true;
                EnqueueBroadcastPhysicsState();
            });
            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageDeleteObject(this));
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                Visibility = true;
            });
            actionChain.AddDelaySeconds(.5);
            actionChain.AddAction(this, () =>
            {
                EnqueueBroadcast(false, new GameMessageCreateObject(this, true, true));
            });

            actionChain.EnqueueChain();
        }

        /// <summary>
        /// Raise the available XP by a percentage of the current level XP or a maximum
        /// </summary>
        public void GrantLevelProportionalXp(double percent, long min, long max, bool shareable = false)
        {
            var nextLevelXP = GetXPBetweenLevels(Level.Value, Level.Value + 1);

            var scaledXP = (long)Math.Round(nextLevelXP * percent);

            if (max > 0)
                scaledXP = Math.Min(scaledXP, max);

            if (min > 0)
                scaledXP = Math.Max(scaledXP, min);

            var shareType = shareable ? ShareType.All : ShareType.None;


            // apply xp modifiers?
            EarnXP(scaledXP, XpType.Quest, shareType);
        }

        static int GetHoTSpell(string spellName)
        {
            string[] spellNames =
            {
            // Health
            "Incantation of Heal Self",
            "Adja's Intervention",
            "Heal Self VI",
            "Heal Self V",
            "Heal Self IV",
            "Heal Self III",
            "Heal Self II",
            "Heal Self I",
            "Incantation of Heal Other",
            "Adja's Gift",
            "Heal Other VI",
            "Heal Other V",
            "Heal Other IV",
            "Heal Other III",
            "Heal Other II",
            "Heal Otehr I",
            
            // Stamina
            "Incantation of Revitalize Self",
            "Robustification",
            "Revitalize Self VI",
            "Revitalize Self V",
            "Revitalize Self IV",
            "Revitalize Self III",
            "Revitalize Self II",
            "Revitalize Self I",
            "Incantation of Revitalize Other",
            "Replenish",
            "Revitalize Other VI",
            "Revitalize Other V",
            "Revitalize Other IV",
            "Revitalize Other III",
            "Revitalize Other II",
            "Revitalize Other I",
        };

            int[] spellMappings =
            {
            // Health
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            
            // Stamina
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
            8,
            7,
            6,
            5,
            4,
            3,
            2,
            1,
        };

            int index = Array.IndexOf(spellNames, spellName);
            int procSpell;

            if (index != -1)
            {
                procSpell = spellMappings[index];
            }
            else
            {
                return 1;
            }

            return procSpell;
        }

        public void LifeMagicHot(Player caster, Player target, int spell)
        {
            // This sets the HoT flag on the target
            var fellows = caster.GetFellowshipTargets();
            double currentUnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            if (fellows != null)
            {
                foreach (var fellow in fellows)
                {
                    if (caster.GetDistance(fellow) < 30.0f)
                    {
                        if (spell == 8)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot8, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 7)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot7, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 6)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot6, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 5)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot5, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 4)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot4, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 3)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot3, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 2)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot2, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 1)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Hot1, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                    }
                }
            }
        }

        public void LifeMagicSot(Player caster, Player target, int spell)
        {
            // This sets the HoT flag on the target
            var fellows = caster.GetFellowshipTargets();
            double currentUnixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

            if (fellows != null)
            {
                foreach (var fellow in fellows)
                {
                    if (caster.GetDistance(fellow) < 30.0f)
                    {
                        if (spell == 8)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot8, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 7)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot7, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 6)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot6, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 5)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot5, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 4)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot4, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 3)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot3, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 2)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot2, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                        else if (spell == 1)
                        {
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.Sot1, true);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.HoTTimestamp, currentUnixTime);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTDuration, MaxHoTDuration);
                            fellow.SetProperty(ACE.Entity.Enum.Properties.PropertyInt.HoTTicks, MaxHoTTicks);
                        }
                    }
                }
            }
        }

        public static void WarMagicChannel(Player caster, Creature target, Spell spell, int numCasts, bool isWeaponSpell)
        {
            var weapon = caster.GetEquippedWand();
            var fellows = caster.GetFellowshipTargets();
            caster.IsWarChanneling = true;

            for (int i = 0; i < numCasts; i++)
            {
                if (fellows != null && target != null && spell != null)
                {
                    foreach (var fellow in fellows)
                    {
                        if (caster.GetDistance(fellow) < 30.0f)
                        {
                            if (fellow.GetDistance(target) < 30.0f)
                            {
                                fellow.WarMagic(target, spell, weapon);
                            }
                        }
                    }
                }
            }

            caster.IsWarChanneling = false;
        }
    }
}
#endregion

namespace ACE.Server.WorldObjects
{
    partial class WorldObject
    {
        /// <summary>
        /// Launches a targeted War Magic spell projectile
        /// </summary>
        protected void WarMagic(WorldObject target, Spell spell, WorldObject weapon, bool isWeaponSpell = false, bool fromProc = false)
        {
            CreateSpellProjectiles(spell, target, weapon, isWeaponSpell, fromProc);
        }
    }
} 

namespace ACE.Server.WorldObjects
{
    partial class Creature
    {

    }
}
