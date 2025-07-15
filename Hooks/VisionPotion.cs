using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	internal class VisionPotion : ModHook
	{
		protected override void Apply()
		{
			On_TileDrawing.GetScreenDrawArea += On_TileDrawing_GetScreenDrawArea;
		}

		private void On_TileDrawing_GetScreenDrawArea(On_TileDrawing.orig_GetScreenDrawArea orig, TileDrawing self, Vector2 screenPosition, Vector2 offSet, out int firstTileX, out int lastTileX, out int firstTileY, out int lastTileY)
		{
			orig(self, screenPosition, offSet, out firstTileX, out lastTileX, out firstTileY, out lastTileY);
			if (Main.player[Main.myPlayer].GetModPlayer<AvalonPlayer>().Vision)
			{
				firstTileX -= 2;
				lastTileX += 2;
				lastTileY += 4;
				for (int i = firstTileY; i < lastTileY; i++)
				{
					if (i > Main.worldSurface - 50 && i <= Main.worldSurface)
					{
						for (int j = firstTileX; j < lastTileX; j++)
						{
							Tile tile = Main.tile[j, i];

							if (!tile.HasTile)
							{
								int diff = -(i - (int)Main.worldSurface);
								if (diff == 0)
								{
									diff = 1;
								}
								if (!Main.gamePaused && Main.rand.NextBool(300 + diff * 75))
								{
									float brightness = Lighting.Brightness(j, i);
									if (brightness < 0.5f)
									{
										Dust d = Dust.NewDustDirect(new Vector2(j * 16, i * 16), 16, 16, ModContent.DustType<Dusts.VisionPotion>(), 0f, 0f, (int)(brightness * 255) * 2, default, 0.1f);
										d.fadeIn = 1f;
										d.velocity *= 0.1f;
										d.noLight = true;
										d.noGravity = true;
									}
								}
							}
						}
					}
					else if (i > Main.worldSurface && i < Main.maxTilesY - 200)
					{
						for (int j = firstTileX; j < lastTileX; j++)
						{
							Tile tile = Main.tile[j, i];

							if (!tile.HasTile)
							{
								if (!Main.gamePaused && Main.rand.NextBool(300))
								{
									float brightness = Lighting.Brightness(j, i);
									if (brightness < 0.5f)
									{
										Dust d = Dust.NewDustDirect(new Vector2(j * 16, i * 16), 16, 16, ModContent.DustType<Dusts.VisionPotion>(), 0f, 0f, (int)(brightness * 255) * 2, default, 0.1f);
										d.fadeIn = 1f;
										d.velocity *= 0.1f;
										d.noLight = true;
										d.noGravity = true;
									}
								}
							}
						}
					}
				}
			}
		}
	}
}
