using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
class NickelGreaves : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 20).AddTile(TileID.Anvils).Register();
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 3;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 10);
        Item.height = dims.Height;
    }
}
