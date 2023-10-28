using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Legs)]
class AeroforceLeggings : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 10;
        Item.rare = ModContent.RarityType<Rarities.CrabbyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 0.2f;
        player.GetAttackSpeed(DamageClass.Summon) += 0.04f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 20)
            .AddIngredient(ItemID.Feather, 20)
            .AddIngredient(ItemID.SoulofFlight, 14)
            .AddIngredient(ModContent.ItemType<Material.Bars.CaesiumBar>(), 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
