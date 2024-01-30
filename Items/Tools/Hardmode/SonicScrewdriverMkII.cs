using Avalon.Items.Tools.PreHardmode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.Hardmode;

class SonicScrewdriverMkII : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.rare = ItemRarityID.LightPurple;
        Item.useTime = 70;
        Item.value = Item.sellPrice(0, 4, 0, 0);
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 70;
        Item.scale = 0.7f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<SonicScrewdriverMkI>())
            .AddIngredient(ItemID.Sapphire, 7)
            .AddIngredient(ItemID.Wire, 10)
            .AddIngredient(ItemID.GPS)
            .AddIngredient(ItemID.SoulofMight, 4)
            .AddIngredient(ItemID.SoulofFright, 4)
            .AddIngredient(ItemID.SoulofSight, 4)
            .AddTile(TileID.TinkerersWorkbench).Register();
    }

    public override void UpdateInventory(Player player)
    {
        player.findTreasure = player.detectCreature = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}
