using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Hooks;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins
{
	public class ExxoAvalonOrigins : Mod
	{
        public override void Load()
        {
            // ----------- Server/Client ----------- //
            while (ModHook.RegisteredHooks.TryDequeue(out ModHook? hook))
            {
                hook.ApplyHook();
            }
            AvalonReflection.Init();
        }
        public override void Unload()
        {
            AvalonReflection.Unload();
        }
    }
}