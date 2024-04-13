using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.PreHardmode;

class ChristmasTome : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Tomes;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.value = 15000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 1;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetCritChance(DamageClass.Generic) += 3;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<MysticalClaw>(), 3)
            .AddIngredient(ModContent.ItemType<Sandstone>(), 5)
            .AddIngredient(ModContent.ItemType<DewOrb>(), 3)
            .AddIngredient(ModContent.ItemType<MysticalTomePage>())
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
