using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common
{
    public class VanillaRespritesThatAreNotSimpleGraphicReplacementsProjectile : GlobalProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement;
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.type is 45 or 44) // Demon Scythes
            {
                float rotationMultiplier = 0.7f;

                Texture2D texture = Mod.Assets.Request<Texture2D>("Assets/Vanilla/Projectiles/DemonScythe").Value;
                int frameHeight = texture.Height;
                Rectangle frame = new Rectangle(0, frameHeight * projectile.frame, texture.Width, frameHeight);
                int length = ProjectileID.Sets.TrailCacheLength[projectile.type];
                for (int i = 0; i < length; i++)
                {
                    //float multiply = ((float)(length - i) / length) * projectile.Opacity * 0.2f;
                    float multiply = (float)(length - i) / length * 0.5f;
                    Main.EntitySpriteDraw(texture, projectile.oldPos[i] - Main.screenPosition + (projectile.Size / 2f), frame, new Color(128, 128, 255, 128) * multiply, projectile.oldRot[i] * rotationMultiplier, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);
                }

                Main.EntitySpriteDraw(texture, projectile.position - Main.screenPosition + (projectile.Size / 2f), frame, new Color(255, 255, 255, 175) * 0.7f, projectile.rotation * rotationMultiplier, new Vector2(texture.Width, frameHeight) / 2, projectile.scale, SpriteEffects.None, 0);
                return false;
            }
            return base.PreDraw(projectile, ref lightColor);
        }
    }
    public class VanillaRespritesThatAreNotSimpleGraphicReplacementsNPC : GlobalNPC
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<AvalonConfig>().VanillaTextureReplacement;
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if(npc.netID == NPCID.Pinky)
            {
                Texture2D texture = Mod.Assets.Request<Texture2D>("Assets/Vanilla/NPCs/Pinky").Value;
                Main.EntitySpriteDraw(texture,npc.Bottom - Main.screenPosition - new Vector2(1,-4),new Rectangle(0,(int)(npc.frame.Y * (32f / 52f)),texture.Width,16),drawColor * 0.7f,npc.rotation,new Vector2(texture.Width / 2f,texture.Height / 2f),1,SpriteEffects.None);
                return false;
            }
            else if(npc.netID == NPCID.JungleSlime)
            {
                Texture2D texture = Mod.Assets.Request<Texture2D>("Assets/Vanilla/NPCs/JungleSlime").Value;
                Main.EntitySpriteDraw(texture, npc.Bottom - Main.screenPosition - new Vector2(1, -4), npc.frame, drawColor * 0.7f, npc.rotation, new Vector2(texture.Width / 2f, texture.Height / 2f), 1, SpriteEffects.None);
                return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}
