using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Other;

class ImmortalityPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
        {
            Color.Red,
            Color.Salmon
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ModContent.ItemType<Material.Herbs.Sweetstem>())
            .AddIngredient(ItemID.Blinkroot)
            .AddIngredient(ItemID.SpecularFish)
            .AddTile(TileID.Bottles)
            .Register();
    }
}
