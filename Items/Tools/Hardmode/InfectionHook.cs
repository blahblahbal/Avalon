using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class InfectionHook : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.useTurn = true;
        Item.shootSpeed = 15f;
        Item.rare = ItemRarityID.Green;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.InfectionHook>();
        Item.value = Item.sellPrice(0, 6);
		Item.UseSound = SoundID.Item1;
		Item.height = dims.Height;
        Item.useStyle = ItemUseStyleID.None;
        Item.useTime = 0;
        Item.useAnimation = 0;
    }
}
