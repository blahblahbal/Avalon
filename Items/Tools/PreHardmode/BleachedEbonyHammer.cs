using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class BleachedEbonyHammer : ModItem
{
    public override void SetDefaults()
    {
            Item.CloneDefaults(ItemID.RichMahoganyHammer);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 8)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
