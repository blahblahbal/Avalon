using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Vanity;

public class AncientHeadphones : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Green;
		Item.vanity = true;
		Item.value = Item.sellPrice(0, 2);
		Item.GetGlobalItem<AvalonGlobalItemInstance>().WorksInVanity = true;
	}
	/*public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.MusicBox)
            .AddRecipeGroup(RecipeGroupID.IronBar, 7)
            .AddIngredient(ItemID.FallenStar, 10)
            .AddTile(TileID.Anvils)
            .Register();
    }*/
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AvalonPlayer>().AncientHeadphones = true;
		if (!hideVisual)
		{
			UpdateVanity(player);
		}
	}

	public override void UpdateVanity(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().AncientHeadphones = true;
	}
}
