using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
class BleachedEbonyHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.defense = 1;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<BleachedEbonyBreastplate>() && legs.type == ModContent.ItemType<BleachedEbonyGreaves>();
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Placeable.Tile.BleachedEbony>(), 20)
            .AddTile(TileID.WorkBenches)
            .Register();
    }
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "1 defense";
        player.statDefense++;
    }
}
