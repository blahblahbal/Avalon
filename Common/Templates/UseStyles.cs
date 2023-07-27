using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates
{
    public class UseStyles
    {
        public static void ShotgunStyle(Player player, float rotation = 0.1f, float backwardsMovement = 3f, float screenshakeIntensity = 3f)
        {
            //Texture2D GunTex = TextureAssets.Item[player.HeldItem.type].Value;
            //if (player.ItemAnimationJustStarted)
            //{
            //    Dust d = Dust.NewDustPerfect(player.Center + new Vector2(GunTex.Width * player.HeldItem.scale * player.direction,0).RotatedBy(player.itemRotation), DustID.Torch);
            //    d.noGravity = true;
            //    d.scale = 1.4f;
            //    d.velocity = new Vector2(-1, 0).RotatedBy(player.itemRotation);
            //}
            //else if (Main.rand.NextBool(3))
            //{
            //    Dust d2 = Dust.NewDustPerfect(player.Center + new Vector2(GunTex.Width * player.HeldItem.scale * player.direction, 0).RotatedBy(player.itemRotation), DustID.Smoke);
            //    d2.noGravity = true;
            //    d2.velocity *= 0.3f;
            //    d2.color = new Color(128, 127, 128);
            //    d2.alpha = 128 * (int)(player.itema);
            //    d2.velocity.Y -= 1f;
            //}
            if (player.ItemAnimationJustStarted && screenshakeIntensity != 0 && player.whoAmI == Main.myPlayer)
            {
                PunchCameraModifier modifier = new PunchCameraModifier(player.Center, new Vector2(Main.rand.NextFloat(-1.5f, -0.7f), 0).RotatedBy(player.itemRotation + Main.rand.NextFloat(-0.7f, 0.7f)), screenshakeIntensity, 6f, 8, 200f, player.name);
                Main.instance.CameraModifiers.Add(modifier);
            }
            if (player.itemAnimationMax - player.itemAnimation < 15)
            {
                player.itemLocation -= new Vector2((float)Math.Sin((player.itemAnimationMax - player.itemAnimation) * 0.4f) * backwardsMovement * player.direction, 0).RotatedBy(player.itemRotation);
                
                //player.itemLocation -= new Vector2(((float)(player.itemAnimationMax - player.itemAnimation) / (float)player.itemAnimationMax) * backwardsMovement * player.direction, 0).RotatedBy(player.itemRotation);
                player.itemRotation -= (float)Math.Sin((player.itemAnimationMax - player.itemAnimation) * 0.41f) * rotation * player.direction * player.gravDir;
            }
        }
    }
}
