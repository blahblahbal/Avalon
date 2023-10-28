using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Avalon.Common.Players;
using Terraria.Localization;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Head)]
internal class AeroforceGuardia : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 8;
        Item.rare = ModContent.RarityType<CrabbyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 15)
            .AddIngredient(ItemID.Feather, 16)
            .AddIngredient(ItemID.SoulofFlight, 10)
            .AddIngredient(ModContent.ItemType<Material.Bars.CaesiumBar>(), 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<AeroforceProtector>() && legs.type == ModContent.ItemType<AeroforceLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Aeroforce");
        player.GetModPlayer<AvalonPlayer>().SkyBlessing = true;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.08f;
        player.maxMinions++;
    }
}
