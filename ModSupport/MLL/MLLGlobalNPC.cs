using Avalon.ModSupport.MLL.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL;
internal class MLLGlobalNPC : GlobalNPC
{
	public override void ModifyShop(NPCShop shop)
	{
		if (shop.NpcType == NPCID.Demolitionist)
		{
			shop.Add(ModContent.ItemType<BloodBomb>(), Condition.PlayerCarriesItem(ModContent.ItemType<BloodBomb>()));
			shop.Add(ModContent.ItemType<AcidBomb>(), Condition.PlayerCarriesItem(ModContent.ItemType<AcidBomb>()));
		}
	}
}
