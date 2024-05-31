using Avalon.Common;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;

namespace Avalon.Hooks;

internal class SkellyBananaHook : ModHook
{
	protected override void Apply()
	{
		On_NPCWasChatWithTracker.RegisterChatStartWith += On_NPCWasChatWithTracker_RegisterChatStartWith;
	}

	private void On_NPCWasChatWithTracker_RegisterChatStartWith(On_NPCWasChatWithTracker.orig_RegisterChatStartWith orig, NPCWasChatWithTracker self, Terraria.NPC npc)
	{
		if (npc.type == NPCID.SkeletonMerchant && Main.rand.NextBool(50))
		{
			npc.GetGlobalNPC<AvalonGlobalNPCInstance>().SkellyBanana = true;
		}
		orig.Invoke(self, npc);
	}
}
