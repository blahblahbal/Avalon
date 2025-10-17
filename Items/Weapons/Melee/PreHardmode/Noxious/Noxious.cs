using Avalon.Common.Extensions;
using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode.Noxious;

public class Noxious : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[Item.type] = true;
		ItemID.Sets.GamepadExtraRange[Item.type] = 10;
		ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.DefaultToYoyo(ModContent.ProjectileType<NoxiousProj>(), 15, 5.25f, 16f);
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<BacciliteBar>(), 12)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class NoxiousProj : ModProjectile
{
	public override LocalizedText DisplayName => ModContent.GetInstance<Noxious>().DisplayName;
	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 8;
		ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 15 * 16f;
		ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 12f;
	}

	public override void SetDefaults()
	{
		Projectile.extraUpdates = 0;
		Projectile.width = 16;
		Projectile.height = 16;
		Projectile.aiStyle = 99;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.scale = 1f;
	}
}
