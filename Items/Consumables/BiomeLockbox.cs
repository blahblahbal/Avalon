using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class BiomeLockbox : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		Item.width = 12;
		Item.height = 12;
		Item.maxStack = Item.CommonMaxStack;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(gold: 3);
	}
	private static Item? keyUsed;
	public override bool CanRightClick()
	{
		keyUsed = null;
		Player player = Main.LocalPlayer;
		if (Main.mouseItem.type == ItemID.None)
		{
			for (int i = 0; i < 50; i++)
			{
				for (int j = 0; j < Data.Sets.ItemSets.BiomeLockboxCollection.Count; j++)
				{
					if (player.inventory[i].type == Data.Sets.ItemSets.BiomeLockboxCollection[j][0])
					{
						keyUsed = player.inventory[i];
						i = 50;
						break;
					}
				}
			}
		}
		else
		{
			for (int j = 0; j < Data.Sets.ItemSets.BiomeLockboxCollection.Count; j++)
			{
				if (Main.mouseItem.type == Data.Sets.ItemSets.BiomeLockboxCollection[j][0])
				{
					keyUsed = Main.mouseItem;
					break;
				}
			}
		}
		return keyUsed != null;
	}
	public override void RightClick(Player player)
	{
		OpenBiomeLockbox(keyUsed, player);
	}
	private static void OpenBiomeLockbox(Item key, Player player)
	{
		for (int i = 0; i < Data.Sets.ItemSets.BiomeLockboxCollection.Count; i++)
		{
			if (key.type == Data.Sets.ItemSets.BiomeLockboxCollection[i][0])
			{
				player.QuickSpawnItem(
					player.GetSource_FromThis(),
					Main.rand.NextFromList(Data.Sets.ItemSets.BiomeLockboxCollection[i].GetRange(1, Data.Sets.ItemSets.BiomeLockboxCollection[i].Count - 1).ToArray()));
			}
		}
		key.stack--;
		if (key.stack <= 0) key.SetDefaults();
	}
}
