using Avalon.Items.Consumables;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Material;

public class BlankScroll : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = new SoundStyle("Avalon/Sounds/Item/Scroll");
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Leather, 2)
            .AddIngredient(ModContent.ItemType<StaminaCrystal>())
            .AddTile(TileID.Loom)
            .Register();
    }
}
