using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class LargeZircon : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = Data.Sets.ItemGroupValues.Gems;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.LargeAmber);
        Item.rare = ItemRarityID.Blue;
        Item.width = 32;
        Item.height = 32;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 15)
            .AddTile(TileID.Anvils)
			.SortAfterFirstRecipesOf(ItemID.LargeDiamond)
			.Register();
    }
    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        return false;
    }
    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Texture2D itemTexture = Item.ModItem.GetTexture().Value;
        float num5 = Item.height - itemTexture.Height;
        float num6 = Item.width / 2 - itemTexture.Width / 2;

        Main.spriteBatch.Draw(itemTexture, new Vector2(Item.position.X - Main.screenPosition.X + (float)(itemTexture.Width / 2) + num6, Item.position.Y - Main.screenPosition.Y + (float)(itemTexture.Height / 2) + num5 + 2f), new Rectangle(0, 0, itemTexture.Width, itemTexture.Height), new Color(250, 250, 250, (int)Main.mouseTextColor / 2), rotation, new Vector2(itemTexture.Width / 2, itemTexture.Height / 2), (float)(int)Main.mouseTextColor / 1000f + 0.8f, SpriteEffects.None, 0f);
    }
}
