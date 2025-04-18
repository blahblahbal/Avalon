using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class SepsisBlock : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.SepsisBlock>();
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
        Terraria.Recipe.Create(Type, 1)
            .AddIngredient(ModContent.ItemType<ChunkstoneBlock>(), 2)
            .AddTile(TileID.MeatGrinder).Register();

        //Terraria.Recipe.Create(Type, 1)
        //    .AddIngredient(ModContent.ItemType<SepsisPlatform>(), 2)
        //    .AddTile(TileID.MeatGrinder).Register();
    }
}
