using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class IridiumPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 32;
        Item.UseSound = SoundID.Item1;
        Item.damage = 15;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1f;
        Item.rare = ItemRarityID.LightRed;
        Item.pick = 85;
        Item.useTime = 15;
        Item.knockBack = 2.6f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useAnimation = 15;
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Type)
        {
            player.pickSpeed -= 0.5f;
        }
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Bars.IridiumBar>(), 13)
            .AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2)
            .AddTile(TileID.Anvils)
            .Register();
    }
}
