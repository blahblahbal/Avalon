using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.Main;

namespace Avalon.Hooks;

[Autoload(Side = ModSide.Client)]
public class DarkMatterRemoveSun : ModHook
{
	protected override void Apply()
	{
		On_Main.DrawSunAndMoon += OnDrawSunAndMoon;
		//On_Main.SetBackColor += OnSetBackColor;
	}

	private static void OnDrawSunAndMoon(On_Main.orig_DrawSunAndMoon orig, Main self, SceneArea sceneArea, Color moonColor, Color sunColor, float tempMushroomInfluence)
	{
		if (!gameMenu && DarkMatterWorld.InArea)
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
