using Avalon.Common.Templates;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles.Contagion.ContagionChest
{
	public class ContagionChest : ModItem
	{
		public override void SetDefaults()
		{
			Item.DefaultToPlaceableTile(ModContent.TileType<ContagionChestTile>());
			Item.width = 26;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 5);
		}
	}
	[LegacyName("ContagionChest")]
	public class ContagionChestTile : ChestTemplate
	{
		public override int DropItem => ModContent.ItemType<ContagionChest>();
		protected override bool CanBeLocked => true;
		protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.ContagionKey>();
		public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
		{
			return NPC.downedPlantBoss;
		}
		public override bool LockChest(int i, int j, ref short frameXAdjustment, ref bool manual)
		{
			if (!NPC.downedPlantBoss)
			{
				return false;
			}
			return base.LockChest(i, j, ref frameXAdjustment, ref manual);
		}
	}
}
