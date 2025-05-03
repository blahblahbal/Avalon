using Avalon.Common;
using Avalon.ModSupport.Thorium.Tiles;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	internal class WindHook : ModHook //Should rename since its nolonger just hooks
	{
		protected override void Apply()
		{
			On_TileDrawing.DrawMultiTileVinesInWind += On_TileDrawing_DrawMultiTileVinesInWind;
			IL_TileDrawing.DrawMultiTileGrassInWind += IL_TileDrawing_DrawMultiTileGrassInWind;
		}

		private void IL_TileDrawing_DrawMultiTileGrassInWind(ILContext il)
		{
			ILCursor c = new(il);
			c.EmitLdarg0(); //self
			c.EmitLdarg3(); //topLeftX
			c.EmitLdarg(4); //topLeftY
			c.EmitLdarga(1); //screenPosition
			c.EmitLdarga(2); //offset
			c.EmitLdarga(5); //sizeX
			c.EmitLdarga(6); //size Y
			c.EmitDelegate((TileDrawing self, int topLeftX, int topLeftY, ref Vector2 screenPosition, ref Vector2 offSet, ref int sizeX, ref int sizeY) =>
			{
				Texture2D? dud = null;
				Color dud2 = Color.White;
				DrawMultiTileGrassVariableEditor(self, ref screenPosition, ref offSet, topLeftX, topLeftY, ref sizeX, ref sizeY, ref dud, ref dud2); //Adds the sizing of the tile
			});

			c.GotoNext(
				MoveType.After,
				i => i.MatchLdarg0(),
				i => i.MatchLdloc(10),
				i => i.MatchLdloc(8),
				i => i.MatchLdloc(9),
				i => i.MatchCall<TileDrawing>("GetTileDrawTexture"),
				i => i.MatchStloc(27)
				); //Goes to before the Main.Spritebatch Drawing and injects, when I mean right before, I mean the next instruction is calling Main.spritebatch, ya know
			c.EmitLdarg0(); //self
			c.EmitLdarg3(); //topLeftX
			c.EmitLdarg(4); //topLeftY
			c.EmitLdloca(4); //val2
			c.EmitLdloca(5); //color
			c.EmitDelegate((TileDrawing self, int topLeftX, int topLeftY, ref Texture2D val2, ref Color color) => //Should note that the val2 is the extra texture thats only used by sunflower's """"flames""""
			{
				int dud = 0;
				Vector2 dud2 = Vector2.Zero;
				DrawMultiTileGrassVariableEditor(self, ref dud2, ref dud2, topLeftX, topLeftY, ref dud, ref dud, ref val2, ref color); //Adds extra content (glowmasks, color, ect)
			});
		}

		public static void DrawMultiTileGrassVariableEditor(TileDrawing self, ref Vector2 screenPosition, ref Vector2 offSet, int topLeftX, int topLeftY, ref int sizeX, ref int sizeY, ref Texture2D overlayTexture, ref Color overlayColor)
		{
			if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage1>() ||
				Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage2>() ||
				Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage3>() ||
				Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.Herbs.LargeHerbsStage4>())
			{
				sizeY = 3;
			}
			if (ExxoAvalonOrigins.ThoriumContentEnabled)
			{
				if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<LargeMarineKelpStage1>() ||
					Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<LargeMarineKelpStage2>() ||
					Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<LargeMarineKelpStage3>() ||
					Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<LargeMarineKelpStage4>())
				{
					sizeY = 3;
					overlayColor = Color.White;
					overlayTexture = ModContent.Request<Texture2D>(TileLoader.GetTile(ModContent.TileType<LargeMarineKelpStage4>()).Texture + "_Glow").Value;
				}
			}
		}

		private void On_TileDrawing_DrawMultiTileVinesInWind(On_TileDrawing.orig_DrawMultiTileVinesInWind orig, TileDrawing self, Vector2 screenPosition, Vector2 offSet, int topLeftX, int topLeftY, int sizeX, int sizeY)
		{
			if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<Tiles.MonsterBanner>())
			{
				sizeY = 3;
			}
			else if (Main.tile[topLeftX, topLeftY].TileType == ModContent.TileType<HangingPots>())
			{
				sizeX = 2;
				sizeY = 3;
			}
			orig.Invoke(self, screenPosition, offSet, topLeftX, topLeftY, sizeX, sizeY);
		}
	}
}
