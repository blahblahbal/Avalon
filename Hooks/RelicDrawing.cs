using Avalon.Common;
using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	internal class RelicDrawing : ModHook
	{
		protected override void Apply()
		{
			On_TileDrawing.DrawMasterTrophies += On_TileDrawing_DrawMasterTrophies;
		}

		private void On_TileDrawing_DrawMasterTrophies(On_TileDrawing.orig_DrawMasterTrophies orig, TileDrawing self)
		{
			orig.Invoke(self);

			for (int i = 0; i < ModContent.GetInstance<Relics>().Coordinates.Count; i++)
			{
				// This is lighting-mode specific, always include this if you draw tiles manually
				//Vector2 offScreen = new Vector2(Main.offScreenRange);
				//if (Main.drawToScreen)
				//{
				//	offScreen = Vector2.Zero;
				//}
				// Take the tile, check if it actually exists
				Point p = new Point(ModContent.GetInstance<Relics>().Coordinates[i].X, ModContent.GetInstance<Relics>().Coordinates[i].Y);
				Tile tile = Main.tile[p.X, p.Y];
				if (tile == null || !tile.HasTile)
				{
					return;
				}

				// Get the initial draw parameters
				Texture2D texture = Relics.RelicTexture.Value;

				int frameY = tile.TileFrameX / Relics.FrameWidth; // Picks the frame on the sheet based on the placeStyle of the item
				Rectangle frame = texture.Frame(Relics.HorizontalFrames, Relics.VerticalFrames, 0, frameY);

				Vector2 origin = frame.Size() / 2f;
				Vector2 worldPos = p.ToWorldCoordinates(24f, 64f);

				Color color = Lighting.GetColor(p.X, p.Y);

				bool direction = tile.TileFrameY / Relics.FrameHeight != 0; // This is related to the alternate tile data we registered before
				SpriteEffects effects = direction ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

				// Some math magic to make it smoothly move up and down over time
				const float TwoPi = (float)Math.PI * 2f;
				float offset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 5f);
				//float offset = (float)Math.Sin((float)((Main.gameTimeCache.TotalGameTime.TotalSeconds + (0.0667f * 2.67f)) % 3600.0) * TwoPi / 5f);
				Vector2 drawPos = worldPos - Main.screenPosition + new Vector2(0f, -40f) + new Vector2(0f, offset * 4f);

				// Draw the main texture
				Main.spriteBatch.Draw(texture, drawPos, frame, color, 0f, origin, 1f, effects, 0f);

				// Draw the periodic glow effect
				float scale = (float)Math.Sin(Main.GlobalTimeWrappedHourly * TwoPi / 2f) * 0.3f + 0.7f;
				Color effectColor = color;
				effectColor.A = 0;
				effectColor = effectColor * 0.1f * scale;
				for (float num5 = 0f; num5 < 1f; num5 += 355f / (678f * (float)Math.PI))
				{
					Main.spriteBatch.Draw(texture, drawPos + (TwoPi * num5).ToRotationVector2() * (6f + offset * 2f), frame, effectColor, 0f, origin, 1f, effects, 0f);
				}
			}
		}
	}
}
