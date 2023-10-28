using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;
using Terraria.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class CursedFlamelash : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item20;
        Item.DamageType = DamageClass.Magic;
        Item.damage = 40;
        Item.channel = true;
        Item.shootSpeed = 6f;
        Item.mana = 17;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 23;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.CursedFlamelash>();
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 250000;
        Item.useAnimation = 23;
        Item.height = dims.Height;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ItemID.Flamelash)
            .AddIngredient(ItemID.CursedFlame, 30)
            .AddIngredient(ItemID.SoulofFright,5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
