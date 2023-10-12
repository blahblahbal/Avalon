using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Ammo;

internal class ContagionSolution : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 99;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.ammo = AmmoID.Solution;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.consumable = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.ContagionSpray>() - ProjectileID.PureSpray;
        Item.value = Item.buyPrice(0, 0, 25);
        Item.maxStack = 2000;
        Item.height = dims.Height;
    }

    public override bool CanConsumeAmmo(Item ammo, Player player) =>
        player.itemAnimation >= player.HeldItem.useAnimation - 3;
}
