using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class Moonphaser : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToConsumable(false, 15, 30);
		Item.shoot = ModContent.ProjectileType<Projectiles.Tools.Moonphaser>();
		Item.maxStack = 1;
		Item.rare = ItemRarityID.LightRed;
		Item.value = Item.sellPrice(0, 2, 70);
	}
	public override void AddRecipes()
	{
		Recipe.Create(Type)
			.AddIngredient(ItemID.Lens, 5)
			.AddIngredient(ItemID.SoulofLight, 10)
			.AddIngredient(ItemID.SoulofNight, 10)
			.AddRecipeGroup("GoldBar", 20)
			.AddIngredient(ItemID.BlackLens)
			.AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 4)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
