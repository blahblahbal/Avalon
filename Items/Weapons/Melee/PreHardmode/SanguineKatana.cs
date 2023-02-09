using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Melee.PreHardmode
{
    public class SanguineKatana : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            Item.damage = 24;
            Item.scale = 1f;
            Item.DamageType = DamageClass.Melee;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Orange;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(0, 1, 0, 0);
        }
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if(target.type != NPCID.TargetDummy)
            {
                int healAmount = Main.rand.Next(4) + 2;
                player.HealEffect(healAmount, true);
                player.statLife += healAmount;
            }
        }
        public override bool? UseItem(Player player)
        {
            int healthSucked = 2;
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
            player.statLife -= healthSucked;
            if(player.statLife <= 0)
            {
                player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name}'s soul has been entombed within a sword."),healthSucked,1,false,true,false,-1,false);
            }
            return true;
        }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                int num209 = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
                Dust dust = Main.dust[num209];
                dust.velocity.X = 2f * player.direction;
                dust.velocity.Y = -0.5f;
            }
        }
    }
}

