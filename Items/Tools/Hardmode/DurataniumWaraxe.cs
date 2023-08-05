using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class DurataniumWaraxe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 40;
        Item.height = 40;
        Item.damage = 25;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.axe = 15;
        Item.rare = ItemRarityID.LightRed;
        Item.useTime = 18;
        Item.knockBack = 4f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 20, 0);
        Item.useAnimation = 18;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.DurataniumBar>(), 10)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
