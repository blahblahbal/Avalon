using Avalon.Items.Banners;
using Avalon.NPCs.Template;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Dungeon;

public class IrateBones : CustomFighterAI
{
	private static Asset<Texture2D> Extra;
    public override void SetStaticDefaults()
    {
		Extra = ModContent.Request<Texture2D>(Texture + "_Extra");
        Main.npcFrameCount[NPC.type] = 17;
        NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
        {
            // Influences how the NPC looks in the Bestiary
            Velocity = 1f // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
		Data.Sets.NPCSets.Undead[NPC.type] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
		NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Bleeding] = true;
	}

    public override void SetDefaults()
    {
		NPC.damage = 35;
        NPC.lifeMax = 350;
        NPC.defense = 15;
        NPC.width = 18;
        NPC.aiStyle = -1;
		NPC.value = 1000f;
        NPC.height = 40;
        NPC.knockBackResist = 0.75f;
        NPC.HitSound = SoundID.NPCHit2;
        NPC.DeathSound = SoundID.NPCDeath2;
        Banner = NPC.type;
        BannerItem = ModContent.ItemType<IrateBonesBanner>();
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheDungeon,
            new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.IrateBones")),
        });
	public override float MaxMoveSpeed => Utils.Remap(NPC.life,0,NPC.lifeMax / 3 * 2,9,3);
	public override float MaxAirSpeed => Utils.Remap(NPC.life, 0, NPC.lifeMax / 3 * 2, 20, 5);
	public override float Acceleration => Utils.Remap(NPC.life, 0, NPC.lifeMax / 3 * 2, 0.2f, 0.1f);
	public override float AirAcceleration => NPC.direction != Math.Sign(NPC.velocity.X)? Utils.Remap(NPC.life, 0, NPC.lifeMax / 3 * 2, 0.5f, 0.1f) : 0;
	public override bool CanOpenDoors => true;
	public override int KnockInterval => (int)Utils.Remap(NPC.life, 0, NPC.lifeMax, 5, 20);
	public override int MaxKnockCount => (int)Utils.Remap(NPC.life, 0, NPC.lifeMax, 10, 4);
	public override void CustomBehavior()
	{
		if(NPC.frame.Y / 60 >= 15)
		{
			Dust d = Dust.NewDustDirect(NPC.BottomLeft + NPC.velocity * 2, NPC.width, 2, DustID.Cloud);
			d.noGravity = true;
			d.velocity *= 0.1f;
			d.velocity.Y -= Main.rand.NextFloat();
			if (NPC.localAI[0] <= 0)
			{
				NPC.localAI[0] = 6;
				SoundEngine.PlaySound(SoundID.Run with { Volume = Utils.Remap(Math.Abs(NPC.velocity.X),0,2,0,0.5f), Pitch = Utils.Remap(Math.Abs(NPC.velocity.X), 0, 2, 0.1f, 0.5f), MaxInstances = 10}, NPC.position);
			}
		}
		NPC.localAI[0]--;
	}
	public override void FindFrame(int frameHeight)
	{
		if(NPC.velocity.Y != 0)
		{
			NPC.frame.Y = 0;
			return;
		}
		else if (TimeSpentAtDoor == 0 && Math.Sign(NPC.velocity.X) != NPC.spriteDirection && NPC.life < NPC.lifeMax * 0.75f)
		{
			NPC.frame.Y = frameHeight * (Math.Abs(NPC.velocity.X) > 2? 15 : 16);
			return;
		}
		NPC.frameCounter += Math.Abs(NPC.velocity.X) + 0.3f;
		if(NPC.frameCounter > 5)
		{
			NPC.frameCounter = 0;
			NPC.frame.Y += frameHeight;
			if(NPC.frame.Y >= 15 * frameHeight)
			{
				NPC.frame.Y = frameHeight;
			}
		}
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		float healthPercent = NPC.life / (float)NPC.lifeMax;
		var effect = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
		var t = TextureAssets.Npc[Type].Value;
		Color actualDrawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
		Vector2 offset = Vector2.UnitY * NPC.gfxOffY;
		Rectangle headFrame = new Rectangle(0, 0, 32, 32);
		Vector2 headOffset = new Vector2(0, -34);
		if (healthPercent < 0.5f)
			headFrame.X += 32;
		if (NPC.frame.Y / 60 is (> 1 and < 5) or (> 8 and < 12))
		{
			headOffset.Y -= 2;
		}
		else if (NPC.frame.Y / 60 == 15)
			headOffset += new Vector2(4 * -NPC.spriteDirection,2);
		else if (NPC.frame.Y / 60 == 16)
			headOffset += new Vector2(4 * -NPC.spriteDirection, 0);
		spriteBatch.Draw(t,NPC.Bottom - screenPos + offset, NPC.frame, actualDrawColor, NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height - 4),NPC.scale, effect, 0);
		spriteBatch.Draw(Extra.Value, NPC.Bottom - screenPos + headOffset + offset, headFrame, actualDrawColor, NPC.rotation, Vector2.One * 16, NPC.scale, effect, 0);

		Color eyeColor = Color.White with { A = 0 } * (0.8f + Main.masterColor * 0.2f) * (1f - healthPercent);
		eyeColor = NPC.GetNPCColorTintedByBuffs(eyeColor);
		for(int i = 0; i < 5; i++)
		{
			spriteBatch.Draw(Extra.Value, NPC.Bottom - screenPos + headOffset + Main.rand.NextVector2Circular(i * 2,i * 2) + offset + NPC.velocity * -i, headFrame with { Y = 32 }, eyeColor * (1f - (i / 5f)), NPC.rotation, Vector2.One * 16, NPC.scale, effect, 0);
		}
		spriteBatch.Draw(Extra.Value, NPC.Bottom - screenPos + offset +headOffset, headFrame with { Y = 32}, eyeColor, NPC.rotation, Vector2.One * 16, NPC.scale, effect, 0);

		Rectangle angryFrame = (int)(Main.timeForVisualEffects / 15) % 2 == 0 ? new Rectangle(66, 4, 10, 10) : new Rectangle(78, 2, 14, 14);
		spriteBatch.Draw(Extra.Value, NPC.Bottom - screenPos + offset + new Vector2(NPC.spriteDirection * -8, -42), angryFrame, actualDrawColor with { A = 64}, (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * 0.1f, angryFrame.Size() / 2, NPC.scale, effect, 0);
		return false;
	}
    public override float SpawnChance(NPCSpawnInfo spawnInfo) => Main.hardMode && spawnInfo.Player.ZoneDungeon
        ? 0.6f : 0f;

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life <= 0 && Main.netMode != NetmodeID.Server)
        {
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity,
				Mod.Find<ModGore>("IrateBonesHelmet").Type);
			Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 42);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 43);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 43);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 44);
            Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, 44);
        }
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(ItemDropRule.Common(ItemID.BoneWand, 250))
			.OnFailedRoll(ItemDropRule.Common(ItemID.TallyCounter, 100))
			.OnFailedRoll(ItemDropRule.Common(ItemID.GoldenKey, 65))
			.OnFailedRoll(ItemDropRule.ByCondition(new Conditions.NotExpert(), ItemID.Bone, 1, 1, 3));
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ItemID.Bone, 1, 2, 6));
	}
}
