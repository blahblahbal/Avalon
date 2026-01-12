using Avalon.Common;
using Avalon.Tiles;
using Avalon.Tiles.Contagion;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class MapTileEdits : ModHook
    {
        protected override void Apply()
        {
            //On_MapHelper.GetMapTileXnaColor += OnGetMapTileXnaColor;
            On_Main.DoUpdate_AnimateDiscoRGB += OnDoUpdate_AnimateDiscoRGB;
			IL_MapHelper.CreateMapTile += CactusMapColor;
			IL_MapHelper.CreateMapTile += preventTileRenderingOnMap;
		}

		private void preventTileRenderingOnMap(ILContext il)
		{
			ILCursor c = new ILCursor(il);
			c.GotoNext(MoveType.After, i => i.MatchLdloca(0), i => i.MatchCall<Tile>("invisibleBlock"));
			c.EmitLdloc(0);
			c.EmitLdarg(0);
			c.EmitLdarg(1);
			c.EmitDelegate((bool isTileInvis, Tile tile, int i, int j) =>
			{
				int x = i;
				int y = j;
				AngelChest.GetCentralTile(ref x, ref y);
				if (Math.Abs(x - Main.LocalPlayer.Center.X / 16) + Math.Abs(y - Main.LocalPlayer.Center.Y / 16) >= 10)
				{
					if (tile.TileType == ModContent.TileType<AngelChest>() && tile.TileFrameY / 18 <= 0)
					{
						return true;
					}
				}
				return isTileInvis;
			});
		}

		private static Color OnGetMapTileXnaColor(On_MapHelper.orig_GetMapTileXnaColor orig, ref MapTile tile)
        {
            Color[] ColorLookup = (Color[])typeof(MapHelper).GetField("colorLookup", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
            Color oldColor = ColorLookup[tile.Type];

            if (oldColor == new Color(0, 1, 2, 127) || oldColor == ExxoAvalonOrigins.LastDiscoRGB)
            {
                tile.Light = 0;
                return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            }

            return orig(ref tile);
        }
        private static void OnDoUpdate_AnimateDiscoRGB(On_Main.orig_DoUpdate_AnimateDiscoRGB orig, Main self)
        {
            ExxoAvalonOrigins.LastDiscoRGB = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
            orig(self);
        }

		private void CactusMapColor(ILContext il)
		{
			ILCursor c = new(il);
			c.GotoNext(
				MoveType.After,
				i => i.MatchLdsfld("Terraria.Map.MapHelper", "tileLookup"),
				i => i.MatchLdloc(7),
				i => i.MatchLdelemU2(),
				i => i.MatchStloc3());
			c.EmitLdarg0(); //i (aka X)
			c.EmitLdarg1(); //j (aka Y)
			c.EmitLdloca(3); //num5
			c.EmitDelegate((int i, int j, ref int num5) => {
				Tile tile = Main.tile[i, j];
				if (tile != null)
				{ //somehow still out of bounds
					GetCactusType(i, j, tile.TileFrameX, tile.TileFrameY, out var sandType);
					if (Main.tile[i, j].TileType == TileID.Cactus && TileLoader.CanGrowModCactus(sandType) && sandType == ModContent.TileType<Snotsand>())
					{
						num5 = MapHelper.tileLookup[ModContent.TileType<IckyCactusDummyTile>()];
					}
				}
			});
		}

		/// Copied from vanilla's WorldGen.GetCactusType due to a critical issue where this is no prevention for checking out of bounds for cacti - Lion8cake
		public static void GetCactusType(int tileX, int tileY, int frameX, int frameY, out int type)
		{
			type = 0;
			int num = tileX;
			if (frameX == 36)
				num--;

			if (frameX == 54)
				num++;

			if (frameX == 108)
				num = ((frameY != 18) ? (num + 1) : (num - 1));

			int num2 = tileY;
			bool flag = false;
			Tile tile = Framing.GetTileSafely(num, num2);
			if (tile == null)
				return;

			if (tile.TileType == 80 && tile.HasTile)
				flag = true;

			while (tile != null && (!tile.HasTile || !Main.tileSolid[tile.TileType] || !flag))
			{
				if (tile.TileType == 80 && tile.HasTile)
					flag = true;

				num2++;
				if (num2 > tileY + 20)
					break;

				if (num < Main.maxTilesX && num2 < Main.maxTilesY)
					tile = Framing.GetTileSafely(num, num2);
			}

			type = tile.TileType;
		}
	}
}
