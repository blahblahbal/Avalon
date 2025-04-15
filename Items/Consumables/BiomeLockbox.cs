using Avalon.Items.Other;
using Avalon.Items.Weapons.Melee.Hardmode;
using Avalon.Items.Weapons.Ranged.Hardmode;
using Microsoft.Xna.Framework;
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
				for (int j = 0; j < Data.Sets.ItemSets.BiomeLockboxCollection.Count; j++)
				{
					if (player.inventory[i].type == Data.Sets.ItemSets.BiomeLockboxCollection[j][0])
					{
						OpenBiomeLockbox(player.inventory[i], player);
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
					OpenBiomeLockbox(Main.mouseItem, player);
					break;
				}
			}
		}
	}
	public override bool CanRightClick()
	{
		return true;
	}
	private void OpenBiomeLockbox(Item key, Player player)
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
