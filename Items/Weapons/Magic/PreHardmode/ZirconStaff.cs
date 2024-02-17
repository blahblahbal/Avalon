using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class ZirconStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
    }

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.SapphireStaff);
        Item.staff[Item.type] = true;
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
        Item.damage = 25;
        Item.autoReuse = true;
        Item.shootSpeed = 9.75f;
        Item.mana = 9;
        Item.rare = ItemRarityID.Green;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.knockBack = 4.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.ZirconBolt>();
        Item.value = Item.buyPrice(0, 3, 60, 0);
        Item.UseSound = SoundID.Item43;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.BismuthBar>(), 10)
            .AddIngredient(ModContent.ItemType<Material.Ores.Zircon>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
