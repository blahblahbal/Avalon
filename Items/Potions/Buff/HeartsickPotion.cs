using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Potions.Buff;

class HeartsickPotion : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 20;
        ItemID.Sets.DrinkParticleColors[Type] = new Color[2]
        {
            new Color(92, 175, 46),
            new Color(159, 224, 124)
        };
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.buffType = ModContent.BuffType<Buffs.Heartsick>();
        Item.consumable = true;
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.useTime = 17;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.maxStack = 9999;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.buffTime = 60 * 60 * 6;
        Item.UseSound = SoundID.Item3;
    }

    public override void AddRecipes()
    {
        CreateRecipe(2)
            .AddIngredient(ModContent.ItemType<Material.BottledLava>(), 2)
            .AddIngredient(ItemID.LifeCrystal)
            .AddIngredient(ItemID.NeonTetra, 2)
            .AddTile(TileID.Bottles)
            .Register();
        //tropics recipe later
    }
}
