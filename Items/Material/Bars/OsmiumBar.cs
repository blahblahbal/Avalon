using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.Bars;

public class OsmiumBar : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 25;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Material;
    }
    public override void SetDefaults()
    {
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.PlacedBars>();
        Item.placeStyle = 4;
        Item.rare = ItemRarityID.Orange;

        Item.useTime = 10;
        Item.value = Item.sellPrice(0, 0, 36, 0);
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.Size = new Vector2(24);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Ores.OsmiumOre>(), 4).AddTile(TileID.Furnaces).Register();
    }
}
