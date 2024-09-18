using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

class PoisonSpike : ModItem
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
        Item.createTile = ModContent.TileType<Tiles.PoisonSpike>();
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.notAmmo = true;
        Item.ammo = ItemID.Spike;
    }
    public override void AddRecipes()
    {
        CreateRecipe(20).AddIngredient(ItemID.Spike, 20).AddIngredient(ItemID.Stinger).AddTile(TileID.Anvils).Register();
        CreateRecipe(20).AddIngredient(ItemID.Spike, 20).AddIngredient(ModContent.ItemType<Material.MosquitoProboscis>()).AddTile(TileID.Anvils).Register();
    }
}
