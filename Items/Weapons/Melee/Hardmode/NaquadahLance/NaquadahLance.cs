using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.NaquadahLance;

public class NaquadahLance : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<NaquadahLanceProj>(), 47, 5.5f, 26, 5f, scale: 1.1f);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 72);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
public class NaquadahLanceProj : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<NaquadahLance>().DisplayName;
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
	protected override float HoldoutRangeMax => 160;
	protected override float HoldoutRangeMin => 40;
}
