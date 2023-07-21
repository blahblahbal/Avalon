using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
class BronzeChainmail : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Torso;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BronzeBar>(), 20).AddTile(TileID.Anvils).Register();
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 2;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 6);
        Item.height = dims.Height;
    }
}
