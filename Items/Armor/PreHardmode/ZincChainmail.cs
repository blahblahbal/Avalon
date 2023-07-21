using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
class ZincChainmail : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 25).AddTile(TileID.Anvils).Register();
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 5;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 30);
        Item.height = dims.Height;
    }
}
