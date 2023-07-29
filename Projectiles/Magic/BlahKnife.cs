using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Magic;

public class BlahKnife : ModProjectile
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Projectile.width = 30;
        Projectile.height = 30;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.ignoreWater = true;
        Projectile.extraUpdates = 0;
    }
    //public override Color? GetAlpha(Color lightColor)
    //{
    //    return new Color(255, 255, 255, Projectile.alpha);
    //}
    public override void AI()
    {
        Lighting.AddLight(Projectile.position, 140 / 255f, 90 / 255f, 50 / 255f);
        var num28 = (float)Math.Sqrt(Projectile.velocity.X * Projectile.velocity.X + Projectile.velocity.Y * Projectile.velocity.Y);
        var num29 = Projectile.localAI[0];
        if (num29 == 0f)
        {
            Projectile.localAI[0] = num28;
            num29 = num28;
        }
        var projPosStoredX = Projectile.position.X;
        var projPosStoredY = Projectile.position.Y;
        var distance = 320f;
        var flag = false;
        var npcArrayIndexStored = 0;
        if (Projectile.ai[1] == 0f)
        {
            for (var npcArrayIndex = 0; npcArrayIndex < 200; npcArrayIndex++)
            {
                if (Main.npc[npcArrayIndex].active && !Main.npc[npcArrayIndex].dontTakeDamage && !Main.npc[npcArrayIndex].friendly && Main.npc[npcArrayIndex].lifeMax > 5 && (Projectile.ai[1] == 0f || Projectile.ai[1] == npcArrayIndex + 1))
                {
                    var npcCenterX = Main.npc[npcArrayIndex].position.X + Main.npc[npcArrayIndex].width / 2;
                    var npcCenterY = Main.npc[npcArrayIndex].position.Y + Main.npc[npcArrayIndex].height / 2;
                    var num37 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
                    if (num37 < distance && Collision.CanHit(new Vector2(Projectile.position.X + Projectile.width / 2, Projectile.position.Y + Projectile.height / 2), 1, 1, Main.npc[npcArrayIndex].position, Main.npc[npcArrayIndex].width, Main.npc[npcArrayIndex].height))
                    {
                        distance = num37;
                        projPosStoredX = npcCenterX;
                        projPosStoredY = npcCenterY;
                        flag = true;
                        npcArrayIndexStored = npcArrayIndex;
                    }
                }
            }
            if (flag)
            {
                Projectile.ai[1] = npcArrayIndexStored + 1;
            }
            flag = false;
        }
        if (Projectile.ai[1] != 0f)
        {
            var npcArrayIndexAgain = (int)(Projectile.ai[1] - 1f);
            if (Main.npc[npcArrayIndexAgain].active)
            {
                var npcCenterX = Main.npc[npcArrayIndexAgain].position.X + Main.npc[npcArrayIndexAgain].width / 2;
                var npcCenterY = Main.npc[npcArrayIndexAgain].position.Y + Main.npc[npcArrayIndexAgain].height / 2;
                var num41 = Math.Abs(Projectile.position.X + Projectile.width / 2 - npcCenterX) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - npcCenterY);
                if (num41 < 1000f)
                {
                    flag = true;
                    projPosStoredX = Main.npc[npcArrayIndexAgain].position.X + Main.npc[npcArrayIndexAgain].width / 2;
                    projPosStoredY = Main.npc[npcArrayIndexAgain].position.Y + Main.npc[npcArrayIndexAgain].height / 2;
                }
            }
        }
        if (flag)
        {
            var num42 = num29;
            var projCenter = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            var num43 = projPosStoredX - projCenter.X;
            var num44 = projPosStoredY - projCenter.Y;
            var num45 = (float)Math.Sqrt(num43 * num43 + num44 * num44);
            num45 = num42 / num45;
            num43 *= num45;
            num44 *= num45;
            var num46 = 8;
            Projectile.velocity.X = (Projectile.velocity.X * (num46 - 1) + num43) / num46;
            Projectile.velocity.Y = (Projectile.velocity.Y * (num46 - 1) + num44) / num46;
        }
        Projectile.rotation += (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) * 0.03f * Projectile.direction;
        if (Projectile.velocity.Y > 16f)
        {
            Projectile.velocity.Y = 16f;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        ghostHurt(Projectile.damage, Projectile.position);
        Main.player[Projectile.owner].VampireHeal((int)(Projectile.damage * 0.4f), Projectile.position);
    }
    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
        ghostHurt(Projectile.damage, Projectile.position);
    }
    public void ghostHurt(int dmg, Vector2 Position)
    {
        if (Projectile.DamageType != DamageClass.Magic || Projectile.damage <= 0)
        {
            return;
        }
        int num = Projectile.damage;
        if (dmg <= 1)
        {
            return;
        }
        int[] array = new int[200];
        int num4 = 0;
        _ = new int[200];
        int num5 = 0;
        for (int i = 0; i < 200; i++)
        {
            if (!Main.npc[i].CanBeChasedBy(this))
            {
                continue;
            }
            float num6 = Math.Abs(Main.npc[i].position.X + Main.npc[i].width / 2 - Projectile.position.X + Projectile.width / 2) + Math.Abs(Main.npc[i].position.Y + Main.npc[i].height / 2 - Projectile.position.Y + Projectile.height / 2);
            if (num6 < 800f)
            {
                if (Collision.CanHit(Projectile.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num6 > 50f)
                {
                    array[num5] = i;
                    num5++;
                }
                else if (num5 == 0)
                {
                    array[num4] = i;
                    num4++;
                }
            }
        }
        if (num4 != 0 || num5 != 0)
        {
            int num2 = ((num5 <= 0) ? array[Main.rand.Next(num4)] : array[Main.rand.Next(num5)]);
            float num7 = Main.rand.Next(-100, 101);
            float num8 = Main.rand.Next(-100, 101);
            float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
            num9 = 4f / num9;
            num7 *= num9;
            num8 *= num9;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Position, new Vector2(num7, num8), ModContent.ProjectileType<BlahKnifeSplit>(), num, 0f, Projectile.owner, num2);
        }
    }
}
