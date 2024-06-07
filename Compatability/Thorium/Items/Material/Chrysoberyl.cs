using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Material;

[ExtendsFromMod("ThoriumMod")]
class Chrysoberyl : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 15;
    }
    public override bool IsLoadingEnabled(Mod mod)
    {
		return false; // ModLoader.HasMod("ThoriumMod");
	}
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = (ContentSamples.CreativeHelper.ItemGroup)Data.Sets.ItemGroupValues.Gems;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        //Item.autoReuse = true;
        //Item.useTurn = true;
        Item.maxStack = 9999;
        //Item.createTile = ModContent.TileType<Tiles.PlacedGems>();
        //Item.placeStyle = 5 + 6;
        //Item.consumable = true;
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        //Item.useTime = 10;
        Item.value = 4400;
        //Item.useStyle = ItemUseStyleID.Swing;
        //Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    //public override bool? UseItem(Player player)
    //{
    //    int i = Player.tileTargetX;
    //    int j = Player.tileTargetY;
    //    if ((WorldGen.SolidTile(i - 1, j, noDoors: true) || WorldGen.SolidTile(i + 1, j, noDoors: true) || WorldGen.SolidTile(i, j - 1) || WorldGen.SolidTile(i, j + 1)))
    //    {
    //        Item.createTile = ModContent.TileType<Tiles.PlacedGems>();
    //        Item.consumable = true;
    //    }
    //    else
    //    {
    //        Item.createTile = -1;
    //        Item.consumable = false;
    //    }
    //    return null;
    //}
}
