using ACE.Entity.Enum.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.WorldObjects;
partial class Creature
{
    public bool IsCombatPet { get; set; }

    //Properties
    public bool IsTank
    {
        get => GetProperty(PropertyBool.IsTank) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsTank); else SetProperty(PropertyBool.IsTank, value); }
    }

    public bool IsDps
    {
        get => GetProperty(PropertyBool.IsDps) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsDps); else SetProperty(PropertyBool.IsDps, value); }
    }

    public bool IsHealer
    {
        get => GetProperty(PropertyBool.IsHealer) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsHealer); else SetProperty(PropertyBool.IsHealer, value); }
    }

    public bool IsTaunting
    {
        get => GetProperty(PropertyBool.IsTaunting) ?? false;
        set { if (!value) RemoveProperty(PropertyBool.IsTaunting); else SetProperty(PropertyBool.IsTaunting, value); }
    }
}
