using Avalon.Common.Templates;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Tiles.Furniture
{
	public class UnderworldChest : ChestTemplate
	{
		public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.UnderworldChest>();
		protected override bool CanBeLocked => true;
		protected override int ChestKeyItemId => ModContent.ItemType<Items.Other.UnderworldKey>();
		public override bool UnlockChest(int i, int j, ref short frameXAdjustment, ref int dustType, ref bool manual)
		{
			return NPC.downedPlantBoss;
		}
		public override ushort GetMapOption(int i, int j)
		{
			return (ushort)(Main.tile[i, j].TileFrameX / 36);
		}
	}
}
