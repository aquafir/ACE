using System;

namespace ACE.Server.Mod
{
	public class ModEntry
	{
		public ModStatus Status = ModStatus.Unloaded;
        public Type ModType { get; set; } 
		public IHarmonyMod ModInstance { get; set; }
		public ModMetadata ModMetadata { get; set; }
        public string Source { get; set; }
        //public string Source { get => ModType is null ? "" : ModType.Assembly.Location; }
        //public string Error { get; set; }
    }

    public enum ModStatus
    {
        Unloaded,	        //Assembly not loaded
        Active,		        //Loaded and active
        Inactive,	        //Loaded and activatable
		LoadFailure,        //Failed to load assembly
        NameConflict,       //Mod loaded but a higher priority mod has the same name
        //MissingDependency,  //Keeping it simple for now
        //Conflict,           //Loaded and conflict detected
    }
}
