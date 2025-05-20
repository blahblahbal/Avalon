using Avalon.Tiles.Furniture;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Avalon.Items.Tools;

public class SonicScrewdriverMkIII : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 32;
		Item.height = 36;
		Item.useTime = 70;
		Item.useAnimation = 70;
		Item.useStyle = ItemUseStyleID.Thrust;
		Item.scale = 0.7f;
		Item.rare = ItemRarityID.Cyan;
		Item.value = Item.sellPrice(0, 10);
		Item.UseSound = new SoundStyle($"{nameof(Avalon)}/Sounds/Item/SonicScrewdriver");
	}

	public override bool? UseItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftControl))
			{
				Point c = Main.MouseWorld.ToTileCoordinates();

				Tile tile = Main.tile[c];
				int xpos = Player.tileTargetX - (tile.TileFrameX / 18 % 2);
				int ypos = Player.tileTargetY - (tile.TileFrameY / 18);

				bool isLocked = Chest.IsLocked(xpos, ypos);

				if (isLocked)
				{
					if (tile.TileType == ModContent.TileType<LockedChests>()) // Temporary until the unlock method for this tile has been changed to use the tmod method
					{
						if (LockedChests.Unlock(xpos, ypos))
						{
							if (Main.netMode != NetmodeID.SinglePlayer)
							{
								Network.SyncLockUnlock.SendPacket(Network.SyncLockUnlock.Unlock, xpos, ypos);
							}
						}
					}
					else
					{
						// Allows us to open chests that otherwise would require plantera to be defeated, unfortunately not possible to allow every single modded chest to be opened (would not be able to ensure their TileLoader.UnlockChest code functions as intended)
						// Does not work in MP
						bool returnflag = false;
						if (!NPC.downedPlantBoss)
						{
							returnflag = true;
							NPC.downedPlantBoss = true;
						}
						if (Chest.Unlock(xpos, ypos))
						{
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 1f, xpos, ypos);
							}
						}
						if (returnflag)
						{
							NPC.downedPlantBoss = false;
						}
					}
				}
				else if (Chest.FindChest(xpos, ypos) >= 0) // Not strictly needed, only prevents the game from playing the lock clicking sound when you click off of a chest tile
				{
					int style = TileObjectData.GetTileStyle(tile);
					if ((style == 0 || style >= 7 && style <= 16 || style == 48) && tile.TileType == TileID.Containers) // Temporary until the lock method for this tile has been changed to use the tmod method (hopefully possible)
					{
						if (LockedChests.Lock(xpos, ypos))
						{
							if (Main.netMode != NetmodeID.SinglePlayer)
							{
								Network.SyncLockUnlock.SendPacket(0, xpos, ypos);
								NetMessage.SendTileSquare(-1, xpos, ypos);
							}
						}
					}
					else
					{
						// Does not work in MP
						bool returnflag = false;
						if (!NPC.downedPlantBoss)
						{
							returnflag = true;
							NPC.downedPlantBoss = true;
						}
						if (Chest.Lock(xpos, ypos))
						{
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.SendData(MessageID.LockAndUnlock, -1, -1, null, player.whoAmI, 3, xpos, ypos);
							}
						}
						if (returnflag)
						{
							NPC.downedPlantBoss = false;
						}
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
