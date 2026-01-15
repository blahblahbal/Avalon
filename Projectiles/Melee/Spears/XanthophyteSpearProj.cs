using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Spears;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Projectiles.Melee.Spears;
public class XanthophyteSpearProj : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<XanthophyteSpear>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = 19;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.scale = 1.1f;
		Projectile.hide = true;
		Projectile.ownerHitCheck = true;
		Projectile.DamageType = DamageClass.Melee;
	}
	protected override float HoldoutRangeMax => 170;
	protected override float HoldoutRangeMin => 40;
}
