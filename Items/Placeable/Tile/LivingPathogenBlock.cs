using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Tile;

public class LivingPathogenBlock : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
		Item.CloneDefaults(ItemID.LivingFireBlock);
        Item.createTile = ModContent.TileType<Tiles.LivingPathogen>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(20)
            .AddIngredient(ItemID.LivingFireBlock, 20)
            .AddIngredient(ModContent.ItemType<Material.Pathogen>())
            .AddTile(TileID.CrystalBall)
            .Register();
    }
}
