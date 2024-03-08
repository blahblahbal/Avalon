using Avalon.Items.Weapons.Melee.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

class Clarent : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.CatchingTool[Item.type] = true;
        ItemID.Sets.LavaproofCatchingTool[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.Excalibur);
        Item.useTurn = true;
        Item.shoot = 0;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<GoldBarbedNet>())
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<PlatinumBarbedNet>())
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<BismuthBarbedNet>())
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
