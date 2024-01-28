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
    }
}

namespace ACE.Server.Entity { 
    partial class OfflinePlayer
    {
        public int? BankAccountNumber
        {
            get => GetProperty(PropertyInt.BankAccountNumber);
            set { if (!value.HasValue) RemoveProperty(PropertyInt.BankAccountNumber); else SetProperty(PropertyInt.BankAccountNumber, value.Value); }
        }

        public long? BankedPyreals
        {
            get => GetProperty(PropertyInt64.BankedPyreals);
            set { if (!value.HasValue) RemoveProperty(PropertyInt64.BankedPyreals); else SetProperty(PropertyInt64.BankedPyreals, value.Value); }
        }

        public long? BankedLuminance
        {
            get => GetProperty(PropertyInt64.BankedLuminance);
            set { if (!value.HasValue) RemoveProperty(PropertyInt64.BankedLuminance); else SetProperty(PropertyInt64.BankedLuminance, value.Value); }
        }

        public long? BankedAshcoin
        {
            get => GetProperty(PropertyInt64.BankedAshcoin);
            set { if (!value.HasValue) RemoveProperty(PropertyInt64.BankedAshcoin); else SetProperty(PropertyInt64.BankedAshcoin, value.Value); }
        }

        public bool Hardcore
        {
            get => GetProperty(PropertyBool.Hardcore) ?? false;
            set { if (value) RemoveProperty(PropertyBool.Hardcore); else SetProperty(PropertyBool.Hardcore, value); }
        }

        public long CreatureKills
        {
            get => GetProperty(PropertyInt64.CreatureKills) ?? 0;
            set { if (value == 0) RemoveProperty(PropertyInt64.CreatureKills); else SetProperty(PropertyInt64.CreatureKills, value); }
        }

        public ulong HcPyrealsWon
        {
            get => (ulong)(GetProperty(PropertyInt64.HcPyrealsWon) ?? 0);
            set { if (value == 0) RemoveProperty(PropertyInt64.HcPyrealsWon); else SetProperty(PropertyInt64.HcPyrealsWon, (long)value); }
        }

        public string HcAge
        {
            get => GetProperty(PropertyString.HcAge) ?? "00:00:00:00:00";
            set { if (value == null) RemoveProperty(PropertyString.HcAge); else SetProperty(PropertyString.HcAge, value); }
        }

        public double HcAgeTimestamp
        {
            get => (double)(GetProperty(PropertyFloat.HcAgeTimestamp) ?? 0.0);
            set { if (value == 0.0) RemoveProperty(PropertyFloat.HcAgeTimestamp); else SetProperty(PropertyFloat.HcAgeTimestamp, value); }
        }

        public long HcScore
        {
            get => GetProperty(PropertyInt64.HcScore) ?? 0;
            set { if (value == 0) RemoveProperty(PropertyInt64.HcScore); else SetProperty(PropertyInt64.HcScore, value); }
        }

        public long? BankedCarnageTokens
        {
            get => GetProperty(PropertyInt64.BankedCarnageTokens);
            set { if (!value.HasValue) RemoveProperty(PropertyInt64.BankedCarnageTokens); else SetProperty(PropertyInt64.BankedCarnageTokens, value.Value); }
        }

        public bool HasBounty
        {
            get => GetProperty(PropertyBool.HasBounty) ?? false;
            set { if (!value) RemoveProperty(PropertyBool.HasBounty); else SetProperty(PropertyBool.HasBounty, value); }
        }

        public long? PriceOnHead
        {
            get => GetProperty(PropertyInt64.PriceOnHead);
            set { if (!value.HasValue) RemoveProperty(PropertyInt64.PriceOnHead); else SetProperty(PropertyInt64.PriceOnHead, value.Value); }
        }
    }
}
