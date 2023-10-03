using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace Avalon.Projectiles.Melee;

public class PossessedFlamesawChop : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.light = 0.9f;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 10;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.MaxUpdates = 1;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        int num34 = 10;
        int num35 = 10;
        Vector2 vector7 = new Vector2(Projectile.position.X + Projectile.width / 2 - num34 / 2, Projectile.position.Y + Projectile.height / 2 - num35 / 2);
        Projectile.velocity = Collision.TileCollision(vector7, Projectile.velocity, num34, num35, true, true, 1);
        Projectile.ai[0] = 1f;
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
        return false;
    }
    public override void AI()
    {
        if (Projectile.soundDelay == 0)
        {
            Projectile.soundDelay = 8;
            SoundEngine.PlaySound(SoundID.Item7, Projectile.position);
        }
        if (Projectile.ai[0] == 0f)
        {
            Projectile.ai[1] += 1f;
            if (Projectile.type == ModContent.ProjectileType<PossessedFlamesawChop>())
            {
                if (Main.rand.NextBool(2))
                {
                    var num88 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, 0f, 0f, 255, default(Color), 0.75f);
                    Main.dust[num88].velocity *= 0.1f;
                    Main.dust[num88].noGravity = true;
                }
                if (Projectile.velocity.X > 0f)
                {
                    Projectile.spriteDirection = 1;
                }
                else if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = -1;
                }
                var num89 = Projectile.position.X;
                var num90 = Projectile.position.Y;
                var flag2 = false;
                if (Projectile.ai[1] > 10f)
                {
                    for (var num91 = 0; num91 < 200; num91++)
                    {
                        if (Main.npc[num91].active && !Main.npc[num91].dontTakeDamage && !Main.npc[num91].friendly && Main.npc[num91].lifeMax > 5)
                        {
                            var num92 = Main.npc[num91].position.X + Main.npc[num91].width / 2;
                            var num93 = Main.npc[num91].position.Y + Main.npc[num91].height / 2;
                            var num94 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num92) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num93);
                            if (num94 < 800f && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[num91].position, Main.npc[num91].width, Main.npc[num91].height))
                            {
                                num89 = num92;
                                num90 = num93;
                                flag2 = true;
                            }
                        }
                    }
                }
                if (!flag2)
                {
                    num89 = Projectile.position.X + Projectile.width / 2 + Projectile.velocity.X * 100f;
                    num90 = Projectile.position.Y + Projectile.height / 2 + Projectile.velocity.Y * 100f;
                    if (Projectile.ai[1] >= 30f)
                    {
                        Projectile.ai[0] = 1f;
                        Projectile.ai[1] = 0f;
                        Projectile.netUpdate = true;
                    }
                }
                var num95 = 12f;
                var num96 = 0.25f;
                var vector3 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                var num97 = num89 - vector3.X;
                var num98 = num90 - vector3.Y;
                var num99 = (float)Math.Sqrt(num97 * num97 + num98 * num98);
                num99 = num95 / num99;
                num97 *= num99;
                num98 *= num99;
                if (Projectile.velocity.X < num97)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num96;
                    if (Projectile.velocity.X < 0f && num97 > 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num96 * 2f;
                    }
                }
                else if (Projectile.velocity.X > num97)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num96;
                    if (Projectile.velocity.X > 0f && num97 < 0f)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num96 * 2f;
                    }
                }
                if (Projectile.velocity.Y < num98)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num96;
                    if (Projectile.velocity.Y < 0f && num98 > 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num96 * 2f;
                    }
                }
                else if (Projectile.velocity.Y > num98)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num96;
                    if (Projectile.velocity.Y > 0f && num98 < 0f)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num96 * 2f;
                    }
                }
                if (Projectile.type == ModContent.ProjectileType<PossessedFlamesawChop>() && Main.tile[(int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f)].HasTile && Main.tile[(int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f)].TileType == 5)
                {
                    WorldGen.KillTile((int)(Projectile.position.X / 16f), (int)(Projectile.position.Y / 16f), false, false, false);
                }
            }
            else if (Projectile.ai[1] >= 30f)
            {
                Projectile.ai[0] = 1f;
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
        }
        else
        {
            Projectile.tileCollide = false;
            var num100 = 9f;
            var num101 = 0.4f;
            if (Projectile.type == ModContent.ProjectileType<PossessedFlamesawChop>())
            {
                num100 = 16f;
                num101 = 1.2f;
            }
            var vector4 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            var num102 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector4.X;
            var num103 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector4.Y;
            var num104 = (float)Math.Sqrt(num102 * num102 + num103 * num103);
            if (num104 > 3000f)
            {
                Projectile.Kill();
            }
            num104 = num100 / num104;
            num102 *= num104;
            num103 *= num104;
            if (Projectile.velocity.X < num102)
            {
                Projectile.velocity.X = Projectile.velocity.X + num101;
                if (Projectile.velocity.X < 0f && num102 > 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X + num101;
                }
            }
            else if (Projectile.velocity.X > num102)
            {
                Projectile.velocity.X = Projectile.velocity.X - num101;
                if (Projectile.velocity.X > 0f && num102 < 0f)
                {
                    Projectile.velocity.X = Projectile.velocity.X - num101;
                }
            }
            if (Projectile.velocity.Y < num103)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + num101;
                if (Projectile.velocity.Y < 0f && num103 > 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y + num101;
                }
            }
            else if (Projectile.velocity.Y > num103)
            {
                Projectile.velocity.Y = Projectile.velocity.Y - num101;
                if (Projectile.velocity.Y > 0f && num103 < 0f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - num101;
                }
            }
            if (Main.myPlayer == Projectile.owner)
            {
                var rectangle = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
                var value4 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
                if (rectangle.Intersects(value4))
                {
                    Projectile.Kill();
                }
            }
        }
        Projectile.rotation += 0.4f * Projectile.direction;
    }
}
