using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Mod
{
    /// <summary>
    /// Defines interactions between mods and their ACE.Server host
    /// </summary>
    public interface IHarmonyMod : IDisposable //, IAsyncDisposable //Todo: Decide on async support
    {
        //https://github.com/natemcmaster/DotNetCorePlugins#what-is-a-shared-type

        void Initialize();
        //void Shutdown();
        //void Patch();
        //void Unpatch();
        //void Reset();
        //Harmony Harmony { get; }
        //MethodInfo[] CustomMethods();

    }
}
