using Avalon.Assets;
using Avalon.Common;
using Avalon.Hooks;
using Avalon.UI;
using Avalon.UI.StatDisplay;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Graphics;
using static Terraria.Graphics.FinalFractalHelper;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Items.Weapons.Melee.Hardmode;
using Microsoft.Xna.Framework;
using System.Reflection;
using System.IO;

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

    internal UserInterface minionSlotInterface;
    internal MinionSlotCounter minionSlotCounter;

    //internal UserInterface statDisplayInterface;
    //internal StatDisplayUIState statDisplay;
    public override void Load()
    {
        //Additional swords to the zenith's projectiles with both their texture, size and trail color
        var fractalProfiles = (Dictionary<int, FinalFractalProfile>)typeof(FinalFractalHelper).GetField("_fractalProfiles", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

        fractalProfiles.Add(ItemID.GoldBroadsword, new FinalFractalProfile(50f, new Color(203, 179, 73))); //Add the Gold Broadsword with a gold trail at 50f the size, would reccomend to look at the dictionary that we are reflecting before adding any swords to know what size and trail color to do
        fractalProfiles.Add(ItemID.PlatinumBroadsword, new FinalFractalProfile(50f, new Color(181, 194, 217)));
        fractalProfiles.Add(ModContent.ItemType<BismuthBroadsword>(), new FinalFractalProfile(50f, new Color(199, 157, 216)));
        fractalProfiles.Add(ItemID.IceBlade, new FinalFractalProfile(48f, new Color(54, 232, 252)));
        fractalProfiles.Add(ItemID.AntlionClaw, new FinalFractalProfile(40f, new Color(233, 174, 78)));
        fractalProfiles.Add(ModContent.ItemType<DesertLongsword>(), new FinalFractalProfile(48f, new Color(220, 214, 137)));
        fractalProfiles.Add(ModContent.ItemType<MinersSword>(), new FinalFractalProfile(40f, new Color(51, 235, 203)));
        fractalProfiles.Add(ModContent.ItemType<IridiumGreatsword>(), new FinalFractalProfile(70f, new Color(165, 209, 148)));
        fractalProfiles.Add(ModContent.ItemType<OsmiumGreatsword>(), new FinalFractalProfile(70f, new Color(43, 186, 217)));
        fractalProfiles.Add(ModContent.ItemType<RhodiumGreatsword>(), new FinalFractalProfile(70f, new Color(176, 42, 82)));
        fractalProfiles.Add(ModContent.ItemType<AeonsEternity>(), new FinalFractalProfile(50f, new Color(0, 155, 251)));
        fractalProfiles.Add(ModContent.ItemType<Snotsabre>(), new FinalFractalProfile(70f, new Color(115, 159, 109)));
        fractalProfiles.Add(ModContent.ItemType<VertexOfExcalibur>(), new FinalFractalProfile(80f, new Color(120, 109, 204)));

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

        //minionSlotInterface = new UserInterface();
        //minionSlotCounter = new MinionSlotCounter();
        //minionSlotInterface.SetState(minionSlotCounter);

        //statDisplay = new StatDisplayUIState();
        //statDisplayInterface = new UserInterface();
        //statDisplayInterface.SetState(statDisplay);

        if (ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement)
        {
            ReplaceVanillaTextures();
        }
    }
    public override void Unload()
    {
        var fractalProfiles = (Dictionary<int, FinalFractalProfile>)typeof(FinalFractalHelper).GetField("_fractalProfiles", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

        fractalProfiles.Remove(ItemID.GoldBroadsword);
        fractalProfiles.Remove(ItemID.PlatinumBroadsword);
        fractalProfiles.Remove(ModContent.ItemType<BismuthBroadsword>());
        fractalProfiles.Remove(ItemID.IceBlade);
        fractalProfiles.Remove(ItemID.AntlionClaw);
        fractalProfiles.Remove(ModContent.ItemType<DesertLongsword>());
        fractalProfiles.Remove(ModContent.ItemType<MinersSword>());
        fractalProfiles.Remove(ModContent.ItemType<IridiumGreatsword>());
        fractalProfiles.Remove(ModContent.ItemType<OsmiumGreatsword>());
        fractalProfiles.Remove(ModContent.ItemType<RhodiumGreatsword>());
        fractalProfiles.Remove(ModContent.ItemType<AeonsEternity>());
        fractalProfiles.Remove(ModContent.ItemType<Snotsabre>());
        fractalProfiles.Remove(ModContent.ItemType<VertexOfExcalibur>());

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
        itemReplacer.ReplaceAsset(ItemID.PulseBow,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/PulseBow"));
        itemReplacer.ReplaceAsset(ItemID.Bell,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/Bell"));
        itemReplacer.ReplaceAsset(ItemID.FairyBell,
            Assets.Request<Texture2D>("Assets/Vanilla/Items/FairyBell"));

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

        var npcReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Npc);
        assetReplacers.Add(npcReplacer);
        npcReplacer.ReplaceAsset(NPCID.BlueSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/Slime"));
        npcReplacer.ReplaceAsset(NPCID.MotherSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/MotherSlime"));
        npcReplacer.ReplaceAsset(NPCID.DungeonSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/DungeonSlime"));
        npcReplacer.ReplaceAsset(NPCID.SlimeSpiked, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/SpikedSlime"));
        npcReplacer.ReplaceAsset(NPCID.IlluminantSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/IlluminantSlime"));
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        Network.MessageHandler.HandlePacket(reader, whoAmI);
    }
}
