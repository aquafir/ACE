using HarmonyLib;
using System;

namespace ACE.Server.Mod
{
    public interface IHarmonyMod
    {
		Harmony Harmony { get; }

		void Initialize();
        void Shutdown();
        //void Patch();
        //void Unpatch();
        //void Reset();
    }
}
