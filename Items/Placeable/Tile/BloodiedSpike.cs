using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class BloodiedSpike : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.BloodiedSpike>();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 1, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.notAmmo = true;
        Item.ammo = ItemID.Spike;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.Spike).AddIngredient(ItemID.TissueSample).AddTile(TileID.Anvils).Register();
    }
}
