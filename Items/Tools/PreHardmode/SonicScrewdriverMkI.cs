using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class SonicScrewdriverMkI : ModItem
{
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
        player.detectCreature = true;
        HighlightTreasure(player);
    }
    public static void HighlightTreasure(Player p)
    {
        int radius = 26;
        int x = (int)p.Center.X / 16;
        int y = (int)p.Center.Y / 16;
        for (int i = x - radius; i <= x + radius; i++)
        {
            for (int j = y - radius; j <= y + radius; j++)
            {
                if (Main.rand.NextBool(7) && Main.tileSpelunker[Main.tile[i, j].TileType] && new Vector2(x - i, y - j).Length() < radius && i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1 && Main.tile[i, j] != null)
                {
                    int num804 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, DustID.WhiteTorch, 0f, 0f, 0, default(Color), 0.6f);
                    Main.dust[num804].fadeIn = 0.75f;
                    Dust dust2 = Main.dust[num804];
                    dust2.velocity *= 0.3f;
                    Main.dust[num804].noGravity = true;
                    Main.dust[num804].noLight = true;
                }
            }
        }
    }
}
