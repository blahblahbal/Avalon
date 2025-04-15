using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Avalon.Items.Weapons.Magic.Hardmode;

public class MagicGrenade : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 85;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shootSpeed = 8f;
        Item.mana = 40;
        Item.rare = ItemRarityID.Yellow;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 22;
        Item.knockBack = 8f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.MagicGrenade>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useAnimation = 22;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.MagicDagger)
            .AddIngredient(ItemID.Grenade, 10)
            .AddIngredient(ItemID.SoulofFright, 20)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
