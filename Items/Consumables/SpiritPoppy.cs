using Avalon.Common.Extensions;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class SpiritPoppy : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 10;
	}

	public override void SetDefaults()
	{
		Item.DefaultToConsumable();
		Item.UseSound = SoundID.Item4;
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 0, 50);
	}

	public override bool CanUseItem(Player player)
	{
		return player.statManaMax >= 200 && player.GetModPlayer<AvalonPlayer>().SpiritPoppyUseCount < 10;
	}

	public override bool? UseItem(Player player)
	{
		player.GetModPlayer<AvalonPlayer>().SpiritPoppyUseCount++;
		return true;
	}
}
