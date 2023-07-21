using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Legs)]
class ViruthornGreaves : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 6;
        Item.rare = ItemRarityID.Blue;
        Item.width = 18;
        Item.height = 18;
        Item.value = Item.sellPrice(0, 0, 54);
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 20)
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Generic) += 0.03f;
    }
}
