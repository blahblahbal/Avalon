//using Terraria.Audio;
//using Terraria;
//using Terraria.ModLoader;
//using Terraria.ID;
//using ExxoAvalonOrigins.Projectiles.Melee;

//namespace ExxoAvalonOrigins.Items.Weapons.Melee.Hardmode
//{
//    public class DarklightLance : ModItem
//    {
//        public override void SetStaticDefaults()
//        {
//            Item.ResearchUnlockCount = 1;
//        }
//        public override void SetDefaults()
//        {
//            Item.UseSound = SoundID.Item1;
//            Item.damage = 99;
//            Item.noUseGraphic = true;
//            Item.scale = 1f;
//            Item.shootSpeed = 4f;
//            Item.rare = ItemRarityID.Yellow;
//            Item.noMelee = true;
//            Item.useTime = 26;
//            Item.useAnimation = 26;
//            Item.knockBack = 5.5f;
//            Item.shoot = ModContent.ProjectileType<DarklightLanceProjectile>();
//            Item.DamageType = DamageClass.Melee;
//            Item.autoReuse = true;
//            Item.useStyle = ItemUseStyleID.Shoot;
//            Item.value = Item.sellPrice(0, 40, 0, 0);
//        }
//        public override bool CanUseItem(Player player)
//        {
//            return player.ownedProjectileCounts[Item.shoot] < 1;
//        }
//        public override bool? UseItem(Player player)
//        {
//            if (!Main.dedServ && Item.UseSound.HasValue)
//            {
//                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
//            }

//            return null;
//        }
//    }
//}
