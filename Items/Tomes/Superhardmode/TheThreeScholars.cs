using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tomes.Superhardmode;

class TheThreeScholars : ModItem
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
        Item.rare = ItemRarityID.Yellow;
        Item.width = dims.Width;
        Item.value = 150000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 5;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.statDefense += 20;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1)
    //        .AddIngredient(ModContent.ItemType<DragonOrb>())
    //        .AddIngredient(ModContent.ItemType<UnvolanditeBar>(), 25)
    //        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
    //        .AddIngredient(ItemID.IronskinPotion, 10)
    //        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
    //        .AddTile(ModContent.TileType<Tiles.TomeForge>())
    //        .Register();

    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<DragonOrb>())
    //        .AddIngredient(ModContent.ItemType<VorazylcumBar>(), 25)
    //        .AddIngredient(ModContent.ItemType<SoulofBlight>(), 10)
    //        .AddIngredient(ItemID.IronskinPotion, 10)
    //        .AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3)
    //        .AddTile(ModContent.TileType<Tiles.TomeForge>())
    //        .Register();
    //}
}
