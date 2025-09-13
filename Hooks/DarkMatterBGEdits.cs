using Avalon.Backgrounds;
using Avalon.Common;
using Avalon.Effects;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using ThoriumMod.NPCs;
using static Terraria.Main;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class DarkMatterBGEdits : ModHook
{
	protected override void Apply()
	{
		On_Main.DrawSunAndMoon += OnDrawSunAndMoon;
		IL_Main.DrawSurfaceBG += DisbaleHallowRainbow;
		//On_Main.SetBackColor += OnSetBackColor;
	}

	private void DisbaleHallowRainbow(ILContext il)
	{
		ILCursor c = new ILCursor(il);
		int rainbowColor_varNum = -1;

		c.GotoNext(MoveType.After, i => i.MatchLdloca(out rainbowColor_varNum), i => i.MatchCall<Color>("get_A"), i => i.MatchConvR4(), i => i.MatchLdloc(out _), i => i.MatchMul(), i => i.MatchLdcR4(0.8f), i => i.MatchMul(), i => i.MatchConvU1(), i => i.MatchCall<Color>("set_A"));
		c.EmitLdloca(rainbowColor_varNum);
		c.EmitDelegate((ref Color color4) =>
		{
			color4 *= 1f - Main.bgAlphaFarBackLayer[ModContent.GetInstance<DarkMatterSurfaceBackgroundStyle>().Slot];
		});
	}

	private static void OnDrawSunAndMoon(On_Main.orig_DrawSunAndMoon orig, Main self, SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
	{
		if (!gameMenu && ModContent.GetInstance<BiomeTileCounts>().DarkMatterMonolithNearby)
		{
			return;
		}
		orig(self, sceneArea, moonColor, sunColor, tempMushroomInfluence);
	}

	// this doesn't really seem to do anything, and it was causing problems, so disabled it
	//private static void OnSetBackColor(On_Main.orig_SetBackColor orig, InfoToSetBackColor info, out Color sunColor, out Color moonColor)
	//{
	//	orig(info, out sunColor, out moonColor);
	//	if (!gameMenu)
	//	{
	//		if (DarkMatterWorld.InArea && dayTime)
	//		{
	//			Color bgColorToSet = new(5, 5, 5);
	//			sunColor = bgColorToSet;
	//			moonColor = bgColorToSet;
	//			ColorOfTheSkies = bgColorToSet;
	//		}
	//	}
	//}
}
