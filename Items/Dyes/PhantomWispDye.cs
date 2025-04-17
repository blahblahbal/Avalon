using Avalon.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Dyes;

public class PhantomWispDye : ModItem
{
	public override void SetStaticDefaults()
	{
		// Avoid loading assets on dedicated servers. They don't use graphics cards.
		if (!Main.dedServ)
		{
			// The following code creates an effect (shader) reference and associates it with this item's type ID.
			GameShaders.Armor.BindShader(
				Item.type,
				GameShaders.Armor.GetShaderFromItemId(ItemID.WispDye).UseColor(new Color(255, 196, 209)).UseSecondaryColor(new Color(244, 19, 0))
			);
		}

		Item.ResearchUnlockCount = 3;
	}

	public override void SetDefaults()
	{
		// Item.dye will already be assigned to this item prior to SetDefaults because of the above GameShaders.Armor.BindShader code in Load().
		// This code here remembers Item.dye so that information isn't lost during CloneDefaults.
		int dye = Item.dye;

		Item.CloneDefaults(ItemID.GelDye);
		Item.rare = ModContent.RarityType<BlueRarity>();
		Item.dye = dye;
	}
}
