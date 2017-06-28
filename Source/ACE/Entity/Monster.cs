﻿using ACE.Entity.Enum;
using ACE.Factories;
using ACE.Managers;
using ACE.Network.Enum;
using ACE.Network.Motion;
using System;

namespace ACE.Entity
{
    public class Monster : Creature
    {
        public Monster(AceObject aceO)
            : base(aceO, new ObjectGuid(GuidManager.NewItemGuid()))
        {
            // TODO: Check why Drudges don't appear on radar yet and don't have a healthbar when you select them
            if (aceO.WeenieClassId < 0x8000u)
                this.WeenieClassid = aceO.WeenieClassId;
            else
                this.WeenieClassid = (ushort)(aceO.WeenieClassId - 0x8000);
            IsAlive = true;
            SetupVitals();
        }
    }
}
