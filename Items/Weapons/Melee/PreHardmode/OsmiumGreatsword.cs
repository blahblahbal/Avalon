using Avalon.Items.Material.Bars;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode; 

public class OsmiumGreatsword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 36;
        Item.damage = 28;
        Item.autoReuse = true;
        Item.useTurn = true;
        //Item.scale = 1.5f;
        Item.crit = 6;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 20;
        Item.knockBack = 5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 50000;
        Item.useAnimation = 20;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<OsmiumBar>(), 14).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3).AddTile(TileID.Anvils).Register();
    }
}
