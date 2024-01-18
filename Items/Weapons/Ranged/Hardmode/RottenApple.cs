using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode;

class RottenApple : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 20;
        Item.noUseGraphic = true;
        Item.maxStack = 9999;
        Item.shootSpeed = 9f;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.knockBack = 3f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.RottenApple>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 0;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(20)
            .AddIngredient(ItemID.Apple)
            .AddIngredient(ModContent.ItemType<Material.Shards.UndeadShard>(), 2)
            .AddTile(TileID.WorkBenches)
            .Register();

        CreateRecipe(20)
            .AddIngredient(ItemID.Apple)
            .AddIngredient(ModContent.ItemType<Material.RottenFlesh>(), 2)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
