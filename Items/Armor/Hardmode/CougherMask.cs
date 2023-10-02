using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Armor.Hardmode;

[AutoloadEquip(EquipType.Head)]
class CougherMask : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.LightRed;
        Item.width = dims.Width;
        Item.value = Item.sellPrice(0, 2);
        Item.height = dims.Height;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().CougherMask = true;
        if (player.PlayerDoublePressedSetBonusActivateKey())
        {
            int proj = Projectile.NewProjectile(player.GetSource_Accessory(new Item(ModContent.ItemType<Items.Armor.Hardmode.CougherMask>())),
                player.Center, new Vector2(3, 0) * player.direction, ModContent.ProjectileType<Projectiles.PlayerCough>(), 0, 0);
        }
    }
}
