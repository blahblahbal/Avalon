using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.NPCs.Bosses.Hardmode;

public class MechanicalLeechHead : WormHead
{
	public override int BodyType => ModContent.NPCType<MechanicalLeechBody>();
	public override int TailType => ModContent.NPCType<MechanicalLeechTail>();
	public override string Texture => "Avalon/NPCs/Bosses/Hardmode/MechanicalLeechHead";

    public override void SetDefaults()
    {
        NPC.width = 14;
        NPC.height = 14;
        NPC.aiStyle = 6;
        NPC.netAlways = true;
        NPC.damage = 40;
        NPC.defense = 6;
        NPC.lifeMax = 300;
        NPC.HitSound = SoundID.NPCHit4;
        NPC.DeathSound = SoundID.NPCDeath14;
        NPC.noGravity = true;
        NPC.noTileCollide = true;
        NPC.knockBackResist = 0f;
        NPC.behindTiles = true;
    }
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
        NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
        NPC.damage = (int)(NPC.damage * 0.8f);
    }
    public override void Init()
    {
		MinSegmentLength = 6;
		MaxSegmentLength = 10;

		CommonWormInit(this);
	}
	internal static void CommonWormInit(Worm worm)
	{
		// These two properties handle the movement of the worm
		worm.MoveSpeed = 9.5f;
		worm.Acceleration = 0.075f;
	}

	public class MechanicalLeechBody : WormBody
	{
		public override string Texture => "Avalon/NPCs/Bosses/Hardmode/MechanicalLeechBody";
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		public override void SetDefaults()
		{
			NPC.width = 14;
			NPC.height = 14;
			NPC.aiStyle = 6;
			NPC.netAlways = true;
			NPC.damage = 35;
			NPC.defense = 6;
			NPC.lifeMax = 300;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.knockBackResist = 0f;
			NPC.behindTiles = true;
		}
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
			NPC.damage = (int)(NPC.damage * 0.8f);
		}
		public override void Init()
		{
			CommonWormInit(this);
		}
	}
}
public class MechanicalLeechTail : WormTail
{
	public override string Texture => "Avalon/NPCs/Bosses/Hardmode/MechanicalLeechTail";
	public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
	{
		return false;
	}
	public override void SetDefaults()
	{
		NPC.width = 14;
		NPC.height = 14;
		NPC.aiStyle = 6;
		NPC.netAlways = true;
		NPC.damage = 30;
		NPC.defense = 15;
		NPC.lifeMax = 300;
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.knockBackResist = 0f;
		NPC.behindTiles = true;
	}
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.55f);
		NPC.damage = (int)(NPC.damage * 0.8f);
	}
	public override void Init()
	{
		MechanicalLeechHead.CommonWormInit(this);
	}
}
