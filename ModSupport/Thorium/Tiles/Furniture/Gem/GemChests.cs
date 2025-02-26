using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System.Collections.Generic;
using Avalon.Common.Templates;

namespace Avalon.ModSupport.Thorium.Tiles.Furniture.Gem;

[ExtendsFromMod("ThoriumMod")]
public class AquamarineChest : ChestTemplate
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.AquamarineChest>();
}
public class ChrysoberylChest : ChestTemplate
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.ChrysoberylChest>();
}
[ExtendsFromMod("ThoriumMod")]
public class OpalChest : ChestTemplate
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return ExxoAvalonOrigins.ThoriumContentEnabled;
	}
	public override bool Shiny => true;
    public override int DropItem => ModContent.ItemType<Items.Placeable.Furniture.Gem.OpalChest>();
}
