using Avalon.NPCs.Template;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Savanna
{
	public class SnakeHead : SnakeHeadTemplate
	{
		public override int BodyType => ModContent.NPCType<SnakeBody>();

		public override int TailType => ModContent.NPCType<SnakeTail>();

		public override void SetDefaults()
		{
			NPC.damage = 15;
			NPC.netAlways = true;
			NPC.noTileCollide = false;
			NPC.lifeMax = 70;
			NPC.defense = 0;
			NPC.noGravity = false;
			NPC.width = 14;
			NPC.aiStyle = -1;
			NPC.behindTiles = false;
			NPC.value = 500f;
			NPC.height = 14;
			NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
		}
		public override void Init()
		{
			MinSegmentLength = 4;
			MaxSegmentLength = 6;
		}
		internal static void CommonsSnakeInit(SnakeTemplate snake)
		{
			// These two properties handle the movement of the worm
			snake.MoveSpeed = 5.5f;
			snake.Acceleration = 0.075f;
		}
	}
	public class SnakeBody : SnakeBodyTemplate
	{
		public override void Init()
		{
			SnakeHead.CommonsSnakeInit(this);
		}

		public override void SetDefaults()
		{
			NPC.damage = 15;
			NPC.netAlways = true;
			NPC.noTileCollide = false;
			NPC.lifeMax = 70;
			NPC.defense = 0;
			NPC.noGravity = false;
			NPC.width = 14;
			NPC.aiStyle = -1;
			NPC.behindTiles = false;
			NPC.value = 500f;
			NPC.height = 14;
			NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
	public class SnakeTail : SnakeTailTemplate
	{
		public override void Init()
		{
			SnakeHead.CommonsSnakeInit(this);
		}
		public override void SetDefaults()
		{
			NPC.damage = 15;
			NPC.netAlways = true;
			NPC.noTileCollide = false;
			NPC.lifeMax = 70;
			NPC.defense = 0;
			NPC.noGravity = false;
			NPC.width = 14;
			NPC.aiStyle = -1;
			NPC.behindTiles = false;
			NPC.value = 500f;
			NPC.height = 14;
			NPC.knockBackResist = 0f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			//SpawnModBiomes = new int[] { ModContent.GetInstance<Biomes.Contagion>().Type, ModContent.GetInstance<Biomes.UndergroundContagion>().Type };
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}
