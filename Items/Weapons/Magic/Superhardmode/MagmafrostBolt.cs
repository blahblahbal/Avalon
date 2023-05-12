using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Superhardmode;

class MagmafrostBolt : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 67;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.shootSpeed = 1.3f;
        Item.mana = 10;
        Item.rare = ItemRarityID.Yellow;
        Item.noMelee = true;
        Item.width = dims.Width;
        Item.useTime = 15;
        Item.knockBack = 5f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.MagmafrostBolt>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item21;
    }
    //public override void AddRecipes()
    //{
    //    CreateRecipe(1).AddIngredient(ModContent.ItemType<FreezeBolt>()).AddIngredient(ModContent.ItemType<Material.DragonScale>(), 10).AddIngredient(ModContent.ItemType<Material.LifeDew>(), 50).AddIngredient(ItemID.LivingFireBlock, 40).AddTile(TileID.MythrilAnvil).Register();
    //}
}
