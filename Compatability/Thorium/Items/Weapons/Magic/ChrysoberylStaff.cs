using Avalon.Compatability.Thorium.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Compatability.Thorium.Items.Weapons.Magic;

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
        Item.damage = 18;
        Item.autoReuse = true;
        Item.shootSpeed = 7.5f;
        Item.mana = 5;
        Item.rare = ItemRarityID.White;
        Item.useTime = 35;
        Item.useAnimation = 35;
        Item.knockBack = 3.75f;
        Item.shoot = ModContent.ProjectileType<ChrysoberylBolt>();
        Item.value = Item.buyPrice(0, 0, 15);
        Item.UseSound = SoundID.Item43;
        Item.GetGlobalItem<ThoriumTweaksGlobalItemInstance>().AvalonThoriumItem = true;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ModContent.ItemType<Avalon.Items.Material.Bars.NickelBar>(), 10)
            .AddIngredient(ModContent.ItemType<Material.Chrysoberyl>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
