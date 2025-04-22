using Avalon.Common.Players;
using Avalon.Dusts;
using Avalon.Items.Placeable.Tile;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Furniture;

public class StarTorch : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.ResearchUnlockCount = 100;
		ItemID.Sets.SingleUseInGamepad[Type] = true;
		ItemID.Sets.Torches[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToTorch(ModContent.TileType<Tiles.Furniture.StarTorch>(), 0, true);
		Item.value = Item.sellPrice(0, 0, 0, 40);
	}
	public override void AddRecipes()
	{
		CreateRecipe(2)
			.AddIngredient(ItemID.FallenStar)
			.AddIngredient(ItemID.SunplateBlock)
			.Register();

		CreateRecipe(2)
			.AddIngredient(ItemID.FallenStar)
			.AddIngredient(ModContent.ItemType<MoonplateBlock>())
			.Register();

		CreateRecipe(2)
			.AddIngredient(ItemID.FallenStar)
			.AddIngredient(ModContent.ItemType<DuskplateBlock>())
			.Register();
	}
	public override void UseAnimation(Player player)
	{
	}
	public override bool? UseItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI && Main.mouseLeft)
		{
			Point tilePos = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
			bool inrange = player.IsInTileInteractionRange(tilePos.X, tilePos.Y, TileReachCheckSettings.Simple);
			if (inrange)
			{

				if (Main.tile[tilePos.X, tilePos.Y].HasTile)
				{
					return null;
				}
				else
				{
					if ((!Main.tile[tilePos.X, tilePos.Y - 1].HasTile || !Main.tileSolid[Main.tile[tilePos.X, tilePos.Y - 1].TileType]) &&
						(!Main.tile[tilePos.X - 1, tilePos.Y].HasTile || !Main.tileSolid[Main.tile[tilePos.X - 1, tilePos.Y].TileType]) &&
						(!Main.tile[tilePos.X, tilePos.Y + 1].HasTile || !Main.tileSolid[Main.tile[tilePos.X, tilePos.Y + 1].TileType]) &&
						(!Main.tile[tilePos.X + 1, tilePos.Y].HasTile || !Main.tileSolid[Main.tile[tilePos.X + 1, tilePos.Y].TileType]))
					{
						Terraria.Tile t = Main.tile[tilePos.X, tilePos.Y];
						t.HasTile = true;
						t.TileType = (ushort)Item.createTile;
						t.TileFrameX = 0;
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, tilePos.X, tilePos.Y, Item.createTile);
						}
						SoundEngine.PlaySound(SoundID.Dig, tilePos.ToWorldCoordinates());
						return true;
					}
					else if (Main.tile[tilePos.X, tilePos.Y + 1].HasTile && Main.tileSolid[Main.tile[tilePos.X, tilePos.Y + 1].TileType])
					{
						Terraria.Tile t = Main.tile[tilePos.X, tilePos.Y + 1];
						t.HasTile = true;
						t.TileType = (ushort)Item.createTile;
						t.TileFrameX = 0;
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, tilePos.X, tilePos.Y, Item.createTile);
						}
						SoundEngine.PlaySound(SoundID.Dig, tilePos.ToWorldCoordinates());
						return true;
					}
					else if (Main.tile[tilePos.X - 1, tilePos.Y].HasTile && Main.tileSolid[Main.tile[tilePos.X - 1, tilePos.Y].TileType])
					{
						Terraria.Tile t = Main.tile[tilePos.X - 1, tilePos.Y];
						t.HasTile = true;
						t.TileType = (ushort)Item.createTile;
						t.TileFrameX = 22;
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, tilePos.X, tilePos.Y, Item.createTile);
						}
						SoundEngine.PlaySound(SoundID.Dig, tilePos.ToWorldCoordinates());
						return true;
					}
					else if (Main.tile[tilePos.X + 1, tilePos.Y].HasTile && Main.tileSolid[Main.tile[tilePos.X + 1, tilePos.Y].TileType])
					{
						Terraria.Tile t = Main.tile[tilePos.X + 1, tilePos.Y];
						t.HasTile = true;
						t.TileType = (ushort)Item.createTile;
						t.TileFrameX = 44;
						if (Main.netMode != NetmodeID.SinglePlayer)
						{
							NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, tilePos.X, tilePos.Y, Item.createTile);
						}
						SoundEngine.PlaySound(SoundID.Dig, tilePos.ToWorldCoordinates());
						return true;
					}
				}
			}
		}
		return base.UseItem(player);
	}
	public override void HoldItem(Player player)
	{
		//if (!player.wet)
		{
			if (Main.rand.NextBool(player.itemAnimation > 0 ? 28 : 120))
			{
				int d = Dust.NewDust(new Vector2(player.itemLocation.X + (player.direction == 1 ? 6 : -16), player.itemLocation.Y - 14f * player.gravDir), 4, 4, ModContent.DustType<BrownTorchDust>(), 0f, 0f, 100);
				if (!Main.rand.NextBool(3))
				{
					Main.dust[d].noGravity = true;
				}
				Main.dust[d].velocity *= 0.3f;
				Main.dust[d].velocity.Y -= 1.5f;
				Main.dust[d].position = player.RotatedRelativePoint(Main.dust[d].position);
			}
			Vector2 position = player.RotatedRelativePoint(new Vector2(player.itemLocation.X + 12f * player.direction + player.velocity.X, player.itemLocation.Y - 14f + player.velocity.Y), true);
			Lighting.AddLight(position, 1f, 0.945f, 0.2f);
		}
	}

	public override void PostUpdate()
	{
		//if (!Item.wet)
		{
			Lighting.AddLight((int)((Item.position.X + Item.width / 2) / 16f), (int)((Item.position.Y + Item.height / 2) / 16f), 1f, 0.945f, 0.2f);
		}
	}
}
//public class StarTorchHook : ModHook
//{
//    protected override void Apply()
//    {
//        On_Player.PlaceThing_Tiles_PlaceIt += On_Player_PlaceThing_Tiles_PlaceIt;
//    }

//    private TileObject On_Player_PlaceThing_Tiles_PlaceIt(On_Player.orig_PlaceThing_Tiles_PlaceIt orig, Player self, bool newObjectType, TileObject data, int tileToCreate)
//    {

//    }
//}
