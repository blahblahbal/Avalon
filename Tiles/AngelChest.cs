using System;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ObjectData;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Avalon.Buffs.AdvancedBuffs;
using ThoriumMod.Projectiles;
using Terraria.GameContent;
using Avalon.Systems;

namespace Avalon.Tiles
{
	public class AngelChest : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;

			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.StyleWrapLimit = 52;
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(233, 207, 94), CreateMapEntryName());
			AddMapEntry(new Color(144, 148, 144), Language.GetText("MapObject.Statue"));
			AddMapEntry(Color.Transparent);
			DustType = DustID.Stone;
			TileID.Sets.DisableSmartCursor[Type] = true;
		}

		public override ushort GetMapOption(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			int x = i;
			int y = j;
			GetCentralTile(ref x, ref y);
			if (Math.Abs(x - Main.LocalPlayer.Center.X / 16) + Math.Abs(y - Main.LocalPlayer.Center.Y / 16) < 10)
			{
				return 1;
			}
			if (tile.TileFrameY / 18 > 0)
			{
				return 0;
			}
			return 2; //change to 2?
		}

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int x = i;
			int y = j;
			GetCentralTile(ref x, ref y);
			float visability = Math.Clamp(Math.Abs(x - Main.LocalPlayer.Center.X / 16) + Math.Abs(y - Main.LocalPlayer.Center.Y / 16), 6, 12);
			visability -= 6;
			visability /= 6;
			Texture2D texture = TextureAssets.Tile[TileID.Statues].Value;
			Texture2D texture2 = TextureAssets.Tile[TileID.Containers].Value;
			Vector2 pos = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + zero;
			Color tileLight = Lighting.GetColor(i, j);
			tileLight = TileGlowDrawing.ActuatedColor(tileLight, tile);
			Color color = tileLight;
			tileLight *= 1 - visability;
			color *= visability;
			Main.spriteBatch.Draw(texture2, pos, (Rectangle?)new Rectangle(36 + tile.TileFrameX, -18 + tile.TileFrameY, 18, 18), color, 0f, Vector2.Zero, 1f, 0, 0f);
			Main.spriteBatch.Draw(texture, pos, (Rectangle?)new Rectangle(36 + tile.TileFrameX, 0 + tile.TileFrameY, 18, 18), tileLight, 0f, Vector2.Zero, 1f, 0, 0f);
			return false;
		}

		internal static void GetCentralTile(ref int x, ref int y)
		{
			Tile tile = Main.tile[x, y];
			if (tile.TileFrameY / 18 == 0)
			{
				y++;
			}
			if (tile.TileFrameY / 18 == 2)
			{
				y--;
			}
			if (tile.TileFrameX / 18 == 0)
			{
				x++;
			}
		}
	}
}
