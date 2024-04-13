using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Tomes.PreHardmode;
using Avalon.Items.Material.TomeMats;

namespace Avalon.Items.Tomes.Hardmode;

class CreatorsTome : ModItem
{
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Tomes;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.value = 150000;
        Item.height = dims.Height;
        Item.defense = 10;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 4;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Generic) += 0.2f;
        player.GetCritChance(DamageClass.Generic) += 5;
        player.manaCost -= 0.2f;
        player.statLifeMax2 += 100;
        player.statManaMax2 += 100;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<DragonOrb>())
            .AddIngredient(ModContent.ItemType<TheVoidlands>())
            .AddIngredient(ModContent.ItemType<ScrollofTome>(), 2)
            .AddIngredient(ModContent.ItemType<FineLumber>(), 15)
            .AddIngredient(ModContent.ItemType<Gravel>(), 15)
            .AddIngredient(ModContent.ItemType<Sandstone>(), 15)
            .AddIngredient(ModContent.ItemType<CarbonSteel>(), 15)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
