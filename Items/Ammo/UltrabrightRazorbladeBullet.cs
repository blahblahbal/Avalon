using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class UltrabrightRazorbladeBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 500;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.shootSpeed = 10f;
        Item.damage = 17;
        Item.ammo = AmmoID.Bullet;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.width = dims.Width;
        Item.knockBack = 3.5f;
        Item.rare = ItemRarityID.Cyan;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.UltrabrightRazorbladeBullet>();
        Item.maxStack = 9999;
        Item.value = Item.sellPrice(0, 0, 2);
        Item.height = dims.Height;
    }

    public override void AddRecipes()
    {
        CreateRecipe(1000)
            .AddIngredient(ItemID.MusketBall, 1000)
            .AddIngredient(ItemID.UltrabrightTorch, 250)
            .AddIngredient(ItemID.RazorbladeTyphoon)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
