using Avalon.Common;
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
            Item.width = 24;
            Item.height = 28;
            Item.DefaultToMagicWeapon(1, 45, 6, true);
            Item.damage = 90;
            Item.UseSound = SoundID.Item46;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(0,8);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            const int Rad = 100;
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
            for(int i = 0; i < Main.npc.Length; i++)
            {
                if (Vector2.Distance(Main.npc[i].Center, AttackPosition) < Rad && !Main.npc[i].friendly && Main.npc[i].active)
                {
                    Divide++;
                }
            }
            if (Divide > 0)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Vector2.Distance(Main.npc[i].Center, AttackPosition) < Rad && !Main.npc[i].friendly && Main.npc[i].active)
                    {
                        int DPS = Main.npc[i].SimpleStrikeNPC((Item.damage / Divide) + (int)MathHelper.Clamp(Divide * 3,0,Item.damage), player.direction, Main.rand.NextBool(101 - player.GetWeaponCrit(player.HeldItem)), Item.knockBack, DamageClass.Magic, true, player.luck);
                        player.addDPS(DPS);
                        if (Main.rand.NextBool(3))
                            Main.npc[i].AddBuff(BuffID.Poisoned, 600 / Divide);
                        for (int j = 0; j < 10; j++) 
                        {
                            Dust d = Dust.NewDustPerfect(Main.npc[i].Center,DustID.Stone,Main.rand.NextVector2Circular(Main.npc[i].width, Main.npc[i].height),0,Color.Lerp(Color.OliveDrab,Color.MediumPurple,Main.rand.NextFloat()),1.5f);
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
            return new Vector2(0,0);
        }
    }
}
