using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.AdvancedPotions;

public class AdvSummoningPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
			new Color(215, 241, 109),
			new Color(150, 178, 31),
			new Color(105, 124, 25)
		};
	}

	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.buffType = ModContent.BuffType<Buffs.AdvancedBuffs.AdvSummoning>();
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
		Item.buffTime = TimeUtils.MinutesToTicks(12);
	}
}
