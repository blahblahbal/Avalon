using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class StrengthPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[1]
        {
            Color.DarkBlue
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Strong>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 17;
        Item.value = 2000;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.buffTime = 32400;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ItemID.AdamantiteBar)
            .AddIngredient(ItemID.Diamond)
            .AddTile(TileID.Bottles)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ItemID.TitaniumBar)
            .AddIngredient(ItemID.Diamond)
            .AddTile(TileID.Bottles)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ModContent.ItemType<Material.Bars.TroxiniumBar>())
            .AddIngredient(ItemID.Diamond)
            .AddTile(TileID.Bottles)
            .Register();
    }
}
