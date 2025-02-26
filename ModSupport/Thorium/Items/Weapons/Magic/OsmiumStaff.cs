using Avalon.ModSupport.Thorium.Projectiles.Magic;
using Avalon.Items.Material;
using Avalon.Items.Material.Bars;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.Thorium.Items.Weapons.Magic;
class OsmiumStaff : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ExxoAvalonOrigins.ThoriumContentEnabled;
    }
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.damage = 18;
        Item.autoReuse = true;
        Item.mana = 5;
		Item.rare = ItemRarityID.Orange;
		Item.useTime = 35;
        Item.useAnimation = 35;
        Item.knockBack = 3.75f;
        Item.shoot = ModContent.ProjectileType<OsmiumBolt>();
        Item.value = Item.buyPrice(0, 0, 50);
        Item.UseSound = SoundID.Item43;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.shootSpeed = 2f;
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Projectile.NewProjectile(source,position,velocity,type,damage,knockback,player.whoAmI,-1);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1f);
		return false;
	}
	public override void AddRecipes()
    {
        CreateRecipe().AddIngredient(ModContent.ItemType<OsmiumBar>(), 14).AddIngredient(ModContent.ItemType<DesertFeather>(), 3).AddTile(TileID.Anvils).Register();
	}
}
