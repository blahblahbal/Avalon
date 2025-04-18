using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.ModSupport.Thorium.Items.Placeable.Wall;

public class ChartreuseStainedGlass : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 400;
    }

    public override void SetDefaults()
    {
        Item.Size = new Vector2(12);
        Item.autoReuse = true;
        Item.consumable = true;
        Item.useTurn = true;
        Item.useTime = 7;
        Item.createWall = ModContent.WallType<Walls.ChartreuseStainedGlass>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Chrysoberyl>())
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}
