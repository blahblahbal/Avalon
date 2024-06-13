using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class XanthophyteChainsaw : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item23;
        Item.damage = 50;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.shootSpeed = 46f;
        Item.axe = 23;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.knockBack = 4.6f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Tools.XanthophyteChainsaw>();
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
