using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Avalon.Items.Tomes.PreHardmode;
using Avalon.Items.Material.TomeMats;

namespace Avalon.Items.Tomes.Hardmode;

public class LoveUpandDown : ModItem
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
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = 4;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Ranged) += 0.15f;
        player.GetDamage(DamageClass.Melee) += 0.15f;
        player.GetDamage(DamageClass.Magic) += 0.15f;
        player.GetDamage(DamageClass.Summon) += 0.15f;
        player.GetDamage(DamageClass.Throwing) += 0.15f;
        player.GetCritChance(DamageClass.Melee) += 7;
        player.GetCritChance(DamageClass.Magic) += 7;
        player.GetCritChance(DamageClass.Ranged) += 7;
        player.GetCritChance(DamageClass.Throwing) += 7;
        player.manaCost -= 0.25f;
        player.statDefense += 12;
        player.statLifeMax2 += 80;
        player.statManaMax2 += 80;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<DragonOrb>())
            .AddIngredient(ModContent.ItemType<AdventuresandMishaps>())
            .AddIngredient(ModContent.ItemType<ScrollofTome>(), 2)
            .AddIngredient(ModContent.ItemType<FineLumber>(), 15)
            .AddIngredient(ModContent.ItemType<Gravel>(), 15)
            .AddIngredient(ModContent.ItemType<Sandstone>(), 15)
            .AddIngredient(ModContent.ItemType<CarbonSteel>(), 15)
            .AddIngredient(ModContent.ItemType<MysteriousPage>(), 4)
            .AddTile(ModContent.TileType<Tiles.TomeForge>())
            .Register();
    }
}
