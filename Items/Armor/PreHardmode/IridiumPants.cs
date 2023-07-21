using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
class IridiumPants : ModItem
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
        Item.defense = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 0, 9, 75);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.11f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 17)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 5)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
