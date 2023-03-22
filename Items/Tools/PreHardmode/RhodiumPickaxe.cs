using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class RhodiumPickaxe : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.damage = 11;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.crit += 5;
        Item.pick = 80;
        Item.rare = ItemRarityID.Orange;
        Item.width = dims.Width;
        Item.useTime = 13;
        Item.knockBack = 2f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = 50000;
        Item.useAnimation = 15;
        Item.height = dims.Height;
        Item.UseSound = SoundID.Item1;
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.RhodiumBar>(), 13).AddIngredient(ModContent.ItemType<Material.DesertFeather>(), 2).AddTile(TileID.Anvils).Register();
    }
    public override void HoldItem(Player player)
    {
        if (player.inventory[player.selectedItem].type == Type)
        {
            player.pickSpeed -= 0.5f;
        }
    }
}
