using Avalon.Common.Extensions;
using Avalon.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Spears;

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
