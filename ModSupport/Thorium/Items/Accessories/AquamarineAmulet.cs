using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace Avalon.ModSupport.Thorium.Items.Accessories;

[AutoloadEquip(EquipType.Neck)]
[ExtendsFromMod("ThoriumMod")]
public class AquamarineAmulet : ModItem
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
            .AddIngredient(ModContent.ItemType<ThoriumMod.Items.Misc.Aquamarine>(), 12)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils)
            .Register();
	}
	public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetDamage(HealerDamage.Instance) += 0.05f;
    }
}
