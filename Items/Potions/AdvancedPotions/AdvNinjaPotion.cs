using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvNinjaPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[1] {
			Color.Black
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvNinja>();
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
		Item.buffTime = TimeUtils.MinutesToTicks(8);
	}
}
