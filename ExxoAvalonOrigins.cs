using Avalon.Assets;
using Avalon.Common;
using Avalon.Effects;
using Avalon.Items.Accessories.Info;
using Avalon.Items.Weapons.Melee.Hardmode.VertexOfExcalibur;
using Avalon.Items.Weapons.Melee.PreHardmode.AeonsEternity;
using Avalon.Items.Weapons.Melee.PreHardmode.DesertLongsword;
using Avalon.Items.Weapons.Melee.PreHardmode.MinersSword;
using Avalon.Items.Weapons.Melee.PreHardmode.OreSwords;
using Avalon.Items.Weapons.Melee.PreHardmode.OsmiumTierSwords;
using Avalon.Items.Weapons.Melee.PreHardmode.Snotsabre;
using Avalon.Network;
using Avalon.Reflection;
using Avalon.UI;
using Avalon.WorldGeneration.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.Graphics.FinalFractalHelper;

namespace Avalon;

public class ExxoAvalonOrigins : Mod
{
	public static string? JungleGenMessage;

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
	///     Gets the instance of the Depths mod.
	/// </summary>
	public static readonly Mod? Depths = ModLoader.TryGetMod("TheDepths", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of Thorium mod.
	/// </summary>
	public static readonly Mod? Thorium = ModLoader.TryGetMod("ThoriumMod", out Mod obtainedMod) ? obtainedMod : null;
	/// <summary>
	///		Debug option to entirely enable/disable thorium x avalon compatibility content
	/// </summary>
	public static bool ThoriumContentEnabled = false; // ModLoader.HasMod("ThoriumMod");

	/// <summary>
	///     Gets the instance of Fargo's Mod.
	/// </summary>
	public static readonly Mod? Fargo = ModLoader.TryGetMod("Fargowiltas", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of Music Display Mod.
	/// </summary>
	public static readonly Mod? MusicDisplay = ModLoader.TryGetMod("MusicDisplay", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of Munchies Mod.
	/// </summary>
	public static readonly Mod? Munchies = ModLoader.TryGetMod("Munchies", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of the music mod for this mod.
	/// </summary>
	public static readonly Mod? MusicMod = ModLoader.TryGetMod("AvalonMusic", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of the Tokens mod.
	/// </summary>
	public static readonly Mod? Tokens = ModLoader.TryGetMod("Tokens", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of the Biome Lava mod.
	/// </summary>
	public static readonly Mod? BiomeLava = ModLoader.TryGetMod("BiomeLava", out Mod obtainedMod) ? obtainedMod : null;

	/// <summary>
	///     Gets the instance of the Ophioid mod.
	/// </summary>
	public static readonly Mod? OphioidMod = ModLoader.TryGetMod("OphioidMod", out Mod obtainedMod) ? obtainedMod : null;

	public static HellcastleFogSystem hellcastleFog = new HellcastleFogSystem();

	public static Color LastDiscoRGB;

	private readonly List<IReplaceAssets> assetReplacers = new();

	public static Effect? CalculatorSpectaclesEffect = default;

	public const string TextureAssetsPath = "Assets/Textures";
	internal UserInterface? staminaInterface;
	internal StaminaBar? staminaBar;

	internal UserInterface? CalculatorSpectaclesInterface;
	internal CalcSpec? calcSpec;
	private static readonly Func<Dictionary<int, FinalFractalProfile>> getFinalFractalHelperFractalProfiles = Utilities.CreateFieldReader<Dictionary<int, FinalFractalProfile>>(typeof(FinalFractalHelper).GetField("_fractalProfiles", BindingFlags.Static | BindingFlags.NonPublic)!);

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

		CalculatorSpectaclesInterface = new UserInterface();
		calcSpec = new CalcSpec();
		CalculatorSpectaclesInterface.SetState(calcSpec);

		//statDisplay = new StatDisplayUIState();
		//statDisplayInterface = new UserInterface();
		//statDisplayInterface.SetState(statDisplay);

		if (ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement)
		{
			ReplaceVanillaTextures();
		}
		if (ModContent.GetInstance<AvalonClientConfig>().BloodyAmulet)
		{
			ReplaceBloodyAmulet();
		}
		AWorldListItemHelper.Load();
		BackgroundHelper.Load();
		Asset<Effect> shader =
			ModContent.Request<Effect>("Avalon/Effects/DarkMatterSkyShader", AssetRequestMode.ImmediateLoad);
		SkyManager.Instance["Avalon:DarkMatter"] = new DarkMatterSky();
		Filters.Scene["Avalon:DarkMatter"] = new Filter(
			new DarkMatterScreenShader(new Ref<Effect>(shader.Value), "DarkMatterSky")
				.UseColor(0.18f, 0.08f, 0.24f), EffectPriority.VeryHigh);

		Asset<Effect> shaderForCalcSpec = ModContent.Request<Effect>("Avalon/Effects/PixelChange", AssetRequestMode.ImmediateLoad);
		GameShaders.Misc["Avalon:CalcSpec"] = new CalculatorSpectaclesShader(new Ref<Effect>(shaderForCalcSpec.Value), "PixelChange");
		CalculatorSpectaclesEffect = shaderForCalcSpec.Value;
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

		AWorldListItemHelper.Unload();
		BackgroundHelper.Unload();

		Mod = null!;
	}
	public override void PostSetupContent()
	{
		if (Main.netMode == NetmodeID.Server) return;
		if (!Main.dedServ)
		{
			var title = Lang.GetRandomGameTitle();
			Platform.Get<IWindowService>().SetUnicodeTitle(Main.instance.Window, title);
		}
	}

	public override object Call(params object[] args)
	{
		//For Content creators: Message me (Lion8cake) on discord if you have any mod call suggestions
		return args switch
		{
			["Contagion"] => ModContent.GetInstance<AvalonWorld>().WorldEvil == WorldEvil.Contagion,
			["SetWorldEvil", int value] => ModContent.GetInstance<AvalonWorld>().WorldEvil = (WorldEvil)value,

			// biome chest loot for the biome lockbox
			["AddBiomeChest", List<int> value] => Data.Sets.ItemSets.BiomeLockboxCollection.AddToListAndReturnIt(value),

			// Torch launcher stuff
			["AddTorchLauncherLightColor", int key, Vector3 value] => Data.Sets.TorchLauncherSets.LightColor.TorchLauncherAdding(key, value),
			["AddTorchLauncherDust", int key, int value] => Data.Sets.TorchLauncherSets.Dust.TorchLauncherAdding(key, value),
			["AddTorchLauncherTexture", int key, string value] => Data.Sets.TorchLauncherSets.Texture.TorchLauncherAdding(key, value),
			["AddTorchLauncherFlameTexture", int key, string value] => Data.Sets.TorchLauncherSets.FlameTexture.TorchLauncherAdding(key, value),
			["AddTorchLauncherDebuffType", int key, int value] => Data.Sets.TorchLauncherSets.DebuffType.TorchLauncherAdding(key, value),

			//IDs
			["ConvertsToContagion", int tileID, int num] => Data.Sets.TileSets.ConvertsToContagion[tileID] = num,
			["ConvertsToContagionWall", int wallID, int num] => Data.Sets.WallSets.ConvertsToContagionWall[wallID] = num,
			_ => throw new Exception("ExxoAvalonOrigins: Unknown mod call, make sure you are calling the right method/field with the right parameters!")
		};
	}
	private void ReplaceBloodyAmulet()
	{
		var itemReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Item);
		assetReplacers.Add(itemReplacer);
		itemReplacer.ReplaceAsset(ItemID.BloodMoonStarter, Assets.Request<Texture2D>("Assets/Vanilla/Items/BloodyAmulet"));
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

		var tileReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Tile);
		assetReplacers.Add(tileReplacer);
		tileReplacer.ReplaceAsset(TileID.CopperCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/CopperCoin"));
		tileReplacer.ReplaceAsset(TileID.SilverCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/SilverCoin"));
		tileReplacer.ReplaceAsset(TileID.GoldCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/GoldCoin"));
		tileReplacer.ReplaceAsset(TileID.PlatinumCoinPile, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/PlatinumCoin"));
		tileReplacer.ReplaceAsset(TileID.Banners, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/VanillaBanners"));
		tileReplacer.ReplaceAsset(TileID.Containers, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/VanillaChests"));
		tileReplacer.ReplaceAsset(TileID.ImmatureHerbs, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/BuddingHerbs"));
		tileReplacer.ReplaceAsset(TileID.MatureHerbs, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/MatureHerbs"));
		tileReplacer.ReplaceAsset(TileID.BloomingHerbs, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/BloomingHerbs"));
		//tileReplacer.ReplaceAsset(TileID.AncientBlueBrick, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/AncientBlueBrick"));
		//tileReplacer.ReplaceAsset(TileID.AncientGreenBrick, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/AncientGreenBrick"));
		//tileReplacer.ReplaceAsset(TileID.AncientPinkBrick, Assets.Request<Texture2D>("Assets/Vanilla/Tiles/AncientPinkBrick"));

		//var wallReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Wall);
		//assetReplacers.Add(wallReplacer);
		//tileReplacer.ReplaceAsset(WallID.AncientBlueBrickWall, Assets.Request<Texture2D>("Assets/Vanilla/Walls/AncientBlueBrickWall"));
		//tileReplacer.ReplaceAsset(WallID.AncientGreenBrickWall, Assets.Request<Texture2D>("Assets/Vanilla/Walls/AncientGreenBrickWall"));
		//tileReplacer.ReplaceAsset(WallID.AncientPinkBrickWall, Assets.Request<Texture2D>("Assets/Vanilla/Walls/AncientPinkBrickWall"));

		var projectileReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Projectile);
		assetReplacers.Add(projectileReplacer);
		projectileReplacer.ReplaceAsset(ProjectileID.MagicDagger, Assets.Request<Texture2D>("Assets/Vanilla/Items/MagicDagger"));

		//var npcReplacer = new VanillaAssetReplacer<Texture2D>(() => TextureAssets.Npc);
		//assetReplacers.Add(npcReplacer);
	}

	public override void HandlePacket(BinaryReader reader, int whoAmI) => MessageHandler.HandlePacket(reader, whoAmI);
}
