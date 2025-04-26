using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class Blueshift : ModItem
{
	int useTimer = 0;
	public override void SetDefaults()
	{
		Item.DefaultToHamaxe(65, 85, 23, 3f, 24, 20, useTurn: false);
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(silver: 54);
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
				Item.useTime = 24;
			}
		}
	}
}
