using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class FeroziumChunk : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Lime;
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(ModContent.ItemType<Placeable.Bar.FeroziumBar>())
    //        .AddIngredient(Type, 5)
    //        .AddTile(TileID.AdamantiteForge)
    //        .Register();

    //    Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
    //        .AddIngredient(ModContent.ItemType<HydrolythChunk>(), 5)
    //        .AddIngredient(Type)
    //        .AddIngredient(ModContent.ItemType<Ore.SolariumOre>())
    //        .AddTile(TileID.AdamantiteForge)
    //        .Register();
    //}
}
