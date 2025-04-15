using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.PreHardmode;

public class Wheezebow : ModItem
{
    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item5;
        Item.damage = 13;
        Item.scale = 1.1f;
        Item.shootSpeed = 7.5f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.rare = ItemRarityID.Blue;
        Item.width = 12;
        Item.height = 28;
        Item.useTime = 21;
        Item.knockBack = 0f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 0, 36, 0);
        Item.useAnimation = 21;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(0, 0);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 9).AddTile(TileID.Anvils).Register();
    }
}
