using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

public class XanthophyteGreataxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 72;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.axe = 23;
        Item.rare = ItemRarityID.Yellow;
        Item.useTime = 30;
        Item.knockBack = 7f;
        Item.DamageType = DamageClass.Melee;
        Item.tileBoost++;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 4, 32);
        Item.useAnimation = 30;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 18)
            .AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
