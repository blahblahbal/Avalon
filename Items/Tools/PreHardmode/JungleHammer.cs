using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class JungleHammer : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 10;
        Item.autoReuse = true;
        Item.hammer = 55;
        Item.useTime = 30;
        Item.knockBack = 13f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 27000;
        Item.useAnimation = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Stinger, 10)
            .AddIngredient(ItemID.JungleSpores, 12)
            .AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>())
            .AddTile(TileID.Anvils)
            .Register();
    }
}
