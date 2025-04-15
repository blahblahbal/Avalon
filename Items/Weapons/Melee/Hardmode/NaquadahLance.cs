using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class NaquadahLance : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.Spears[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 38;
        Item.UseSound = SoundID.Item1;
        Item.damage = 47;
        Item.noUseGraphic = true;
        Item.scale = 1.1f;
        Item.shootSpeed = 5f;
        Item.rare = ItemRarityID.LightRed;
        Item.noMelee = true;
        Item.useTime = 26;
        Item.knockBack = 5.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.NaquadahLance>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 86000;
        Item.useAnimation = 26;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
