using Avalon.Items.Accessories.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Face)]
class SpaceHelmet : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 24;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
        Item.value = 100000;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<OxygenTank>())
            .AddIngredient(ModContent.ItemType<Vortex>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.OnFire] = true;
		player.buffImmune[BuffID.Suffocation] = true;
    }
}
