using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.StaminaScrolls;

class TeleportScroll : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.StaminaScrolls;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.width = dims.Width;
		Item.rare = ItemRarityID.Green;
		Item.useStyle = ItemUseStyleID.HoldUp;
		Item.UseSound = new SoundStyle("Avalon/Sounds/Item/Scroll");
		Item.accessory = true;
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll = true;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ModContent.ItemType<Material.BlankScroll>())
			.AddIngredient(ModContent.ItemType<Material.ChaosDust>(), 15)
			.AddIngredient(ItemID.SoulofSight, 5)
			.AddTile(TileID.Loom)
			.Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			player.GetModPlayer<AvalonStaminaPlayer>().TeleportUnlocked = true;
		}
	}
}
