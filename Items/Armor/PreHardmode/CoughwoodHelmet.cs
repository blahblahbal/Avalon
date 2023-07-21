using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
class CoughwoodHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Headgear;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 2;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<CoughwoodBreastplate>() && legs.type == ModContent.ItemType<CoughwoodGreaves>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.Coughwood>(), 20)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "1 defense";
        player.statDefense++;
    }
}
