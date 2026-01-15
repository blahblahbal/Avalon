using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Spears;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Spears;
public class CaesiumPikeProj : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<CaesiumPike>().DisplayName;
	protected override float HoldoutRangeMax => 200;
	protected override float HoldoutRangeMin => 40;
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		SoundEngine.PlaySound(SoundID.Item14, target.position);
		Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<CaesiumExplosion>(), Projectile.damage, 5f, Projectile.owner);
		target.AddBuff(BuffID.OnFire3, 60 * 5);
	}
}