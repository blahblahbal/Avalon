using ExxoAvalonOrigins.Common;
using Terraria;
using Terraria.Graphics.Renderers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avalon.Hooks;
public class ShadowEffect : ModHook
{
    protected override void Apply()
    {
        On_LegacyPlayerRenderer.DrawPlayerFull += OnDrawPlayerFull;
    }
    private static void OnDrawPlayerFull(On_LegacyPlayerRenderer.orig_DrawPlayerFull orig, LegacyPlayerRenderer self, Terraria.Graphics.Camera camera, Player player)
    {
        SpriteBatch spriteBatch = camera.SpriteBatch;
        SamplerState samplerState = camera.Sampler;
        if (player.mount.Active && player.fullRotation != 0f)
        {
            samplerState = LegacyPlayerRenderer.MountedSamplerState;
        }
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, samplerState, DepthStencilState.None, camera.Rasterizer, null, camera.GameViewMatrix.TransformationMatrix);
        if (player.GetModPlayer<AvalonPlayer>().ShadowCharm)
        {
            for (int num12 = 0; num12 < 3; num12++)
            {
                self.DrawPlayer(camera, player, player.shadowPos[num12], player.shadowRotation[num12], player.shadowOrigin[num12], 0.5f + 0.2f * num12);
            }
        }
        if (player.GetModPlayer<AvalonPlayer>().PulseCharm)
        {
            _ = player.position;
            if (!Main.gamePaused)
            {
                player.ghostFade += player.ghostDir * 0.075f;
            }
            if (player.ghostFade < 0.1)
            {
                player.ghostDir = 1f;
                player.ghostFade = 0.1f;
            }
            else if (player.ghostFade > 0.9)
            {
                player.ghostDir = -1f;
                player.ghostFade = 0.9f;
            }
            float num5 = player.ghostFade * 5f;
            for (int l = 0; l < 4; l++)
            {
                float num6;
                float num7;
                switch (l)
                {
                    default:
                        num6 = num5;
                        num7 = 0f;
                        break;
                    case 1:
                        num6 = 0f - num5;
                        num7 = 0f;
                        break;
                    case 2:
                        num6 = 0f;
                        num7 = num5;
                        break;
                    case 3:
                        num6 = 0f;
                        num7 = 0f - num5;
                        break;
                }
                Vector2 position = new Vector2(player.position.X + num6, player.position.Y + player.gfxOffY + num7);
                self.DrawPlayer(camera, player, position, player.fullRotation, player.fullRotationOrigin, player.ghostFade);
            }
        }
        spriteBatch.End();
        orig(self, camera, player);
    }
}
