using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACE.Server.Mod
{
    /// <summary>
    /// Defines interactions between mods and their ACE.Server host
    /// </summary>
    public interface IHarmonyMod : IDisposable //, IAsyncDisposable //Todo: Decide on async support
    {
        //https://github.com/natemcmaster/DotNetCorePlugins#what-is-a-shared-type

        void Initialize();

        //Hack for handling chat commands?  Fails to give access to Session
        //void Command(string command);
    }
}
