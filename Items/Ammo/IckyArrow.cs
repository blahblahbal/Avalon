using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class IckyArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 8;
        Item.shootSpeed = 3.4f;
        Item.ammo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.knockBack = 2f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.IckyArrow>();
        Item.value = Item.sellPrice(0, 0, 0, 8);
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(5)
            .AddIngredient(ItemID.WoodenArrow, 5)
            .AddIngredient(ModContent.ItemType<YuckyBit>())
            .AddTile(TileID.Anvils)
            .Register();
    }
}
