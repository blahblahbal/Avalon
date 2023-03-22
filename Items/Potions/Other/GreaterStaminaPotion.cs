using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Other;

class GreaterStaminaPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 30;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1] { Color.Green };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.consumable = true;
        Item.rare = ItemRarityID.Pink;
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = 95;
        Item.maxStack = 9999;
        Item.value = 2000;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(10)
            .AddIngredient(ModContent.ItemType<StaminaPotion>(), 10)
            .AddIngredient(ItemID.Feather, 2)
            .AddIngredient(ItemID.SoulofFlight)
            .AddTile(TileID.Bottles)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam >= player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2) return false;
        return true;
    }
    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<AvalonStaminaPlayer>().StatStam += 95;
        player.GetModPlayer<AvalonStaminaPlayer>().StaminaHealEffect(95, true);
        player.AddBuff(ModContent.BuffType<Buffs.StaminaDrain>(), 60 * 9);
        if (player.GetModPlayer<AvalonStaminaPlayer>().StatStam > player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2)
        {
            player.GetModPlayer<AvalonStaminaPlayer>().StatStam = player.GetModPlayer<AvalonStaminaPlayer>().StatStamMax2;
        }
        return true;
    }
}
