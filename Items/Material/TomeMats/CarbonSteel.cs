using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material.TomeMats;

class CarbonSteel : ModItem
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
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.maxStack = 9999;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(10)
            .AddIngredient(ItemID.IronOre, 30)
            .AddTile(TileID.Hellforge)
            .Register();

        CreateRecipe(10)
            .AddIngredient(ItemID.LeadOre, 30)
            .AddTile(TileID.Hellforge)
            .Register();

        CreateRecipe(10)
            .AddIngredient(ModContent.ItemType<Ores.NickelOre>(), 30)
            .AddTile(TileID.Hellforge)
            .Register();
    }
}
