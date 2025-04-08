using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class Moonfury : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
    }
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 10;
        Item.UseSound = SoundID.Item1;
        Item.damage = 35;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.scale = 1.1f;
        Item.shootSpeed = 12f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 42;
        Item.knockBack = 6.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Moonfury>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.useAnimation = 42;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.BlueMoon)
            .AddIngredient(ItemID.Sunfury)
            .AddIngredient(ItemID.BallOHurt)
            .AddIngredient(ModContent.ItemType<Sporalash>())
            .AddTile(TileID.DemonAltar).Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.BlueMoon)
            .AddIngredient(ItemID.Sunfury)
            .AddIngredient(ItemID.TheMeatball)
            .AddIngredient(ModContent.ItemType<Sporalash>())
            .AddTile(TileID.DemonAltar).Register();

        Terraria.Recipe.Create(Type)
            .AddIngredient(ItemID.BlueMoon)
            .AddIngredient(ItemID.Sunfury)
            .AddIngredient(ModContent.ItemType<TheCell>())
            .AddIngredient(ModContent.ItemType<Sporalash>())
            .AddTile(TileID.DemonAltar).Register();
    }
}
