using Avalon.Common;
using Avalon.Items.Weapons.Melee.Swords;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
	public class MasterSwordNPC : GlobalNPC
	{
		public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
		{
			return entity.type is NPCID.Spazmatism or NPCID.Retinazer or NPCID.SkeletronPrime or NPCID.TheDestroyer;
		}

		internal static bool[] hitTwins = new bool[Main.maxPlayers];
		internal static bool[] hitPrime = new bool[Main.maxPlayers];
		internal static bool[] hitDestroyer = new bool[Main.maxPlayers];
		public override void OnKill(NPC npc)
		{
			if (MasterSwordHook.allMechsSpawned)
			{
				if (npc.type is NPCID.Spazmatism or NPCID.Retinazer)
				{
					if (!NPC.AnyNPCs((npc.type == NPCID.Spazmatism) ? NPCID.Retinazer : NPCID.Spazmatism))
					{
						MasterSwordHook.killedTwins = true;
						foreach (Player p in Main.ActivePlayers)
						{
							if (!hitTwins[p.whoAmI])
							{
								hitTwins[p.whoAmI] = npc.playerInteraction[p.whoAmI];
							}
						}
					}
				}
				else if (npc.type is NPCID.SkeletronPrime)
				{
					MasterSwordHook.killedPrime = true;
					foreach (Player p in Main.ActivePlayers)
					{
						if (!hitPrime[p.whoAmI])
						{
							hitPrime[p.whoAmI] = npc.playerInteraction[p.whoAmI];
						}
					}
				}
				else if (npc.type is NPCID.TheDestroyer)
				{
					MasterSwordHook.killedDestroyer = true;
					foreach (Player p in Main.ActivePlayers)
					{
						if (!hitDestroyer[p.whoAmI])
						{
							hitDestroyer[p.whoAmI] = npc.playerInteraction[p.whoAmI];
						}
					}
				}

				if (MasterSwordHook.killedTwins && MasterSwordHook.killedPrime && MasterSwordHook.killedDestroyer)
				{
					foreach (Player p in Main.ActivePlayers)
					{
						if (hitTwins[p.whoAmI] && hitPrime[p.whoAmI] && hitDestroyer[p.whoAmI])
						{
							int item = Item.NewItem(Player.GetSource_NaturalSpawn(), p.position, ModContent.ItemType<MasterSword>(), noBroadcast: true, prefixGiven: -2);
							Main.timeItemSlotCannotBeReusedFor[item] = TimeUtils.MinutesToTicks(15);
							//Main.item[item].playerIndexTheItemIsReservedFor = p.whoAmI;
							if (Main.netMode == NetmodeID.Server)
							{
								NetMessage.SendData(MessageID.InstancedItem, p.whoAmI, -1, null, item);
								Main.item[item].active = false;
							}
						}
					}

					MasterSwordHook.killedTwins = false;
					MasterSwordHook.killedPrime = false;
					MasterSwordHook.killedDestroyer = false;
					MasterSwordHook.allMechsSpawned = false;
					hitTwins.Initialize();
					hitPrime.Initialize();
					hitDestroyer.Initialize();
				}
			}
		}
	}
	public class MasterSwordHook : ModHook
	{
		internal static bool killedTwins;
		internal static bool killedPrime;
		internal static bool killedDestroyer;
		internal static bool allMechsSpawned;
		protected override void Apply()
		{
			On_NPC.SpawnBoss += On_NPC_SpawnBoss;
		}

		// todo: maybe check if the other bosses are above a certain HP threshold? (like 90%?)
		// this would prevent cheesing by getting two really low, then spawning the last and then killing the first two
		// might be a little superfluous though
		private void On_NPC_SpawnBoss(On_NPC.orig_SpawnBoss orig, int spawnPositionX, int spawnPositionY, int Type, int targetPlayerIndex)
		{
			bool twins = false;
			bool prime = false;
			bool destroyer = false;

			if (Type is NPCID.Spazmatism or NPCID.Retinazer)
			{
				twins = true;
				if (killedTwins)
				{
					prime = killedPrime;
					destroyer = killedDestroyer;
				}
			}
			else if (Type is NPCID.SkeletronPrime)
			{
				prime = true;
				if (killedPrime)
				{
					twins = killedTwins;
					destroyer = killedDestroyer;
				}
			}
			else if (Type is NPCID.TheDestroyer)
			{
				destroyer = true;
				if (killedDestroyer)
				{
					twins = killedTwins;
					prime = killedPrime;
				}
			}
			else
			{
				orig(spawnPositionX, spawnPositionY, Type, targetPlayerIndex);
				return;
			}


			if (!twins && !killedTwins)
			{
				bool retAlive = NPC.AnyNPCs(NPCID.Retinazer);
				bool spazAlive = NPC.AnyNPCs(NPCID.Spazmatism);
				if ((retAlive && spazAlive) == false)
				{
					if ((prime && !killedPrime) || (destroyer && !killedDestroyer))
					{
						killedPrime = false;
						killedDestroyer = false;
						allMechsSpawned = false;
						MasterSwordNPC.hitTwins.Initialize();
						MasterSwordNPC.hitPrime.Initialize();
						MasterSwordNPC.hitDestroyer.Initialize();
						orig(spawnPositionX, spawnPositionY, Type, targetPlayerIndex);
						return;
					}
					else if (retAlive || spazAlive)
					{
						twins = true;
					}
				}
				else
				{
					twins = true;
				}
			}
			if ((!prime && !killedPrime) && NPC.AnyNPCs(NPCID.SkeletronPrime))
			{
				prime = true;
			}
			if ((!destroyer && !killedDestroyer) && NPC.AnyNPCs(NPCID.TheDestroyer))
			{
				destroyer = true;
			}

			allMechsSpawned = (twins && prime && destroyer);
			if (!allMechsSpawned)
			{
				killedTwins = false;
				killedPrime = false;
				killedDestroyer = false;
				MasterSwordNPC.hitTwins.Initialize();
				MasterSwordNPC.hitPrime.Initialize();
				MasterSwordNPC.hitDestroyer.Initialize();
			}

			orig(spawnPositionX, spawnPositionY, Type, targetPlayerIndex);
		}
	}
}
