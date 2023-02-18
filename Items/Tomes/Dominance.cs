using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Tomes;

class Dominance : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.value = 250000;
        Item.height = dims.Height;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Magic) += 0.22f;
        player.GetDamage(DamageClass.Summon) += 0.22f;
        player.GetDamage(DamageClass.Melee) += 0.22f;
        player.GetDamage(DamageClass.Ranged) += 0.22f;
        player.GetDamage(DamageClass.Throwing) += 0.22f;
        player.GetCritChance(DamageClass.Melee) += 8;
        player.GetCritChance(DamageClass.Magic) += 8;
        player.GetCritChance(DamageClass.Ranged) += 8;
        player.GetCritChance(DamageClass.Throwing) += 8;
        player.manaCost -= 0.1f;
        player.statDefense += 11;
        player.statLifeMax2 += 80;
        player.statManaMax2 += 140;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<TheOasisRemembered>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<TheOasisRemembered>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<SceneofCarnage>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<SceneofCarnage>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<ThePlumHarvest>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<ThePlumHarvest>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<ChantoftheWaterDragon>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<ChantoftheWaterDragon>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<CreatorsTome>()).AddIngredient(ModContent.ItemType<TheThreeScholars>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<LoveUpandDown>()).AddIngredient(ModContent.ItemType<TheThreeScholars>()).AddIngredient(ModContent.ItemType<DragonOrb>(), 2).AddIngredient(ModContent.ItemType<Onyx>(), 50).AddTile(ModContent.TileType<Tiles.TomeForge>()).Register();
    //}
}
