using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Compatability.Thorium.Items.Placeable.Wall;

[ExtendsFromMod("ThoriumMod")]
class ChrysoberylGemsparkWall : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
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
        Item.createWall = ModContent.WallType<Walls.ChrysoberylGemsparkWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.ChrysoberylGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.ChrysoberylGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
    public override void PostUpdate()
    {
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.745f * 0.8f, 0.925f * 0.8f, 0.1f * 0.8f);
    }
}
[ExtendsFromMod("ThoriumMod")]
class ChrysoberylGemsparkWallOff : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
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
        Item.createWall = ModContent.WallType<Walls.ChrysoberylGemsparkWallOff>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.ChrysoberylGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.ChrysoberylGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}
