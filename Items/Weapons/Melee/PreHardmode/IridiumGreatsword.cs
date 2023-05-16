using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class IridiumGreatsword : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 34;
        Item.height = 36;
        Item.damage = 30;
        Item.crit = 6;
        Item.rare = ItemRarityID.LightRed;
        Item.autoReuse = true;
        Item.useTurn = true;
        //Item.scale = 1.4f;
        Item.useTime = 18;
        Item.knockBack = 5.4f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 50000;
        Item.useAnimation = 18;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 14)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 3)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
