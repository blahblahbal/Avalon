using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Tiles.Hellcastle;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.HitTile;

namespace Avalon.Hooks;
internal class KillTileHooks : ModHook
{
	protected override void Apply()
	{
		IL_Player.PickTile += IL_Player_PickTile;
		On_HitTile.AddDamage += On_HitTile_AddDamage;
	}

	private void IL_Player_PickTile(ILContext il)
	{
		ILCursor c = new(il);
		ILLabel IL_030f = c.DefineLabel();
		int stloc = -1;

		c.GotoNext(MoveType.AfterLabel,
		x => x.MatchLdsflda<Main>("tile"),
		x => x.MatchLdarg(1),
		x => x.MatchLdarg(2),
		x => x.MatchCall<Tilemap>("get_Item"),
		x => x.MatchStloc(out stloc),
		x => x.MatchLdloca(stloc),
		x => x.MatchCall<Tile>("active")
		);
		c.EmitLdarg0(); // self
		c.EmitLdarg1(); // x
		c.EmitLdarg2(); // y
		c.EmitDelegate((Player self, int x, int y) =>
		{
			Tile t = Main.tile[x, y];
			if (TileID.Sets.Ore[t.TileType] && self.GetModPlayer<AvalonPlayer>().OreDupe && Data.Sets.TileSets.OresToChunks.ContainsKey(t.TileType))
			{
				int drop = Data.Sets.TileSets.OresToChunks[t.TileType];
				int stack = 1;
				if (Main.rand.NextBool(3))
				{
					stack = 2;
				}
				int a = Item.NewItem(WorldGen.GetItemSource_FromTileBreak(x, y), x * 16, y * 16, 16, 16, drop, stack);

				// below is just a copy of the vanilla code but with noItem set to true for KillTile and SendData
				bool flag = Main.tile[x, y].active();
				WorldGen.KillTile(x, y, noItem: true);
				if (!Main.dedServ && flag && !t.active())
				{
					AchievementsHelper.HandleMining();
				}
				if (Main.netMode == NetmodeID.MultiplayerClient)
				{
					NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 4, x, y);
				}
				return true;
			}
			return false;
		});
		c.EmitBrtrue(IL_030f); // branch to the instruction where 0 is pushed to the stack in preparation for AchievementsHelper.CurrentlyMining to be set to that value (essentially, skip to AchievementsHelper.CurrentlyMining = false;)
		c.GotoNext(MoveType.Before,
			x => x.MatchLdcI4(0),
			x => x.MatchCall<AchievementsHelper>("set_CurrentlyMining")
		);
		c.MarkLabel(IL_030f); // mark the label so that the branch instruction inserted above can actually locate the instruction to branch to
	}

	private int On_HitTile_AddDamage(On_HitTile.orig_AddDamage orig, HitTile self, int tileId, int damageAmount, bool updateAmount)
	{
		if (tileId is not < 0 and not > 500)
		{
			HitTileObject hitTileObject = self.data[tileId];
			if (Main.tile[hitTileObject.X, hitTileObject.Y].TileType == ModContent.TileType<UltraResistantWood>())
			{
				if (Main.CurrentPlayer.HeldItem.axe < 40)
				{
					damageAmount = 0;
				}
			}
		}
		return orig.Invoke(self, tileId, damageAmount, updateAmount);
	}
}
