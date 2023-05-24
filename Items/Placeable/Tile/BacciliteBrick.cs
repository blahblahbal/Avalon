using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class BacciliteBrick : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.DemoniteBrick);
        Item.createTile = ModContent.TileType<Tiles.BacciliteBrick>();
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type,5)
            .AddIngredient(ModContent.ItemType<Material.Ores.BacciliteOre>())
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(),5)
            .AddTile(TileID.Furnaces)
            .Register();
    }
}
