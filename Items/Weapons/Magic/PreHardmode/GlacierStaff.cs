using Avalon.Items.Material.Shards;
using Avalon.Items.Weapons.Ranged.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class GlacierStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.damage = 27;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 10;
        Item.rare = ItemRarityID.Blue;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useTime = 50;
        Item.useAnimation = 50;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.GlacierBall>();
        Item.autoReuse = false;
        Item.shootSpeed = 10f;
        Item.value = Item.buyPrice(0, 3, 50, 0);
        Item.UseSound = SoundID.Item43;
        Item.noMelee = true;
    }
    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 255, 255, 200);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.IceBlock, 50)
            .AddIngredient(ModContent.ItemType<Icicle>(), 50)
            .AddIngredient(ItemID.FallenStar, 8)
            .AddIngredient(ModContent.ItemType<FrostShard>(), 4)
            .AddTile(TileID.IceMachine)
            .Register();
    }
}
