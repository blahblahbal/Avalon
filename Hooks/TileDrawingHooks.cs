using Avalon.Common;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Tiles.Furniture;
using Avalon.Tiles.Ores;
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
			//IL_TileDrawing.DrawTiles_EmitParticles += TintTileSparkle;
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
		private void TintTileSparkle(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<Color>("get_White"), i => i.MatchStloc(55));
			c.EmitLdarg(1); //x
			c.EmitLdarg(2); //y
			c.EmitLdarg(3); //tileCache
			c.EmitLdarg(4); //typeCache
			c.EmitLdloca(55); //ref newColor
			c.EmitDelegate((int i, int j, Tile tileCache, int typeCache, ref Color tileShineColor) =>
			{
				// tileShineColor doesn't show clearly with desaturated colours, so make sure to modify whatever colour you're using so the highest value is 255 or close

				// ores
				if (typeCache == ModContent.TileType<BacciliteOre>())
				{
					tileShineColor = new Color(240, 255, 50, 255);
				}
				if (typeCache == ModContent.TileType<ShroomiteOre>())
				{
					tileShineColor = new Color(0, 200, 255, 255);
				}
				if (typeCache == ModContent.TileType<XanthophyteOre>())
				{
					tileShineColor = new Color(255, 255, 0, 255);
				}

				// gems
				if (typeCache == ModContent.TileType<Tourmaline>() || (typeCache == ModContent.TileType<PlacedGems>() && Main.tile[i, j].TileFrameX / 18 == 3))
				{
					tileShineColor = new Color(0, 255, 255, 255);
				}
				if (typeCache == ModContent.TileType<Peridot>() || (typeCache == ModContent.TileType<PlacedGems>() && Main.tile[i, j].TileFrameX / 18 == 4))
				{
					tileShineColor = new Color(235, 255, 0, 255);
				}
				if (typeCache == ModContent.TileType<Zircon>() || (typeCache == ModContent.TileType<PlacedGems>() && Main.tile[i, j].TileFrameX / 18 == 5))
				{
					tileShineColor = new Color(255, 243, 235, 255);
				}

				// misc
				if (typeCache == ModContent.TileType<PlacedStaminaCrystal>())
				{
					tileShineColor = new Color(0, 255, 0, 255);
				}
			});
		}
	}
}
