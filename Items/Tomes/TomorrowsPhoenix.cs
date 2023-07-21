using Avalon.Common;
using Avalon.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes;

class TomorrowsPhoenix : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Tomes;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Summon) += 0.08f;
        player.GetKnockback(DamageClass.Summon) += 0.05f;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Gel, 100)
            .AddIngredient(ModContent.ItemType<StrongVenom>(), 5)
            .AddIngredient(ItemID.FallenStar, 20)
            .AddIngredient(ModContent.ItemType<MysticalClaw>(), 4)
            .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 2)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
