using ACE.Entity.Enum.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.WorldObjects;
partial class Player
{
    //Properties
    public int RaisedStr
    {
        get => GetProperty(PropertyInt.RaisedStr) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedStr); else SetProperty(PropertyInt.RaisedStr, value); }
    }

    public int RaisedEnd
    {
        get => GetProperty(PropertyInt.RaisedEnd) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedEnd); else SetProperty(PropertyInt.RaisedEnd, value); }
    }

    public int RaisedCoord
    {
        get => GetProperty(PropertyInt.RaisedCoord) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedCoord); else SetProperty(PropertyInt.RaisedCoord, value); }
    }

    public int RaisedQuick
    {
        get => GetProperty(PropertyInt.RaisedQuick) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedQuick); else SetProperty(PropertyInt.RaisedQuick, value); }
    }

    public int RaisedFocus
    {
        get => GetProperty(PropertyInt.RaisedFocus) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedFocus); else SetProperty(PropertyInt.RaisedFocus, value); }
    }

    public int RaisedSelf
    {
        get => GetProperty(PropertyInt.RaisedSelf) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedSelf); else SetProperty(PropertyInt.RaisedSelf, value); }
    }
    //new vitals
    public int RaisedHealth
    {
        get => GetProperty(PropertyInt.RaisedHealth) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedHealth); else SetProperty(PropertyInt.RaisedHealth, value); }
    }

    public int RaisedStamina
    {
        get => GetProperty(PropertyInt.RaisedStamina) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedStamina); else SetProperty(PropertyInt.RaisedStamina, value); }
    }

    public int RaisedMana
    {
        get => GetProperty(PropertyInt.RaisedMana) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.RaisedMana); else SetProperty(PropertyInt.RaisedMana, value); }
    }

    public long? TotalXpBeyond
    {
        get => GetProperty(PropertyInt64.TotalXpBeyond);
        set { if (!value.HasValue) RemoveProperty(PropertyInt64.TotalXpBeyond); else SetProperty(PropertyInt64.TotalXpBeyond, value.Value); }
    }

    public int? LastLevel
    {
        get => GetProperty(PropertyInt.LastLevel);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.LastLevel); else SetProperty(PropertyInt.LastLevel, value.Value); }
    }

    public int? NumberOfPets
    {
        get => GetProperty(PropertyInt.NumberOfPets);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.NumberOfPets); else SetProperty(PropertyInt.NumberOfPets, value.Value); }
    }

    public int? BestTime
    {
        get => GetProperty(PropertyInt.BestTime);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.BestTime); else SetProperty(PropertyInt.BestTime, value.Value); }
    }

    public bool Ascended
    {
        get => GetProperty(PropertyBool.Ascended) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Ascended); else SetProperty(PropertyBool.Ascended, value); }
    }

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

    public long? PyrealSavings
    {
        get => GetProperty(PropertyInt64.PyrealSavings);
        set { if (!value.HasValue) RemoveProperty(PropertyInt64.PyrealSavings); else SetProperty(PropertyInt64.PyrealSavings, value.Value); }
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

    public double? BankCommandTimer
    {
        get => GetProperty(PropertyFloat.BankCommandTimer);
        set { if (!value.HasValue) RemoveProperty(PropertyFloat.BankCommandTimer); else SetProperty(PropertyFloat.BankCommandTimer, value.Value); }
    }

    public double? InterestTimer
    {
        get => GetProperty(PropertyFloat.InterestTimer);
        set { if (!value.HasValue) RemoveProperty(PropertyFloat.InterestTimer); else SetProperty(PropertyFloat.InterestTimer, value.Value); }
    }
    public double? WithdrawTimer
    {
        get => GetProperty(PropertyFloat.WithdrawTimer);
        set { if (!value.HasValue) RemoveProperty(PropertyFloat.WithdrawTimer); else SetProperty(PropertyFloat.WithdrawTimer, value.Value); }
    }

    public bool Hardcore
    {
        get => GetProperty(PropertyBool.Hardcore) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hardcore); else SetProperty(PropertyBool.Hardcore, value); }
    }

    public ulong HcPyrealsWon
    {
        get => (ulong)(GetProperty(PropertyInt64.HcPyrealsWon) ?? 0);
        set { if (value == 0) RemoveProperty(PropertyInt64.HcPyrealsWon); else SetProperty(PropertyInt64.HcPyrealsWon, (long)value); }
    }

    public string HcAge
    {
        get => GetProperty(PropertyString.HcAge);
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

    public int MonsterKillsMilestones
    {
        get => GetProperty(PropertyInt.MonsterKillsMilestones) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.MonsterKillsMilestones); else SetProperty(PropertyInt.MonsterKillsMilestones, value); }
    }

    public int HcPyrealsWonMilestones
    {
        get => GetProperty(PropertyInt.HcPyrealsWonMilestones) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.HcPyrealsWonMilestones); else SetProperty(PropertyInt.HcPyrealsWonMilestones, value); }
    }

    public int HcScoreMilestones
    {
        get => GetProperty(PropertyInt.HcScoreMilestones) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.HcScoreMilestones); else SetProperty(PropertyInt.HcScoreMilestones, value); }
    }

    public int LevelMilestones
    {
        get => GetProperty(PropertyInt.LevelMilestones) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.LevelMilestones); else SetProperty(PropertyInt.LevelMilestones, value); }
    }

    public int PrestigeMilestones
    {
        get => GetProperty(PropertyInt.PrestigeMilestones) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.PrestigeMilestones); else SetProperty(PropertyInt.PrestigeMilestones, value); }
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

    public bool IsHoTTicking
    {
        get => GetProperty(PropertyBool.IsHoTTicking) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsHoTTicking); else SetProperty(PropertyBool.IsHoTTicking, value); }
    }

    public bool IsWarChanneling
    {
        get => GetProperty(PropertyBool.IsWarChanneling) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsWarChanneling); else SetProperty(PropertyBool.IsWarChanneling, value); }
    }

    public bool Hot1
    {
        get => GetProperty(PropertyBool.Hot1) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot1); else SetProperty(PropertyBool.Hot1, value); }
    }

    public bool Hot2
    {
        get => GetProperty(PropertyBool.Hot2) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot2); else SetProperty(PropertyBool.Hot2, value); }
    }

    public bool Hot3
    {
        get => GetProperty(PropertyBool.Hot3) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot3); else SetProperty(PropertyBool.Hot3, value); }
    }

    public bool Hot4
    {
        get => GetProperty(PropertyBool.Hot4) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot4); else SetProperty(PropertyBool.Hot4, value); }
    }

    public bool Hot5
    {
        get => GetProperty(PropertyBool.Hot5) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot5); else SetProperty(PropertyBool.Hot5, value); }
    }

    public bool Hot6
    {
        get => GetProperty(PropertyBool.Hot6) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot6); else SetProperty(PropertyBool.Hot6, value); }
    }

    public bool Hot7
    {
        get => GetProperty(PropertyBool.Hot7) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot7); else SetProperty(PropertyBool.Hot7, value); }
    }

    public bool Hot8
    {
        get => GetProperty(PropertyBool.Hot8) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Hot8); else SetProperty(PropertyBool.Hot8, value); }
    }

    public bool Sot1
    {
        get => GetProperty(PropertyBool.Sot1) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot1); else SetProperty(PropertyBool.Sot1, value); }
    }

    public bool Sot2
    {
        get => GetProperty(PropertyBool.Sot2) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot2); else SetProperty(PropertyBool.Sot2, value); }
    }

    public bool Sot3
    {
        get => GetProperty(PropertyBool.Sot3) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot3); else SetProperty(PropertyBool.Sot3, value); }
    }

    public bool Sot4
    {
        get => GetProperty(PropertyBool.Sot4) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot4); else SetProperty(PropertyBool.Sot4, value); }
    }

    public bool Sot5
    {
        get => GetProperty(PropertyBool.Sot5) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot5); else SetProperty(PropertyBool.Sot5, value); }
    }

    public bool Sot6
    {
        get => GetProperty(PropertyBool.Sot6) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot6); else SetProperty(PropertyBool.Sot6, value); }
    }

    public bool Sot7
    {
        get => GetProperty(PropertyBool.Sot7) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot7); else SetProperty(PropertyBool.Sot7, value); }
    }

    public bool Sot8
    {
        get => GetProperty(PropertyBool.Sot8) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Sot8); else SetProperty(PropertyBool.Sot8, value); }
    }

    public double HoTTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.HoTTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.HoTTimestamp); else SetProperty(PropertyFloat.HoTTimestamp, value); }
    }

    public int HoTTicks
    {
        get => GetProperty(PropertyInt.HoTTicks) ?? 3;
        set { if (value == 0) RemoveProperty(PropertyInt.HoTTicks); else SetProperty(PropertyInt.HoTTicks, value); }
    }

    public int HoTDuration
    {
        get => GetProperty(PropertyInt.HoTDuration) ?? 15;
        set { if (value == 0) RemoveProperty(PropertyInt.HoTDuration); else SetProperty(PropertyInt.HoTDuration, value); }
    }

    public int MaxHoTTicks
    {
        get => GetProperty(PropertyInt.MaxHoTTicks) ?? 3;
        set { if (value == 0) RemoveProperty(PropertyInt.MaxHoTTicks); else SetProperty(PropertyInt.MaxHoTTicks, value); }
    }

    public int MaxHoTDuration
    {
        get => GetProperty(PropertyInt.MaxHoTDuration) ?? 15;
        set { if (value == 0) RemoveProperty(PropertyInt.MaxHoTDuration); else SetProperty(PropertyInt.MaxHoTDuration, value); }
    }

    public double LastHoTTickTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastHoTTickTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastHoTTickTimestamp); else SetProperty(PropertyFloat.LastHoTTickTimestamp, value); }
    }

    public double LastWarChannelTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastWarChannelTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastWarChannelTimestamp); else SetProperty(PropertyFloat.LastWarChannelTimestamp, value); }
    }

    public double WarChannelChance
    {
        get => (double)(GetProperty(PropertyFloat.WarChannelChance) ?? 0.05);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.WarChannelChance); else SetProperty(PropertyFloat.WarChannelChance, value); }
    }

    public double MeleeDoTChance
    {
        get => (double)(GetProperty(PropertyFloat.MeleeDoTChance) ?? 0.25);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.MeleeDoTChance); else SetProperty(PropertyFloat.MeleeDoTChance, value); }
    }

    public int WarChannelTimerDuration
    {
        get => GetProperty(PropertyInt.WarChannelTimerDuration) ?? 10;
        set { if (value == 0) RemoveProperty(PropertyInt.WarChannelTimerDuration); else SetProperty(PropertyInt.WarChannelTimerDuration, value); }
    }

    public double MissileAoEChance
    {
        get => (double)(GetProperty(PropertyFloat.MissileAoEChance) ?? 0.25);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.MissileAoEChance); else SetProperty(PropertyFloat.MissileAoEChance, value); }
    }

    public int NumOfChannelCasts
    {
        get => GetProperty(PropertyInt.NumOfChannelCasts) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.NumOfChannelCasts); else SetProperty(PropertyInt.NumOfChannelCasts, value); }
    }

    public bool IsTankBuffed
    {
        get => GetProperty(PropertyBool.IsTankBuffed) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsTankBuffed); else SetProperty(PropertyBool.IsTankBuffed, value); }
    }

    public double LastTankBuffTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastTankBuffTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastTankBuffTimestamp); else SetProperty(PropertyFloat.LastTankBuffTimestamp, value); }
    }

    public int TankDefenseRatingIncrease
    {
        get => GetProperty(PropertyInt.TankDefenseRatingIncrease) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.TankDefenseRatingIncrease); else SetProperty(PropertyInt.TankDefenseRatingIncrease, value); }
    }

    public bool TankBuffedTimer
    {
        get => GetProperty(PropertyBool.TankBuffedTimer) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.TankBuffedTimer); else SetProperty(PropertyBool.TankBuffedTimer, value); }
    }

    public bool IsMonk
    {
        get => GetProperty(PropertyBool.IsMonk) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsMonk); else SetProperty(PropertyBool.IsMonk, value); }
    }

    public double LastDamageBuffTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastDamageBuffTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastDamageBuffTimestamp); else SetProperty(PropertyFloat.LastDamageBuffTimestamp, value); }
    }

    public bool IsDamageBuffed
    {
        get => GetProperty(PropertyBool.IsDamageBuffed) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsDamageBuffed); else SetProperty(PropertyBool.IsDamageBuffed, value); }
    }

    public int DamageRatingIncrease
    {
        get => GetProperty(PropertyInt.DamageRatingIncrease) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.DamageRatingIncrease); else SetProperty(PropertyInt.DamageRatingIncrease, value); }
    }

    public bool DamageBuffedTimer
    {
        get => GetProperty(PropertyBool.DamageBuffedTimer) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.DamageBuffedTimer); else SetProperty(PropertyBool.DamageBuffedTimer, value); }
    }

    public int HoTsTicked
    {
        get => GetProperty(PropertyInt.HoTsTicked) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.HoTsTicked); else SetProperty(PropertyInt.HoTsTicked, value); }
    }

    public bool Brutalize
    {
        get => GetProperty(PropertyBool.Brutalize) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Brutalize); else SetProperty(PropertyBool.Brutalize, value); }
    }

    public double LastBrutalizeTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastBrutalizeTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastBrutalizeTimestamp); else SetProperty(PropertyFloat.LastBrutalizeTimestamp, value); }
    }

    public bool DoBrutalizeAttack
    {
        get => GetProperty(PropertyBool.DoBrutalizeAttack) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.DoBrutalizeAttack); else SetProperty(PropertyBool.DoBrutalizeAttack, value); }
    }

    public double BrutalizeTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.BrutalizeTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.BrutalizeTimestamp); else SetProperty(PropertyFloat.BrutalizeTimestamp, value); }
    }

    public double LastLifeWellTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastLifeWellTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastLifeWellTimestamp); else SetProperty(PropertyFloat.LastLifeWellTimestamp, value); }
    }

    public bool LifeWell
    {
        get => GetProperty(PropertyBool.LifeWell) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.LifeWell); else SetProperty(PropertyBool.LifeWell, value); }
    }

    public bool Stealth
    {
        get => GetProperty(PropertyBool.Stealth) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Stealth); else SetProperty(PropertyBool.Stealth, value); }
    }

    public bool IsSneaking
    {
        get => GetProperty(PropertyBool.IsSneaking) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsSneaking); else SetProperty(PropertyBool.IsSneaking, value); }
    }

    public double LastSneakTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastSneakTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastSneakTimestamp); else SetProperty(PropertyFloat.LastSneakTimestamp, value); }
    }

    public double LastTauntTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastTauntTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastTauntTimestamp); else SetProperty(PropertyFloat.LastTauntTimestamp, value); }
    }

    public bool TauntTimerActive
    {
        get => GetProperty(PropertyBool.TauntTimerActive) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.TauntTimerActive); else SetProperty(PropertyBool.TauntTimerActive, value); }
    }

    public bool IsHoTCasting
    {
        get => GetProperty(PropertyBool.IsHoTCasting) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsHoTCasting); else SetProperty(PropertyBool.IsHoTCasting, value); }
    }

    public bool IsSoTCasting
    {
        get => GetProperty(PropertyBool.IsSoTCasting) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsSoTCasting); else SetProperty(PropertyBool.IsSoTCasting, value); }
    }

    public double LastHoTCastTimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastHoTCastTimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastHoTCastTimestamp); else SetProperty(PropertyFloat.LastHoTCastTimestamp, value); }
    }

    public int HoTLevel
    {
        get => GetProperty(PropertyInt.HoTLevel) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyInt.HoTLevel); else SetProperty(PropertyInt.HoTLevel, value); }
    }

    public bool MissileAoE
    {
        get => GetProperty(PropertyBool.MissileAoE) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.MissileAoE); else SetProperty(PropertyBool.MissileAoE, value); }
    }

    public double LastMissileAoETimestamp
    {
        get => (double)(GetProperty(PropertyFloat.LastMissileAoETimestamp) ?? 0.0);
        set { if (value == 0.0) RemoveProperty(PropertyFloat.LastMissileAoETimestamp); else SetProperty(PropertyFloat.LastMissileAoETimestamp, value); }
    }

    public bool DoMissileAoE
    {
        get => GetProperty(PropertyBool.DoMissileAoE) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.DoMissileAoE); else SetProperty(PropertyBool.DoMissileAoE, value); }
    }
}
