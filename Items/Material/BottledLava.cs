using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BottledLava : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable(true, 17, 17, 14, 24);
		Item.value = Item.sellPrice(0, 0, 1);
		Item.useTurn = true;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.UseSound = SoundID.Item3;
	}
	public override bool? UseItem(Player player)
	{
		player.AddBuff(BuffID.OnFire3, 60 * 20);
		player.AddBuff(BuffID.OnFire, 60 * 20);
		ExxoAvalonOrigins.Achievements?.Call("Event", "DrinkBottledLava");
		return true;
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.Bottle)
			.AddIngredient(ItemID.Obsidian)
			.AddCondition(Condition.NearLava)
			.Register();
	}
}
