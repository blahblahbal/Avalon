using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class AxeofSickness : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 28;
        Item.UseSound = SoundID.Item1;
        Item.damage = 24;
        Item.autoReuse = true;
        Item.useTurn = true;
        //Item.scale = 1.2f;
        Item.axe = 15;
        Item.rare = ItemRarityID.Blue;
        Item.useTime = 34;
        Item.knockBack = 6f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 36, 0);
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 34;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10).AddTile(TileID.Anvils).Register();
    }
}
