using Avalon.Tiles.Furniture.Functional;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items;

public class TilePlacer : ModItem
{
	int style = 0;
	bool justPressedKey = false;
	public override bool IsLoadingEnabled(Mod mod)
	{
		return true;
	}
	public override void SetDefaults()
	{
		Item.DefaultToPlaceableTile(ModContent.TileType<EnemyGauntletChest>(), style);
	}
	public override void HoldItem(Player player)
	{
		if (!justPressedKey)
		{
			if (player.controlUp)
			{
				style++;
				justPressedKey = true;
				CombatText.NewText(player.Hitbox, new Color(200,128,32,0), "Style " + style, dot: true);
				SoundEngine.PlaySound(SoundID.MenuTick);
			}
			else if (player.controlDown)
			{
				style--;
				justPressedKey = true;
				CombatText.NewText(player.Hitbox, new Color(200, 128, 32, 0), "Style " + style, dot: true);
				SoundEngine.PlaySound(SoundID.MenuTick);
			}
			Item.placeStyle = style;
		}
		else if(!player.controlDown && !player.controlUp)
		{
			justPressedKey = false;
		}
	}
}
