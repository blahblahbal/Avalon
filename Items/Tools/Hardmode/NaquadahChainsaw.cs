using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class NaquadahChainsaw : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 46;
        Item.damage = 29;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.axe = 18;
        Item.shootSpeed = 40f;
        Item.rare = ItemRarityID.LightRed;
        Item.noMelee = true;
        Item.useTime = 6;
        Item.knockBack = 4.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.NaquadahChainsaw>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 102500;
        Item.useAnimation = 25;
        Item.UseSound = SoundID.Item23;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
