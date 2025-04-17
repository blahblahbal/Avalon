using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

public class ElementDust : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 25;
	}
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.DroppedTomeMats;
	}
	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(200, 200, 200, 100);
	}
	public override void SetDefaults()
	{
		Item.DefaultToTomeMaterial();
		Item.rare = ItemRarityID.Blue;
	}
	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		if (Main.rand.NextBool(6))
		{
			int num28 = Dust.NewDust(Item.position, Item.width, Item.height, DustID.UltraBrightTorch, 0f, 0f, 200, Item.color);
			Main.dust[num28].velocity *= 0.3f;
			Main.dust[num28].velocity.Y--;
			Main.dust[num28].scale *= 0.5f;
			Main.dust[num28].noGravity = true;
		}
	}
}
