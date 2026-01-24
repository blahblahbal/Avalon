using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class PathogenicBullet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CursedBullet);
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.Ammo.PathogenicBullet>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(150).AddIngredient(ItemID.MusketBall, 150).AddIngredient(ModContent.ItemType<Pathogen>()).AddTile(TileID.MythrilAnvil).Register();
    }
}
