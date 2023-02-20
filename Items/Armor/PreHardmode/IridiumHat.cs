using ExxoAvalonOrigins.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Armor.PreHardmode;

[AutoloadEquip(EquipType.Head)]
internal class IridiumHat : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.defense = 7;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 1, 20);
        Item.height = dims.Height;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<IridiumPlateMail>() && legs.type == ModContent.ItemType<IridiumPants>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "9% increased critical strike chance";
        player.GetCritChance<GenericDamageClass>() += 9;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.11f;
        player.GetAttackSpeed(DamageClass.Melee) += 0.11f;
        player.GetDamage(DamageClass.Ranged) += 0.11f;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 15)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 4)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
