using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class AxeofEmancipation : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 38;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.axe = 40;
        Item.rare = ItemRarityID.Cyan;
        Item.width = dims.Width;
        Item.useTime = 18;
        Item.knockBack = 7f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 8);
        Item.useAnimation = 18;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe()
    //        .AddIngredient(ItemID.HallowedBar, 10)
    //        .AddIngredient(ItemID.LunarBar, 10)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofPlight>(), 15)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
