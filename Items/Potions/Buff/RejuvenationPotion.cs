using Avalon.Buffs;
using Avalon.Items.Material.Herbs;
using Microsoft.Xna.Framework;
using Terraria.ID;
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

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.CloneDefaults(ItemID.RegenerationPotion);
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.buffType = ModContent.BuffType<Rejuvenation>();
        Item.buffTime = 60 * 30;
        Item.potion = true;
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
