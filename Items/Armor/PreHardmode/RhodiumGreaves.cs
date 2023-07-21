using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
class RhodiumGreaves : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Pants;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 7;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 17).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5).AddTile(TileID.Anvils).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.14f;
    }
}
