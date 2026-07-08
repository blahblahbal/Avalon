using Avalon.Buffs.AdvancedBuffs;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Data.Sets;

[ReinitializeDuringResizeArrays]
public static class BuffSets
{
	public static bool[] ImmunityCannotBeRemovedFromEnemies = BuffID.Sets.Factory.CreateNamedSet("ImmunityCannotBeRemovedFromEnemies")
		.Description("Buffs in this set won't have enemies's immunities cleared from them while afflicted by Infernal Judgement")
		.RegisterBoolSet(
		BuffID.Confused
		);
	public static bool[] ImmunityCannotBeRemovedFromPlayers = BuffID.Sets.Factory.CreateNamedSet("ImmunityCannotBeRemovedFromPlayers")
	.Description("Buffs in this set won't have players's immunities cleared from them while afflicted by Infernal Judgement")
	.RegisterBoolSet();

	public static bool[] Elixir = BuffID.Sets.Factory.CreateNamedSet("Elixir").RegisterBoolSet(
        ModContent.BuffType<AdvAmmoReservation>(),
        ModContent.BuffType<AdvArchery>(),
        ModContent.BuffType<AdvBattle>(),
        ModContent.BuffType<AdvBloodCast>(),
        ModContent.BuffType<AdvBuilder>(),
        ModContent.BuffType<AdvCalming>(),
        ModContent.BuffType<AdvCrate>(),
        ModContent.BuffType<AdvCrimson>(),
        ModContent.BuffType<AdvDangersense>(),
        ModContent.BuffType<AdvEndurance>(),
        ModContent.BuffType<AdvFeatherfall>(),
        ModContent.BuffType<AdvFishing>(),
        ModContent.BuffType<AdvFlipper>(),
        ModContent.BuffType<AdvForceField>(),
        ModContent.BuffType<AdvFury>(),
        ModContent.BuffType<AdvGauntlet>(),
        ModContent.BuffType<AdvGills>(),
        ModContent.BuffType<AdvGPS>(),
        ModContent.BuffType<AdvGravitation>(),
        ModContent.BuffType<AdvHeartreach>(),
        ModContent.BuffType<AdvHeartsick>(),
        ModContent.BuffType<AdvHunter>(),
        ModContent.BuffType<AdvInferno>(),
        ModContent.BuffType<AdvInvincibility>(),
        ModContent.BuffType<AdvInvisibility>(),
        ModContent.BuffType<AdvIronskin>(),
        ModContent.BuffType<AdvLeaping>(),
        ModContent.BuffType<AdvLifeforce>(),
        ModContent.BuffType<AdvLuck>(),
        ModContent.BuffType<AdvMagicPower>(),
        ModContent.BuffType<AdvMagnet>(),
        ModContent.BuffType<AdvManaRegeneration>(),
        ModContent.BuffType<AdvMining>(),
        ModContent.BuffType<AdvNightOwl>(),
        ModContent.BuffType<AdvNinja>(),
        ModContent.BuffType<AdvObsidianSkin>(),
        ModContent.BuffType<AdvRage>(),
        ModContent.BuffType<AdvRegeneration>(),
        ModContent.BuffType<AdvRogue>(),
        ModContent.BuffType<AdvShadow>(),
        ModContent.BuffType<AdvShine>(),
        ModContent.BuffType<AdvShockwave>(),
        ModContent.BuffType<AdvSonar>(),
        ModContent.BuffType<AdvSpelunker>(),
        ModContent.BuffType<AdvStarbright>(),
        ModContent.BuffType<AdvStrength>(),
        ModContent.BuffType<AdvSummoning>(),
        ModContent.BuffType<AdvSupersonic>(),
        ModContent.BuffType<AdvSwiftness>(),
        ModContent.BuffType<AdvThorns>(),
        ModContent.BuffType<AdvTimeShift>(),
        ModContent.BuffType<AdvTitan>(),
        ModContent.BuffType<AdvTitanskin>(),
        ModContent.BuffType<AdvWarmth>(),
        ModContent.BuffType<AdvWaterWalking>(),
        ModContent.BuffType<AdvWisdom>(),
        ModContent.BuffType<AdvWrath>()
    );
}
