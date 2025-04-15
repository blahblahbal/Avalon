using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode;

public class XanthophyteSpear : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 36;
        Item.height = 40;
        Item.UseSound = SoundID.Item1;
        Item.damage = 52;
        Item.noUseGraphic = true;
        Item.scale = 1f;
        Item.shootSpeed = 5.4f;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.knockBack = 5.5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Melee.XanthophyteSpear>();
        Item.DamageType = DamageClass.Melee;
        Item.autoReuse = true;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 40, 0, 0);
    }
    public override bool CanUseItem(Player player)
    {
        return player.ownedProjectileCounts[Item.shoot] < 1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 12)
            .AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
