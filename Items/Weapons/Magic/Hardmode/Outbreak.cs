using Avalon.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Magic.Hardmode
{
	public class Outbreak : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
		}
		public override void SetDefaults()
		{
			Item.DefaultToStaff(ProjectileID.WoodenArrowFriendly /* it was set to this before and honestly idk what happens if you set it to 0 */, 90, 0f, 20, 6f, 45, 45, true);
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(0, 8);
			Item.UseSound = SoundID.Item46;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			const int Rad = 100;
			const int AttackRad = 130;
			Vector2 AttackPosition = Main.MouseWorld;
			for (int j = 0; j < 30; j++)
			{
				Dust d = Dust.NewDustPerfect(AttackPosition, DustID.Stone, Main.rand.NextVector2CircularEdge(Rad, Rad), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1.5f); ;
				d.velocity *= 0.1f;
				d.noGravity = true;
				d.color.A = 200;
				Dust d2 = Dust.NewDustPerfect(AttackPosition, DustID.Stone, Main.rand.NextVector2CircularEdge(Rad, Rad), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1f);
				d2.velocity *= 0.05f;
				d2.noGravity = true;
				d.color.A = 200;
			}

			int Divide = 0;
			foreach (var npc in Main.ActiveNPCs)
			{
				if (Vector2.Distance(npc.Center, AttackPosition) < AttackRad && !npc.dontTakeDamage && (!npc.friendly || (npc.type == NPCID.Guide && player.killGuide) || (npc.type == NPCID.Clothier && player.killClothier)))
				{
					Divide++;
				}
			}
			if (Divide > 0)
			{
				foreach (var npc in Main.ActiveNPCs)
				{
					if (Vector2.Distance(npc.Center, AttackPosition) < AttackRad && !npc.dontTakeDamage && (!npc.friendly || (npc.type == NPCID.Guide && player.killGuide) || (npc.type == NPCID.Clothier && player.killClothier)))
					{
						int DPS = npc.SimpleStrikeNPC((int)(Item.damage / (1f + Divide / 10f)), player.direction, Main.rand.NextBool(player.GetWeaponCrit(player.HeldItem), 100), Item.knockBack, DamageClass.Magic, true, player.luck);
						player.addDPS(DPS);
						if (Main.rand.NextBool(5))
							npc.AddBuff(ModContent.BuffType<Pathogen>(), 600 / Divide);
						if (Main.rand.NextBool(3))
							npc.AddBuff(BuffID.Poisoned, 600 / Divide);
						for (int j = 0; j < 10; j++)
						{
							Dust d = Dust.NewDustPerfect(npc.Center, DustID.Stone, Main.rand.NextVector2Circular(npc.width, npc.height), 0, Color.Lerp(Color.OliveDrab, Color.MediumPurple, Main.rand.NextFloat()), 1.5f);
							d.velocity *= 0.1f;
							d.noGravity = true;
							d.color.A = 200;
						}
					}
				}
			}
			return false;
		}
		public override Vector2? HoldoutOrigin()
		{
			return new Vector2(0, 0);
		}
	}
}
