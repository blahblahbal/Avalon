using Avalon.Common.Players;
using Avalon.Items.Material.Shards;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
class RevenantHelm : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 7;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.value = 100000;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.ShadowScale, 5)
            .AddIngredient(ItemID.Bone, 40)
            .AddIngredient(ModContent.ItemType<UndeadShard>(), 8)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 5)
            .AddIngredient(ItemID.Bone, 40)
            .AddIngredient(ModContent.ItemType<UndeadShard>(), 8)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 5)
            .AddIngredient(ItemID.Bone, 40)
            .AddIngredient(ModContent.ItemType<UndeadShard>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<RevenantChestplate>() && legs.type == ModContent.ItemType<RevenantGreaves>();
    }
    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = Language.GetTextValue("Mods.Avalon.SetBonuses.Zombie");
        player.GetModPlayer<AvalonPlayer>().ZombieArmor = true;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.04f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
    }
}
