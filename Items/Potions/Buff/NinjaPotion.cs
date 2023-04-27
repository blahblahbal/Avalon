using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class NinjaPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
        {
            Color.Black,
            Color.DarkGray
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Ninja>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 4 * 3600;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ItemID.Blinkroot)
            .AddIngredient(ItemID.DemoniteOre)
            .AddTile(TileID.Bottles)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ItemID.Blinkroot)
            .AddIngredient(ItemID.CrimtaneOre)
            .AddTile(TileID.Bottles)
            .Register();

        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>())
            .AddIngredient(ItemID.Blinkroot)
            .AddIngredient(ModContent.ItemType<Material.Ores.BacciliteOre>())
            .AddTile(TileID.Bottles)
            .Register();
    }
}
