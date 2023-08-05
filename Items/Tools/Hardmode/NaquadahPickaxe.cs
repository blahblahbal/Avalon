using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class NaquadahPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        //Tooltip.SetDefault("Can mine Adamantite, Titanium, and Troxinium");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 12;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.pick = 150;
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.useTime = 10;
        Item.knockBack = 1f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 5);
        Item.useAnimation = 25;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 15)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
