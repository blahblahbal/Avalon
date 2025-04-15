using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class IridiumHamaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 32;
        Item.damage = 20;
        Item.autoReuse = true;
        Item.hammer = 75;
        Item.useTurn = true;
        //Item.scale = 1.2f;
        Item.axe = 22;
        Item.crit += 4;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 13;
        Item.knockBack = 2.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 50000;
        Item.useAnimation = 13;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>())
            .AddTile(TileID.Anvils)
            .Register();
    }
}
