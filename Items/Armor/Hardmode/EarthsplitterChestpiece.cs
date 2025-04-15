using Avalon.Common.Players;
using Avalon.Items.Armor.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Body)]
public class EarthsplitterChestpiece : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 9;
        Item.rare = ModContent.RarityType<Rarities.CrispyRarity>();
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 3);
        Item.height = dims.Height;
    }
    public override void UpdateEquip(Player player)
    {
        player.nightVision = true;
        player.GetModPlayer<AvalonPlayer>().HookBonus = true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ItemID.ShadowScalemail)
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ItemID.AncientShadowScalemail)
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ItemID.CrimsonScalemail)
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.FleshyTendril>(), 4)
            .AddIngredient(ModContent.ItemType<ViruthornScalemail>())
            .AddIngredient(ItemID.MiningShirt)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
