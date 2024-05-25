using Avalon.Dusts;
using Avalon.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Avalon.NPCs.Bosses;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common;
using Avalon.Buffs.Debuffs;

namespace Avalon.Projectiles.Hostile.Phantasm;

public class SoulDagger : ModProjectile
{
    public int alpha = 255;
    public override void SetDefaults()
    {
        Projectile.width = 9;
        Projectile.height = 9;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
        Projectile.alpha = 0;
        Projectile.friendly = false;
        Projectile.hostile = true;
        Projectile.scale = 1.3f;
        Projectile.timeLeft = 99999999;
        //Projectile.GetGlobalProjectile<AvalonGlobalProjectileInstance>().notReflect = true;
    }
    public Vector2 towardsBoss;
    public bool isIdle = true;
    public int count;
    public Vector2 spawnPos = new Vector2(0, -125f);
    public bool runOnce;
    public int shootTimer;
    public float lerpAmount;
    public float rotTo;
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (Main.rand.NextBool(3))
		{
			target.AddBuff(ModContent.BuffType<ShadowCurse>(), 60 * 5);
		}
	}
	public override void AI()
    {
        if (AvalonGlobalNPC.PhantasmBoss != -1)
        {
            NPC boss = Main.npc[AvalonGlobalNPC.PhantasmBoss];
            NPCs.Bosses.Hardmode.Phantasm phantasm = (NPCs.Bosses.Hardmode.Phantasm)boss.ModNPC;
            Player player = GetClosestTo(Projectile.Center);

            float timing = 60 + (Projectile.ai[1] * 20);

            if(Projectile.ai[1] <= 6)
            {
                if (isIdle)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        if (Projectile.ai[1] == i)
                        {
                            double rotateAmount = 60 * i * (MathHelper.Pi / 180);
                            Projectile.Center = boss.Center + spawnPos.RotatedBy(rotateAmount);
                            spawnPos = spawnPos.RotatedBy(0.1);
                        }
                    }
                    Projectile.rotation = Vector2.Normalize(boss.Center - Projectile.Center).ToRotation() - MathHelper.PiOver2;
                }

                if (!isIdle)
                {
                    if (Projectile.ai[0] == 0)
                    {
                        Projectile.rotation = Vector2.Normalize(player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2;
                        Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 35f;
                        Projectile.timeLeft = 60;
                        Projectile.ai[0]++;
                        for (int i = 0; i < 30; i++)
                        {
                            int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1.5f);
                            Main.dust[num893].velocity *= 3f;
                            Main.dust[num893].noGravity = true;
                        }
                    }
                    if (Main.rand.NextBool((60 - Projectile.timeLeft + 10) / 10))
                    {
                        int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num893].velocity *= 0f;
                        Main.dust[num893].noGravity = true;
                    }
                    if (Projectile.timeLeft < 30)
                    {
                        Projectile.tileCollide = true;
                    }
                    if (Projectile.timeLeft < 20)
                    {
                        alpha -= 15;
                    }
                    if (alpha <= 0)
                    {
                        Projectile.Kill();
                    }
                }

                if (boss.ai[0] == 1)
                {
                    count++;
                    if (count == timing)
                    {
                        isIdle = false;
                    }
                    if (isIdle)
                    {
                        Projectile.rotation = Vector2.Normalize(player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2;
                    }
                }

                if (!runOnce)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num893].velocity *= 2f;
                        Main.dust[num893].noGravity = true;
                    }
                    runOnce = true;
                }
            }
            if(Projectile.ai[1] == 7)
            {
                if(shootTimer == 0)
                {
                    Projectile.rotation = boss.velocity.ToRotation() + MathHelper.PiOver2;
                    Projectile.rotation += 90 * (MathHelper.Pi / 180) * -phantasm.playerDir;
                }
                shootTimer++;

                if (shootTimer == 30)
                {
                    Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 30f;
                    rotTo = Vector2.Normalize(player.Center - Projectile.Center).ToRotation() + MathHelper.PiOver2;
                    Projectile.timeLeft = 60;
                    for (int i = 0; i < 30; i++)
                    {
                        int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num893].velocity *= 3f;
                        Main.dust[num893].noGravity = true;
                    }
                }
                if(shootTimer > 30)
                {
                    lerpAmount += 0.05f;
                    lerpAmount = MathHelper.Clamp(lerpAmount, 0f, 1f);
                    Projectile.rotation = Projectile.rotation.AngleLerp(rotTo, lerpAmount);
                    if (Main.rand.NextBool((60 - Projectile.timeLeft + 10) / 10))
                    {
                        int num893 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.DungeonSpirit, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[num893].velocity *= 0f;
                        Main.dust[num893].noGravity = true;
                    }
                    if (Projectile.timeLeft < 30)
                    {
                        Projectile.tileCollide = true;
                    }
                    if (Projectile.timeLeft < 20)
                    {
                        alpha -= 15;
                    }
                    if (alpha <= 0)
                    {
                        Projectile.Kill();
                    }
                }
            }
        }
        else
        {
            Projectile.Kill();
        }
    }
    static Player GetClosestTo(Vector2 position)
    {
        Player closest = null;
        float closestDistSQ = -1;
        for (int i = 0; i < Main.player.Length - 1; i++)
        {
            Player player = Main.player[i];
            if (player.active && (player.DistanceSQ(position) < closestDistSQ || closestDistSQ == -1))
            {
                closest = player;
                closestDistSQ = player.DistanceSQ(position);
            }
        }
        return closest;
    }
    public int randTex = Main.rand.Next(3);
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Hostile/Phantasm/SoulDagger").Value;
        if (randTex == 1)
        {
            texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Hostile/Phantasm/SoulDagger1").Value;
        }
        if (randTex == 2)
        {
            texture = ModContent.Request<Texture2D>("Avalon/Projectiles/Hostile/Phantasm/SoulDagger2").Value;
        }
        Rectangle frame = texture.Frame();
        Vector2 drawPos = Projectile.Center - Main.screenPosition;
        Color color = new Color(alpha, alpha, alpha, (alpha / 4) * 3);
        for (int i = 1; i < 4; i++)
        {
            Main.EntitySpriteDraw(texture, drawPos + new Vector2(Projectile.velocity.X * (-i * 1), Projectile.velocity.Y * (-i * 1)), frame, (color * (1 - (i * 0.25f))) * 0.75f, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 20f), Projectile.scale, SpriteEffects.None, 0);
        }
        Main.EntitySpriteDraw(texture, drawPos, frame, color, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 20f), Projectile.scale, SpriteEffects.None, 0);
        Main.EntitySpriteDraw(texture, drawPos, frame, color * 0.3f, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 14f), Projectile.scale * 1.3f, SpriteEffects.None, 0);
        Main.EntitySpriteDraw(texture, drawPos, frame, color * 0.15f, Projectile.rotation, texture.Size() / 2f - new Vector2(0, 10f), Projectile.scale * 1.6f, SpriteEffects.None, 0);
        return false;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity *= 0f;
        return false;
    }
}
