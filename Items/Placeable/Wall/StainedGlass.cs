using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Placeable.Wall;

public class LimeStainedGlass : ModItem
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
        Item.createWall = ModContent.WallType<Walls.LimeStainedGlass>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Ores.Peridot>())
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}

public class CyanStainedGlass : ModItem
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
        Item.createWall = ModContent.WallType<Walls.CyanStainedGlass>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Ores.Tourmaline>())
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}

public class BrownStainedGlass : ModItem
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
        Item.createWall = ModContent.WallType<Walls.BrownStainedGlass>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
    }
    public override void AddRecipes()
    {
        CreateRecipe(20)
		.AddIngredient(ItemID.GlassWall, 20)
		.AddIngredient(ModContent.ItemType<Material.Ores.Zircon>())
		.AddTile(TileID.WorkBenches)
		.Register();
    }
}