using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Steamworks;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Pets;

public class ArgusLantern : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 8;
        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.LightPet[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.aiStyle = -1;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        // If the player is no longer active (online) - deactivate (remove) the projectile.
        if (!player.active)
        {
            Projectile.active = false;
            return;
        }

        // Keep the projectile disappearing as long as the player isn't dead and has the pet buff.
        if (!player.dead && player.HasBuff(ModContent.BuffType<Buffs.Pets.ArgusLantern>()))
        {
            Projectile.timeLeft = 2;
        }

        Vector2 targetPos = player.Center + new Vector2(player.direction * 40,-50) + player.velocity;
        Projectile.spriteDirection = -player.direction;

        if (Projectile.Center.Distance(targetPos) < 200)
        {
            Projectile.ai[0] = 0;
            Projectile.velocity = Projectile.Center.DirectionTo(targetPos) * 4;
            if (Projectile.Center.Distance(targetPos) < 4)
            {
                Projectile.Center = targetPos;
                Projectile.velocity = Vector2.Zero;
            }
            if (Projectile.Center.Distance(targetPos) < 190)
            {
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.rotation <= MathHelper.Pi ? 0 : MathHelper.TwoPi, 0.07f);
            }
            //if(Projectile.rotation == MathHelper.TwoPi)
            //{
            //    Projectile.rotation = 0;
            //}
        }
        else
        {
            Projectile.velocity = Projectile.Center.DirectionTo(targetPos) * MathHelper.Clamp(Projectile.Center.Distance(targetPos) * 0.03f,4,255);
            Projectile.ai[0] = 1;

            float num808 = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            if ((double)Math.Abs(Projectile.rotation - num808) >= 3.14)
            {
                if (num808 < Projectile.rotation)
                {
                    Projectile.rotation -= 6.28f;
                }
                else
                {
                    Projectile.rotation += 6.28f;
                }
            }
            Projectile.rotation = (Projectile.rotation * 4f + num808) / 5f;

            //Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            //Projectile.rotation = MathHelper.Lerp(Projectile.rotation, Projectile.velocity.ToRotation() + MathHelper.PiOver2, 0.2f);
        }

        if (Projectile.Center.Distance(player.Center) > 1000)
            Projectile.Center = player.Center;

        if (!Main.dedServ)
        {
            Lighting.AddLight(Projectile.Center, new Vector3(1,0.7f,0.7f) * 0.5f);
        }

        HighlightDangerTiles();

        //Animation

        if (Projectile.frame >= 4)
        {
            Projectile.frame -= 4;
        }

        Projectile.frameCounter++;
        if(Projectile.frameCounter > 6)
        {
            Projectile.frame++;
            Projectile.frameCounter = 0;
        }
        if(Projectile.frame == 4)
        {
            Projectile.frame = 0;
        }
        if (Projectile.ai[0] == 1)
        {
            Projectile.frameCounter++;
            Projectile.frame += 4;
        }
    }
    public void HighlightDangerTiles()
    {
        int num799 = 17;
        if ((Projectile.Center - Main.player[Main.myPlayer].Center).Length() < (float)(Main.screenWidth + num799 * 16))
        {
            int num800 = (int)Projectile.Center.X / 16;
            int num801 = (int)Projectile.Center.Y / 16;
            for (int num802 = num800 - num799; num802 <= num800 + num799; num802++)
            {
                for (int num803 = num801 - num799; num803 <= num801 + num799; num803++)
                {
                    //TileDrawInfo drawData = new TileDrawInfo();
                    //drawData.tileCache = Main.tile[num802, num803];
                    //drawData.typeCache = drawData.tileCache.TileType;
                    if (Main.rand.NextBool(7) && TileDrawing.IsTileDangerous(num802, num803, Main.LocalPlayer) && new Vector2(num800 - num802, num801 - num803).Length() < (float)num799 && num802 > 0 && num802 < Main.maxTilesX - 1 && num803 > 0 && num803 < Main.maxTilesY - 1 && Main.tile[num802, num803] != null/* && Main.tile[num802, num803].active()*/)
                    {
                        int num804 = Dust.NewDust(new Vector2(num802 * 16, num803 * 16), 16, 16, DustID.RedTorch, 0f, 0f, 0, default(Color), 0.6f);
                        Main.dust[num804].fadeIn = 0.75f;
                        Dust dust2 = Main.dust[num804];
                        dust2.velocity *= 0.3f;
                        Main.dust[num804].noGravity = true;
                        Main.dust[num804].noLight = true;
                        //Main.dust[804].color = Color.Red;
                    }
                }
            }
        }
    }
}
