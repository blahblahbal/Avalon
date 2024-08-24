using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.PlayerDrawLayers
{
	public class DrawLayerHelper : ModSystem
	{
		public delegate void DrawSittingLegsDelegate(ref PlayerDrawSet drawinfo, Texture2D textureToDraw, Color matchingColor, int shaderIndex = 0, bool glowmask = false);

		public static DrawSittingLegsDelegate DrawSittingLegsMethod { private set; get; } //Method is too big to bother copying, so use reflection instead. Monitor tml developments

		public override void Load()
		{
			var playerDrawLayersType = typeof(Terraria.DataStructures.PlayerDrawLayers);
			var drawSittingLegsMethodInfo = playerDrawLayersType.GetMethod("DrawSittingLegs", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
			DrawSittingLegsMethod = (DrawSittingLegsDelegate)Delegate.CreateDelegate(typeof(DrawSittingLegsDelegate), drawSittingLegsMethodInfo);
		}

		public override void Unload()
		{
			DrawSittingLegsMethod = null;
		}

		//Copied from vanilla
		public static bool ShouldOverrideLegs_CheckShoes(ref PlayerDrawSet drawInfo)
		{
			return drawInfo.drawPlayer.shoe > 0 && ArmorIDs.Shoe.Sets.OverridesLegs[drawInfo.drawPlayer.shoe];
		}
	}
}
