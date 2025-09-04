using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL;
public class MLLGlobalItem : GlobalItem
{
	public override bool AppliesToEntity(Item entity, bool lateInstantiation)
	{
		return entity.type is ItemID.SuperAbsorbantSponge or ItemID.LavaAbsorbantSponge or ItemID.HoneyAbsorbantSponge or ItemID.UltraAbsorbantSponge or ItemID.BottomlessBucket or ItemID.BottomlessLavaBucket or ItemID.BottomlessHoneyBucket or ItemID.BottomlessShimmerBucket;
	}
	public static int GetLiquidType(int itemType) => itemType switch
	{
		ItemID.BottomlessBucket => LiquidID.Water,
		ItemID.LavaAbsorbantSponge or ItemID.BottomlessLavaBucket => LiquidID.Lava,
		ItemID.HoneyAbsorbantSponge or ItemID.BottomlessHoneyBucket => LiquidID.Honey,
		ItemID.BottomlessShimmerBucket => LiquidID.Shimmer,
		_ => throw new System.NotImplementedException(),
	};
	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.SuperAbsorbantSponge] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.LavaAbsorbantSponge] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.HoneyAbsorbantSponge] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.UltraAbsorbantSponge] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.BottomlessBucket] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.BottomlessLavaBucket] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.BottomlessHoneyBucket] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[ItemID.BottomlessShimmerBucket] = true;
	}
	public override bool AltFunctionUse(Item item, Player player)
	{
		return true;
	}
	public override void HoldItem(Item item, Player player)
	{
		if (!player.JustDroppedAnItem)
		{
			if (player.altFunctionUse == 2)
			{
				if (player.whoAmI != Main.myPlayer)
				{
					return;
				}
				if (player.noBuilding || !player.IsTargetTileInItemRange(item))
				{
					return;
				}
				if (!Main.GamepadDisableCursorItemIcon)
				{
					player.cursorItemIconEnabled = true;
					Main.ItemIconCacheUpdate(item.type);
				}
				if (!player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
				{
					return;
				}
				if (item.type is ItemID.BottomlessBucket or ItemID.BottomlessLavaBucket or ItemID.BottomlessHoneyBucket or ItemID.BottomlessShimmerBucket)
				{
					MLLSystems.PourBucket(item, player, GetLiquidType(item.type), true, false);
				}
				else if (item.type is ItemID.SuperAbsorbantSponge or ItemID.LavaAbsorbantSponge or ItemID.HoneyAbsorbantSponge or ItemID.UltraAbsorbantSponge)
				{
					Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
					bool anyLiquid = item.type is ItemID.UltraAbsorbantSponge;
					if (anyLiquid || (item.type is ItemID.SuperAbsorbantSponge && (tile.LiquidType is LiquidID.Water or LiquidID.Shimmer)) || tile.LiquidType == GetLiquidType(item.type))
					{
						int origType = tile.LiquidType;
						if (tile.LiquidAmount <= 0)
						{
							return;
						}
						MLLSystems.SpongeAbsorb(item, player, tile, origType, player.altFunctionUse == 2, anyLiquid);
					}
				}
			}
		}
	}
}
