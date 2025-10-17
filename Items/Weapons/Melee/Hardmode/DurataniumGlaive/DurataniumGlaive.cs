using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.DurataniumGlaive;

public class DurataniumGlaive : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<DurataniumGlaiveProjectile>(), 44, 5.1f, 26, 5f);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1);

	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
public class DurataniumGlaiveProjectile : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<DurataniumGlaive>().DisplayName;
	public override void SetDefaults()
	{
		Projectile.width = 18;
		Projectile.height = 18;
		Projectile.aiStyle = ProjAIStyleID.Spear;
		Projectile.friendly = true;
		Projectile.penetrate = -1;
		Projectile.tileCollide = false;
		Projectile.scale = 1.1f;
		Projectile.hide = true;
		Projectile.ownerHitCheck = true;
		Projectile.DamageType = DamageClass.Melee;
	}
	protected override float HoldoutRangeMax => 175;
	protected override float HoldoutRangeMin => 40;
}

