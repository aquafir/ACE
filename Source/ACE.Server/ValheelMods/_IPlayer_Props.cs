using ACE.Entity.Enum.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.Entity;
public partial interface IPlayer
{
    int? BankAccountNumber { get; set; }

    long? BankedPyreals { get; set; }

    long? BankedLuminance { get; set; }

    long? BankedAshcoin { get; set; }

    bool Hardcore { get; set; }

    long? CreatureKills { get; set; }

    ulong HcPyrealsWon { get; set; }

    string HcAge { get; set; }

    double HcAgeTimestamp { get; set; }

    long HcScore { get; set; }

    long? BankedCarnageTokens { get; set; }

    bool HasBounty { get; set; }

    long? PriceOnHead { get; set; }
}
