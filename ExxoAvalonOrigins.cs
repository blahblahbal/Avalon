using Avalon.Assets;
using Avalon.Common;
using Avalon.Hooks;
using Avalon.UI;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
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

    private readonly List<IReplaceAssets> assetReplacers = new();

    /// <summary>
    ///     Gets or sets the transition value for fading the caesium background in and out.
    /// </summary>
    public static float CaesiumTransition { get; set; }

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

        if (ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement)
        {
            ReplaceVanillaTextures();
        }
    }
    public override void Unload()
    {
        Mod = null;
        AvalonReflection.Unload();
    }
    private void ReplaceVanillaTextures()
    {
        var itemReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Item);
        assetReplacers.Add(itemReplacer);
        itemReplacer.ReplaceAsset(ItemID.HallowedKey, Assets.Request<Texture2D>("Assets/Vanilla/Items/HallowedKey"));
        itemReplacer.ReplaceAsset(ItemID.MagicDagger, Assets.Request<Texture2D>("Assets/Vanilla/Items/MagicDagger"));
        itemReplacer.ReplaceAsset(ItemID.PaladinBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/PaladinBanner"));
        itemReplacer.ReplaceAsset(ItemID.PossessedArmorBanner,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/PossessedArmorBanner"));
        itemReplacer.ReplaceAsset(ItemID.BoneLeeBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/BoneLeeBanner"));
        itemReplacer.ReplaceAsset(ItemID.AngryTrapperBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/AngryTrapperBanner"));
        itemReplacer.ReplaceAsset(ItemID.Deathweed, Assets.Request<Texture2D>("Assets/Vanilla/Items/Deathweed"));
        itemReplacer.ReplaceAsset(ItemID.WaterleafSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/WaterleafSeeds"));
        itemReplacer.ReplaceAsset(ItemID.ShroomiteDiggingClaw,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/ShroomiteDiggingClaws"));
        itemReplacer.ReplaceAsset(ItemID.BloodMoonStarter,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/BloodyAmulet"));

        var tileReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Tile);
        assetReplacers.Add(tileReplacer);
        tileReplacer.ReplaceAsset(TileID.CopperCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/CopperCoin"));
        tileReplacer.ReplaceAsset(TileID.SilverCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/SilverCoin"));
        tileReplacer.ReplaceAsset(TileID.GoldCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/GoldCoin"));
        tileReplacer.ReplaceAsset(TileID.PlatinumCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/PlatinumCoin"));
        tileReplacer.ReplaceAsset(TileID.Banners, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/VanillaBanners"));
        tileReplacer.ReplaceAsset(TileID.Containers, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/VanillaChests"));

        var projectileReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Projectile);
        assetReplacers.Add(projectileReplacer);
        projectileReplacer.ReplaceAsset(ProjectileID.MagicDagger, Assets.Request<Texture2D>("Assets/Vanilla/Items/MagicDagger"));
    }
}
