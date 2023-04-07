using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items; 

public class SpawnTool : ModItem
{
    public override void AddRecipes()
    {
        CreateRecipe().Register();
    }
    public override void SetDefaults()
    {
        Item.useStyle = 1;
        Item.DefaultToBow(17, 17);
        Item.useAmmo = AmmoID.None;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, NPCID.Merchant);
        return false;
    }
}
