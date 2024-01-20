using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Avalon.Items.OreChunks;

class VorazylcumChunk : ModItem
{
    // remove after this is added
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 200;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 100;
        Item.height = dims.Height;
        Item.rare = ModContent.RarityType<Rarities.TealRarity>();
    }
    //public override void AddRecipes()
    //{
    //    Recipe.Create(ModContent.ItemType<Material.Bars.VorazylcumBar>())
    //        .AddIngredient(Type, 6)
    //        .AddTile(TileID.WorkBenches)
    //        .Register();
    //}
}
