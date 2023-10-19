using Avalon.Data.Sets;
using Avalon.Items.Material;
using Avalon.Items.Material.Ores;
using Avalon.NPCs.Bosses.PreHardmode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Hostile.bacteriumPrime;

public class MushroomWall : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 60;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.friendly = false;
        Projectile.timeLeft = 16 * 60;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
        Projectile.scale = 1f;
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        if (Projectile.height < 40 * 20)
        {
            Projectile.height++;
            Projectile.position.Y -= 1;
        }

        bool KillFast = true;
        for (int i = 0; i < Main.npc.Length; i++)
        {
            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<BacteriumPrime>())
            {
                KillFast = false;
            }
        }
        if (KillFast)
            Projectile.timeLeft -= 120;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
        Texture2D textureBottom = ModContent.Request<Texture2D>(Texture + "Bot").Value;
        Texture2D textureTop = ModContent.Request<Texture2D>(Texture + "Top").Value;
        Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
        Rectangle frameTop = new Rectangle(0, 0, texture.Width, textureTop.Height);
        for (int i = 0; i < (int)Math.Max(Math.Floor(Projectile.height / 20f),1); i++) 
        {
            Vector2 drawPos = Projectile.Bottom - Main.screenPosition - new Vector2(0,20 * i);
            Main.EntitySpriteDraw(texture, drawPos, frame, Lighting.GetColor((drawPos + Main.screenPosition).ToTileCoordinates()), 0f, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);
        }

        Vector2 drawPosBottom = Projectile.Bottom - Main.screenPosition;
        Main.EntitySpriteDraw(textureBottom, drawPosBottom, frame, Lighting.GetColor((drawPosBottom + Main.screenPosition).ToTileCoordinates()), 0f, new Vector2(texture.Width / 2, texture.Height), 1f, SpriteEffects.None, 0);

        Vector2 drawPosTop = Projectile.Top - Main.screenPosition + new Vector2(0,-16);
        Main.EntitySpriteDraw(textureTop, drawPosTop, frameTop, Lighting.GetColor((drawPosTop + Main.screenPosition).ToTileCoordinates()), 0f, new Vector2(texture.Width / 2, 0), 1f, SpriteEffects.None, 0);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);
        for(int i = 0; i < Projectile.height; i++)
        {
            Dust d = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.UnusedBrown);
            d.position.X += Main.rand.NextFloat(-20, 20);
            d.scale *= Main.rand.NextFloat(1,1.3f);
            d.fadeIn = Main.rand.NextFloat(0, 1);
            d.noGravity = !Main.rand.NextBool(5);
        }
    }
}
