using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace Avalon.Common.Interfaces;

public interface IBuffThatNeedsToUpdatePlayerEarly
{
	void UpdateEarly(Player player, ref int buffIndex);
}
public interface IBuffThatNeedsToUpdateNPCEarly
{
	void UpdateEarly(NPC npc, ref int buffIndex);
}