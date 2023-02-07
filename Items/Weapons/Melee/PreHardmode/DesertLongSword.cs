using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Melee;

class DesertLongSword : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.UseSound = SoundID.Item1;
        Item.damage = 29;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.Green;
        Item.useTime = 27;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 54, 0);
        Item.useAnimation = 27;
        Item.Size = new Vector2(44);
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.AntlionMandible)
            .AddIngredient(ItemID.SandBlock, 60)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 5)
            .AddIngredient(ItemID.Topaz, 5)
            .AddTile(TileID.Anvils).Register();
    }
}
