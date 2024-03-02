using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Other;

internal class ShimmerBucket : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)
    {
        return false;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.White;
        Item.width = dims.Width;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.height = dims.Height;
    }
    public override bool? UseItem(Player player)
    {
        //Vector2 pos = player.GetModPlayer<AvalonPlayer>().MousePosition;
        //Point tilePos = pos.ToTileCoordinates();
        //if (player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple) && !Main.tile[tilePos.X, tilePos.Y].HasTile &&
        //    (Main.tile[tilePos.X, tilePos.Y].LiquidAmount == 0 || Main.tile[tilePos.X, tilePos.Y].LiquidType == LiquidID.Shimmer))
        //{
        //    SoundEngine.PlaySound(SoundID.Splash, player.position);
        //    Tile t = Main.tile[tilePos.X, tilePos.Y];
        //    t.LiquidType = LiquidID.Shimmer;
        //    t.LiquidAmount = 255;

        //    Item.stack--;
        //    player.PutItemInInventoryFromItemUsage(ItemID.EmptyBucket, player.selectedItem);
        //    player.ApplyItemTime(Item);

        //    if (Main.netMode == NetmodeID.MultiplayerClient)
        //        NetMessage.sendWater(tilePos.X, tilePos.Y);
        //    return true;
        //}
        return false;
    }
}
