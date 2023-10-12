using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class ShadowPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2] {
            Color.Black,
            Color.DarkGray
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Shadows>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.buffTime = 25200;
        Item.UseSound = SoundID.Item3;
    }
    public override void AddRecipes()
    {
        CreateRecipe(5).AddIngredient(ItemID.BottledWater, 5).AddIngredient(ModContent.ItemType<Material.ChaosDust>(), 9).AddIngredient(ItemID.Waterleaf, 3).AddIngredient(ItemID.Fireblossom, 3).AddTile(TileID.Bottles).Register();
    }
}
