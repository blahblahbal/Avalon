using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode;

class EnergyRevolver : ModItem
{
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.DamageType = DamageClass.Magic;
        Item.damage = 36;
        Item.autoReuse = true;
        Item.shootSpeed = 15f;
        Item.mana = 6;
        Item.noMelee = true;
        Item.rare = ItemRarityID.Lime;
        Item.width = dims.Width;
        Item.knockBack = 2f;
        Item.useTime = 6;
        Item.shoot = ProjectileID.GreenLaser;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 4);
        Item.useAnimation = 6;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item12;
    }
    public override Vector2? HoldoutOffset()
    {
        return new Vector2(-4f, 0);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.LaserRifle)
            .AddIngredient(ItemID.Lens, 10)
            .AddIngredient(ModContent.ItemType<Material.BloodshotLens>(), 5)
            .AddIngredient(ItemID.BlackLens)
            .AddIngredient(ItemID.SoulofFright, 16)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}
