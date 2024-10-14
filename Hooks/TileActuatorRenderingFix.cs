using Avalon.Common;
using Avalon.Systems;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.Graphics;

namespace Avalon.Hooks
{
	public class TileActuatorRenderingFix : ModHook
	{
		protected override void Apply()
		{
			IL_WallDrawing.DrawWalls += InactiveWallColoring;

			IL_TileDrawing.DrawMultiTileVinesInWind += ActuatorVineFix;
			IL_TileDrawing.DrawMultiTileGrassInWind += ActuatorGrassFix;
			IL_TileDrawing.DrawGrass += ActuatorBasicGrassFix;
			IL_TileDrawing.DrawTrees += ActuatedTreesFix;
		}

		private void ActuatedTreesFix(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<Lighting>("GetColor"), i => i.MatchStloc(27));
			c.EmitLdloca(27);
			c.EmitLdloc(9);
			c.EmitDelegate((ref Color color6, Tile tile) =>
			{
				color6 = TileGlowDrawing.ActuatedColor(color6, tile);
			});
			c.GotoNext(MoveType.After, i => i.MatchCall<Lighting>("GetColor"), i => i.MatchStloc(43));
			c.EmitLdloca(43);
			c.EmitLdloc(9);
			c.EmitDelegate((ref Color color4, Tile tile) =>
			{
				color4 = TileGlowDrawing.ActuatedColor(color4, tile);
			});
			c.GotoNext(MoveType.After, i => i.MatchCall<Lighting>("GetColor"), i => i.MatchStloc(61));
			c.EmitLdloca(61);
			c.EmitLdloc(9);
			c.EmitDelegate((ref Color color2, Tile tile) =>
			{
				color2 = TileGlowDrawing.ActuatedColor(color2, tile);
			});
			c.GotoNext(MoveType.After, i => i.MatchCall<Lighting>("GetColor"), i => i.MatchStloc(82));
			c.EmitLdloca(82);
			c.EmitLdloc(9);
			c.EmitDelegate((ref Color color7, Tile tile) =>
			{
				color7 = TileGlowDrawing.ActuatedColor(color7, tile);
			});
		}

		private void ActuatorBasicGrassFix(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<TileDrawing>("DrawTiles_GetLightOverride"), i => i.MatchStloc(22));
			c.EmitLdloca(22);//tileLight
			c.EmitLdloc(7);
			c.EmitDelegate((ref Color tileLight, Tile tile) =>
			{
				tileLight = TileGlowDrawing.ActuatedColor(tileLight, tile);
			});
		}

		private void ActuatorGrassFix(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<TileDrawing>("DrawTiles_GetLightOverride"), i => i.MatchStloc(23));
			c.EmitLdloca(23);//tileLight
			c.EmitLdloc(10);
			c.EmitDelegate((ref Color tileLight, Tile tile) =>
			{
				tileLight = TileGlowDrawing.ActuatedColor(tileLight, tile);
			});
		}

		private void ActuatorVineFix(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchCall<TileDrawing>("DrawTiles_GetLightOverride"), i => i.MatchStloc(33));
			c.EmitLdloca(33);//tileLight
			c.EmitLdloc(20);
			c.EmitDelegate((ref Color tileLight, Tile tile2) =>
			{
				tileLight = TileGlowDrawing.ActuatedColor(tileLight, tile2);
			});
		}

		private void InactiveWallColoring(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(MoveType.After, i => i.MatchLdloc(26), i => i.MatchCall<Lighting>("GetColor"), i => i.MatchStloc(29));
			c.EmitLdloca(29); //color
			c.EmitLdloc(27); //tile
			c.EmitDelegate((ref Color color, Tile tile) =>
			{
				color = TileGlowDrawing.ActuatedRetroWallColor(color, tile);
			});
			c.GotoNext(MoveType.After, i => i.MatchCall<Lighting>("GetCornerColors"));
			c.EmitLdloca(21);
			c.EmitLdloc(27);
			c.EmitDelegate((ref VertexColors vertices, Tile tile) =>
			{
				vertices = TileGlowDrawing.ActuatedWallColor(vertices, tile);
			});
		}
	}
}
