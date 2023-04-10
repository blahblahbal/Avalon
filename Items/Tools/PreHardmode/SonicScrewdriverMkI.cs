using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

class SonicScrewdriverMkI : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.rare = ItemRarityID.Orange;
        Item.useTime = 70;
        Item.value = Item.sellPrice(0, 1, 0, 0);
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 70;
        Item.scale = 0.7f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Ruby, 10)
            .AddIngredient(ItemID.MeteoriteBar, 5)
            .AddIngredient(ItemID.Wire, 30)
            .AddIngredient(ItemID.HunterPotion, 3)
            .AddIngredient(ItemID.SpelunkerPotion, 3)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }
    public override void UpdateInventory(Player player)
    {
        player.findTreasure = player.detectCreature = true;
    }
}
