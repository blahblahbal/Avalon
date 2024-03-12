using Avalon.Compatability.Thorium.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

[ExtendsFromMod("ThoriumMod")]
class ChrysoberylStaff : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModLoader.HasMod("ThoriumMod");
    }
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
        Item.damage = 18;
        Item.autoReuse = true;
        Item.shootSpeed = 7.5f;
        Item.mana = 5;
        Item.rare = ItemRarityID.Green;
        Item.useTime = 35;
        Item.useAnimation = 35;
        Item.knockBack = 3.75f;
        Item.shoot = ModContent.ProjectileType<ChrysoberylBolt>();
        Item.value = Item.buyPrice(0, 0, 15);
        Item.UseSound = SoundID.Item43;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1)
            .AddIngredient(ModContent.ItemType<Material.Bars.NickelBar>(), 10)
            .AddIngredient(ModContent.ItemType<Compatability.Thorium.Items.Material.Chrysoberyl>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
