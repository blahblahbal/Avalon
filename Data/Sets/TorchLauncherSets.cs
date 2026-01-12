using Avalon.Dusts;
using Avalon.Items.Placeable.Furniture;
using Avalon.Tiles.Furniture.Contagion;
using Avalon.Tiles.Furniture.Gem;
using Avalon.Tiles.Furniture.Pathogen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Avalon.Data.Sets;

internal class TorchLauncherSets
{
	public static Dictionary<int, string> Texture = new Dictionary<int, string>()
	{
		{ ItemID.Torch,                 "Tiles_4" },
		{ ItemID.BlueTorch,             "Tiles_4" },
		{ ItemID.RedTorch,              "Tiles_4" },
		{ ItemID.GreenTorch,            "Tiles_4" },
		{ ItemID.PurpleTorch,           "Tiles_4" },
		{ ItemID.WhiteTorch,            "Tiles_4" },
		{ ItemID.YellowTorch,           "Tiles_4" },
		{ ItemID.DemonTorch,            "Tiles_4" },
		{ ItemID.CursedTorch,           "Tiles_4" },
		{ ItemID.IceTorch,              "Tiles_4" },
		{ ItemID.OrangeTorch,           "Tiles_4" },
		{ ItemID.IchorTorch,            "Tiles_4" },
		{ ItemID.UltrabrightTorch,      "Tiles_4" },
		{ ItemID.BoneTorch,             "Tiles_4" },
		{ ItemID.RainbowTorch,          "Tiles_4" },
		{ ItemID.PinkTorch,             "Tiles_4" },
		{ ItemID.DesertTorch,           "Tiles_4" },
		{ ItemID.CoralTorch,            "Tiles_4" },
		{ ItemID.CorruptTorch,          "Tiles_4" },
		{ ItemID.CrimsonTorch,          "Tiles_4" },
		{ ItemID.HallowedTorch,         "Tiles_4" },
		{ ItemID.JungleTorch,           "Tiles_4" },
		{ ItemID.MushroomTorch,         "Tiles_4" },
		{ ItemID.ShimmerTorch,          "Tiles_4" },
		{ ItemType<Items.Placeable.Furniture.BrownTorch>(),       GetInstance<Tiles.Furniture.Gem.BrownTorch>().Texture },
		{ ItemType<Items.Placeable.Furniture.ContagionTorch>(),   GetInstance<Tiles.Furniture.Contagion.ContagionTorch>().Texture },
		{ ItemType<Items.Placeable.Furniture.CyanTorch>(),        GetInstance<Tiles.Furniture.Gem.CyanTorch>().Texture },
		{ ItemType<Items.Placeable.Furniture.LimeTorch>(),        GetInstance<Tiles.Furniture.Gem.LimeTorch>().Texture },
		{ ItemType<Items.Placeable.Furniture.PathogenTorch>(),    GetInstance<Tiles.Furniture.Pathogen.PathogenTorch>().Texture },
		{ ItemType<SavannaTorch>(),     GetInstance<Tiles.Savanna.SavannaTorch>().Texture },
		{ ItemType<SlimeTorch>(),       GetInstance<Tiles.Furniture.SlimeTorch>().Texture },
		{ ItemType<StarTorch>(),        GetInstance<Tiles.Furniture.StarTorch>().Texture }
	};

	public static Dictionary<int, string> FlameTexture = new Dictionary<int, string>()
	{
		{ ItemID.Torch,                 "Flame_0" },
		{ ItemID.BlueTorch,             "Flame_0" },
		{ ItemID.RedTorch,              "Flame_0" },
		{ ItemID.GreenTorch,            "Flame_0" },
		{ ItemID.PurpleTorch,           "Flame_0" },
		{ ItemID.WhiteTorch,            "Flame_0" },
		{ ItemID.YellowTorch,           "Flame_0" },
		{ ItemID.DemonTorch,            "Flame_0" },
		{ ItemID.CursedTorch,           "Flame_0" },
		{ ItemID.IceTorch,              "Flame_0" },
		{ ItemID.OrangeTorch,           "Flame_0" },
		{ ItemID.IchorTorch,            "Flame_0" },
		{ ItemID.UltrabrightTorch,      "Flame_0" },
		{ ItemID.BoneTorch,             "Flame_0" },
		{ ItemID.RainbowTorch,          "Flame_0" },
		{ ItemID.PinkTorch,             "Flame_0" },
		{ ItemID.DesertTorch,           "Flame_0" },
		{ ItemID.CoralTorch,            "Flame_0" },
		{ ItemID.CorruptTorch,          "Flame_0" },
		{ ItemID.CrimsonTorch,          "Flame_0" },
		{ ItemID.HallowedTorch,         "Flame_0" },
		{ ItemID.JungleTorch,           "Flame_0" },
		{ ItemID.MushroomTorch,         "Flame_0" },
		{ ItemID.ShimmerTorch,          "Flame_0" },
		{ ItemType<Items.Placeable.Furniture.BrownTorch>(),       GetInstance<Tiles.Furniture.Gem.BrownTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.ContagionTorch>(),   GetInstance<Tiles.Furniture.Contagion.ContagionTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.CyanTorch>(),        GetInstance<Tiles.Furniture.Gem.CyanTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.LimeTorch>(),        GetInstance<Tiles.Furniture.Gem.LimeTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.PathogenTorch>(),    GetInstance<Tiles.Furniture.Pathogen.PathogenTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.SavannaTorch>(),     GetInstance<Tiles.Savanna.SavannaTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.SlimeTorch>(),       GetInstance<Tiles.Furniture.SlimeTorch>().Texture + "_Flame" },
		{ ItemType<Items.Placeable.Furniture.StarTorch>(),        GetInstance<Tiles.Furniture.StarTorch>().Texture + "_Flame" }
	};

	public static Dictionary<int, int> DebuffType = new Dictionary<int, int>()
	{
		{ ItemID.Torch,                 -1 },
		{ ItemID.BlueTorch,             -1 },
		{ ItemID.RedTorch,              -1 },
		{ ItemID.GreenTorch,            -1 },
		{ ItemID.PurpleTorch,           -1 },
		{ ItemID.WhiteTorch,            -1 },
		{ ItemID.YellowTorch,           -1 },
		{ ItemID.DemonTorch,            -1 },
		{ ItemID.CursedTorch,           BuffID.CursedInferno },
		{ ItemID.IceTorch,              BuffID.Frostburn },
		{ ItemID.OrangeTorch,           -1 },
		{ ItemID.IchorTorch,            -1 },
		{ ItemID.UltrabrightTorch,      -1 },
		{ ItemID.BoneTorch,             -1 },
		{ ItemID.RainbowTorch,          -1 },
		{ ItemID.PinkTorch,             -1 },
		{ ItemID.DesertTorch,           -1 },
		{ ItemID.CoralTorch,            -1 },
		{ ItemID.CorruptTorch,          -1 },
		{ ItemID.CrimsonTorch,          -1 },
		{ ItemID.HallowedTorch,         -1 },
		{ ItemID.JungleTorch,           -1 },
		{ ItemID.MushroomTorch,         -1 },
		{ ItemID.ShimmerTorch,          -1 },
		{ ItemType<Items.Placeable.Furniture.BrownTorch>(),       -1 },
		{ ItemType<Items.Placeable.Furniture.ContagionTorch>(),   -1 },
		{ ItemType<Items.Placeable.Furniture.CyanTorch>(),        -1 },
		{ ItemType<Items.Placeable.Furniture.LimeTorch>(),        -1 },
		{ ItemType<Items.Placeable.Furniture.PathogenTorch>(),    -1 },
		{ ItemType<SavannaTorch>(),     -1 },
		{ ItemType<SlimeTorch>(),       -1 },
		{ ItemType<StarTorch>(),        -1 }
	};

	/// <summary>
	/// Dictionary that holds torch dust types. Used for the Torch Launcher.
	/// </summary>
	public static Dictionary<int, int> Dust = new Dictionary<int, int>()
	{
		//{ ItemID.None, -1 },
		{ ItemID.Torch, DustID.Torch },
		{ ItemID.BlueTorch, DustID.BlueTorch },
		{ ItemID.RedTorch, DustID.RedTorch },
		{ ItemID.GreenTorch, DustID.GreenTorch },
		{ ItemID.PurpleTorch, DustID.PurpleTorch },
		{ ItemID.WhiteTorch, DustID.WhiteTorch },
		{ ItemID.YellowTorch, DustID.YellowTorch },
		{ ItemID.DemonTorch, DustID.DemonTorch },
		{ ItemID.CursedTorch, DustID.CursedTorch },
		{ ItemID.IceTorch, DustID.IceTorch },
		{ ItemID.OrangeTorch, DustID.OrangeTorch },
		{ ItemID.IchorTorch, DustID.IchorTorch },
		{ ItemID.UltrabrightTorch, DustID.UltraBrightTorch },
		{ ItemID.BoneTorch, DustID.BoneTorch },
		{ ItemID.RainbowTorch, DustID.RainbowMk2 }, // shows up a much more vibrant colour than the actual rainbow torch dust, looks better as the trail
		{ ItemID.PinkTorch, DustID.PinkTorch },
		{ ItemID.DesertTorch, DustID.DesertTorch },
		{ ItemID.CoralTorch, DustID.CoralTorch },
		{ ItemID.CorruptTorch, DustID.CorruptTorch },
		{ ItemID.CrimsonTorch, DustID.CrimsonTorch },
		{ ItemID.HallowedTorch, DustID.HallowedTorch },
		{ ItemID.JungleTorch, DustID.JungleTorch },
		{ ItemID.MushroomTorch, DustID.MushroomTorch },
		{ ItemID.ShimmerTorch, DustID.ShimmerTorch },
		{ ItemType<Items.Placeable.Furniture.BrownTorch>(), DustType<BrownTorchDust>() },
		{ ItemType<Items.Placeable.Furniture.ContagionTorch>(), DustID.JungleTorch },
		{ ItemType<Items.Placeable.Furniture.CyanTorch>(), DustType<CyanTorchDust>() },
		{ ItemType<Items.Placeable.Furniture.LimeTorch>(), DustType<LimeTorchDust>() },
		{ ItemType<Items.Placeable.Furniture.PathogenTorch>(), DustType<PathogenDust>() },
		{ ItemType<SavannaTorch>(), DustID.JungleTorch },
		{ ItemType<SlimeTorch>(), DustID.t_Slime },
		{ ItemType<StarTorch>(), -2 }
	};

	/// <summary>
	/// Dictionary that holds torch light colors. Used for the Torch Launcher.
	/// </summary>
	public static Dictionary<int, Vector3> LightColor = new Dictionary<int, Vector3>()
	{
		//{ ItemID.None, Vector3.Zero },
		{ ItemID.Torch, new Vector3(1f, 0.95f, 0.8f) },
		{ ItemID.BlueTorch, new Vector3(0f, 0.1f, 1.3f) },
		{ ItemID.RedTorch, new Vector3(1f, 0.1f, 0.1f) },
		{ ItemID.GreenTorch, new Vector3(0f, 1f, 0.1f) },
		{ ItemID.PurpleTorch, new Vector3(0.9f, 0f, 0.9f) },
		{ ItemID.WhiteTorch, new Vector3(1.3f, 1.3f, 1.3f) },
		{ ItemID.YellowTorch, new Vector3(0.9f, 0.9f, 0f) },
		{ ItemID.DemonTorch, new Vector3(0f, 0f, 0f) }, //
		{ ItemID.CursedTorch, new Vector3(0.7f, 0.85f, 1f) },
		{ ItemID.IceTorch, new Vector3(0.75f, 0.85f, 1.4f) },
		{ ItemID.OrangeTorch, new Vector3(1f, 0.5f, 0f) },
		{ ItemID.IchorTorch, new Vector3(1f, 0.5f, 0f) },
		{ ItemID.UltrabrightTorch, new Vector3(0.75f, 1.3499999f, 1.5f) },
		{ ItemID.BoneTorch, new Vector3(0.95f, 0.75f, 1.3f) },
		{ ItemID.RainbowTorch, new Vector3(0f, 0f, 0f) }, //
		{ ItemID.PinkTorch, new Vector3(1f, 0f, 1f) },
		{ ItemID.DesertTorch, new Vector3(1.4f, 0.85f, 0.55f) },
		{ ItemID.CoralTorch, new Vector3(0.25f, 1.3f, 0.8f) },
		{ ItemID.CorruptTorch, new Vector3(0.95f, 0.4f, 1.4f) },
		{ ItemID.CrimsonTorch, new Vector3(1.4f, 0.7f, 0.5f) },
		{ ItemID.HallowedTorch, new Vector3(1.25f, 0.6f, 1.2f) },
		{ ItemID.JungleTorch, new Vector3(0.75f, 1.45f, 0.9f) },
		{ ItemID.MushroomTorch, new Vector3(0.3f, 0.78f, 1.2f) },
		{ ItemID.ShimmerTorch, new Vector3(0f, 0f, 0f) }, //
		{ ItemType<Items.Placeable.Furniture.BrownTorch>(), new Vector3(1.1f, 0.75f, 0.5f) },
		{ ItemType<Items.Placeable.Furniture.ContagionTorch>(), new Vector3(0.8f, 1.4f, 0f) },
		{ ItemType<Items.Placeable.Furniture.CyanTorch>(), new Vector3(0f, 1f, 1f) },
		{ ItemType<Items.Placeable.Furniture.LimeTorch>(), new Vector3(0.714f, 1f, 0f) },
		{ ItemType<Items.Placeable.Furniture.PathogenTorch>(), new Vector3(0.5f, 0f, 2f) },
		{ ItemType<SavannaTorch>(), new Vector3(0.69f, 1f, 0.42f) },
		{ ItemType<SlimeTorch>(), new Vector3(0.25f, 0.72f, 1f) },
		{ ItemType<StarTorch>(), new Vector3(1f, 0.945f, 0.2f) }
	};
}
