using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;
public abstract class LiquidSpongeItemBase : ModItem
{
	public abstract int LiquidType();
	public override void SetStaticDefaults()
	{
		ItemID.Sets.AlsoABuildingItem[Type] = true; //Unused, but useful to have here for both other mods and future game updates
		ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;

		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

		ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTurn = true;
		Item.useAnimation = 12;
		Item.useTime = 5;
		Item.width = 20;
		Item.height = 20;
		Item.autoReuse = true;
		Item.rare = ItemRarityID.Lime;
		Item.value = Item.sellPrice(0, 10);
		Item.tileBoost += 2;
	}
	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	//Here in HoldItem we do our sponge logic
	//We use HoldItem as it is the hook/method run right before sponge logic
	//This is so no extra item logic is run inbetween this hook/method and when the normal sponge logic would occur
	public override void HoldItem(Player player)
	{
		if (!player.JustDroppedAnItem)//make sure the player hasn't dropped an item
		{
			if (player.whoAmI != Main.myPlayer)//only run this logic on clients, we sync this in multiplayer later
			{
				return;
			}
			//we check that the player's cursor is in range to suck up liquid
			if (player.noBuilding || !player.IsTargetTileInItemRange(Item))
			{
				return;
			}
			//We then set the player's cursor to be that of this item
			if (!Main.GamepadDisableCursorItemIcon)
			{
				player.cursorItemIconEnabled = true;
				Main.ItemIconCacheUpdate(Item.type);
			}
			//Make sure that the player isn't using the item
			if (!player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
			{
				return;
			}
			Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
			if (tile.LiquidType == LiquidType()) //if the cursor's liquid selected is example liquid, then run the following logic
			{
				int origType = tile.LiquidType;
				if (tile.LiquidAmount <= 0)
				{
					return;
				}
				MLLSystems.SpongeAbsorb(Item, player, tile, origType, player.altFunctionUse == 2);
			}
		}
	}
}
