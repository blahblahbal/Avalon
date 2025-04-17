using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Dyes;

public class EbonstoneDye : ModItem
{
	public override void SetStaticDefaults()
	{
		// Avoid loading assets on dedicated servers. They don't use graphics cards.
		if (!Main.dedServ)
		{
			// The following code creates an effect (shader) reference and associates it with this item's type Id.
			GameShaders.Armor.BindShader(
				Item.type,
				new ArmorShaderData(Mod.Assets.Request<Effect>("Effects/EbonstoneDye"), "EbonstoneDye") // Be sure to update the effect path and pass name here.
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
		Item.rare = ItemRarityID.Green;
		Item.dye = dye;
	}
}
