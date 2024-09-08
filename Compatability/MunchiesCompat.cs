using Avalon.Common.Players;
using Avalon.Items.Consumables;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.Compatability;

internal class MunchiesCompat : ModSystem
{
	public override void PostSetupContent()
	{
		if (ExxoAvalonOrigins.Munchies == null)
		{
			return;
		}

		ExxoAvalonOrigins.Munchies.Call(
			"AddMultiUseConsumable", Mod, "1.4", ExxoAvalonOrigins.Mod.Find<ModItem>("StaminaCrystal"), "player",
			() => Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().StatStamMax / 30 - 1, () => 9, null, Language.GetText("Mods.Avalon.MunchiesSupport.StaminaCrystal"), null,
			null, "Craft from [i:Avalon/Boltstone] or obtain from bosses");

		ExxoAvalonOrigins.Munchies.Call(
			"AddSingleConsumable", Mod, "1.4", ExxoAvalonOrigins.Mod.Find<ModItem>("EnergyCrystal"), "player",
			() => Main.LocalPlayer.GetModPlayer<AvalonStaminaPlayer>().EnergyCrystal, null, Language.GetText("Mods.Avalon.MunchiesSupport.EnergyCrystal"), null,
			null, "Throw a [i:Avalon/StaminaCrystal] into Shimmer");
	}
}
