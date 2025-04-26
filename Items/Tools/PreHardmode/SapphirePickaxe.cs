using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class SapphirePickaxe : ModItem
{
	int useTimer = 0;
	public override void SetDefaults()
	{
		Item.DefaultToPickaxe(68, 9, 2f, 20, 20);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 36);
	}
	public override void HoldItem(Player player)
	{
		if (player.whoAmI == Main.myPlayer)
		{
			if (player.controlUseItem)
			{
				useTimer++;
				if (useTimer % 60 == 0)
				{
					useTimer = 0;
					if (Item.useAnimation > 8)
					{
						Item.useAnimation--;
					}
					if (Item.useTime > 8)
					{
						Item.useTime--;
					}
				}
			}
			else
			{
				Item.useAnimation = 20;
				Item.useTime = 20;
			}
		}
	}
}
