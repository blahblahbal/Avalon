using Avalon.Common;
using Avalon.Common.Extensions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Ranged.Bows;

public class Thompson : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToGun(10, 1f, 10f, 10, 10, true, width: 44);
		//Item.reuseDelay = 5;
		Item.UseSound = null;
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 2);
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-12, 0);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		SoundEngine.PlaySound(SoundID.Item11 with { Volume = 0.9f, Pitch = 0.4f }, player.Center);
		velocity = AvalonUtils.GetShootSpread(velocity, position, Type, MathHelper.ToRadians(10), ammoExtraShootSpeedItemID: ItemID.MusketBall, random: true);
	}
}
