using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Ranged.PreHardmode;

class RhodiumLongbow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 32;
        Item.UseSound = SoundID.Item5;
        Item.damage = 21;
        Item.scale = 1f;
        Item.shootSpeed = 9f;
        Item.useAmmo = AmmoID.Arrow;
        Item.DamageType = DamageClass.Ranged;
        Item.noMelee = true;
        Item.useTime = 18;
        Item.knockBack = 1.3f;
        Item.shoot = ProjectileID.WoodenArrowFriendly;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.rare = ItemRarityID.Orange;
        Item.value = Item.sellPrice(0, 0, 50);
        Item.useAnimation = 18;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 13)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
            .AddTile(TileID.Anvils).Register();
    }
}
