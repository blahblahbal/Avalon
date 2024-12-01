using Avalon.Projectiles.Magic;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Avalon.Buffs.Debuffs;

namespace Avalon.Items.Weapons.Magic.PreHardmode;

public class LotusLeech : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.damage = 0;
        Item.shoot = ModContent.ProjectileType<LotusLeechHeal>();
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.UseSound = SoundID.Item8;
        Item.autoReuse= true;
        Item.rare = ItemRarityID.Blue;
    }

    public override void UseStyle(Player player, Rectangle heldItemFrame)
    {

        player.itemRotation += MathHelper.Pi / 6f * player.direction * player.gravDir;
        player.itemLocation -= new Vector2(player.direction * 10, player.gravDir * 10);
	}
	public override void UseAnimation(Player player)
	{
		bool Success = false;
		int Count = 0;
		foreach (var npc in Main.ActiveNPCs)
		{
			if (!npc.friendly && npc.lifeMax > 5 && npc.netID != NPCID.TargetDummy && npc.Distance(player.Center) < 400)
			{
				Count++;
				Success = true;
			}
			if (Count > 2)
			{
				break;
			}
		}

		if (!Success)
		{
			for (float j = 0; j < MathHelper.TwoPi; j += MathHelper.TwoPi / 32f)
			{
				var dust = Dust.NewDustPerfect(player.Center - new Vector2(player.direction * -14, player.gravDir * 24), DustID.ViciousPowder, new Vector2(0, 3).RotatedBy(j));
				dust.noGravity = true;
			}
		}
		else
		{
			for (float j = 0; j < MathHelper.TwoPi; j += MathHelper.TwoPi / 32f)
			{
				var dust = Dust.NewDustPerfect(player.Center - new Vector2(player.direction * -14, player.gravDir * 24), DustID.PurificationPowder, new Vector2(0, 3).RotatedBy(j));
				dust.noGravity = true;
			}
		}
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        bool Success = false;
        int Count = 0;
		foreach (var npc in Main.ActiveNPCs)
		{
            if(!npc.friendly && npc.lifeMax > 5 && npc.netID != NPCID.TargetDummy && npc.Distance(player.Center) < 400)
            {
                Count++;
                Success = true;
                Projectile P = Projectile.NewProjectileDirect(source,Main.rand.NextVector2FromRectangle(npc.Hitbox) + npc.velocity,npc.velocity,type,damage,knockback,player.whoAmI);
            }
            if(Count > 2)
            {
                break;
            }
        }

        if (!Success)
        {
            player.CheckMana(25, true);
            player.manaRegenDelay = 100;
            player.AddBuff(BuffID.ManaSickness, 60 * 5);
            player.AddBuff(ModContent.BuffType<LotusCurse>(), 60 * 7);
            //int[] Debuffs = new int[] { BuffID.Slow, BuffID.WitheredArmor, BuffID.WitheredWeapon, BuffID.Blackout};
            //player.AddBuff(Debuffs[Main.rand.Next(Debuffs.Length)], 60 * 4);
        }

        return false;
    }
}
