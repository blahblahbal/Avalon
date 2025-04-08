using Avalon.Common;
using Avalon.Data.Sets;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class TaleoftheRedLotus : ModItem
{
	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = Data.Sets.ItemGroupValues.Tomes;
	}
	public override void SetDefaults()
	{
		Rectangle dims = this.GetDims();
		Item.rare = ItemRarityID.Green;
		Item.width = dims.Width;
		Item.value = 5000;
		Item.height = dims.Height;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
		Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 1;
	}

	//public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	//{
	//    Texture2D tex = (Texture2D)ModContent.Request<Texture2D>(Texture + "Glow");
	//    spriteBatch.Draw(tex, position + new Vector2(-4 * scale), new Rectangle(0,0,tex.Width,tex.Height), new Color(255, 255, 255, 0) * (float)(Math.Sin(Main.timeForVisualEffects * 0.03f) * 0.2f + 0.8f), 0, origin, scale, SpriteEffects.None,0);
	//    return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
	//}
	//public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	//{
	//    Texture2D tex = (Texture2D)ModContent.Request<Texture2D>(Texture + "Glow");
	//    spriteBatch.Draw(tex, Item.position + new Vector2(-4) + tex.Size() / 2 - Main.screenPosition, new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255, 0) * (float)(Math.Sin(Main.timeForVisualEffects * 0.03f) * 0.2f + 0.8f), rotation, tex.Size() / 2f, scale, SpriteEffects.None, 0);
	//    return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
	//}
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage(DamageClass.Ranged) += 0.05f;
		player.statLifeMax2 += 20;
	}

	public override void AddRecipes()
	{
		CreateRecipe(1)
			.AddIngredient(ModContent.ItemType<DewOrb>(), 3)
			.AddIngredient(ModContent.ItemType<CarbonSteel>(), 5)
			.AddIngredient(ModContent.ItemType<Sandstone>(), 10)
			.AddIngredient(ItemID.FallenStar, 15)
			.AddIngredient(ModContent.ItemType<MysticalTomePage>())
			.AddTile(ModContent.TileType<Tiles.TomeForge>())
			.Register();
	}
}
