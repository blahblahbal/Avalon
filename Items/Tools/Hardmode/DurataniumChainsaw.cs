using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class DurataniumChainsaw : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item23;
        Item.damage = 25;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.axe = 15;
        Item.shootSpeed = 40f;
        Item.rare = ItemRarityID.LightRed;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 6;
        Item.knockBack = 3.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.DurataniumChainsaw>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 60000;
        Item.useAnimation = 25;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
