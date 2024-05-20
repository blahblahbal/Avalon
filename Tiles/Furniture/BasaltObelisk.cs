using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using System;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.GameContent.ObjectInteractions;
using Avalon.Waters;

namespace Avalon.Tiles.Furniture
{
    public class BasaltObelisk : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true; // Any multitile requires this
			Main.tileLighted[Type] = true;
			TileID.Sets.InteractibleByNPCs[Type] = true; // Town NPCs will palm their hand at this tile

            DustType = DustID.Wraith;

            TileObjectData.newTile.CopyFrom(TileObjectData.GetTileData(TileID.VoidMonolith, 0));
            TileObjectData.newTile.LavaDeath = false; // Does not break when lava touches it
            TileObjectData.newTile.DrawYOffset = 2; // So the tile sinks into the ground

            //TileObjectData.newTile.StyleLineSkip = 7; // This needs to be added to work for modded tiles.

            // Register the tile data itself
            TileObjectData.addTile(Type);

            // Register map name and color
            AddMapEntry(new Color(59, 62, 66), Language.GetText("MapObject.BasaltObelisk"));
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			//r = 210f / 255f;
			//g = 67f / 255f;
			//b = 20f / 255f;
			r = 1f;
			g = 110f / 255f;
			b = 55f / 255f;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			var tile = Main.tile[i, j];
			Color color = new Color(255, 255, 255, 0);

			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}

			int width = 16;
			int offsetY = 0;
			int height = 16;
			short frameX = tile.TileFrameX;
			short frameY = tile.TileFrameY;

			TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY); // calculates the draw offsets

			Rectangle drawRectangle = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);

			// The flame is manually drawn separate from the tile texture so that it can be drawn at full brightness.
			Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, drawRectangle, color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
		}
	}
}
