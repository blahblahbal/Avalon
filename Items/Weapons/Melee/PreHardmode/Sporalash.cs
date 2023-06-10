using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class Sporalash : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Sporalash");
        //Tooltip.SetDefault("Has a chance to poison");
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
    }

    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 10;
        Item.damage = 26;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.scale = 1.1f;
        Item.shootSpeed = 10f;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 46;
        Item.knockBack = 6.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Sporalash>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 27000;
        Item.useAnimation = 46;
    }
    public override void AddRecipes()
    {
        Terraria.Recipe.Create(Type)
            .AddIngredient(ItemID.JungleSpores, 15)
            .AddIngredient(ItemID.Stinger, 10)
            .AddIngredient(ItemID.Vine, 2)
            .AddIngredient(ModContent.ItemType<Material.Shards.ToxinShard>(), 2)
            .AddTile(TileID.Anvils).Register();
    }
}
