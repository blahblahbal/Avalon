using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Avalon.Items.Material.Ores;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class StaminaCrystal : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable();
		Item.UseSound = SoundID.Item29;
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 1, 90);
	}

	public override bool CanUseItem(Player player)
	{
		return player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax < 300;
	}

	public override bool? UseItem(Player player)
	{
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax += 30;
		player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2 += 30;
		player.GetModPlayer<AvalonStaminaPlayer>().StatStam += 30;
		if (ExxoAvalonOrigins.Achievements != null)
		{
			ExxoAvalonOrigins.Achievements.Call("Event", "UseStaminaCrystal");
		}
		return true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Boltstone>(), 25)
			.AddTile(TileID.Furnaces)
			.DisableDecraft()
			.Register();
	}
}
