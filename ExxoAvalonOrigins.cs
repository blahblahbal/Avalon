using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Hooks;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins
{
	public class ExxoAvalonOrigins : Mod
	{
        /// <summary>
        ///     Gets reference to the main instance of the mod.
        /// </summary>
        public static ExxoAvalonOrigins Mod { get; private set; } = ModContent.GetInstance<ExxoAvalonOrigins>();

        public const string TextureAssetsPath = "Assets/Textures";
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