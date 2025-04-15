using Avalon.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Hardmode;

public class SandSharkHook : ModHook
{
	protected override void Apply()
	{
		IL_NPC.UpdateCollision += preventModdedSandsharkCollision;
		On_NPC.ApplyTileCollision += SandsharkCollision;
	}
	private void preventModdedSandsharkCollision(ILContext il)
	{
		ILCursor c = new(il);
		ILLabel IL_0175 = c.DefineLabel();
		c.GotoNext(MoveType.After, i => i.MatchLdarg(0), i => i.MatchLdfld<NPC>("type"), i => i.MatchLdcI4(72), i => i.MatchBeq(out IL_0175));
		c.EmitLdarg0();
		c.EmitDelegate((NPC self) =>
		{
			return self.type == ModContent.NPCType<BaskingSpewer>();
		});
		c.EmitBrtrue(IL_0175);
	}
	private void SandsharkCollision(On_NPC.orig_ApplyTileCollision orig, NPC self, bool fall, Vector2 cPosition, int cWidth, int cHeight)
	{
		if (self.type == ModContent.NPCType<BaskingSpewer>())
		{
			typeof(NPC).GetMethod("Collision_MoveSandshark", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(self, new object[] { fall, cPosition, cWidth, cHeight });
		}
		else
		{
			orig.Invoke(self, fall, cPosition, cWidth, cHeight);
		}
	}
}
public class BaskingSpewer : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[Type] = 4;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
		NPCID.Sets.TrailingMode[Type] = 6;
		Data.Sets.NPCSets.Wicked[NPC.type] = true;

		NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
		{
			// Influences how the NPC looks in the Bestiary
			Position = new Vector2(35f, -3f),
			PortraitPositionXOverride = 0
		};
		NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
	}
	public override void SetDefaults()
	{
		NPC.CloneDefaults(NPCID.SandShark);
		NPC.aiStyle = NPCAIStyleID.SandShark;
		NPC.damage = 67;
		NPC.defense = 23;
		NPC.lifeMax = 420;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.value = 400f;
		NPC.npcSlots = 0.5f;
		NPC.noGravity = true;
		NPC.behindTiles = true;
		AIType = NPCID.SandsharkCorrupt;
		AnimationType = NPCID.SandsharkCorrupt;
		SpawnModBiomes = [ModContent.GetInstance<Biomes.ContagionDesert>().Type];
	}
	public override bool? CanFallThroughPlatforms()
	{
		return true;
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return Main.tile[spawnInfo.SpawnTileX, spawnInfo.SpawnTileY].HasTile && spawnInfo.Player.InModBiome<Biomes.ContagionDesert>() && Main.hardMode ? 0.2f : 0f;
	}
	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.BaskingSpewer"))
		});
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			return;
		}
		else if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
		{
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerHead").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerBody").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerFin").Type, NPC.scale);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("SpewerFin2").Type, NPC.scale);
		}
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ItemID.SharkFin, 8));
		npcLoot.Add(ItemDropRule.Common(ItemID.Nachos, 33));
		npcLoot.Add(ItemDropRule.Common(ItemID.DarkShard, 25));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.WindyEnoughForKiteDrops(), ItemID.KiteSandShark, 25));
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		Vector2 frameSize = new Vector2(NPC.frame.Width, NPC.frame.Height);
		Vector2 halfSize = new(frameSize.X / 2, frameSize.Y / 2);

		SpriteEffects spriteEffects = SpriteEffects.None;
		if (NPC.spriteDirection == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}

		int trailAmount = 6;
		int trailIncrement = trailAmount / 2;
		float trailColorMod = trailAmount * 2;

		for (int i = 1; i < trailAmount; i += trailIncrement)
		{
			Color color = NPC.GetAlpha(drawColor);
			color *= (trailAmount - i) / trailColorMod;
			Vector2 posTrail = NPC.oldPos[i] + new Vector2(NPC.width, NPC.height) / 2f - screenPos;
			posTrail -= frameSize * NPC.scale / 2f;
			posTrail += halfSize * NPC.scale + new Vector2(0f, NPC.gfxOffY);
			spriteBatch.Draw(TextureAssets.Npc[Type].Value, posTrail, NPC.frame, color, NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
		}
		Vector2 pos = NPC.Center - screenPos;
		pos -= frameSize * NPC.scale / 2f;
		pos += halfSize * NPC.scale + new Vector2(0f, NPC.gfxOffY);
		spriteBatch.Draw(TextureAssets.Npc[Type].Value, pos, NPC.frame, NPC.GetAlpha(drawColor), NPC.rotation, halfSize, NPC.scale, spriteEffects, 0f);
		return false;
	}
	public override Color? GetAlpha(Color drawColor)
	{
		float aMod = (255 - NPC.alpha) / 255f;
		int r = (int)(drawColor.R * aMod);
		int g = (int)(drawColor.G * aMod);
		int b = (int)(drawColor.B * aMod);
		int a = drawColor.A - NPC.alpha;
		if (r + g + b > 10 && r + g + b >= 60)
		{
			r *= 2;
			g *= 2;
			b *= 2;
			if (r > 255)
			{
				r = 255;
			}
			if (g > 255)
			{
				g = 255;
			}
			if (b > 255)
			{
				b = 255;
			}
		}
		return new Color(r, g, b, a);
	}
}
