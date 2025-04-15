using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

public class PathogenicArrow : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CursedArrow);
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.PathogenicArrow>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(150).AddIngredient(ItemID.WoodenArrow, 150).AddIngredient(ModContent.ItemType<Pathogen>()).AddTile(TileID.MythrilAnvil).Register();
    }
}
