using Terraria;
using Avalon.Common.Players;
using Terraria.ModLoader;
using Terraria.ID;

namespace Avalon.Projectiles.Summon;
public class PrimeArmsCounter : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
        Main.projPet[Projectile.type] = true;
    }
    public override void SetDefaults()
    {
        Projectile.netImportant = true;
        Projectile.width = 10;
        Projectile.height = 10;
        Projectile.penetrate = -1;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.timeLeft = 60;
        Projectile.aiStyle = -1;
        Projectile.hide = true;
    }
    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];
        owner.AddBuff(ModContent.BuffType<Buffs.Minions.PrimeArms>(), 3600);
        Projectile.position = owner.position;
        Projectile.damage = 0;
        if (owner.dead)
        {
            owner.GetModPlayer<AvalonPlayer>().PrimeMinion = false;
        }
        if (owner.GetModPlayer<AvalonPlayer>().PrimeMinion)
        {
            Projectile.timeLeft = 2;
        }
    }
}
