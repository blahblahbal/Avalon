using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Material.TomeMats;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Tomes;

class TheVoidlands : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightPurple;
        Item.width = dims.Width;
        Item.value = 105000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Magic) += 0.15f;
        player.GetDamage(DamageClass.Summon) += 0.15f;
        player.GetDamage(DamageClass.Melee) += 0.15f;
        player.GetDamage(DamageClass.Ranged) += 0.15f;
        player.GetDamage(DamageClass.Throwing) += 0.15f;
        player.GetCritChance(DamageClass.Melee) += 3;
        player.GetCritChance(DamageClass.Magic) += 3;
        player.GetCritChance(DamageClass.Ranged) += 3;
        player.GetCritChance(DamageClass.Throwing) += 3;
        player.statLifeMax2 += 60;
        player.statManaMax2 += 40;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BurningDesire>()).AddIngredient(ModContent.ItemType<SoutheasternPeacock>()).AddIngredient(ModContent.ItemType<FlankersTome>()).AddIngredient(ModContent.ItemType<TomeofDistance>()).AddIngredient(ModContent.ItemType<EternitysMoon>()).AddIngredient(ModContent.ItemType<MediationsFlame>()).AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BurningDesire>()).AddIngredient(ModContent.ItemType<SoutheasternPeacock>()).AddIngredient(ModContent.ItemType<FlankersTome>()).AddIngredient(ModContent.ItemType<TomeofDistance>()).AddIngredient(ModContent.ItemType<TomeoftheRiverSpirits>()).AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BurningDesire>()).AddIngredient(ModContent.ItemType<SoutheasternPeacock>()).AddIngredient(ModContent.ItemType<TaleoftheDolt>()).AddIngredient(ModContent.ItemType<TaleoftheRedLotus>()).AddIngredient(ModContent.ItemType<EternitysMoon>()).AddIngredient(ModContent.ItemType<MediationsFlame>()).AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
        CreateRecipe(1).AddIngredient(ModContent.ItemType<BurningDesire>()).AddIngredient(ModContent.ItemType<SoutheasternPeacock>()).AddIngredient(ModContent.ItemType<TaleoftheDolt>()).AddIngredient(ModContent.ItemType<TaleoftheRedLotus>()).AddIngredient(ModContent.ItemType<TomeoftheRiverSpirits>()).AddIngredient(ModContent.ItemType<MysticalTomePage>(), 3).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    }
}
