using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Wall;

class PeridotGemsparkWall : ModItem
{
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
        Item.createWall = ModContent.WallType<Walls.PeridotGemsparkWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.PeridotGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.PeridotGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
    public override void PostUpdate()
    {
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.714f * 0.8f, 1f * 0.8f, 0);
    }
}
class PeridotGemsparkWallOff : ModItem
{
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
        Item.createWall = ModContent.WallType<Walls.PeridotGemsparkWallOff>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.PeridotGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.PeridotGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}

class TourmalineGemsparkWall : ModItem
{
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
        Item.createWall = ModContent.WallType<Walls.TourmalineGemsparkWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.TourmalineGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.TourmalineGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
    public override void PostUpdate()
    {
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0, 1f * 0.8f, 1f * 0.8f);
    }
}
class TourmalineGemsparkWallOff : ModItem
{
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
        Item.createWall = ModContent.WallType<Walls.TourmalineGemsparkWallOff>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.TourmalineGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.TourmalineGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}

class ZirconGemsparkWall : ModItem
{
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
        Item.createWall = ModContent.WallType<Walls.ZirconGemsparkWall>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.ZirconGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.ZirconGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
    public override void PostUpdate()
    {
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 1.1f * 0.8f, 0.8f * 0.8f, 0.4f);
    }
}
class ZirconGemsparkWallOff : ModItem
{
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
        Item.createWall = ModContent.WallType<Walls.ZirconGemsparkWallOff>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(4)
		.AddIngredient(ModContent.ItemType<Tile.ZirconGemsparkBlock>())
		.AddTile(TileID.WorkBenches)
		.Register();
		
        Recipe.Create(ModContent.ItemType<Tile.ZirconGemsparkBlock>()).AddIngredient(this, 4)
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}
