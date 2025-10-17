using Avalon.Common;
using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Avalon.Items.Material.Bars;
using Avalon.Items.Weapons.Melee.Hardmode.CaesiumScimitar;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.CaesiumMace;

public class CaesiumMace : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
	}
	public override void SetDefaults()
	{
		Item.DefaultToFlail(ModContent.ProjectileType<CaesiumMaceProj>(), 49, 9f, 40, 25f);
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 1, 8);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<CaesiumBar>(), 30)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class CaesiumMaceProj : FlailTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<CaesiumMace>().DisplayName;
	public override int LaunchTimeLimit => 20;
	public override float LaunchSpeed => 24f;
	public override float MaxLaunchLength => 1200f;
	public override float RetractAcceleration => 5f;
	public override float MaxRetractSpeed => 16f;
	public override float ForcedRetractAcceleration => 6f;
	public override float MaxForcedRetractSpeed => 48f;
	public override int DefaultHitCooldown => 10;
	public override int SpinHitCooldown => 20;
	public override int MovingHitCooldown => 10;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		Projectile.width = 28;
		Projectile.height = 28;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		SoundEngine.PlaySound(SoundID.Item14, target.position);
		Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<CaesiumExplosion>(), Projectile.damage, 5f, Projectile.owner);
		target.AddBuff(BuffID.OnFire3, TimeUtils.SecondsToTicks(5));
		base.OnHitNPC(target, hit, damageDone);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		SoundEngine.PlaySound(SoundID.Item14, target.position);
		Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0, 0, ModContent.ProjectileType<CaesiumExplosion>(), Projectile.damage, 5f, Projectile.owner);
		target.AddBuff(BuffID.OnFire3, TimeUtils.SecondsToTicks(5));
		base.OnHitPlayer(target, info);
	}
}
