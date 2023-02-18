using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Material.TomeMats;

namespace ExxoAvalonOrigins.Items.Tomes;

class ThePlumHarvest : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 150000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    //Update Accs not needed - ammo done in ModPlayer

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1)
    //        .AddIngredient(ModContent.ItemType<DragonOrb>())
    //        .AddIngredient(ModContent.ItemType<Opal>(), 50)
    //        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
    //        .AddIngredient(ItemID.ShroomiteBar, 12)
    //        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 5)
    //        .AddTile(ModContent.TileType<Tiles.TomeForge>())
    //        .Register();
    //}
}
