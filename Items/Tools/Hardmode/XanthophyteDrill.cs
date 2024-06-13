using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class XanthophyteDrill : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item23;
        Item.damage = 35;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.shootSpeed = 40f;
        Item.pick = 202;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.knockBack = 1f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.XanthophyteDrill>();
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 4, 32);
        Item.useAnimation = 15;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.XanthophyteBar>(), 18)
            .AddIngredient(ModContent.ItemType<Material.Shards.VenomShard>())
            .AddTile(TileID.Anvils).Register();
    }
}
