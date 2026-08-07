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
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Avalon.NPCs.Underground;

public record struct OreSlimeData
{
	public OreSlimeData(int oreItemID, int dustID, Color oreColor)
	{
		OreItemID = oreItemID;
		DustID = dustID;
		OreColor = oreColor;
	}
	public int OreItemID { get; set; }
	public int DustID { get; set; }
	public Color OreColor { get; set; }
}
public class OreSlime : ModNPC
{
	public static bool AddExtraOre(int ore, int oreDust, Color oreColor)
	{
		Ores = Ores.Append(new OreSlimeData(ore,oreDust,oreColor)).ToArray();
		return true;
	}
	public virtual int BestiaryOre => 9;

	public static OreSlimeData[] Ores = [
		new OreSlimeData(ItemID.CopperOre, DustID.Copper, new Color(183, 88, 25)),
		new OreSlimeData(ItemID.TinOre, DustID.Tin, new Color(187, 165, 124)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.BronzeOre>(), ModContent.DustType<Dusts.BronzeDust>(), new Color(193, 133, 127)),
		new OreSlimeData(ItemID.IronOre, DustID.Iron, new Color(181, 164, 149)),
		new OreSlimeData(ItemID.LeadOre, DustID.Lead, new Color(62, 82, 114)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.NickelOre>(), ModContent.DustType<Dusts.NickelDust>(), new Color(107, 158, 149)),
		new OreSlimeData(ItemID.SilverOre, DustID.Silver, new Color(179, 179, 179)),
		new OreSlimeData(ItemID.TungstenOre, DustID.Tungsten, new Color(154, 190, 155)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.ZincOre>(), ModContent.DustType<Dusts.ZincDust>(), new Color(182, 169, 182)),
		new OreSlimeData(ItemID.GoldOre, DustID.Gold, new Color(231, 213, 65)),
		new OreSlimeData(ItemID.PlatinumOre, DustID.Platinum, new Color(181, 194, 217)),
		new OreSlimeData(ModContent.ItemType<Items.Material.Ores.BismuthOre>(), ModContent.DustType<Dusts.BismuthDust>(), new Color(173, 58, 191)),
		new OreSlimeData(ItemID.Obsidian, DustID.Obsidian, Color.DarkSlateBlue)
		];
	public int WhichOre;

	public virtual OreSlimeData[] ListOfOres => Ores;
	public override void OnSpawn(IEntitySource source)
	{
		WhichOre = Main.rand.Next(0, ListOfOres.Length);
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
	}
	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.IsABestiaryIconDummy)
			WhichOre = BestiaryOre;
		Color slimeColor = NPC.GetNPCColorTintedByBuffs(drawColor.MultiplyRGB(ListOfOres[WhichOre].OreColor)) * NPC.Opacity;

		if (!TextureAssets.Item[WhichOre].IsLoaded)
			Main.instance.LoadItem(ListOfOres[WhichOre].OreItemID);
		var tex = TextureAssets.Npc[Type].Value;

		DrawData d = new DrawData(tex, NPC.Bottom - screenPos, NPC.frame, slimeColor, NPC.rotation, new Vector2(NPC.frame.Width / 2, NPC.frame.Height - 4), NPC.scale, SpriteEffects.None);

		Main.EntitySpriteDraw(d with { color = NPC.GetNPCColorTintedByBuffs(drawColor) * NPC.Opacity });
		float rotate = MathHelper.SmoothStep(0.1f, -0.1f, Main.masterColor);
		Main.GetItemDrawFrame(ListOfOres[WhichOre].OreItemID, out var oreTexture, out var oreFrame);
		Vector2 frameOrigin = oreFrame.Size() / 2f;
		Main.EntitySpriteDraw(oreTexture, NPC.Center - screenPos + new Vector2(0, NPC.frame.Y * -0.05f), oreFrame, ContentSamples.ItemsByType[ListOfOres[WhichOre].OreItemID].GetAlpha(drawColor), NPC.rotation + rotate, frameOrigin, NPC.scale, SpriteEffects.None);
		Main.EntitySpriteDraw(d);
		return false;
	}
	public override void OnKill()
	{
		Item.NewItem(NPC.GetSource_FromThis(), NPC.Hitbox, ListOfOres[WhichOre].OreItemID, Main.rand.Next(15, 35));
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
				Main.dust[d].color = ListOfOres[WhichOre].OreColor * 0.3f;
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1f, 4) * hit.HitDirection, Main.rand.NextFloat(-1, -4));
			}
			for (int i = 0; i < 5; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ListOfOres[WhichOre].DustID, 0, 0, 0, default, Main.rand.NextFloat(0.75f, 1.5f));
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-0.5f, 3) * hit.HitDirection, Main.rand.NextFloat(-1, -3));
			}
		}
		else
		{
			for (int i = 0; i < 30; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.TintableDust, 0, 0, Main.rand.Next(100, 200), default, Main.rand.NextFloat(1, 1.5f));
				Main.dust[d].color = ListOfOres[WhichOre].OreColor * 0.3f;
				Main.dust[d].velocity = new Vector2(Main.rand.NextFloat(-1.5f, 5) * hit.HitDirection, Main.rand.NextFloat(-1, -5));
			}
			for (int i = 0; i < 1; i++)
			{
				int d = Dust.NewDust(NPC.position, NPC.width, NPC.height, ListOfOres[WhichOre].OreItemID, 0, 0, 0, default, Main.rand.NextFloat(0.75f, 1.5f));
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
