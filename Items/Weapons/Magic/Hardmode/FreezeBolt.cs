using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class FreezeBolt : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 43;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shootSpeed = 7f;
        Item.mana = 11;
        Item.rare = ItemRarityID.Pink;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 17;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.FreezeBolt>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 50000;
        Item.useAnimation = 17;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item21;
    }

    //public override void AddRecipes()
    //{
    //    Terraria.Recipe.Create(ModContent.ItemType<FreezeBolt>())
    //        .AddIngredient(ItemID.WaterBolt)
    //        .AddIngredient(ModContent.ItemType<Material.SoulofIce>(), 20)
    //        .AddIngredient(ItemID.FrostCore, 2)
    //        .AddTile(TileID.Bookcases)
    //        .Register();
    //}
}
