using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Potions.Buff;

public class FuryPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.GreenYellow
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Fury>();
        Item.UseSound = SoundID.Item3;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 100;
        Item.value = Item.sellPrice(0, 0, 2, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 14400;
    }

    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Ickfish>()).AddIngredient(ModContent.ItemType<Barfbush>()).AddTile(TileID.Bottles).Register();
    //}
}
