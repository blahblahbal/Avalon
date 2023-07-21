using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Legs)]
class CaesiumGreaves : ModItem
{
    //public override void SetStaticDefaults()
    //{
    //    DisplayName.SetDefault("Caesium Greaves");
    //    Tooltip.SetDefault("15% increased melee and movement speed");
    //    SacrificeTotal = 1;
    //}

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 21;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 8, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<CaesiumBar>(), 28)
            .AddIngredient(ItemID.HellstoneBar, 9)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
        player.moveSpeed += 0.15f;
    }
}
