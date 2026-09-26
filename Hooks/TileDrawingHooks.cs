using Avalon.Common;
using Avalon.Systems;
using Avalon.Tiles;
using Avalon.Tiles.Furniture;
using Avalon.Tiles.Ores;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	public class TileDrawingHooks : ModHook
	{
		private static int ShroomiteOre;
		private static int XanthophyteOre;
		private static int CoolGemspark;
		private static int PeridotGemspark;
		private static int TourmalineGemspark;
		private static int WarmGemspark;
		private static int ZirconGemspark;
		private static int PlacedGems;
		public override void SetStaticDefaults()
		{
			ShroomiteOre = ModContent.TileType<ShroomiteOre>();
			XanthophyteOre = ModContent.TileType<XanthophyteOre>();
			CoolGemspark = ModContent.TileType<CoolGemsparkBlock>();
			PeridotGemspark = ModContent.TileType<PeridotGemspark>();
			TourmalineGemspark = ModContent.TileType<TourmalineGemspark>();
			WarmGemspark = ModContent.TileType<WarmGemsparkBlock>();
			ZirconGemspark = ModContent.TileType<ZirconGemspark>();
			PlacedGems = ModContent.TileType<PlacedGems>();
		}
		protected override void Apply()
		{
			On_Main.shine_Color_int += On_Main_shine_Color_int;
			On_Main.shine_refVector3_int += On_Main_shine_refVector3_int;

			IL_TileDrawing.DrawSingleTile += BetterDrawEffects;
			IL_TileDrawing.DrawTiles_EmitParticles += TintTileSparkle;
		}
		private Color On_Main_shine_Color_int(On_Main.orig_shine_Color_int orig, Color newColor, int type)
		{
			newColor = new Color(GetShineColor(newColor.ToVector3(), type));
			return orig.Invoke(newColor, type);
		}

		private void On_Main_shine_refVector3_int(On_Main.orig_shine_refVector3_int orig, ref Vector3 newColor, int type)
		{
			newColor = GetShineColor(newColor, type);
			orig.Invoke(ref newColor, type);
		}

		/// <summary>
		/// Add tiles here to modify the color of light that shines onto them (not the light they emit)
		/// </summary>
		/// <param name="color"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		private static Vector3 GetShineColor(Vector3 color, int type)
		{
			Vector3 tempColor = color;

			if (type == ShroomiteOre)
			{
				float num = 0.3f + Utils.Remap(Main.mouseTextColor, 190, 255, 185, 260) / 300f;
				color.Y *= 1.5f * num;
				color.Z *= 1.1f * num;
			}
			else if (type == XanthophyteOre)
			{
				float num = 0.3f + Main.mouseTextColor / 300f;
				color.X *= 1.1f * num;
				color.Y *= 1.5f * num;
			}
			else if (
				type == CoolGemspark ||
				type == PeridotGemspark ||
				type == TourmalineGemspark ||
				type == WarmGemspark ||
				type == ZirconGemspark
				)
			{
				color.X += 0.39f;
				color.Y += 0.39f;
				color.Z += 0.39f;
			}
			else if (type == PlacedGems)
			{
				color *= 1.5f;
			}
			if (Main.tileShine2[type] && color != tempColor) // vanilla multiplies tileShine2 tiles by 1.6 by default UNLESS they receive a custom colour, so negate that by dividing (except we actually multiply by 1/1.6 which is the same thing and avoid division like a boss (the vector3/float division operator also does this))
			{
				color *= 0.625f;
			}
			return color;
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
					Color color = Main.tile[i, j].actColor(new Color(CoolGemsparkBlock.R, CoolGemsparkBlock.G, CoolGemsparkBlock.B, 255));
					drawdata.finalColor = color;
				}
				if (type == ModContent.TileType<WarmGemsparkBlock>())
				{
					Color color = Main.tile[i, j].actColor(new Color(255, WarmGemsparkBlock.G, 0, 255));
					drawdata.finalColor = color;
				}
			});
		}

		private void TintTileSparkle(ILContext il)
		{
			ILCursor c = new(il);
			int newColorvar = -1;

			c.GotoNext(MoveType.After, i => i.MatchRet(), i => i.MatchCall<Color>("get_White"), i => i.MatchStloc(out newColorvar));
			c.EmitLdarg(2); //x
			c.EmitLdarg(1); //y
			c.EmitLdarg(3); //tileCache
			c.EmitLdarg(4); //typeCache
			c.EmitLdloca(newColorvar); //ref newColor
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
				if (typeCache == ModContent.TileType<Tourmaline>() || (typeCache == ModContent.TileType<PlacedGems>() && tileCache.TileFrameX / 18 == 3))
				{
					tileShineColor = new Color(0, 255, 255, 255);
				}
				if (typeCache == ModContent.TileType<Peridot>() || (typeCache == ModContent.TileType<PlacedGems>() && tileCache.TileFrameX / 18 == 4))
				{
					tileShineColor = new Color(235, 255, 0, 255);
				}
				if (typeCache == ModContent.TileType<Zircon>() || (typeCache == ModContent.TileType<PlacedGems>() && tileCache.TileFrameX / 18 == 5))
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
