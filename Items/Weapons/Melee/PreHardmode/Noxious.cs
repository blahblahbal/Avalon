using Avalon.Items.Material.Bars;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

public class Noxious : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.Yoyo[Item.type] = true;
        ItemID.Sets.GamepadExtraRange[Item.type] = 15;
        ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.width = 24;
        Item.height = 24;
        Item.useAnimation = 25;
        Item.useTime = 25;
        Item.shootSpeed = 16f;
        Item.knockBack = 8.5f;
        Item.damage = 15;
        Item.rare = ItemRarityID.Blue;
        Item.DamageType = DamageClass.Melee;
        Item.channel = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.UseSound = SoundID.Item1;
        Item.value = Item.sellPrice(1);
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.Noxious>();
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<BacciliteBar>(), 12)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
