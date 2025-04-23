using Avalon.Rarities;
using Avalon.Tiles;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Items.Placeable.Painting;

public class CurseofOblivion : ModItem
{
	public override void SetDefaults()
	{
		Item.DefaultToPainting(ModContent.TileType<Paintings>(), 10);
		Item.rare = ModContent.RarityType<DarkRedRarity>();
	}
}
