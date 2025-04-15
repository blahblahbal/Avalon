using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Avalon.Common.Players;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class ToxinStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 46;
        Item.reuseDelay = 14;
        Item.autoReuse = true;
        Item.scale = 1f;
        Item.shootSpeed = 14f;
        Item.mana = 26;
        Item.rare = ItemRarityID.Yellow;
        Item.UseSound = SoundID.Item43;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 0f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.ToxicFang>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 505000;
        Item.useAnimation = 20;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.PoisonStaff)
            .AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 14)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        position = player.Center + new Vector2(65, 0).RotatedBy(player.AngleTo(player.GetModPlayer<AvalonPlayer>().MousePosition));
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int x = ModContent.ProjectileType<Projectiles.Magic.ToxicFang>();
        int num157 = 4;
        if (Main.rand.NextBool(3))
        {
            num157++;
        }
        if (Main.rand.NextBool(4))
        {
            num157++;
        }
        if (Main.rand.NextBool(5))
        {
            num157++;
        }
        for (int spread = 0; spread < num157; spread++)
        {
            float xVel = velocity.X;
            float yVel = velocity.Y;
            float num161 = 0.04f * spread;
            xVel += Main.rand.Next(-35, 36) * num161;
            yVel += Main.rand.Next(-35, 36) * num161;
            int dmg = Item.damage;
            Projectile.NewProjectile(source, position.X, position.Y, xVel, yVel, x, (int)player.GetDamage(DamageClass.Magic).ApplyTo(dmg), knockback, player.whoAmI, 0f, 0f);
        }
        return false;
    }
}
