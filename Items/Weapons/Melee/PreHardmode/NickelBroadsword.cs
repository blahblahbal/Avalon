using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class NickelBroadsword : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 14;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.width = dims.Width;
        Item.useTime = 20;
        Item.knockBack = 5.5f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 2250;
        Item.useAnimation = 20;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
