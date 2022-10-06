using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ACE.Server.Mod
{
    public class ModMetadata
    {
        public const string FILENAME = "Meta.json";
        public const string TYPENAME = "Mod";

        public string Name { get; set; } = "SomeMod";
        public string Author { get; set; }
        public string Description { get; set; }
        public string Version { get; set; } = "1.0";
        public uint Priority { get; set; }

        /// <summary>
        /// Determines whether mod is patched on load.
        /// </summary>
        public bool Enabled { get; set; }

        #region Requirements/Conflicts
        ////Todo
        ///// <summary>
        ///// Mods that must be available
        ///// </summary>
        //public IList<ModMetadata> Dependencies { get; set; }

        ///// <summary>
        ///// 
        ///// Mods that cannot be available
        ///// </summary>
        //public IList<ModMetadata> Conflicts { get; set; }

        ///// <summary>
        ///// Server requirements
        ///// </summary>
        //public string ACEVersion { get; set; } = "0.0"; 
        #endregion
    }
}
