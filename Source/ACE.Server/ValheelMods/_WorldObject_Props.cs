using ACE.Entity.Enum.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.WorldObjects;
partial class WorldObject
{
    //Properties
    public bool Empowered
    {
        get => GetProperty(PropertyBool.Empowered) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Empowered); else SetProperty(PropertyBool.Empowered, value); }
    }

    public bool Proto
    {
        get => GetProperty(PropertyBool.Proto) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Proto); else SetProperty(PropertyBool.Proto, value); }
    }

    public bool GunBlade
    {
        get => GetProperty(PropertyBool.GunBlade) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Proto); else SetProperty(PropertyBool.GunBlade, value); }
    }

    public bool Arramoran
    {
        get => GetProperty(PropertyBool.Arramoran) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.Arramoran); else SetProperty(PropertyBool.Arramoran, value); }
    }

    public int? MirraArmorBonus
    {
        get => GetProperty(PropertyInt.MirraArmorBonus);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.MirraArmorBonus); else SetProperty(PropertyInt.MirraArmorBonus, value.Value); }
    }

    public int? MirraWeaponBonus
    {
        get => GetProperty(PropertyInt.MirraWeaponBonus);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.MirraWeaponBonus); else SetProperty(PropertyInt.MirraWeaponBonus, value.Value); }
    }

    public int? MirraRatingBonus
    {
        get => GetProperty(PropertyInt.MirraWeaponBonus);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.MirraWeaponBonus); else SetProperty(PropertyInt.MirraWeaponBonus, value.Value); }
    }

    public int? Sockets
    {
        get => GetProperty(PropertyInt.Sockets);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.Sockets); else SetProperty(PropertyInt.Sockets, value.Value); }
    }

    //Speedrunning
    public int? LastTime
    {
        get => GetProperty(PropertyInt.LastTime);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.LastTime); else SetProperty(PropertyInt.LastTime, value.Value); }
    }

    /*public int? BestTime
    {
        get => GetProperty(PropertyInt.BestTime);
        set { if (!value.HasValue) RemoveProperty(PropertyInt.BestTime); else SetProperty(PropertyInt.BestTime, value.Value); }
    }*/

    public string SpeedRunTime
    {
        get => GetProperty(PropertyString.SpeedRunTime);
        set { if (value == null) RemoveProperty(PropertyString.SpeedRunTime); else SetProperty(PropertyString.SpeedRunTime, value); }
    }

    public bool SpeedRunning
    {
        get => GetProperty(PropertyBool.SpeedRunning) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.SpeedRunning); else SetProperty(PropertyBool.SpeedRunning, value); }
    }

    public double? SpeedrunStartTime
    {
        get => GetProperty(PropertyFloat.SpeedrunStartTime);
        set { if (!value.HasValue) RemoveProperty(PropertyFloat.SpeedrunStartTime); else SetProperty(PropertyFloat.SpeedrunStartTime, value.Value); }
    }

    public double? SpeedrunEndTime
    {
        get => GetProperty(PropertyFloat.SpeedrunEndTime);
        set { if (!value.HasValue) RemoveProperty(PropertyFloat.SpeedrunEndTime); else SetProperty(PropertyFloat.SpeedrunEndTime, value.Value); }
    }

    public double CTInCirculation
    {
        get => GetProperty(PropertyFloat.CTInCirculation) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyFloat.CTInCirculation); else SetProperty(PropertyFloat.CTInCirculation, (double)value); }
    }

    public double ACInCirculation
    {
        get => GetProperty(PropertyFloat.ACInCirculation) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyFloat.ACInCirculation); else SetProperty(PropertyFloat.ACInCirculation, (double)value); }
    }

    public double PyrealValue
    {
        get => GetProperty(PropertyFloat.PyrealValue) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyFloat.PyrealValue); else SetProperty(PropertyFloat.PyrealValue, (double)value); }
    }

    public double MMDValue
    {
        get => GetProperty(PropertyFloat.MMDValue) ?? 250000;
        set { if (value == 0) RemoveProperty(PropertyFloat.MMDValue); else SetProperty(PropertyFloat.MMDValue, (double)value); }
    }

    public double ACValue
    {
        get => GetProperty(PropertyFloat.ACValue) ?? 1;
        set { if (value == 0) RemoveProperty(PropertyFloat.ACValue); else SetProperty(PropertyFloat.ACValue, (double)value); }
    }

    public int DoTOwnerGuid
    {
        get => GetProperty(PropertyInt.DoTOwnerGuid) ?? 0;
        set { if (value == 0) RemoveProperty(PropertyInt.DoTOwnerGuid); else SetProperty(PropertyInt.DoTOwnerGuid, value); }
    }

    public bool IsAbilityItem
    {
        get => GetProperty(PropertyBool.IsAbilityItem) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsAbilityItem); else SetProperty(PropertyBool.IsAbilityItem, value); }
    }
}
