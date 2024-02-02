using Avalon.Items.Material.Ores;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

class SulphurCrystal : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 150;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Gems;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(silver: 4);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(25)
            .AddIngredient(ModContent.ItemType<Sulphur>(), 50)
            .AddIngredient(ModContent.ItemType<CoreShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
