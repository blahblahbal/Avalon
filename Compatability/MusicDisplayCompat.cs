using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;


namespace Avalon.Compatability;

internal class MusicDisplayCompat : ModSystem
{
	public override void PostSetupContent()
	{
		if (ExxoAvalonOrigins.MusicMod != null && ExxoAvalonOrigins.MusicDisplay != null)
		{
			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Contagion"),
				Language.GetText("Mods.Avalon.TrackNames.Contagion.Name"),
				Language.GetText("Mods.Avalon.TrackNames.Contagion.Author"),
				Language.GetText("Mods.Avalon.TrackNames.Contagion.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/UndergroundContagion"),
				Language.GetText("Mods.Avalon.TrackNames.UndergroundContagion.Name"),
				Language.GetText("Mods.Avalon.TrackNames.UndergroundContagion.Author"),
				Language.GetText("Mods.Avalon.TrackNames.UndergroundContagion.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/BacteriumPrime"),
				Language.GetText("Mods.Avalon.TrackNames.BacteriumPrime.Name"),
				Language.GetText("Mods.Avalon.TrackNames.BacteriumPrime.Author"),
				Language.GetText("Mods.Avalon.TrackNames.BacteriumPrime.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeak"),
				Language.GetText("Mods.Avalon.TrackNames.DesertBeak.Name"),
				Language.GetText("Mods.Avalon.TrackNames.DesertBeak.Author"),
				Language.GetText("Mods.Avalon.TrackNames.DesertBeak.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DesertBeakOtherworldly"),
				Language.GetText("Mods.Avalon.TrackNames.DesertBeakOtherworldly.Name"),
				Language.GetText("Mods.Avalon.TrackNames.DesertBeakOtherworldly.Author"),
				Language.GetText("Mods.Avalon.TrackNames.DesertBeakOtherworldly.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Hellcastle"),
				Language.GetText("Mods.Avalon.TrackNames.Hellcastle.Name"),
				Language.GetText("Mods.Avalon.TrackNames.Hellcastle.Author"),
				Language.GetText("Mods.Avalon.TrackNames.Hellcastle.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Phantasm"),
				Language.GetText("Mods.Avalon.TrackNames.Phantasm.Name"),
				Language.GetText("Mods.Avalon.TrackNames.Phantasm.Author"),
				Language.GetText("Mods.Avalon.TrackNames.Phantasm.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Myriapoda"),
				Language.GetText("Mods.Avalon.TrackNames.Myriapoda.Name"),
				Language.GetText("Mods.Avalon.TrackNames.Myriapoda.Author"),
				Language.GetText("Mods.Avalon.TrackNames.Myriapoda.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/SkyFortress"),
				Language.GetText("Mods.Avalon.TrackNames.SkyFortress.Name"),
				Language.GetText("Mods.Avalon.TrackNames.SkyFortress.Author"),
				Language.GetText("Mods.Avalon.TrackNames.SkyFortress.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/TuhrtlOutpost"),
				Language.GetText("Mods.Avalon.TrackNames.TuhrtlOutpost.Name"),
				Language.GetText("Mods.Avalon.TrackNames.TuhrtlOutpost.Author"),
				Language.GetText("Mods.Avalon.TrackNames.TuhrtlOutpost.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/Mechasting"),
				Language.GetText("Mods.Avalon.TrackNames.Mechasting.Name"),
				Language.GetText("Mods.Avalon.TrackNames.Mechasting.Author"),
				Language.GetText("Mods.Avalon.TrackNames.Mechasting.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/ArmageddonSlime"),
				Language.GetText("Mods.Avalon.TrackNames.ArmageddonSlime.Name"),
				Language.GetText("Mods.Avalon.TrackNames.ArmageddonSlime.Author"),
				Language.GetText("Mods.Avalon.TrackNames.ArmageddonSlime.ModName"));

			ExxoAvalonOrigins.MusicDisplay.Call("AddMusic", (short)MusicLoader.GetMusicSlot(ExxoAvalonOrigins.MusicMod, "Sounds/Music/DarkMatter"),
				Language.GetText("Mods.Avalon.TrackNames.DarkMatter.Name"),
				Language.GetText("Mods.Avalon.TrackNames.DarkMatter.Author"),
				Language.GetText("Mods.Avalon.TrackNames.DarkMatter.ModName"));
		}
	}
}
