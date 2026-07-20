using Avalon;
using Avalon.Items.Banners;
using Avalon.Particles;
using Avalon.Tiles.Furniture.Functional;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Underground;

public class OreSlime : ModNPC
{
	public virtual int BestiaryOre => 9;
	public virtual int[] Ores =>
	[
		ItemID.CopperOre, ItemID.TinOre, ModContent.ItemType<Items.Material.Ores.BronzeOre>(),
		ItemID.IronOre, ItemID.LeadOre, ModContent.ItemType<Items.Material.Ores.NickelOre>(),
		ItemID.SilverOre, ItemID.TungstenOre, ModContent.ItemType<Items.Material.Ores.ZincOre>(),
		ItemID.GoldOre, ItemID.PlatinumOre, ModContent.ItemType<Items.Material.Ores.BismuthOre>(),
		ItemID.Obsidian
	];
	public virtual Color[] OreColor =>
	[
		new Color(183, 88, 25), new Color(187, 165, 124), new Color(193, 133, 127),
		new Color(181, 164, 149), new Color(62, 82, 114), new Color(107, 158, 149),
		new Color(179, 179, 179), new Color(154, 190, 155), new Color(182, 169, 182),
		new Color(231, 213, 65), new Color(181, 194, 217), new Color(173, 58, 191),
		Color.DarkSlateBlue
	];
	public virtual int[] OreDusts =>
	[
		DustID.Copper, DustID.Tin, ModContent.DustType<Dusts.BronzeDust>(),
		DustID.Iron, DustID.Lead, ModContent.DustType<Dusts.NickelDust>(),
		DustID.Silver, DustID.Tungsten, ModContent.DustType<Dusts.ZincDust>(),
		DustID.Gold, DustID.Platinum, ModContent.DustType<Dusts.BismuthDust>(),
		DustID.Obsidian
	];
	public int WhichOre;
	public override void OnSpawn(IEntitySource source)
	{
		WhichOre = Main.rand.Next(0, Ores.Length);
	}
	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(WhichOre);
	}
	public override void ReceiveExtraAI(BinaryReader reader)
	{
		WhichOre = reader.ReadInt32();
	}
	public override void AI()
	{
		var light = Lighting.GetSubLight(NPC.Center);
		float brightness = Math.Max(Math.Max(light.X, light.Y), light.Z);
		if (Main.rand.NextBool(20) && Main.rand.NextFloat() < brightness)
		{
			Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.TintableDustLighted, 0f, 0f, 254, Color.White, 0.5f);
			d.velocity += NPC.velocity * 3;
			d.velocity *= 0.1f;
			d.noGravity = true;
		}
		//if (Main.rand.NextBool(30) && Main.rand.NextFloat() < brightness)
		//{
		//	var p = VanillaParticles.RequestPrettySparkleParticle();
		//	p.LocalPosition = Main.rand.NextVector2FromRectangle(NPC.Hitbox);
		//	p.ColorTint = OreColor[WhichOre];
		//}
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.IsABestiaryIconDummy)
			WhichOre = BestiaryOre;
		Color slimeColor = NPC.GetNPCColorTintedByBuffs(drawColor.MultiplyRGB(OreColor[WhichOre])) * NPC.Opacity;

		if (!TextureAssets.Item[WhichOre].IsLoaded)
			Main.instance.LoadItem(WhichOre);
		Asset<Texture2D> dyeTex = TextureAssets.Item[WhichOre];
		var tex = TextureAssets.Npc[Type].Value;

		DrawData d = new DrawData(tex, NPC.Bottom - screenPos, NPC.frame, slimeColor, NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height - 4), NPC.scale, SpriteEffects.None);

		Main.EntitySpriteDraw(d with { color = NPC.GetNPCColorTintedByBuffs(drawColor) * NPC.Opacity });
		float rotate = MathHelper.SmoothStep(0.1f, -0.1f, Main.masterColor);
		Asset<Texture2D> oreTexture = TextureAssets.Item[Ores[WhichOre]];
		Rectangle frame = oreTexture.Frame();
		Vector2 frameOrigin = frame.Size() / 2f;
		Main.EntitySpriteDraw(oreTexture.Value, NPC.Center - screenPos + new Vector2(0, NPC.frame.Y * -0.05f), frame, drawColor, NPC.rotation + rotate, frameOrigin, NPC.scale, SpriteEffects.None);
		Main.EntitySpriteDraw(d);
		return false;
	}
	public override void OnKill()
	{
		Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, Ores[WhichOre], Main.rand.Next(15, 35));
	}
	public override void ModifyNPCLoot(NPCLoot npcLoot)
	{
		npcLoot.Add(new CommonDrop(ItemID.Gel, 1, 3, 6));
	}
	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			for (int i = 0; i < 7; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, Main.rand.Next(100, 200), default, Main.rand.NextFloat(1, 1.5f));
				Main.dust[d].color = OreColor[WhichOre] * 0.3f;
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1f, 4) * hit.HitDirection, Main.rand.NextFloat(-1, -4));
			}
			for (int i = 0; i < 5; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, OreDusts[WhichOre], 0, 0, 0, default, Main.rand.NextFloat(0.75f, 1.5f));
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 3) * hit.HitDirection, Main.rand.NextFloat(-1, -3));
			}
		}
		else
		{
			for (int i = 0; i < 30; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, Main.rand.Next(100, 200), default, Main.rand.NextFloat(1, 1.5f));
				Main.dust[d].color = OreColor[WhichOre] * 0.3f;
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.5f, 5) * hit.HitDirection, Main.rand.NextFloat(-1, -5));
			}
			for (int i = 0; i < 1; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, OreDusts[WhichOre], 0, 0, 0, default, Main.rand.NextFloat(0.75f, 1.5f));
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1f, 4) * hit.HitDirection, Main.rand.NextFloat(-1, -4));
			}
		}
	}
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 2;
		Data.Sets.NPCSets.Earthen[NPC.type] = true;
	}
	public override void SetDefaults()
	{
		NPC.damage = 20;
		NPC.lifeMax = 200;
		NPC.defense = 6;
		NPC.width = 36;
		NPC.aiStyle = 1;
		NPC.value = 1000f;
		NPC.knockBackResist = 0.1f;
		NPC.height = 24;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.alpha = 128;
		AnimationType = NPCID.BlueSlime;
		BannerItem = ModContent.ItemType<OreSlimeBanner>();
		Banner = NPC.type;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) =>
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
		{
			BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
			new FlavorTextBestiaryInfoElement(Language.GetTextValue("Mods.Avalon.Bestiary.OreSlime"))
		});
	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.65f);
	}
	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (spawnInfo.Player.ZoneUndergroundDesert)
		{
			return 0.02f;
		}
		return spawnInfo.Player.ZoneRockLayerHeight && !spawnInfo.Player.ZoneDungeon ? 0.06f : 0f;
	}
}
