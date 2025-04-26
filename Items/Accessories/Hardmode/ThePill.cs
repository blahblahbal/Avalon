using Avalon.Buffs;
using Avalon.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Accessories.Hardmode
{
	public class ThePill : ModItem
	{
		public const float LifeBonusAmount = 1.5f;
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.PutridScent);
		}
		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<AvalonPlayer>().ThePill = true;
			player.potionDelayTime = (int)(player.potionDelayTime * 1.15);
			player.restorationDelayTime = (int)(player.restorationDelayTime * 1.15);
			player.mushroomDelayTime = (int)(player.mushroomDelayTime * 1.15);
		}
	}
	public class ThePillGlobalItem : GlobalItem
	{
		public override void GetHealLife(Item item, Player player, bool quickHeal, ref int healValue)
		{
			if (player.GetModPlayer<AvalonPlayer>().ThePill) healValue = (int)(healValue * ThePill.LifeBonusAmount);
		}
		public override bool? UseItem(Item item, Player player)
		{
			if (player.GetModPlayer<AvalonPlayer>().ThePill && item.buffType == ModContent.BuffType<Rejuvenation>())
			{
				player.buffTime[player.FindBuffIndex(ModContent.BuffType<Rejuvenation>())] = (int)(player.buffTime[player.FindBuffIndex(ModContent.BuffType<Rejuvenation>())] * ThePill.LifeBonusAmount);
			}
			return base.UseItem(item, player);
		}
	}
	public class ThePillPlayer : ModPlayer
	{
		public bool DoUpdateDebuffTime;
		public override void PostUpdate()
		{
			DoUpdateDebuffTime = Player.GetModPlayer<AvalonPlayer>().ThePill;
		}
		public override void PostUpdateEquips()
		{
			if (DoUpdateDebuffTime != Player.GetModPlayer<AvalonPlayer>().ThePill)
			{
				//Main.NewText(Player.GetModPlayer<AvalonPlayer>().ThePill ? $"[i:{ModContent.ItemType<ThePill>()}]True" : $"[i:{ModContent.ItemType<ThePill>()}]False", Player.GetModPlayer<AvalonPlayer>().ThePill ? Color.Lime : Color.DarkRed);
				for (int i = 0; i < Player.MaxBuffs; i++)
				{
					if (Main.debuff[Player.buffType[i]] && Player.buffType[i] != BuffID.PotionSickness)
					{
						Player.buffTime[i] = Player.GetModPlayer<AvalonPlayer>().ThePill ? (int)(Player.buffTime[i] * 0.8f) : (int)(Player.buffTime[i] / 0.8f);
					}
				}
			}
		}
	}
}

