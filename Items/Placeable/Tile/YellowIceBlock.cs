using Avalon.Items.Placeable.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class YellowIceBlock : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.Contagion.YellowIce>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(ItemID.IceTorch, 3)
            .AddIngredient(ItemID.Torch, 3)
            .AddIngredient(this)
            .Register();

        Recipe.Create(ModContent.ItemType<ContagionTorch>(), 3)
            .AddIngredient(ItemID.Torch, 3)
            .AddIngredient(this)
            .Register();
    }
}
