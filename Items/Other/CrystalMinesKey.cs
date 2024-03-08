using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

public class CrystalMinesKey : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Crystal Mines Key");
        //Tooltip.SetDefault("Opens a Crystal Mines Chest");
        Item.ResearchUnlockCount = 1;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.Keys;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ModContent.RarityType<Rarities.DarkGreenRarity>();
        Item.width = dims.Width;
        Item.scale = 1f;
        Item.maxStack = 9999;
        Item.height = dims.Height;
    }

    // TODO: ADD RECIPE
    //public override void AddRecipes()
    //{
    //    CreateRecipe()
    //        .AddIngredient(ItemID.TempleKey)
    //        .AddIngredient(ModContent.ItemType<ContagionKeyMold>())
    //        .AddIngredient(ItemID.SoulofFright, 5)
    //        .AddIngredient(ItemID.SoulofMight, 5)
    //        .AddIngredient(ItemID.SoulofSight, 5)
    //        .AddTile(TileID.MythrilAnvil)
    //        .Register();
    //}
}
