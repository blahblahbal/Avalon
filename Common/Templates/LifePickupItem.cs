using Avalon.Common.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Templates;

public abstract class LifePickupItem : ModItem
{
	public virtual float HealAmount => 0f;
	public override void SetStaticDefaults()
	{
		ItemID.Sets.IgnoresEncumberingStone[Type] = true;
		ItemID.Sets.IsAPickup[Type] = true;
	}
	public override void SetDefaults()
	{
		Item.Size = new Vector2(12);
	}
	public override bool GrabStyle(Player player)
	{
		if (player.lifeMagnet)
		{
			float speed = 15;
			float acc = 5;
			Vector2 vector = new Vector2(Item.position.X + (float)(Item.width / 2), Item.position.Y + (float)(Item.height / 2));
			float num = player.Center.X - vector.X;
			float num2 = player.Center.Y - vector.Y;
			float num3 = (float)Math.Sqrt(num * num + num2 * num2);
			num3 = speed / num3;
			num *= num3;
			num2 *= num3;
			Item.velocity.X = (Item.velocity.X * (float)(acc - 1) + num) / (float)acc;
			Item.velocity.Y = (Item.velocity.Y * (float)(acc - 1) + num2) / (float)acc;
			return true;
		}
		return base.GrabStyle(player);
	}
	public override bool OnPickup(Player player)
	{
		SoundEngine.PlaySound(SoundID.Grab, player.position);
		player.Heal((int)Math.Round(HealAmount * player.GetModPlayer<AvalonPlayer>().HeartPickupValueMultiplier));
		return false;
	}
	public override void GrabRange(Player player, ref int grabRange)
	{
		grabRange = player.GetItemGrabRange(ContentSamples.ItemsByType[ItemID.Heart]);
	}
	public override bool CanPickup(Player player)
	{
		return true;
	}
}
