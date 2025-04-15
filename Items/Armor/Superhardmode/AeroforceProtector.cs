using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Superhardmode;

[AutoloadEquip(EquipType.Body)]
public class AeroforceProtector : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 12;
        Item.rare = ModContent.RarityType<Rarities.CrabbyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Summon) += 0.1f;
        player.maxMinions++;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.HallowedBar, 30)
            .AddIngredient(ItemID.Feather, 24)
            .AddIngredient(ItemID.SoulofFlight, 16)
            .AddIngredient(ModContent.ItemType<Material.Bars.CaesiumBar>(), 5)
            .AddTile(TileID.MythrilAnvil) // update to shm anvil later
            .Register();
    }
}
