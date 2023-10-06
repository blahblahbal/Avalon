using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.Hardmode;

class GastropodStaff : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Summon;
        Item.damage = 40;
        Item.shootSpeed = 14f;
        Item.mana = 13;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.GastrominiSummon>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 30;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item44;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.HallowedBar, 14)
            .AddIngredient(ItemID.Gel, 100)
            .AddIngredient(ItemID.SoulofLight, 20)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float posX = (float)Main.mouseX + Main.screenPosition.X;
        float posY = (float)Main.mouseY + Main.screenPosition.Y;
        Projectile.NewProjectile(source, posX, posY, 0f, 0f, type, damage, knockback, player.whoAmI, 0f, 0f);
        return false;
    }
}
