using Avalon.Projectiles.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.PreHardmode
{
	public class Smogscreen : ModItem
	{
		public SoundStyle gas = new("Terraria/Sounds/Item_34")
		{
			Volume = 0.5f,
			Pitch = -0.5f,
			PitchVariance = 0.1f,
			MaxInstances = 10,
		};
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
		}
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Vilethorn);
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ModContent.ProjectileType<Smog>();
			Item.useAnimation = 40;
			Item.useTime = 10;
			Item.damage = 8;
			Item.consumeAmmoOnFirstShotOnly = true;
			Item.shootSpeed = 6.5f;
			Item.ArmorPenetration = 7;
			Item.UseSound = gas;
		}
	}
}
