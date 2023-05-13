using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class TheCell : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 18;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.scale = 1f;
        Item.shootSpeed = 12f;
        Item.rare = ItemRarityID.Blue;
        Item.noMelee = true;
        Item.width = 30;
        Item.height = 10;
        Item.useTime = 45;
        Item.knockBack = 6.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Cell>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 27000;
        Item.useAnimation = 45;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10).AddIngredient(ModContent.ItemType<Material.Booger>(), 2).AddTile(TileID.Anvils).Register();
    }
}
