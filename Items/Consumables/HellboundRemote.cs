using Avalon.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

public class HellboundRemote : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.BlueRarity>();
        Item.width = dims.Width;
        Item.maxStack = 1;
        Item.value = 0;
        Item.height = dims.Height;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ItemID.BeetleHusk).AddIngredient(ItemID.LunarBar, 10).AddIngredient(ModContent.ItemType<GhostintheMachine>()).AddIngredient(ItemID.GuideVoodooDoll).AddIngredient(ModContent.ItemType<FleshyTendril>(), 5).AddTile(ModContent.TileType<Tiles.HallowedAltar>()).Register();
    //}
}
