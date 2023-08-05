using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
class NaquadahShinguards : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("6% increased movement speed");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 12;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 2, 30);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 15)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.06f;
    }
}
