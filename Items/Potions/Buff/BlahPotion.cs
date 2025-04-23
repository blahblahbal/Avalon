using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

public class BlahPotion : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.DrinkParticleColors[Type] = [
			Color.Orange,
			Color.LightGray,
			Color.Goldenrod
		];
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BuffPotion;
	}
	public override void SetDefaults()
	{
		Item.DefaultToBuffPotion(ModContent.BuffType<Buffs.Blah>(), TimeUtils.HoursToTicks(5), ClassExtensions.PotionCorkType.None);
		Item.maxStack = 1;
		Item.consumable = false;
		Item.rare = ModContent.RarityType<Rarities.BlahRarity>();
	}
}
