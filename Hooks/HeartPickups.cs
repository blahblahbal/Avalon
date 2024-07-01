using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Audio;
using Avalon.Items.Other;

namespace Avalon.Hooks
{
    internal class HeartPickups : ModHook
    {
        protected override void Apply()
        {
            On_Item.NewItem_Inner += OnItemNewItem;
            On_Player.PickupItem += OnPickupItem;
			On_Player.GetItemGrabRange += On_Player_GetItemGrabRange;
        }

		private int On_Player_GetItemGrabRange(On_Player.orig_GetItemGrabRange orig, Player self, Item item)
		{
			int range = orig.Invoke(self, item);
			if (item.type == ModContent.ItemType<GoldApple>() || item.type == ModContent.ItemType<GoldHeart>() || item.type == ModContent.ItemType<GoldCandyCane>() ||
				item.type == ModContent.ItemType<PlatinumApple>() || item.type == ModContent.ItemType<PlatinumHeart>() || item.type == ModContent.ItemType<PlatinumCandyCane>())
			{
				range += Item.lifeGrabRange;
			}
			return range;
		}

		private static Item OnPickupItem(On_Player.orig_PickupItem orig, Player self, int playerIndex, int worldItemArrayIndex, Item itemToPickUp)
        {
            if (self.GetModPlayer<AvalonPlayer>().Heartsick || self.GetModPlayer<AvalonPlayer>().HeartsickElixir)
            {
                if (itemToPickUp.type == ItemID.Heart || itemToPickUp.type == ItemID.CandyApple || itemToPickUp.type == ItemID.CandyCane)
                {
                    SoundEngine.PlaySound(SoundID.Grab, self.position);
                    int amt = 25;
                    if (self.GetModPlayer<AvalonPlayer>().HeartsickElixir) amt = 30;
                    self.Heal(amt);
                    itemToPickUp = new Item();
                }
                if (itemToPickUp.type == ModContent.ItemType<GoldHeart>() || itemToPickUp.type == ModContent.ItemType<GoldApple>() || itemToPickUp.type == ModContent.ItemType<GoldCandyCane>())
                {
                    SoundEngine.PlaySound(SoundID.Grab, self.position);
                    int amt = 40;
                    if (self.GetModPlayer<AvalonPlayer>().HeartsickElixir) amt = 45;
                    self.Heal(amt);
                    itemToPickUp = new Item();
                }
                if (itemToPickUp.type == ModContent.ItemType<PlatinumHeart>() || itemToPickUp.type == ModContent.ItemType<PlatinumApple>() || itemToPickUp.type == ModContent.ItemType<PlatinumCandyCane>())
                {
                    SoundEngine.PlaySound(SoundID.Grab, self.position);
                    int amt = 57;
                    if (self.GetModPlayer<AvalonPlayer>().HeartsickElixir) amt = 60;
                    self.Heal(amt);
                    itemToPickUp = new Item();
                }
            }
            else
            {
                if (itemToPickUp.type == ModContent.ItemType<GoldHeart>() || itemToPickUp.type == ModContent.ItemType<GoldApple>() || itemToPickUp.type == ModContent.ItemType<GoldCandyCane>())
                {
                    SoundEngine.PlaySound(SoundID.Grab, self.position);
                    self.Heal(35);
                    itemToPickUp = new Item();
                }
                if (itemToPickUp.type == ModContent.ItemType<PlatinumHeart>() || itemToPickUp.type == ModContent.ItemType<PlatinumApple>() || itemToPickUp.type == ModContent.ItemType<PlatinumCandyCane>())
                {
                    SoundEngine.PlaySound(SoundID.Grab, self.position);
                    self.Heal(50);
                    itemToPickUp = new Item();
                }
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
