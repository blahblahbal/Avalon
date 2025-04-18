using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class EruptionHook : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.noUseGraphic = true;
        Item.useTurn = true;
        Item.shootSpeed = 14f;
        Item.rare = ItemRarityID.Orange;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 0;
        Item.knockBack = 7f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.EruptionHook>();
        Item.value = Item.sellPrice(0, 0, 54, 0);
		Item.UseSound = SoundID.Item1;
		Item.useStyle = ItemUseStyleID.None;
        Item.useAnimation = 0;
        Item.height = dims.Height;
    }
}
