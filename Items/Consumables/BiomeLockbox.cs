using Avalon.Items.Other;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Ranged.Hardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class BiomeLockbox : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 5;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.maxStack = 9999;
		Item.value = Item.sellPrice(gold: 3);
        Item.height = dims.Height;
    }
	public override void RightClick(Player player)
	{
		if (Main.mouseItem.type == ItemID.None)
		{
			for (int i = 0; i < 50; i++)
			{


				if (player.inventory[i].type == Data.Sets.Item.HallowedChest[0] || player.inventory[i].type == Data.Sets.Item.CorruptionChest[0] ||
					player.inventory[i].type == Data.Sets.Item.CrimsonChest[0] || player.inventory[i].type == Data.Sets.Item.DesertChest[0] ||
					player.inventory[i].type == Data.Sets.Item.JungleChest[0] || player.inventory[i].type == Data.Sets.Item.FrozenChest[0] ||
					player.inventory[i].type == Data.Sets.Item.UnderworldChest[0] || player.inventory[i].type == Data.Sets.Item.ContagionChest[0])
				{
					OpenBiomeLockbox(player.inventory[i], player);
					break;
				}
			}
		}
		else
		{
			Item key = new Item();
			key.SetDefaults(Main.mouseItem.type);
			if (Main.mouseItem.type == ItemID.HallowedKey || Main.mouseItem.type == ItemID.CorruptionKey ||
				Main.mouseItem.type == ItemID.CrimsonKey || Main.mouseItem.type == ItemID.DungeonDesertKey ||
				Main.mouseItem.type == ItemID.JungleKey || Main.mouseItem.type == ItemID.FrozenKey ||
				Main.mouseItem.type == ModContent.ItemType<UnderworldKey>() || Main.mouseItem.type == ModContent.ItemType<ContagionKey>())
			{
				OpenBiomeLockbox(Main.mouseItem, player);
			}
		}
	}
	public override bool CanRightClick()
	{
		return true;
	}
	private void OpenBiomeLockbox(Item key, Player player)
	{
		for (int i = 0; i < Data.Sets.Item.BiomeLockboxCollection.Count; i++)
		{
			if (key.type == Data.Sets.Item.BiomeLockboxCollection[i][0])
			{
				player.QuickSpawnItem(
					player.GetSource_FromThis(),
					Main.rand.NextFromList(Data.Sets.Item.BiomeLockboxCollection[i].GetRange(1, Data.Sets.Item.BiomeLockboxCollection[i].Count).ToArray()));
			}
		}
		//if (key.type == Data.Sets.Item.HallowedChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.HallowedChest.GetRange(1, Data.Sets.Item.HallowedChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.CorruptionChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.CorruptionChest.GetRange(1, Data.Sets.Item.CorruptionChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.CrimsonChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.CrimsonChest.GetRange(1, Data.Sets.Item.CrimsonChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.DesertChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.DesertChest.GetRange(1, Data.Sets.Item.DesertChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.JungleChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.JungleChest.GetRange(1, Data.Sets.Item.JungleChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.FrozenChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.FrozenChest.GetRange(1, Data.Sets.Item.FrozenChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.UnderworldChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.UnderworldChest.GetRange(1, Data.Sets.Item.UnderworldChest.Count).ToArray()));
		//}
		//else if (key.type == Data.Sets.Item.ContagionChest[0])
		//{
		//	player.QuickSpawnItem(player.GetSource_FromThis(), Main.rand.NextFromList(Data.Sets.Item.ContagionChest.GetRange(1, Data.Sets.Item.ContagionChest.Count).ToArray()));
		//}
		key.stack--;
		if (key.stack <= 0) key.SetDefaults();
	}
}
