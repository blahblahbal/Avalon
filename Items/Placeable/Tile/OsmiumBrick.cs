using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile; 

public class OsmiumBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.OsmiumBrick>();
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.Size = new Vector2(16);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Ores.OsmiumOre>()).AddIngredient(ItemID.StoneBlock).AddTile(TileID.Furnaces).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Walls.OsmiumBrickWallItem>(), 4).AddTile(TileID.WorkBenches).Register();
    }
}