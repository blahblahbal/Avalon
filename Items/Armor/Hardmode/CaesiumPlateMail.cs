using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
class CaesiumPlateMail : ModItem
{
    //public override void SetStaticDefaults()
    //{
    //    DisplayName.SetDefault("Caesium Plate Mail");
    //    Tooltip.SetDefault("5% increased melee critical strike chance\nMelee attacks inflict On Fire!");
    //    SacrificeTotal = 1;
    //}

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 25;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 9, 0, 0);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<CaesiumBar>(), 40)
            .AddIngredient(ItemID.HellstoneBar, 12)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddTile(TileID.MythrilAnvil).Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetCritChance(DamageClass.Melee) += 5;
        player.magmaStone = true;
    }
}
