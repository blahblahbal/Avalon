using Avalon.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Hardmode;

public class SunsShadow : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item63; // change this
        Item.autoReuse = true;
        Item.damage = 27;
        Item.scale = 1f;
        Item.shootSpeed = 4.5f;
        Item.useAmmo = AmmoID.Dart;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 40;
        Item.knockBack = 3.5f;
        Item.shoot = ProjectileID.Seed;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 1800;
        Item.useAnimation = 40;
        Item.height = dims.Height;
        Item.rare = ItemRarityID.Green;
    }
    //public override void UseItemFrame(Player player)
    //{
    //    player.bodyFrame.Y = player.bodyFrame.Height * 2;
    //}
    //public override Vector2? HoldoutOffset()
    //{
    //    return new Vector2(0, -7);
    //}
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(4, -2);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile P = Projectile.NewProjectileDirect(source, position - new Vector2(0, 4), velocity, ModContent.ProjectileType<SunsShadowProj>(), damage, knockback);
        P.ai[0] = type;
        return false;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.BeetleHusk, 8)
            .AddIngredient(ItemID.TurtleShell, 1)
            .AddIngredient(ItemID.ChlorophyteBar, 3)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
