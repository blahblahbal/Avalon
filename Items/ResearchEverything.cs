using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items; 

public class ResearchEverything : ModItem
{
    public override void AddRecipes()
    {
        CreateRecipe().Register();
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.IronAxe);
    }
    public override bool? UseItem(Player player)
    {
        for (int i = 1; i < ItemLoader.ItemCount; i++)
        {
            CreativeUI.ResearchItem(i);
        }
        return base.UseItem(player);
    }
    //public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    //{
    //    int p = Projectile.NewProjectile(source,position,velocity,type,damage,knockback,player.whoAmI);
    //    Main.projectile[p].hostile = false;
    //    Main.projectile[p].friendly = true;
    //    return false;
    //}
}
