using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Avalon.Items.Material.Bars;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.PreHardmode;
[AutoloadEquip(EquipType.Neck)]
public class NerveNumbNecklace : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 1, 50);
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		for (int i = 0; i < player.buffTime.Length; i++)
		{
			if (Main.debuff[player.buffType[i]] && player.GetModPlayer<AvalonStaminaPlayer>().StatStam > 0 && !BuffID.Sets.NurseCannotRemoveDebuff[player.buffType[i]] && player.buffType[i] != BuffID.Tipsy)
			{
				if (player.buffTime[i] % 3 == 0)
				{
					player.buffTime[i]--;
				}
				if (player.buffTime[i] % 7 == 0)
				{
					player.ConsumeStamina(1);
					player.GetModPlayer<AvalonStaminaPlayer>().StamRegenDelay = 60;
				}
			}
		}
	}
	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ItemID.BandofStarpower)
			.AddIngredient(ModContent.ItemType<StaminaCrystal>())
			.AddTile(TileID.TinkerersWorkbench)
			.AddCondition(Condition.InGraveyard)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ItemID.PanicNecklace)
			.AddIngredient(ModContent.ItemType<StaminaCrystal>())
			.AddTile(TileID.TinkerersWorkbench)
			.AddCondition(Condition.InGraveyard)
			.Register();

		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<StaminaCrystal>(), 3)
			.AddIngredient(ModContent.ItemType<BacciliteBar>(), 4)
			.AddIngredient(ModContent.ItemType<CorruptShard>(), 2)
			.AddTile(TileID.TinkerersWorkbench)
			.Register();
	}
}
