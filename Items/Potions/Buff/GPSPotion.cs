using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Potions.Buff;

class GPSPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
        {
            Color.Red,
            Color.Blue
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.GPS>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.value = 2000;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.buffTime = 18000;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.IronOre).AddIngredient(ItemID.RottenChunk).AddTile(TileID.Bottles).Register();
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.LeadOre).AddIngredient(ItemID.RottenChunk).AddTile(TileID.Bottles).Register();
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.IronOre).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Bottles).Register();
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.LeadOre).AddIngredient(ItemID.Vertebrae).AddTile(TileID.Bottles).Register();
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.IronOre).AddIngredient(ModContent.ItemType<Material.YuckyBit>()).AddTile(TileID.Bottles).Register();
        CreateRecipe(1).AddIngredient(ItemID.BottledWater).AddIngredient(ModContent.ItemType<Material.Beak>()).AddIngredient(ItemID.LeadOre).AddIngredient(ModContent.ItemType<Material.YuckyBit>()).AddTile(TileID.Bottles).Register();
    }
}
