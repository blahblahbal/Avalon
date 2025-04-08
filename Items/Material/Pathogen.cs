using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class Pathogen : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Contagion;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Orange;
		Item.width = dims.Width;
		Item.maxStack = 9999;
		Item.value = 4500;
		Item.height = dims.Height;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
	public override void PostUpdate()
	{
		float num12 = (float)Main.rand.Next(90, 111) * 0.01f;
		num12 *= Main.essScale * 0.3f;
		Lighting.AddLight((int)((Item.position.X + (float)(Item.width / 2)) / 16f), (int)((Item.position.Y + (float)(Item.height / 2)) / 16f), 1f * num12, 0.1f * num12, 1f * num12);
	}
}
