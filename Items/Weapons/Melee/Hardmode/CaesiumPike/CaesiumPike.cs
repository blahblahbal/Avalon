using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Items.Weapons.Melee.Hardmode.CaesiumScimitar;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.CaesiumPike;

public class CaesiumPike : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<CaesiumPikeProj>(), 120, 4.5f, 30, 4f, true, scale: 1.1f);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 20);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
}
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
