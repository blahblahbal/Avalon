using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
class ViruthornScalemail : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Torso;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ItemRarityID.Blue;
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(0, 0, 54, 0);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 25).AddIngredient(ModContent.ItemType<Material.Booger>(), 15).AddTile(TileID.Anvils).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetAttackSpeed(DamageClass.Melee) += 0.07f;
    }
}
