using Avalon.Compatability.Thorium.Items.Material;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Accessories;

[AutoloadEquip(EquipType.Neck)]
public class ChrysoberylAmulet : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
		return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.accessory = true;
        Item.value = Item.sellPrice(0, 0, 50);
        Item.height = dims.Height;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Chrysoberyl>(), 12)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils)
            .Register();
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(DamageClass.Throwing) += 0.05f;
    }
}
