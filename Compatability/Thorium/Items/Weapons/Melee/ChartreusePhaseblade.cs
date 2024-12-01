using Avalon.Compatability.Thorium.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Weapons.Melee;

public class ChartreusePhaseblade : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.RedPhaseblade);
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return Color.White;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Chrysoberyl>(), 10)
            .AddIngredient(ItemID.MeteoriteBar, 15)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        Lighting.AddLight((int)((player.itemLocation.X + 6f + player.velocity.X) / 16f), (int)((player.itemLocation.Y - 14f) / 16f), 0.909f, 0.909f, 0.478f);
    }
}
