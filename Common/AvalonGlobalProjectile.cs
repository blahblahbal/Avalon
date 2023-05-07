using Avalon.Buffs.Debuffs;
using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Items.Accessories.Hardmode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common; 

internal class AvalonGlobalProjectile : GlobalProjectile
{
    public override bool PreAI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.TerraBlade2 && projectile.localAI[0] == 0)
        {
            projectile.localAI[0] = 1;
            SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            return true;
        }
        return base.PreAI(projectile);
    }
    public override void AI(Projectile projectile)
    {
        if (projectile.type == ProjectileID.TerraBlade2)
        {
            if (projectile.localAI[0] == 1)
            {
                projectile.localAI[0] = 2;
                SoundEngine.PlaySound(SoundID.Item8, projectile.position);
            }
        }
        if (!projectile.npcProj && !projectile.noEnchantments && projectile.DamageType == DamageClass.Melee)
        {
            Vector2 boxPosition = projectile.position;
            int boxWidth = projectile.width;
            int boxHeight = projectile.height;
            if (projectile.aiStyle == 190 || projectile.aiStyle == 191)
            {
                for (float num = -(float)Math.PI / 4f; num <= (float)Math.PI / 4f; num += (float)Math.PI / 2f)
                {
                    Rectangle r = Utils.CenteredRectangle(projectile.Center + (projectile.rotation + num).ToRotationVector2() * 70f * projectile.scale, new Vector2(60f * projectile.scale, 60f * projectile.scale));
                    EmitAvalonEnchants(r.TopLeft(), r.Width, r.Height, projectile);
                }
            }
            else
            {
                EmitAvalonEnchants(boxPosition, boxWidth, boxHeight, projectile);
            }
        }
    }

    public void EmitAvalonEnchants(Vector2 boxPosition, int boxWidth, int boxHeight, Projectile projectile)
    {
        if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().PathogenImbue)
        {
            if (Main.rand.NextBool(2))
            {
                int num5 = Dust.NewDust(boxPosition, boxWidth, boxHeight, ModContent.DustType<PathogenDust>(), projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 128, default, 1.5f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.7f;
                Main.dust[num5].velocity.Y -= 0.5f;
            }
        }
        if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().FrostGauntlet)
        {
            if (Main.rand.NextBool(2))
            {
                int num5 = Dust.NewDust(boxPosition, boxWidth, boxHeight, DustID.IceTorch, projectile.velocity.X * 0.2f + (float)(projectile.direction * 3), projectile.velocity.Y * 0.2f, 100, default, 2);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].velocity *= 0.7f;
                Main.dust[num5].velocity.Y -= 0.5f;
            }
        }
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!projectile.npcProj && !projectile.noEnchantments && projectile.DamageType == DamageClass.Melee)
        {
            if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().FrostGauntlet && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(BuffID.Frostburn2, 60 * 4);
            }
            if (Main.player[projectile.owner].GetModPlayer<AvalonPlayer>().PathogenImbue && hit.DamageType == DamageClass.Melee)
            {
                target.AddBuff(ModContent.BuffType<Pathogen>(), 60 * Main.rand.Next(3, 7));
            }
        }
            base.OnHitNPC(projectile, target, hit, damageDone);
    }
}
