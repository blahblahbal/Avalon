using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.Material.OreChunks;

class HydrolythChunk : ModItem
{
    // remove after this is added
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
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
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
    //        .AddIngredient(Type, 5)
    //        .AddIngredient(ModContent.ItemType<FeroziumChunk>())
    //        .AddIngredient(ModContent.ItemType<Ore.SolariumOre>())
    //        .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
    //        .Register();

    //    Recipe.Create(ModContent.ItemType<Placeable.Bar.HydrolythBar>())
    //        .AddIngredient(Type, 5)
    //        .AddIngredient(ModContent.ItemType<Ore.FeroziumOre>())
    //        .AddIngredient(ModContent.ItemType<Ore.SolariumOre>())
    //        .AddTile(ModContent.TileType<Tiles.CaesiumForge>())
    //        .Register();
    //}
}
