using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class BloodyAmulet : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Green;
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.consumable = true;
        Item.useTime = 30;
        Item.maxStack = 9999;
        Item.useAnimation = 15;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.UseSound = SoundID.Item29;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.BloodyAmulet>();
        Item.value = 0;
    }
}
