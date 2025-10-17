using Avalon.Common.Extensions;
using Avalon.Common.Templates;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.Hardmode.DarklightLance;

public class DarklightLance : ModItem
{
	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[Item.type] = true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToSpear(ModContent.ProjectileType<DarklightLanceProjectile>(), 99, 5.5f, 26, 4f, true);
		Item.rare = ItemRarityID.Yellow;
		Item.value = Item.sellPrice(0, 40);

	}
	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[Item.shoot] < 1;
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.DarkLance)
			.AddIngredient(ItemID.Gungnir)
			.AddIngredient(ItemID.SoulofFright)
			.AddIngredient(ItemID.DarkShard)
			.AddIngredient(ItemID.LightShard);
	}
}
public class DarklightLanceProjectile : SpearTemplate
{
	public override LocalizedText DisplayName => ModContent.GetInstance<DarklightLance>().DisplayName;
	protected override float HoldoutRangeMax => 200;
	protected override float HoldoutRangeMin => 40;
	public override void PostAI()
	{
		int S = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame);
		Main.dust[S].noGravity = true;
		Main.dust[S].velocity = Projectile.velocity * 2;
		Main.dust[S].fadeIn = Main.rand.NextFloat(0, 1.5f);
		int H = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.HallowedWeapons);
		Main.dust[H].noGravity = true;
		Main.dust[H].velocity = Projectile.velocity * -3;
		Main.dust[H].fadeIn = Main.rand.NextFloat(0, 1.5f);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
		particleOrchestraSettings.PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
		ParticleOrchestraSettings settings = particleOrchestraSettings;
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
		target.AddBuff(BuffID.ShadowFlame, 360);
	}
	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (!info.PvP)
		{
			return;
		}
		ParticleOrchestraSettings particleOrchestraSettings = default(ParticleOrchestraSettings);
		particleOrchestraSettings.PositionInWorld = Main.rand.NextVector2FromRectangle(target.Hitbox);
		ParticleOrchestraSettings settings = particleOrchestraSettings;
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.NightsEdge, settings, Projectile.owner);
		ParticleOrchestrator.RequestParticleSpawn(clientOnly: false, ParticleOrchestraType.TrueExcalibur, settings, Projectile.owner);
		target.AddBuff(BuffID.ShadowFlame, 360);
	}
}
