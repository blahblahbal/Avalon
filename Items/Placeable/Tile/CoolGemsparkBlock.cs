using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class CoolGemsparkBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.CoolGemsparkBlock>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
	public override void AddRecipes()
	{
		CreateRecipe(15)
			.AddIngredient(ItemID.Glass, 15)
			.AddIngredient(ItemID.Emerald)
			.AddIngredient(ItemID.Sapphire)
			.AddIngredient(ItemID.Amethyst)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        spriteBatch.Draw(Terraria.GameContent.TextureAssets.Item[Item.type].Value, position, frame, new Color(Tiles.CoolGemsparkBlock.R, Tiles.CoolGemsparkBlock.G, Tiles.CoolGemsparkBlock.B), 0f, origin, scale, SpriteEffects.None, 0f);
        return false;
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        spriteBatch.Draw(Terraria.GameContent.TextureAssets.Item[Item.type].Value, Item.position - Main.screenPosition, null, new Color(Tiles.CoolGemsparkBlock.R, Tiles.CoolGemsparkBlock.G, Tiles.CoolGemsparkBlock.B), 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
    }
}
