using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvLifeforcePotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
			new Color(239, 17, 0),
			new Color(209, 15, 0),
			new Color(136, 9, 0)
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvLifeforce>();
		Item.UseSound = SoundID.Item3;
		Item.consumable = true;
		Item.rare = ItemRarityID.Lime;
		Item.width = dims.Width;
		Item.useTime = 15;
		Item.useStyle = ItemUseStyleID.DrinkLiquid;
		Item.maxStack = 9999;
		Item.value = Item.sellPrice(0, 0, 4, 0);
		Item.useAnimation = 15;
		Item.height = dims.Height;
		Item.buffTime = TimeUtils.MinutesToTicks(10);
	}
}
