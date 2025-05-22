using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

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
			if (!(player.position.X / 16f - Player.tileRangeX - Item.tileBoost - player.blockRange <= Player.tileTargetX) || !((player.position.X + player.width) / 16f + Player.tileRangeX + Item.tileBoost - 1f + player.blockRange >= Player.tileTargetX) || !(player.position.Y / 16f - Player.tileRangeY - Item.tileBoost - player.blockRange <= Player.tileTargetY) || !((player.position.Y + player.height) / 16f + Player.tileRangeY + Item.tileBoost - 2f + player.blockRange >= Player.tileTargetY) || !player.ItemTimeIsZero || player.itemAnimation <= 0 || !player.controlUseItem)
			{
				return true;
			}
			Point c = Main.MouseWorld.ToTileCoordinates();

			Tile tile = Main.tile[c];
			int xpos = Player.tileTargetX - (tile.TileFrameX / 18 % 2);
			int ypos = Player.tileTargetY - (tile.TileFrameY / 18);

			bool isLocked = Chest.IsLocked(xpos, ypos);

			if (isLocked)
			{
				// Allows us to open chests that otherwise would require plantera to be defeated, unfortunately not possible to allow every single modded chest to be opened (would not be able to ensure their TileLoader.UnlockChest code functions as intended)
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
						Network.SyncLockUnlock.SendPacket(Network.SyncLockUnlock.Unlock, xpos, ypos, -1, player.whoAmI); // Custom net message which temporarily sets NPC.downedPlantBoss on server and other clients
					}
				}
				if (returnflag)
				{
					NPC.downedPlantBoss = false;
				}
			}
			else if (Chest.FindChest(xpos, ypos) >= 0) // Not strictly needed, only prevents the game from playing the lock clicking sound when you click off of a chest tile
			{
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
						Network.SyncLockUnlock.SendPacket(Network.SyncLockUnlock.Lock, xpos, ypos, -1, player.whoAmI);
					}
				}
				if (returnflag)
				{
					NPC.downedPlantBoss = false;
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
