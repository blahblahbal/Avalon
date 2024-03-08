using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

class PeridotStaff : ModItem
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
        Item.damage = 21;
        Item.autoReuse = true;
        Item.shootSpeed = 7.75f;
        Item.mana = 7;
        Item.rare = ItemRarityID.Blue;
        Item.useTime = 31;
        Item.useAnimation = 31;
        Item.knockBack = 4.75f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.PeridotBolt>();
        Item.value = Item.buyPrice(0, 1, 90, 0);
        Item.UseSound = SoundID.Item43;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.ZincBar>(), 10)
            .AddIngredient(ModContent.ItemType<Material.Ores.Peridot>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
