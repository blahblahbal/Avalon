using Avalon;
using Avalon.Items.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs;

public class DyeSlime : ModNPC
{
	public static int[] PreHardmodeDyes = {ItemID.BrownDye,ItemID.BlackDye,ItemID.SilverDye, ItemID.BrightBrownDye, ItemID.BrightSilverDye, ItemID.BrownAndBlackDye, ItemID.BrownAndSilverDye, ItemID.SilverAndBlackDye, ItemID.BlackAndWhiteDye, 
		ItemID.FlameDye, ItemID.FlameAndBlackDye, ItemID.FlameAndSilverDye, ItemID.GreenFlameDye, ItemID.GreenFlameAndBlackDye, ItemID.GreenFlameAndSilverDye, ItemID.BlueFlameDye, ItemID.BlueFlameAndBlackDye, ItemID.BlueFlameAndSilverDye, ItemID.IntenseFlameDye, ItemID.IntenseBlueFlameDye, ItemID.IntenseGreenFlameDye,
		ItemID.CyanGradientDye, ItemID.VioletGradientDye, ItemID.YellowGradientDye, ItemID.RainbowDye, ItemID.IntenseRainbowDye };
	public static int[] HardmodeDyes = { ItemID.AcidDye,ItemID.BlueAcidDye,ItemID.RedAcidDye,ItemID.GelDye,ItemID.MushroomDye,ItemID.GrimDye,ItemID.HadesDye,ItemID.BurningHadesDye,ItemID.ShadowflameHadesDye,ItemID.MirageDye,ItemID.NegativeDye,ItemID.PhaseDye,ItemID.PurpleOozeDye,ItemID.ReflectiveDye,ItemID.ReflectiveCopperDye,ItemID.ReflectiveCopperDye,ItemID.ReflectiveGoldDye,ItemID.ReflectiveMetalDye,ItemID.ReflectiveObsidianDye,ItemID.ReflectiveSilverDye,ItemID.ShadowDye,ItemID.TwilightDye,ItemID.ShiftingSandsDye };
	public static int[] LaterHardmodeDyes = {ItemID.ChlorophyteDye, ItemID.LivingOceanDye, ItemID.LivingFlameDye, ItemID.LivingRainbowDye, ItemID.PixieDye, ItemID.WispDye, ItemID.InfernalWispDye, ItemID.UnicornWispDye };
	int WhichDye;
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 2;
		Data.Sets.NPCSets.Earthen[NPC.type] = true;
		for(int i = 1007; i <= 1018; i++) // add basic dyes
		{
			PreHardmodeDyes = PreHardmodeDyes.Append(i).ToArray();
		}
		for (int i = 1038; i <= 1049; i++) // add bright dyes
		{
			PreHardmodeDyes = PreHardmodeDyes.Append(i).ToArray();
		}
		for (int i = 1019; i <= 1030; i++) // add black dyes
		{
			PreHardmodeDyes = PreHardmodeDyes.Append(i).ToArray();
		}
		for (int i = 1051; i <= 1062; i++) // add silver dyes
		{
			PreHardmodeDyes = PreHardmodeDyes.Append(i).ToArray();
		}
	}
	public override void SetDefaults()
	{
		NPC.damage = 20;
		NPC.lifeMax = 125;
		NPC.defense = 0;
		NPC.width = 36;
		NPC.aiStyle = 1;
		NPC.value = 1000f;
		AnimationType = NPCID.BlueSlime;
		NPC.knockBackResist = 0.75f;
		NPC.height = 24;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.alpha = 128;
		BannerItem = ModContent.ItemType<DyeSlimeBanner>();
		Banner = Type;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.DyeSlime"))
		});
	public override void OnSpawn(IEntitySource source)
	{
		if(!Main.rand.NextBool(3) && Main.hardMode)
		{
			if(!Main.rand.NextBool() && NPC.downedPlantBoss)
				WhichDye = LaterHardmodeDyes[Main.rand.Next(LaterHardmodeDyes.Length)];
			else
				WhichDye = HardmodeDyes[Main.rand.Next(HardmodeDyes.Length)];
		}
		else
		{
			WhichDye = PreHardmodeDyes[Main.rand.Next(PreHardmodeDyes.Length)];
		}
		NPC.netUpdate = true;
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(WhichDye);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		WhichDye = reader.ReadInt32();
	}
	public override void AI()
	{
		if (Main.rand.NextBool(10))
		{
			Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(DustID.CopperCoin, DustID.PlatinumCoin + 1));
			d.noGravity = true;
			d.noLightEmittence = true;
			d.velocity *= 0.2f;
			d.velocity += NPC.velocity * 0.7f;
		}
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.IsABestiaryIconDummy)
			WhichDye = ItemID.RainbowDye;

		else if (WhichDye == 0)
			return false;
		float rotate = MathHelper.SmoothStep(0.1f, -0.1f, Main.masterColor);
		var tex = TextureAssets.Npc[Type].Value;

		if (!TextureAssets.Item[WhichDye].IsLoaded)
			Main.instance.LoadItem(WhichDye);
		Asset<Texture2D> dyeTex = TextureAssets.Item[WhichDye];

		Main.GetItemDrawFrame(WhichDye, out var itemTexture, out var rectangle);
		float itemScale = 0.8f;
		Main.EntitySpriteDraw(itemTexture, NPC.Center - screenPos - new Vector2(0,2), rectangle, drawColor, NPC.rotation + (float)Math.Sin(Main.timeForVisualEffects * 0.1f) * (float)Math.Cos(Main.timeForVisualEffects * 0.03f) * 0.1f, rectangle.Size() / 2, itemScale, SpriteEffects.None, 0);

		Rectangle capFrame = new Rectangle(0, 0, itemTexture.Width, 10);
		Main.EntitySpriteDraw(itemTexture, NPC.Center - screenPos - new Vector2(0, 19 + (NPC.frame.Y / 50) * 2), capFrame, NPC.GetNPCColorTintedByBuffs(drawColor), NPC.rotation, capFrame.Size() / 2, 1, SpriteEffects.None);

		DrawData d = new DrawData(tex, NPC.Bottom - screenPos, NPC.frame, NPC.GetNPCColorTintedByBuffs(drawColor) * 0.75f, NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height - 4), NPC.scale, SpriteEffects.None);
		if (!NPC.IsABestiaryIconDummy)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			GameShaders.Armor.GetShaderFromItemId(WhichDye).Apply(NPC, d);
			Main.EntitySpriteDraw(d with { color = d.color * (NPC.life / (float)NPC.lifeMax) });
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, null, Main.Transform);
			Main.EntitySpriteDraw(d with { color = d.color * (1f - (NPC.life / (float)NPC.lifeMax)) });
		}
		else
		{
			Main.EntitySpriteDraw(d);
		}
		return false;
	}
	public override void OnKill()
	{
		Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, WhichDye, Main.rand.Next(1, 3));
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(new CommonDrop(ItemID.Gel,1,2,4));
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		for(int i = 0; i < hit.Damage / 3; i++)
		{
			Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, Main.rand.Next(DustID.CopperCoin,DustID.PlatinumCoin + 1));
			d.noGravity = true;
			d.velocity = new Vector2(Main.rand.NextFloat(-1f, 8) * hit.HitDirection, Main.rand.NextFloat(-1, -4));
		}
	}
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.85f);
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		return 0.06f;
	}
}
