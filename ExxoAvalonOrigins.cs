using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Hooks;
using ExxoAvalonOrigins.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace ExxoAvalonOrigins
{
	public class ExxoAvalonOrigins : Mod
	{
        /// <summary>
        ///     Gets reference to the main instance of the mod.
        /// </summary>
        public static ExxoAvalonOrigins Mod { get; private set; } = ModContent.GetInstance<ExxoAvalonOrigins>();

        public const string TextureAssetsPath = "Assets/Textures";
        internal UserInterface staminaInterface;
        internal StaminaBar staminaBar;
        public override void Load()
        {
            // ----------- Server/Client ----------- //
            while (ModHook.RegisteredHooks.TryDequeue(out ModHook? hook))
            {
                hook.ApplyHook();
            }
            AvalonReflection.Init();
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }

            // ----------- Client Only ----------- //
            staminaInterface = new UserInterface();
            staminaBar = new StaminaBar();
            staminaInterface.SetState(staminaBar);
        }
        public override void Unload()
        {
            Mod = null;
            AvalonReflection.Unload();
        }
    }
}