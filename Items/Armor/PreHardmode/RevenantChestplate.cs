using Microsoft.Xna.Framework;
using Avalon.Items.Material.Shards;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Body)]
public class RevenantChestplate : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
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
            .AddIngredient(ItemID.ShadowScale, 8)
            .AddIngredient(ItemID.Bone, 60)
            .AddIngredient(ModContent.ItemType<UndeadShard>(), 10)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.TissueSample, 8)
            .AddIngredient(ItemID.Bone, 60)
            .AddIngredient(ModContent.ItemType<UndeadShard>(), 10)
            .AddTile(TileID.Anvils)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.Booger>(), 8)
            .AddIngredient(ItemID.Bone, 60)
            .AddIngredient(ModContent.ItemType<UndeadShard>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.04f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.03f;
    }
}
