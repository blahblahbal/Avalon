using ExxoAvalonOrigins.Hooks;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins
{
	public class ExxoAvalonOrigins : Mod
	{
        public override void Load()
        {
            AvalonReflection.Init();
        }
        public override void Unload()
        {
            AvalonReflection.Unload();
        }
    }
}