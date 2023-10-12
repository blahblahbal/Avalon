using Avalon.Buffs;
using Avalon.Common.Players;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class RejuvenationPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
        {
            Color.Red,
            Color.Pink
        };
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        int index = tooltips.FindLastIndex(tt => (tt.Mod.Equals("Terraria") || tt.Mod.Equals(Mod.Name)) && tt.Name.Equals("Tooltip0"));
        if (Main.LocalPlayer.GetModPlayer<AvalonPlayer>().ThePill)
        {
            var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.RejuvenationPotion.ThePill"));
            tooltips.Insert(index, thing);
        }
        else
        {
            var thing = new TooltipLine(Mod, "Tooltip0", Language.GetTextValue("Mods.Avalon.RejuvenationPotion.NoThePill"));
            tooltips.Insert(index, thing);
        }
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item3;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useTurn = true;
        Item.useAnimation = 17;
        Item.useTime = 17;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.value = 1000;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.potion = true;
    }
    public override bool? UseItem(Player player)
    {
        player.AddBuff(ModContent.BuffType<Rejuvenation>(), 60 * 30);
        return true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.BottledWater)
            .AddIngredient(ItemID.Mushroom)
            .AddIngredient(ItemID.PinkGel)
            .AddIngredient(ModContent.ItemType<Sweetstem>())
            .AddTile(TileID.Bottles)
            .Register();
    }
}
