using Avalon.Common;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
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
            for (int j = firstTileX - 2; j < lastTileX + 2; j++)
            {
                for (int i = firstTileY; i < lastTileY + 4; i++)
                {
                    Tile tile = Main.tile[j, i];

                    if (!tile.HasTile && Main.player[Main.myPlayer].GetModPlayer<AvalonPlayer>().Vision && i > Main.worldSurface)
                    {
                        Color color = Lighting.GetColor(j, i);
                        byte b = 200;
                        byte b2 = 170;
                        if (color.R < b)
                        {
                            color.R = b;
                        }
                        if (color.G < b2)
                        {
                            color.G = b2;
                        }
                        color.A = Main.mouseTextColor;
                        if (!Main.gamePaused && Main.rand.NextBool(200))
                        {
                            int num28 = Dust.NewDust(new Vector2(j * 16, i * 16), 16, 16, ModContent.DustType<Dusts.VisionPotion>(), 0f, 0f, 150, Color.LightBlue, 0.1f);
                            Main.dust[num28].fadeIn = 1f;
                            Main.dust[num28].velocity *= 0.1f;
                            Main.dust[num28].noLight = true;
                            Main.dust[num28].noGravity = true;
                        }
                    }
                }
            }
                    
        }
    }
}
