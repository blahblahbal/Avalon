using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class CoughwoodSword : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 11;
        Item.width = 24;
        Item.height = 28;
        Item.useTurn = true;
        Item.useTime = 18;
        Item.scale = 1f;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 0, 20);
        Item.useAnimation = 18;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Coughwood>(), 7)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
}
