using Avalon.Common.Players;
using Avalon.Network;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class SonicScrewdriverMkIII : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 36;
        Item.rare = ItemRarityID.Cyan;
        Item.useTime = 70;
        Item.value = Item.sellPrice(0, 10, 0, 0);
        Item.useStyle = ItemUseStyleID.Thrust;
        Item.useAnimation = 70;
        Item.scale = 0.7f;
        Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool? UseItem(Player player)
    {
        //if (player.altFunctionUse == 2)
        if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
        {
            //Vector2 mousePos = Main.MouseScreen + Main.screenPosition;
            //if (Main.netMode == NetmodeID.MultiplayerClient)
            //{
            //    player.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
            //    CursorPosition.SendPacket(mousePos, player.whoAmI);
            //}
            //else if (Main.netMode == NetmodeID.SinglePlayer)
            //{
            //    player.GetModPlayer<AvalonPlayer>().MousePosition = mousePos;
            //}
            Point c = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();

            if (Main.tile[c.X, c.Y].TileType == TileID.Containers)
            {
                int xpos;
                for (xpos = (int)(Main.tile[c.X, c.Y].TileFrameX / 18); xpos > 1; xpos -= 2)
                {
                }
                xpos = Player.tileTargetX - xpos;
                int ypos = Player.tileTargetY - (int)(Main.tile[c.X, c.Y].TileFrameY / 18);

                if (Chest.IsLocked(xpos, ypos))
                {
                    Chest.Unlock(xpos, ypos);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(Terraria.ID.MessageID.LockAndUnlock, -1, -1, null, 1, xpos, ypos);
                    }
                }
                else
                {
                    Chest.Lock(xpos, ypos);
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        NetMessage.SendData(Terraria.ID.MessageID.LockAndUnlock, -1, -1, null, 3, xpos, ypos);
                    }
                }
            }
        }
        return true;
    }

    //public override void AddRecipes()
    //{
    //    Recipe.Create(Type)
    //        .AddIngredient(ModContent.ItemType<SonicScrewdriverMkII>())
    //        .AddIngredient(ItemID.Emerald, 10)
    //        .AddIngredient(ItemID.Wire, 20)
    //        .AddIngredient(ItemID.SoulofMight, 5)
    //        .AddIngredient(ItemID.SoulofFright, 5)
    //        .AddIngredient(ItemID.SoulofSight, 5)
    //        .AddIngredient(ModContent.ItemType<Material.Onyx>(), 10)
    //        .AddTile(TileID.TinkerersWorkbench).Register();
    //}
    public override void UpdateInventory(Player player)
    {
        player.findTreasure = player.detectCreature = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
        //player.GetModPlayer<AvalonPlayer>().OpenLocks = true;
    }
}
