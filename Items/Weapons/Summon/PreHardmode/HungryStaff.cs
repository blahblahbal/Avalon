using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Summon.PreHardmode;

class HungryStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Summon;
        Item.damage = 27;
        Item.shootSpeed = 14f;
        Item.mana = 15;
        Item.noMelee = true;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.knockBack = 5.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Summon.HungrySummon>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 30;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item44;
    }
    // public override void AddRecipes()
    // {
        // CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 14).AddTile(TileID.Anvils).Register();
    // }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float posX = (float)Main.mouseX + Main.screenPosition.X;
        float posY = (float)Main.mouseY + Main.screenPosition.Y;
        int num227 = Projectile.NewProjectile(source, posX, posY, 0f, 0f, type, damage, knockback, player.whoAmI, 0f, 0f);
        // if (player.Avalon().FleshArmor)
        // {
            // Main.projectile[num227].minionSlots = 0.25f;
        // }

        return false;
    }
}
