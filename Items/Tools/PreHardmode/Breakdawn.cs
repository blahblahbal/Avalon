using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class Breakdawn : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 44;
        Item.height = 44;
        Item.UseSound = SoundID.Item1;
        Item.damage = 26;
        Item.autoReuse = true;
        Item.hammer = 70;
        Item.axe = 22;
        Item.useTime = 24;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 27000;
        Item.useAnimation = 20;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.TheBreaker)
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ModContent.ItemType<JungleHammer>())
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ItemID.FleshGrinder)
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ModContent.ItemType<JungleHammer>())
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();

        CreateRecipe()
            .AddIngredient(ModContent.ItemType<MucusHammer>())
            .AddIngredient(ModContent.ItemType<Blueshift>())
            .AddIngredient(ModContent.ItemType<JungleHammer>())
            .AddIngredient(ItemID.MoltenHamaxe)
            .AddTile(TileID.DemonAltar)
            .Register();
    }
}
