using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Avalon.Assets;
using Avalon.Biomes;
using Avalon.Common;
using Avalon.Effects;
using Avalon.Hooks;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Melee.PreHardmode;
using Avalon.Network;
using Avalon.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.Graphics.FinalFractalHelper;

namespace Avalon;

public class ExxoAvalonOrigins : Mod
{
    public static string JungleGenMessage;

    public static Color ContagionBiomeSightColor = new Color(170, 255, 0);
    /// <summary>
    ///     Gets reference to the main instance of the mod.
    /// </summary>
    public static ExxoAvalonOrigins Mod { get; private set; } = ModContent.GetInstance<ExxoAvalonOrigins>();

    public static readonly Mod? DragonLens = ModLoader.TryGetMod("DragonLens", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets the instance of the Confection mod.
    /// </summary>
    public static readonly Mod? Confection = ModLoader.TryGetMod("TheConfectionRebirth", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets the instance of Fargo's Mod.
    /// </summary>
    public static readonly Mod? Fargo = ModLoader.TryGetMod("Fargowiltas", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets the instance of the TMLAchievements mod.
    /// </summary>
    public static readonly Mod? Achievements = ModLoader.TryGetMod("TMLAchievements", out Mod obtainedMod) ? obtainedMod : null;

    /// <summary>
    ///     Gets the instance of the music mod for this mod.
    /// </summary>
    public static readonly Mod? MusicMod = ModLoader.TryGetMod("AvalonMusic", out Mod obtainedMod) ? obtainedMod : null;

    public static HellcastleFogSystem hellcastleFog = new HellcastleFogSystem();

    /// <summary>
    ///     Gets the instance of the Tokens mod.
    /// </summary>
    public static readonly Mod? Tokens = ModLoader.TryGetMod("Tokens", out Mod obtainedMod) ? obtainedMod : null;

    public static Color LastDiscoRGB;

    private readonly List<IReplaceAssets> assetReplacers = new();

    /// <summary>
    ///     Gets or sets the transition value for fading the caesium background in and out.
    /// </summary>
    public static float CaesiumTransition { get; set; }

    public const string TextureAssetsPath = "Assets/Textures";
    internal UserInterface staminaInterface;
    internal StaminaBar staminaBar;
    private static readonly Func<Dictionary<int, FinalFractalProfile>> getFinalFractalHelperFractalProfiles = Reflection.Utilities.CreateFieldReader<Dictionary<int, FinalFractalProfile>>(typeof(FinalFractalHelper).GetField("_fractalProfiles", BindingFlags.Static | BindingFlags.NonPublic)!);

    //internal UserInterface statDisplayInterface;
    //internal StatDisplayUIState statDisplay;
    public override void Load()
    {
        JungleGenMessage = Lang.gen[11].Value;
        //Additional swords to the zenith's projectiles with both their texture, size and trail color
        var fractalProfiles = getFinalFractalHelperFractalProfiles();

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
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        // ----------- Client Only ----------- //
        staminaInterface = new UserInterface();
        staminaBar = new StaminaBar();
        staminaInterface.SetState(staminaBar);

        //statDisplay = new StatDisplayUIState();
        //statDisplayInterface = new UserInterface();
        //statDisplayInterface.SetState(statDisplay);

        if (ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement)
        {
            ReplaceVanillaTextures();
        }
        Asset<Effect> shader =
            ModContent.Request<Effect>("Avalon/Effects/DarkMatterSkyShader", AssetRequestMode.ImmediateLoad);
        SkyManager.Instance["Avalon:DarkMatter"] = new DarkMatterSky();
        Filters.Scene["Avalon:DarkMatter"] = new Filter(
            new DarkMatterScreenShader(new Ref<Effect>(shader.Value), "DarkMatterSky")
                .UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);
    }
    public override void Unload()
    {
        var fractalProfiles = getFinalFractalHelperFractalProfiles();

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

        foreach (var assetReplacer in assetReplacers)
        {
            assetReplacer.RestoreAssets();
        }
        
        Mod = null!;
    }
    private void ReplaceVanillaTextures()
    {
        //if (DragonLens != null)
        //{
        //    return;
        //}
        var itemReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Item);
        assetReplacers.Add(itemReplacer);
        itemReplacer.ReplaceAsset(ItemID.HallowedKey, Assets.Request<Texture2D>("Assets/Vanilla/Items/HallowedKey"));
        itemReplacer.ReplaceAsset(ItemID.MagicDagger, Assets.Request<Texture2D>("Assets/Vanilla/Items/MagicDagger"));
        itemReplacer.ReplaceAsset(ItemID.PaladinBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/PaladinBanner"));
        itemReplacer.ReplaceAsset(ItemID.PossessedArmorBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/PossessedArmorBanner"));
        itemReplacer.ReplaceAsset(ItemID.BoneLeeBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/BoneLeeBanner"));
        itemReplacer.ReplaceAsset(ItemID.AngryTrapperBanner, Assets.Request<Texture2D>("Assets/Vanilla/Items/AngryTrapperBanner"));
        itemReplacer.ReplaceAsset(ItemID.Deathweed, Assets.Request<Texture2D>("Assets/Vanilla/Items/Deathweed"));
        itemReplacer.ReplaceAsset(ItemID.WaterleafSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/WaterleafSeeds"));
        itemReplacer.ReplaceAsset(ItemID.GrassSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/GrassSeeds"));
        itemReplacer.ReplaceAsset(ItemID.MushroomGrassSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/MushroomGrassSeeds"));
        itemReplacer.ReplaceAsset(ItemID.JungleGrassSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/JungleGrassSeeds"));
        itemReplacer.ReplaceAsset(ItemID.AshGrassSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/AshGrassSeeds"));
        itemReplacer.ReplaceAsset(ItemID.HallowedSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/HallowedSeeds"));
        itemReplacer.ReplaceAsset(ItemID.CorruptSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/CorruptSeeds"));
        itemReplacer.ReplaceAsset(ItemID.CrimsonSeeds, Assets.Request<Texture2D>("Assets/Vanilla/Items/CrimsonSeeds"));
        itemReplacer.ReplaceAsset(ItemID.CrimtaneBar, Assets.Request<Texture2D>("Assets/Vanilla/Items/CrimtaneBar"));
        itemReplacer.ReplaceAsset(ItemID.HellstoneBar, Assets.Request<Texture2D>("Assets/Vanilla/Items/HellstoneBar"));
        itemReplacer.ReplaceAsset(ItemID.ShroomiteDiggingClaw, Assets.Request<Texture2D>("Assets/Vanilla/Items/ShroomiteDiggingClaws"));
        itemReplacer.ReplaceAsset(ItemID.BloodMoonStarter, Assets.Request<Texture2D>("Assets/Vanilla/Items/BloodyAmulet"));
        itemReplacer.ReplaceAsset(ItemID.PulseBow, Assets.Request<Texture2D>("Assets/Vanilla/Items/PulseBow"));
        itemReplacer.ReplaceAsset(ItemID.Bell, Assets.Request<Texture2D>("Assets/Vanilla/Items/Bell"));
        itemReplacer.ReplaceAsset(ItemID.FairyBell, Assets.Request<Texture2D>("Assets/Vanilla/Items/FairyBell"));
        itemReplacer.ReplaceAsset(ItemID.TerraBlade, Assets.Request<Texture2D>("Assets/Vanilla/Items/Terrablade"));
        itemReplacer.ReplaceAsset(ItemID.TheHorsemansBlade, Assets.Request<Texture2D>("Assets/Vanilla/Items/HorsemansBlade"));
        itemReplacer.ReplaceAsset(ItemID.TrueExcalibur, Assets.Request<Texture2D>("Assets/Vanilla/Items/TrueExcalibur"));
        itemReplacer.ReplaceAsset(ItemID.CandyCornRifle, Assets.Request<Texture2D>("Assets/Vanilla/Items/CandyCornRifle"));
        itemReplacer.ReplaceAsset(ItemID.BouncyBomb, Assets.Request<Texture2D>("Assets/Vanilla/Items/BouncyBomb"));
        itemReplacer.ReplaceAsset(ItemID.BouncyGrenade, Assets.Request<Texture2D>("Assets/Vanilla/Items/BouncyGrenade"));
        itemReplacer.ReplaceAsset(ItemID.BatScepter, Assets.Request<Texture2D>("Assets/Vanilla/Items/BatScepter"));
        itemReplacer.ReplaceAsset(ItemID.EnchantedSword, Assets.Request<Texture2D>("Assets/Vanilla/Items/EnchantedSword"));

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
        projectileReplacer.ReplaceAsset(ProjectileID.BouncyBomb, Assets.Request<Texture2D>("Assets/Vanilla/Items/BouncyBomb"));
        projectileReplacer.ReplaceAsset(ProjectileID.BouncyGrenade, Assets.Request<Texture2D>("Assets/Vanilla/Items/BouncyGrenade"));

        var npcReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Npc);
        assetReplacers.Add(npcReplacer);
        npcReplacer.ReplaceAsset(NPCID.BlueSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/Slime"));
        npcReplacer.ReplaceAsset(NPCID.MotherSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/MotherSlime"));
        npcReplacer.ReplaceAsset(NPCID.DungeonSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/DungeonSlime"));
        npcReplacer.ReplaceAsset(NPCID.SlimeSpiked, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/SpikedSlime"));
        npcReplacer.ReplaceAsset(NPCID.IlluminantSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/IlluminantSlime"));
        npcReplacer.ReplaceAsset(NPCID.LavaSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/LavaSlime"));
        npcReplacer.ReplaceAsset(NPCID.IceSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/IceSlime"));
        npcReplacer.ReplaceAsset(NPCID.SpikedIceSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/SpikedIceSlime"));
        npcReplacer.ReplaceAsset(NPCID.SpikedJungleSlime, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/SpikedJungleSlime"));
        npcReplacer.ReplaceAsset(NPCID.WindyBalloon, Assets.Request<Texture2D>("Assets/Vanilla/NPCs/BalloonSlime"));
    }

    public void BTitlesHook_SetupBiomeCheckers(out Func<Player, string> miniBiomeChecker, out Func<Player, string> biomeChecker)
    {
        miniBiomeChecker = player => {
            

            return "";
        };
        biomeChecker = player => {
            if (player.InModBiome<NearHellcastle>()) return "PhantomGardens";
            if (player.InModBiome<Hellcastle>()) return "Hellcastle";
            if (player.InModBiome<Contagion>()) return "Contagion";

            return "";
        };
    }

    public dynamic BTitlesHook_GetBiome(int index)
    {
        switch (index)
        {
            case 0:
                return new
                {
                    Key = "PhantomGardens",
                    Title = "Phantom Gardens",
                    SubTitle = "Avalon",
                    Icon = ModContent.Request<Texture2D>("Avalon/Biomes/Hellcastle_Icon").Value,
                    TitleColor = new Color(35, 200, 254),
                    TitleStroke = new Color(13, 77, 113),
                };
            case 1:
                return new
                {
                    Key = "Hellcastle",
                    Title = "Hellcastle",
                    SubTitle = "Avalon",
                    Icon = ModContent.Request<Texture2D>("Avalon/Biomes/Hellcastle_Icon").Value,
                    TitleColor = Color.LightGray
                };
            case 2:
                return new
                {
                    Key = "Contagion",
                    Title = "Contagion",
                    SubTitle = "Avalon",
                    TitleColor = new Color(191, 212, 52),
                    TitleStroke = new Color(39, 29, 22),
                    Icon = ModContent.Request<Texture2D>("Avalon/Biomes/Contagion_Icon").Value,
                };
            default:
                return null;
        }
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI) => MessageHandler.HandlePacket(reader, whoAmI);
}
