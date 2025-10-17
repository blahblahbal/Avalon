using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.XanthophyteSpear;

public class XanthophyteSpear : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<XanthophyteSpearProj>(), 52, 5.5f, 22, 5.4f, true);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 40);
	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 12)
			.AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
}
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
