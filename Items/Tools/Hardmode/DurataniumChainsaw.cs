using Avalon.Common.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class DurataniumChainsaw : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsChainsaw[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToChainsaw(ModContent.ProjectileType<Projectiles.Tools.DurataniumChainsaw>(), 75, 25, 3.5f, 6);
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 1, 20);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
			.AddTile(TileID.Anvils)
			.Register();
	}
}
