using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class JungleAxe : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 12;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.axe = 22;
        Item.width = dims.Width;
        Item.useTime = 23;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 5000;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 23;
        Item.height = dims.Height;
		Item.rare = ItemRarityID.Orange;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.JungleSpores, 12)
            .AddIngredient(ItemID.Stinger, 4)
            .AddIngredient(ItemID.Vine)
            .AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>())
            .AddTile(TileID.Anvils)
			.SortBeforeFirstRecipesOf(ItemID.BladeofGrass)
			.Register();
    }
}
