using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Tools.PreHardmode;

public class Breakdawn : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHamaxe(70, 160, 26, 3f, 24, 24, useTurn: false);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.TheBreaker)
			.AddIngredient(ModContent.ItemType<Blueshift>())
			.AddIngredient(ModContent.ItemType<JungleAxe>())
			.AddIngredient(ItemID.MoltenHamaxe)
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe()
			.AddIngredient(ItemID.FleshGrinder)
			.AddIngredient(ModContent.ItemType<Blueshift>())
			.AddIngredient(ModContent.ItemType<JungleAxe>())
			.AddIngredient(ItemID.MoltenHamaxe)
			.AddTile(TileID.DemonAltar)
			.Register();

		CreateRecipe()
			.AddIngredient(ModContent.ItemType<MucusHammer>())
			.AddIngredient(ModContent.ItemType<Blueshift>())
			.AddIngredient(ModContent.ItemType<JungleAxe>())
			.AddIngredient(ItemID.MoltenHamaxe)
			.AddTile(TileID.DemonAltar)
			.Register();
	}
	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory && !player.controlUseItem)
			{
				SoundEngine.PlaySound(SoundID.Unlock, player.position);
				int pfix = Item.prefix;
				Item.ChangeItemType(ModContent.ItemType<Breakdawn3x3>());
				Item.Prefix(pfix);
			}
		}
		//if (player.whoAmI == Main.myPlayer && player.ItemAnimationJustStarted)
		//{
		//    Point tilePosPoint = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
		//    int tileType = Main.tile[tilePosPoint.X, tilePosPoint.Y].TileType;
		//    int dmgAmt = (int)(Item.axe * 1.2f);
		//    bool isAxeableTile = Main.tileAxe[tileType];
		//    if (isAxeableTile)
		//    {
		//        if (!WorldGen.CanKillTile(tilePosPoint.X, tilePosPoint.Y))
		//        {
		//            dmgAmt = 0;
		//        }
		//        //Main.NewText(player.hitTile.AddDamage(tileType, dmgAmt));
		//        if (player.hitTile.AddDamage(tileType, dmgAmt) >= 100)
		//        {
		//            player.ClearMiningCacheAt(tilePosPoint.X, tilePosPoint.Y, 1);
		//            bool flag = player.IsBottomOfTreeTrunkNoRoots(tilePosPoint.X, tilePosPoint.Y);

		//            // kill the tile, with mp support
		//            WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y);
		//            if (Main.netMode == NetmodeID.MultiplayerClient)
		//            {
		//                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y);
		//            }

		//            // replant the sapling
		//            if (flag)
		//            {
		//                player.TryReplantingTree(tilePosPoint.X, tilePosPoint.Y);
		//            }
		//            player.hitTile.Clear(tileType);
		//        }
		//        else
		//        {
		//            if (tileType is TileID.TreeAmber or TileID.TreeAmethyst or TileID.TreeDiamond or TileID.TreeEmerald or TileID.TreeRuby or TileID.TreeSapphire or TileID.TreeTopaz)
		//            {
		//                WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y);
		//                if (Main.netMode == NetmodeID.MultiplayerClient)
		//                {
		//                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y);
		//                }
		//            }
		//            else
		//            {
		//                WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y, fail: true);
		//                if (Main.netMode == NetmodeID.MultiplayerClient)
		//                {
		//                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y, 1f);
		//                }
		//            }
		//        }
		//        if (dmgAmt != 0)
		//        {
		//            player.hitTile.Prune();
		//        }
		//        player.ApplyItemTime(Item);
		//    }
		//}
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		if (Main.rand.NextBool(5))
			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite, player.direction * 2, 0f, 150, default, 1.4f);

		Dust dust = Dust.NewDustDirect
		(
			new Vector2(hitbox.X, hitbox.Y),
			hitbox.Width,
			hitbox.Height,
			DustID.Shadowflame,
			player.velocity.X * 0.2f + (player.direction * 3),
			player.velocity.Y * 0.2f,
			100,
			default,
			1.2f
		);

		dust.noGravity = true;
		dust.velocity.X /= 2f;
		dust.velocity.Y /= 2f;
	}
	//public override bool? UseItem(Player player)
	//{
	//    if (player.whoAmI == Main.myPlayer && player.controlUseItem)
	//    {
	//        Point tilePosPoint = player.GetModPlayer<AvalonPlayer>().MousePosition.ToTileCoordinates();
	//        int tileType = Main.tile[tilePosPoint.X, tilePosPoint.Y].TileType;
	//        int dmgAmt = (int)(Item.axe / 2 * 1.2f);
	//        if (Main.tileAxe[tileType])
	//        {
	//            if (player.hitTile.AddDamage(tileType, dmgAmt) >= 100)
	//            {
	//                MethodInfo clearCache = typeof(Player).GetMethod("ClearMiningCacheAt", BindingFlags.NonPublic | BindingFlags.Instance);
	//                clearCache.Invoke(player, new object[] { tilePosPoint.X, tilePosPoint.Y, 1 });

	//                MethodInfo bottomOfTree = typeof(Player).GetMethod("IsBottomOfTreeTrunkNoRoots", BindingFlags.NonPublic | BindingFlags.Instance);
	//                bool flag = (bool)bottomOfTree.Invoke(player, new object[] { tilePosPoint.X, tilePosPoint.Y });
	//                WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y);
	//                if (Main.netMode == NetmodeID.MultiplayerClient)
	//                {
	//                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y);
	//                }
	//                if (flag)
	//                {
	//                    MethodInfo tryReplantingTree = typeof(Player).GetMethod("TryReplantingTree", BindingFlags.NonPublic | BindingFlags.Instance);
	//                    tryReplantingTree.Invoke(player, new object[] { tilePosPoint.X, tilePosPoint.Y });
	//                }
	//            }
	//            else
	//            {
	//                WorldGen.KillTile(tilePosPoint.X, tilePosPoint.Y, fail: true);
	//                if (Main.netMode == NetmodeID.MultiplayerClient)
	//                {
	//                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, tilePosPoint.X, tilePosPoint.Y, 1f);
	//                }
	//            }
	//            if (dmgAmt != 0)
	//            {
	//                player.hitTile.Prune();
	//            }
	//        }
	//    }
	//    return null;
	//}
}
public class BreakdawnHook : ModHook
{
	protected override void Apply()
	{
		//IL_WorldGen.KillTile_GetItemDrops += IL_WorldGen_KillTile_GetItemDrops;
	}

	private void IL_WorldGen_KillTile_GetItemDrops(MonoMod.Cil.ILContext il)
	{
		Hooks.Utilities.AddAlternativeIdChecks(il, (ushort)ItemID.AcornAxe, id => ItemID.Sets.Factory.CreateBoolSet(ModContent.ItemType<Breakdawn>())[id]);
	}
}
public class Breakdawn3x3 : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToHamaxe(0, 0, 26, 3f, 24, 24, useTurn: false);
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(silver: 54);
	}
	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen && !Main.playerInventory)
			{
				SoundEngine.PlaySound(SoundID.Unlock, player.position);
				int pfix = Item.prefix;
				Item.ChangeItemType(ModContent.ItemType<Breakdawn>());
				Item.Prefix(pfix);
			}
			if (player.controlUseItem)
			{
				if (player.IsInTileInteractionRange(Player.tileTargetX, Player.tileTargetY, TileReachCheckSettings.Simple))
				{
					Point p = Main.MouseWorld.ToTileCoordinates();
					for (int x = p.X - 1; x <= p.X + 1; x++)
					{
						for (int y = p.Y - 1; y <= p.Y + 1; y++)
						{
							if (Main.tile[x, y].HasTile && Main.tileAxe[Main.tile[x, y].TileType])
							{
								if (!TileID.Sets.BasicChest[Main.tile[x, y].TileType])
								{
									WorldGen.KillTile(x, y);
									if (Main.netMode != NetmodeID.SinglePlayer)
									{
										NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 0, x, y);
									}
								}
							}
							if (Main.tile[x, y].WallType > 0)
							{
								WorldGen.KillWall(x, y);
								if (Main.netMode != NetmodeID.SinglePlayer)
								{
									NetMessage.SendData(MessageID.TileManipulation, -1, -1, NetworkText.Empty, 2, x, y);
								}
							}
						}
					}
				}
			}
		}
	}
	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		if (Main.rand.NextBool(5))
			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Demonite, player.direction * 2, 0f, 150, default, 1.4f);

		Dust dust = Dust.NewDustDirect
		(
			new Vector2(hitbox.X, hitbox.Y),
			hitbox.Width,
			hitbox.Height,
			DustID.Shadowflame,
			player.velocity.X * 0.2f + (player.direction * 3),
			player.velocity.Y * 0.2f,
			100,
			default,
			1.2f
		);

		dust.noGravity = true;
		dust.velocity.X /= 2f;
		dust.velocity.Y /= 2f;
	}
}
