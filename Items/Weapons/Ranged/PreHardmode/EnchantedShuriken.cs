using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Ranged.PreHardmode;

class EnchantedShuriken : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Enchanted Shuriken");
        //SacrificeTotal = 99;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 13;
        Item.noUseGraphic = true;
        Item.maxStack = Item.CommonMaxStack;
        Item.shootSpeed = 10.5f;
        Item.DamageType = DamageClass.Ranged;
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.EnchantedShuriken>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 30;
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    float real;
    public override bool GrabStyle(Player player)
    {
        real += 0.3f;
        //if (Main.rand.NextBool(3))
        //{
        //    Dust d = Dust.NewDustPerfect(Main.rand.NextVector2FromRectangle(Item.Hitbox), DustID.MagicMirror);
        //    d.velocity *= 0.1f;
        //}
        Item.velocity = new Vector2(real, 0).RotatedBy(Item.position.AngleTo(player.Center));
        return true;
    }
    public override void GrabRange(Player player, ref int grabRange)
    {
        if (Item.useLimitPerAnimation != null && Item.useLimitPerAnimation == player.whoAmI)
        {
            grabRange += (int)(300 * (1 + real));
        }
    }

    public override bool OnPickup(Player player)
    {
        Item.useLimitPerAnimation = 1;
        return base.OnPickup(player);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type, 25)
            .AddIngredient(ModContent.ItemType<EnchantedBar>())
            .AddTile(TileID.Anvils).Register();
    }
}
