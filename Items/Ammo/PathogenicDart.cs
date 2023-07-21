using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

class PathogenicDart : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.CursedDart);
        Item.shoot = ModContent.ProjectileType<Projectiles.Ranged.PathogenicDart>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(100).AddIngredient(ModContent.ItemType<Pathogen>()).Register();
    }
}
