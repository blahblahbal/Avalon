using Avalon.Common;
using Avalon.Hooks;
using Avalon.UI;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon; 

public class ExxoAvalonOrigins : Mod
{
    /// <summary>
    ///     Gets reference to the main instance of the mod.
    /// </summary>
    public static ExxoAvalonOrigins Mod { get; private set; } = ModContent.GetInstance<ExxoAvalonOrigins>();

    /// <summary>
    ///     Gets the instance of the music mod for this mod.
    /// </summary>
    public static readonly Mod? MusicMod = ModLoader.TryGetMod("AvalonMusic", out Mod obtainedMod) ? obtainedMod : null;

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
