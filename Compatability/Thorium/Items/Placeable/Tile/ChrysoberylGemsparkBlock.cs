using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Placeable.Tile;

[ExtendsFromMod("ThoriumMod")]
class ChrysoberylGemsparkBlock : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 100;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.autoReuse = true;
        Item.consumable = true;
        Item.createTile = ModContent.TileType<Tiles.ChrysoberylGemspark>();
        Item.width = dims.Width;
        Item.useTurn = true;
        Item.useTime = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type, 20)
            .AddIngredient(ItemID.Glass, 20)
            .AddIngredient(ModContent.ItemType<Material.Chrysoberyl>())
            .AddTile(TileID.WorkBenches).Register();
    }

    public override void PostUpdate()
    {
        Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 0.745f, 0.925f, 0.1f);
    }
}
