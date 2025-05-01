using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

public class PrimeStaff : ModItem
{
	//public override void SetStaticDefaults()
	//{
	//	ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
	//	ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

	//	ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
	//}
	public override void SetDefaults()
	{
		Item.DefaultToMinionWeaponUpgradeable(50, 6.5f, 30, 14);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = SoundID.Item44;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.Bone, 50)
			.AddIngredient(ItemID.HallowedBar, 12)
			.AddIngredient(ItemID.SoulofFright, 20)
			.AddIngredient(ModContent.ItemType<Material.Shards.DemonicShard>(), 3)
			.AddTile(TileID.MythrilAnvil)
			.Register();
	}
	public override bool CanUseItem(Player player)
	{
		return true;
	}
	public override bool? UseItem(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			player.GetModPlayer<AvalonPlayer>().UpdatePrimeMinionStatus(player.GetSource_ItemUse_WithPotentialAmmo(Item, 0));
			return true;
		}
		return base.UseItem(player);
	}
}
