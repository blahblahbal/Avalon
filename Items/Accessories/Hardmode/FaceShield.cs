using Avalon.Items.Accessories.PreHardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Face)]
class FaceShield : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 32;
        Item.rare = ItemRarityID.LightRed;
        Item.accessory = true;
        Item.value = 100000;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Windshield>())
            .AddIngredient(ModContent.ItemType<SurgicalMask>())
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.buffImmune[BuffID.WindPushed] = true;
		player.buffImmune[ModContent.BuffType<Buffs.Debuffs.Pathogen>()] = true;
    }
}
