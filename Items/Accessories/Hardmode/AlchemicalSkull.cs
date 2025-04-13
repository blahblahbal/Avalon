using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode;

[AutoloadEquip(EquipType.Face)]
public class AlchemicalSkull : ModItem
{
    public override void SetStaticDefaults()
    {
        ArmorIDs.Face.Sets.PreventHairDraw[Item.faceSlot] = true;
    }
    public override void SetDefaults()
    {
        Item.defense = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.width = 22;
        Item.accessory = true;
        Item.value = 150000;
        Item.height = 26;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.ObsidianShield)
            .AddIngredient(ItemID.IronskinPotion, 10)
            .AddIngredient(ItemID.BattlePotion, 15)
            .AddIngredient(ItemID.ThornsPotion, 15)
            .AddIngredient(ItemID.WaterWalkingPotion, 10)
            .AddIngredient(ItemID.Bone, 99)
            .AddTile(TileID.Hellforge)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.waterWalk = player.waterWalk2 = player.enemySpawns = player.noKnockback = player.fireWalk = true;
        player.thorns = 1f;
    }
}
