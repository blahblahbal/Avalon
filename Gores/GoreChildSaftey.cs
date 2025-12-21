using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Gores;
public class GoreChildSaftey : ModSystem
{
	public override void SetStaticDefaults()
	{
		if (Main.netMode == NetmodeID.Server) return;

		MakeChildSafe<Bubble>();
		MakeChildSafe<LargeBubble>();
		MakeChildSafe<SmallBubble>();

		MakeChildSafe<ContagionPotGore1>();
		MakeChildSafe<ContagionPotGore2>();
		MakeChildSafe<ContagionPotGore3>();
		MakeChildSafe<TropicsPotGore1>();
		MakeChildSafe<TropicsPotGore2>();
		MakeChildSafe<TuhrtlPotGore1>();
		MakeChildSafe<TuhrtlPotGore2>();

		MakeChildSafe<GlacierChunk1>();
		MakeChildSafe<GlacierChunk2>();
		MakeChildSafe<GlacierChunk3>();
		MakeChildSafe<GlacierShard1>();
		MakeChildSafe<GlacierShard2>();
		MakeChildSafe<GlacierShard3>();

		MakeChildSafe("BoneFishHead");
		MakeChildSafe("BoneFishTail");

		MakeChildSafe("GargoyleGore3");
		MakeChildSafe("GargoyleGore4");
		MakeChildSafe("GargoyleGore5");
		MakeChildSafe("GargoyleHead");
		MakeChildSafe("GargoyleWing");

		MakeChildSafe("IrateBonesHelmet");

		MakeChildSafe("SolarSystem1");
		MakeChildSafe("SolarSystem2");
		MakeChildSafe("SolarSystem3");
		MakeChildSafe("SolarSystem4");
		MakeChildSafe("SolarSystem5");
		MakeChildSafe("SolarSystem6");

		MakeChildSafe("VultureShell1");
		MakeChildSafe("VultureShell2");
		MakeChildSafe("VultureShell3");
		MakeChildSafe("VultureShell4");

		MakeChildSafe("WallofSteelGore1");
		MakeChildSafe("WallofSteelGore2");
		MakeChildSafe("WallofSteelGore3");
		MakeChildSafe("WallofSteelGore4"); // It has blood on the sprite but... feels weird to exclude only THIS one... oh well!
		MakeChildSafe("WallofSteelGore5");
		MakeChildSafe("WallofSteelGore6");
		MakeChildSafe("WallofSteelGore7");
		MakeChildSafe("WallofSteelGore8");
		MakeChildSafe("WallofSteelGore9");
		MakeChildSafe("WallofSteelGore10");
		MakeChildSafe("WallofSteelGore11");
		MakeChildSafe("WallofSteelGore12");
		MakeChildSafe("WallofSteelGore13");
		MakeChildSafe("WallofSteelGore14");
	}

	private static void MakeChildSafe<T>() where T : ModGore
	{
		ChildSafety.SafeGore[ModContent.GoreType<T>()] = true;
	}
	private void MakeChildSafe(string type)
	{
		ChildSafety.SafeGore[Mod.Find<ModGore>(type).Type] = true;
	}
}
