using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class NaquadahSword : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 38;
        Item.damage = 55;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.autoReuse = true;
        Item.rare = ItemRarityID.LightRed;
        Item.useTime = 24;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 2, 64, 0);
        Item.useAnimation = 24;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NaquadahBar>(), 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
