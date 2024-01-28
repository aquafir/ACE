using System.ComponentModel;

namespace ACE.Entity.Enum.Properties
{
    // properties marked as ServerOnly are properties we never saw in PCAPs, from here:
    // http://ac.yotesfan.com/ace_object/not_used_enums.php
    // source: @OptimShi
    // description attributes are used by the weenie editor for a cleaner display name
    public enum PropertyInt64 : ushort
    {
        Undef               = 0,
        [SendOnLogin]
        TotalExperience     = 1,
        [SendOnLogin]
        AvailableExperience = 2,
        AugmentationCost    = 3,
        ItemTotalXp         = 4,
        ItemBaseXp          = 5,
        [SendOnLogin]
        AvailableLuminance  = 6,
        [SendOnLogin]
        MaximumLuminance    = 7,
        InteractionReqs     = 8,

        /* custom */
        [ServerOnly]
        AllegianceXPCached    = 9000,
        [ServerOnly]
        AllegianceXPGenerated = 9001,
        [ServerOnly]
        AllegianceXPReceived  = 9002,
        [ServerOnly]
        VerifyXp              = 9003,

        //Valheel
        [SendOnLogin]
        VitaeCpPool = 129,
        [ServerOnly]
        TotalXpBeyond = 9004,
        [ServerOnly]
        BankedPyreals = 9005,
        [ServerOnly]
        BankedLuminance = 9006,
        [ServerOnly]
        BankedAshcoin = 9007,
        [ServerOnly]
        PyrealSavings = 9008,
        [ServerOnly]
        HcPyrealsWon = 9009,
        [ServerOnly]
        BankedCarnageTokens = 9010,
        [ServerOnly]
        HcScore = 9011,
        [ServerOnly]
        CreatureKills = 9012,
        [ServerOnly]
        PriceOnHead = 9013,
    }

    public static class PropertyInt64Extensions
    {
        public static string GetDescription(this PropertyInt64 prop)
        {
            var description = prop.GetAttributeOfType<DescriptionAttribute>();
            return description?.Description ?? prop.ToString();
        }
    }
}
