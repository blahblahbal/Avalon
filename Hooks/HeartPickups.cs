using Avalon.Common;
using Avalon.Common.Players;
using Avalon.Items.Other;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class HeartPickups : ModHook
    {
        protected override void Apply()
        {
            On_Item.NewItem_Inner += OnItemNewItem;
            On_Player.PickupItem += OnPickupItem;
        }

		private static Item OnPickupItem(On_Player.orig_PickupItem orig, Player self, int playerIndex, int worldItemArrayIndex, Item itemToPickUp)
		{
			if (itemToPickUp.type == ItemID.Heart || itemToPickUp.type == ItemID.CandyApple || itemToPickUp.type == ItemID.CandyCane)
			{
				SoundEngine.PlaySound(SoundID.Grab, self.position);
				self.Heal((int)Math.Round(20f * self.GetModPlayer<AvalonPlayer>().HeartPickupValueMultiplier));
				itemToPickUp = new Item();
			}
			return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
		}

		private static int OnItemNewItem(On_Item.orig_NewItem_Inner orig, IEntitySource source, int X, int Y,
            int Width, int Height, Item itemToClone, int Type, int Stack = 1, bool noBroadcast = false, int pfix = 0,
            bool noGrabDelay = false, bool reverseLookup = false)
        {
            int p = Player.FindClosest(new Vector2(X, Y), Width, Height);
            if (p != -1)
            {
                if (Type == ItemID.Heart && source is not EntitySource_TileBreak && source is not EntitySource_Wiring)
                {
					if (Main.player[p].ConsumedLifeFruit is > 0 and < 19 && Main.rand.Next(Main.player[p].ConsumedLifeFruit, 21) == 20)
					//if (Main.player[p].statLifeMax2 > 400 && Main.player[p].statLifeMax2 < 500 && Main.rand.Next((Main.player[p].statLifeMax2 - 400) / 5, 21) == 20)
                    {
                        if (Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart && Main.rand.NextBool(10) ||
                            Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.rand.NextBool(20) ||
                            (Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart &&
                            Main.rand.Next(100) < 15))
                        {
                            Type = ModContent.ItemType<PlatinumHeart>();
                        }
                        else Type = ModContent.ItemType<GoldHeart>();
                    }
                    else if (Main.player[p].statLifeMax2 >= 500 && Main.player[p].ConsumedLifeFruit >= 20)
                    {
                        if (Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart && Main.rand.NextBool(10) ||
                            Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.rand.NextBool(20) ||
                            (Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart &&
                            Main.rand.Next(100) < 15))
                        {
                            Type = ModContent.ItemType<PlatinumHeart>();
                        }
                        else Type = ModContent.ItemType<GoldHeart>();
                    }
                }
                if (Type == ItemID.CandyApple && source is not EntitySource_TileBreak && source is not EntitySource_Wiring)
                {
					//if (Main.player[p].statLifeMax2 > 400 && Main.player[p].statLifeMax2 < 500 && Main.rand.Next((Main.player[p].statLifeMax2 - 400) / 5, 21) == 20)
					if (Main.player[p].ConsumedLifeFruit is > 0 and < 19 && Main.rand.Next(Main.player[p].ConsumedLifeFruit, 21) == 20)
					{
                        if (Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart && Main.rand.NextBool(10) ||
                            Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.rand.NextBool(20) ||
                            (Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart &&
                            Main.rand.Next(100) < 15))
                        {
                            Type = ModContent.ItemType<PlatinumApple>();
                        }
                        else Type = ModContent.ItemType<GoldApple>();
                    }
                    else if (Main.player[p].statLifeMax2 >= 500 && Main.player[p].ConsumedLifeFruit >= 20)
                    {
                        if (Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart && Main.rand.NextBool(10) ||
                            Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.rand.NextBool(20) ||
                            (Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart &&
                            Main.rand.Next(100) < 15))
                        {
                            Type = ModContent.ItemType<PlatinumApple>();
                        }
                        else Type = ModContent.ItemType<GoldApple>();
                    }
                }
                if (Type == ItemID.CandyCane && source is not EntitySource_TileBreak && source is not EntitySource_Wiring)
                {
					//if (Main.player[p].statLifeMax2 > 400 && Main.player[p].statLifeMax2 < 500 && Main.rand.Next((Main.player[p].statLifeMax2 - 400) / 5, 21) == 20)
					if (Main.player[p].ConsumedLifeFruit is > 0 and < 19 && Main.rand.Next(Main.player[p].ConsumedLifeFruit, 21) == 20)
					{
                        if (Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart && Main.rand.NextBool(10) ||
                            Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.rand.NextBool(20) ||
                            (Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart &&
                            Main.rand.Next(100) < 15))
                        {
                            Type = ModContent.ItemType<PlatinumCandyCane>();
                        }
                        else Type = ModContent.ItemType<GoldCandyCane>();
                    }
                    else if (Main.player[p].statLifeMax2 >= 500 && Main.player[p].ConsumedLifeFruit >= 20)
                    {
                        if (Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart && Main.rand.NextBool(10) ||
                            Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.rand.NextBool(20) ||
                            (Main.player[p].GetModPlayer<AvalonPlayer>().HeartGolem && Main.player[p].GetModPlayer<AvalonPlayer>().EtherealHeart &&
                            Main.rand.Next(100) < 15))
                        {
                            Type = ModContent.ItemType<PlatinumCandyCane>();
                        }
                        else Type = ModContent.ItemType<GoldCandyCane>();
                    }
                }
            }
            return orig(source, X, Y, Width, Height, itemToClone, Type, Stack, noBroadcast, pfix, noGrabDelay, reverseLookup);
        }
    }
}
