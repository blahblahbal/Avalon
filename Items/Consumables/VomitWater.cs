using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class VomitWater : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.shootSpeed = 9f;
        Item.rare = ItemRarityID.Orange;
        Item.damage = 20;
        Item.shoot = ModContent.ProjectileType<Projectiles.VomitWater>();
        Item.width = 18;
        Item.height = 20;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.knockBack = 3f;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.value = 100;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BottledWater, 10)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.SnotsandBlock>())
            .AddIngredient(ModContent.ItemType<Placeable.Seed.ContagionSeeds>())
            .Register();
    }
}
