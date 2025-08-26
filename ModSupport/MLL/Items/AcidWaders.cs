using Avalon.Items.Material;
using Avalon.ModSupport.MLL.Liquids;
using ModLiquidLib.ModLoader;
using ModLiquidLib.Utils.LiquidContent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

[AutoloadEquip(EquipType.Shoes)]
public class AcidWaders : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToAccessory();
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 10);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<AcidWadersPlayer>().AcidWalk = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.WaterWalkingBoots)
			.AddIngredient(ModContent.ItemType<AcidBucket>(), 3)
			.AddIngredient(ModContent.ItemType<LifeDew>(), 5)
			.AddTile(TileID.LunarCraftingStation)
			.Register();
	}
}
public class AcidWadersPlayer : ModPlayer
{
	public bool AcidWalk;

	public override void ResetEffects()
	{
		AcidWalk = false;
	}

	//PreUpdateMovement is called directly before water walking behaviour is executed
	//We use this hook to set what liquids can be walked on, as the defaults are set right before (water walking -> all liquids, water walking 2 -> all liquids minus lava).
	public override void PreUpdateMovement()
	{
		//Like waterWalk and waterWalk2, reversing gravity prevents the player from being able to walk on liquids
		if (Player.gravDir == -1f)
		{
			AcidWalk = false;
		}
		Player.GetModPlayer<ModLiquidPlayer>().canLiquidBeWalkedOn[LiquidLoader.LiquidType<Acid>()] = AcidWalk;
	}
}
