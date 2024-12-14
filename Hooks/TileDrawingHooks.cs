using Avalon.Common;
using Avalon.Systems;
using Avalon.Tiles;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	public class TileDrawingHooks : ModHook
	{
		protected override void Apply()
		{
			IL_TileDrawing.DrawSingleTile += BetterDrawEffects;
		}
		private void BetterDrawEffects(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchLdfld<TileDrawInfo>("colorTint"), i => i.MatchCall<TileDrawing>("GetFinalLight"), i => i.MatchStfld<TileDrawInfo>("finalColor"));
			c.EmitLdarg(6); //tileX
			c.EmitLdarg(7); //tileY
			c.EmitLdarg(1); //instanced TileDrawInfo type is gotten from
			c.EmitLdfld(typeof(TileDrawInfo).GetField("typeCache")); //type
			c.EmitLdarga(1); //drawData ref
			c.EmitDelegate((int i, int j, int type, ref TileDrawInfo drawdata) =>
			{
				//Use Main.spriteBatch for rendering
				if (type == ModContent.TileType<CoolGemsparkBlock>())
				{
					Color color = TileGlowDrawing.ActuatedColor(new Color(CoolGemsparkBlock.R, CoolGemsparkBlock.G, CoolGemsparkBlock.B, 255), Main.tile[i, j]);
					drawdata.finalColor = color;
				}
				if (type == ModContent.TileType<WarmGemsparkBlock>())
				{
					Color color = TileGlowDrawing.ActuatedColor(new Color(255, WarmGemsparkBlock.G, 0, 255), Main.tile[i, j]);
					drawdata.finalColor = color;
				}
			});
		}
	}
}
