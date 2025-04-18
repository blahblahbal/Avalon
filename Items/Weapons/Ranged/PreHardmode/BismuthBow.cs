using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class BismuthBow : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 12;
        Item.height = 28;
        Item.UseSound = SoundID.Item5;
        Item.damage = 12;
        Item.scale = 1f;
        Item.shootSpeed = 6.8f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.useTime = 24;
        Item.knockBack = 0f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 9000;
        Item.useAnimation = 24;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 7).AddTile(TileID.Anvils).Register();
    }
}
