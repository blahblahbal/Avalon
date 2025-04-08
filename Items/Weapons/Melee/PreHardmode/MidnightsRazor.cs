using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class MidnightsRazor : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return false;
	}
	public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 28;
        Item.damage = 40;
        Item.noUseGraphic = true;
        Item.shootSpeed = 25f;
        Item.rare = ItemRarityID.Orange;
        Item.noMelee = true;
        Item.useTime = 15;
        Item.useAnimation = 15;
        Item.knockBack = 2.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.MidnightsRazor>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.UseSound = SoundID.Item1;
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
}
