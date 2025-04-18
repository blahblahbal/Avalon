using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode;

public class NaquadahRepeater : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 41;
        Item.autoReuse = true;
        Item.useAmmo = AmmoID.Arrow;
        Item.shootSpeed = 10.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 2.05f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 115000;
        Item.useAnimation = 20;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item5;
    }

    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-6,0);
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
