using System;
using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Summon;

public class HungrySummon : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 6;
    }
    public override void SetDefaults()
    {
        Projectile.aiStyle = ProjAIStyleID.MiniTwins;
        AIType = ProjectileID.Spazmamini;
        Rectangle dims = this.GetDims();
        Projectile.netImportant = true;
        Projectile.width = 24;
        Projectile.height = 24;
        Projectile.penetrate = -1;
        Projectile.timeLeft *= 5;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.friendly = true;
        DrawOffsetX = -(int)((dims.Width / 2) - (Projectile.Size.X / 2));
        DrawOriginOffsetY = -(int)(((dims.Height / Main.projFrames[Projectile.type]) / 2) - (Projectile.Size.Y / 2));
        //Main.projPet[projectile.type] = true;
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        return false;
    }
    public override void AI()
    {
        Main.player[Projectile.owner].AddBuff(ModContent.BuffType<Buffs.Minions.Hungry>(), 3600);
        if (Projectile.type == ModContent.ProjectileType<HungrySummon>())
        {
            if (Main.player[Projectile.owner].dead)
            {
                Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().hungryMinion = false;
            }
            if (Main.player[Projectile.owner].GetModPlayer<AvalonPlayer>().hungryMinion)
            {
                Projectile.timeLeft = 2;
            }
        }

        if (Projectile.velocity.Length() >= 5f)
        {
            Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 5f;
        }
        //does not fucking work! aistyle overrides it
        //    if (++Projectile.frameCounter >= 5)
        //    {
        //        Projectile.frameCounter = 0;
        //        if (++Projectile.frame >= Main.projFrames[Projectile.type])
        //        {
        //            Projectile.frame = 0;
        //        }
        //    }

        // dunno how to offset dust behind the minion based on rotation lol
        //Dust.NewDustPerfect(Projectile.Center, DustID.Blood);
    }

    public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
        for (int npcIndex = 0; npcIndex < 200; npcIndex++)
        {
            NPC npc = Main.npc[npcIndex];
            if (Projectile.Hitbox.Intersects(npc.Hitbox) && !npc.friendly)
            {
                Projectile.position = Projectile.position + npc.velocity;
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 0.4f;
            }
        }
    }
}
