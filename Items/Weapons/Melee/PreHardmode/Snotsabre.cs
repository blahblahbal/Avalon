using Avalon.Dusts;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Weapons.Melee.PreHardmode;

class Snotsabre : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.UseSound = SoundID.Item1;
        Item.damage = 24;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.scale = 1.1f;
        Item.rare = ItemRarityID.Blue;
        Item.width = 24;
        Item.height = 28;
        Item.useTime = 30;
        Item.knockBack = 3f;
        Item.DamageType = DamageClass.Melee;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.sellPrice(0, 0, 36, 0);
        Item.useAnimation = 30;
    }
    bool hasHit;

    public override bool? UseItem(Player player)
    {
        hasHit = false;
        return base.UseItem(player);
    }
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (!hasHit)
        {
            //int j = 0;
            //for (int i = 0; i < Main.npc.Length; i++)
            //{
            //    NPC npc = Main.npc[i];
            //    if (npc.Center.Distance(target.Center) < 200 && npc.active && !npc.friendly && npc != target)
            //    {
            //        npc.SimpleStrikeNPC((int)(hit.Damage * 0.4f), 0, hit.Crit, 0, hit.DamageType, true, player.luck);
            //        npc.AddBuff(BuffID.Poisoned, 4 * 60);
            //        DustLine(Main.rand.NextVector2FromRectangle(target.Hitbox),Main.rand.NextVector2FromRectangle(npc.Hitbox));
            //    }
            //    j++;
            //    if (j > 10)
            //        break;
            //}
            List<int> AlreadyHit = new List<int> { };
            for(int i = 0; i < 10; i++)
            {
                int npc = ClassExtensions.FindClosestNPC(target, 200, npc => !npc.active || npc.townNPC || npc.dontTakeDamage || npc.lifeMax <= 5 || npc.type == NPCID.TargetDummy || npc.type == NPCID.CultistBossClone || npc.friendly || AlreadyHit.Contains(npc.whoAmI) || npc.whoAmI == target.whoAmI || !Collision.CanHit(npc, target));
                if(npc != -1)
                {
                    int dmg = Main.npc[npc].SimpleStrikeNPC((int)(hit.Damage * 0.4f), 0, hit.Crit, 0, hit.DamageType, true, player.luck);
                    player.addDPS(dmg);
                    Main.npc[npc].AddBuff(BuffID.Poisoned, 4 * 60);
                    DustLine(Main.rand.NextVector2FromRectangle(target.Hitbox), Main.rand.NextVector2FromRectangle(Main.npc[npc].Hitbox));
                    AlreadyHit.Add(npc);
                }
            }
            AlreadyHit.Clear();
        }
        target.AddBuff(BuffID.Poisoned, 8 * 60);
        hasHit = true;
        for (int i = 0; i < 15; i++)
        {
            Dust d = Dust.NewDustDirect(target.Center, 0, 0, ModContent.DustType<ContagionWeapons>(), 0, 0, 128);
            d.velocity *= 3;
            d.scale = 1f;
            d.noGravity = true;
        }
    }
    public void DustLine(Vector2 start, Vector2 end)
    {
        for(int i = 0; i < (int)start.Distance(end); i+= 5)
        {
            Dust d = Dust.NewDustPerfect(Vector2.Lerp(start,end,i / start.Distance(end)),ModContent.DustType<ContagionWeapons>(),Vector2.Zero,128);
            d.fadeIn = 1;
            d.velocity = start.DirectionTo(end).RotatedByRandom(1) * 6;
            d.noGravity = true;
        }
        for(int i = 0; i < 15; i++)
        {
            Dust d = Dust.NewDustDirect(end,0,0, ModContent.DustType<ContagionWeapons>(), 0, 0, 128);
            d.velocity *= 2;
            d.scale = 1.5f;
            d.noGravity = true;
        }
    }
    public override void MeleeEffects(Player player, Rectangle hitbox)
    {
        if (player.itemAnimation % 2 == 0)
        {
            ClassExtensions.GetPointOnSwungItemPath(60f, 60f, 0.4f + 0.4f * Main.rand.NextFloat(), Item.scale, out var location2, out var outwardDirection2, player);
            Vector2 vector2 = outwardDirection2.RotatedBy((float)Math.PI / 2f * (float)player.direction * player.gravDir);

            int DustType = ModContent.DustType<ContagionWeapons>();
            if (Main.rand.NextBool(3))
                DustType = DustID.CorruptGibs;

            int num15 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustType, player.velocity.X * 0.2f + (float)(player.direction * 3), player.velocity.Y * 0.2f, 140, default(Color), 0.7f);
            Main.dust[num15].position = location2;
            Main.dust[num15].fadeIn = 1.2f;
            Main.dust[num15].noGravity = true;
            Main.dust[num15].velocity *= 0.25f;
            Main.dust[num15].velocity += vector2 * 5f;
            Main.dust[num15].velocity.Y *= 0.3f;
        }
    }
    public override void AddRecipes()
    {
        CreateRecipe(1).AddIngredient(ModContent.ItemType<Material.Bars.BacciliteBar>(), 10).AddTile(TileID.Anvils).Register();
    }
}
