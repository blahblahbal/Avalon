using Avalon.Common.DebuffEfficiency;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;

namespace Avalon.Common.Extensions;

public static class NPCExtensions
{
	extension(NPC npc)
	{
		public void DrawConfusionIcon(Vector2 screenPos, float heightOffset = 0f)
		{
			if (npc.confused)
			{
				float num36 = Main.NPCAddHeight(npc);
				Vector2 halfSize = new(TextureAssets.Npc[npc.type].Width() / 2, TextureAssets.Npc[npc.type].Height() / Main.npcFrameCount[npc.type] / 2);
				Main.spriteBatch.Draw(TextureAssets.Confuse.Value, new Vector2(npc.position.X - screenPos.X + (float)(npc.width / 2) - (float)TextureAssets.Npc[npc.type].Width() * npc.scale / 2f + halfSize.X * npc.scale, npc.position.Y - screenPos.Y + (float)npc.height - (float)TextureAssets.Npc[npc.type].Height() * npc.scale / (float)Main.npcFrameCount[npc.type] + 4f + halfSize.Y * npc.scale + num36 + heightOffset - (float)TextureAssets.Confuse.Height() - 20f), new Rectangle(0, 0, TextureAssets.Confuse.Width(), TextureAssets.Confuse.Height()), npc.GetShimmerColor(new Color(250, 250, 250, 70)), npc.velocity.X * -0.05f, new Vector2(TextureAssets.Confuse.Width() / 2, TextureAssets.Confuse.Height() / 2), Main.essScale + 0.2f, SpriteEffects.None, 0f);
			}
		}
		/// <summary>
		/// This checks if the npc is immune to slows, so you can safely put this on a debuff that does things other than slowing and it'll be fine.
		/// </summary>
		public void ChangeSpeed(float speed)
		{
			if (ApplyImmunitiesToStunAndCCDebuffs.ImmuneToCC(npc.type))
				return;
			var n = npc.GetGlobalNPC<AvalonGlobalNPCInstance>();
			if (speed < 1)
				speed /= npc.DebuffEfficiency;
			n.Speed *= speed;
		}
		public float DebuffEfficiency => npc.GetGlobalNPC<DebuffEfficiencyNPC>().DebuffEfficiency;
	}
}
