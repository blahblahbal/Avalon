using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Common.Templates;

namespace Avalon.Projectiles.Melee;

public class CaesiumPike : SpearTemplate
{
    public override void SetDefaults()
    {
        base.SetDefaults();
    }
    protected override float HoldoutRangeMax => 200;
    protected override float HoldoutRangeMin => 40;
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        SoundEngine.PlaySound(SoundID.Item14, target.position);
        Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<CaesiumExplosion>(), Projectile.damage, 5f, Projectile.owner);
        target.AddBuff(BuffID.OnFire3, 60 * 5);
    }
}
