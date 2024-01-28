using ACE.Entity.Enum.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.Entity;
public partial class OfflinePlayer
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

    public long? CreatureKills
    {
        get => GetProperty(PropertyInt64.CreatureKills) ?? 0;
        set { if (!value.HasValue) RemoveProperty(PropertyInt64.CreatureKills); else SetProperty(PropertyInt64.CreatureKills, value.Value); }
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
