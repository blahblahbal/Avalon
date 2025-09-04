using ModLiquidLib.ID;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Items;

public abstract class LiquidBucketItemBase : ModItem
{
	public abstract int LiquidType();
	public virtual bool OverwriteLiquids => false;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
		ItemID.Sets.AlsoABuildingItem[Type] = true;
		ItemID.Sets.DuplicationMenuToolsFilter[Type] = true;
		LiquidID_TLmod.Sets.CreateLiquidBucketItem[LiquidType()] = Type;

		CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 24;
		Item.maxStack = Item.CommonMaxStack;
		Item.useTurn = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.useStyle = ItemUseStyleID.Swing;
	}

	public override void HoldItem(Player player)
	{
		if (!player.JustDroppedAnItem)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			if (player.noBuilding || !player.IsTargetTileInItemRange(Item))
			{
				return;
			}
			if (!Main.GamepadDisableCursorItemIcon)
			{
				player.cursorItemIconEnabled = true;
				Main.ItemIconCacheUpdate(Item.type);
			}
			if (!player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
			{
				return;
			}
			MLLSystems.PourBucket(Item, player, LiquidType(), false, OverwriteLiquids);
		}
	}
}
