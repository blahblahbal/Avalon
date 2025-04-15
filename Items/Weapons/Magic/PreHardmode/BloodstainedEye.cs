using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class BloodstainedEye : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 34;
        Item.scale = 1.1f;
        Item.DamageType = DamageClass.Magic;
        Item.autoReuse = true;
        Item.noMelee = true;
        Item.useTurn = false;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.knockBack = 2f;
        Item.mana = 3;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BloodyTear>();
        Item.shootSpeed = 14f;
        Item.UseSound = SoundID.NPCHit1;
        Item.value = Item.sellPrice(0, 0, 60, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int numberProjectiles = 1 + Main.rand.Next(2);
        for (int i = 0; i < numberProjectiles; i++)
        {
            Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(8));
            float scale = 1f - (Main.rand.NextFloat() * .3f);
            perturbedSpeed = perturbedSpeed * scale;
            Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
        }
        return false;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<GlassEye>()).AddIngredient(ModContent.ItemType<BloodshotLens>()).AddIngredient(ModContent.ItemType<BottledLava>()).AddTile(TileID.Anvils).Register();
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(5, 0);
    }
}
