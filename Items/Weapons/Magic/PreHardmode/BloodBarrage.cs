using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ExxoAvalonOrigins.Items.Weapons.Magic.PreHardmode;

class BloodBarrage : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Blood Barrage");
        //Tooltip.SetDefault("Uses 4 life\nReturns life on hit");
        Item.ResearchUnlockCount = 1;
    }
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Magic;
        Item.damage = 20;
        Item.autoReuse = true;
        Item.scale = 0.9f;
        Item.shootSpeed = 12f;
        Item.mana = 8;
        Item.rare = ItemRarityID.LightRed;
        Item.Size = new Vector2(32);
        Item.useTime = 8;
        Item.knockBack = 4f;
        Item.shoot = ModContent.ProjectileType<Projectiles.Magic.BloodBlob>();
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = 50000;
        Item.useAnimation = 24;
        Item.reuseDelay = 20;
    }
    public SoundStyle note = new SoundStyle("Terraria/Sounds/NPC_Hit_18")
    {
        Volume = 0.5f,
        Pitch = -0.5f,
        PitchVariance = 0.5f,
        MaxInstances = 10,
    };
    public override bool? UseItem(Player player)
    {
        int healthSucked = 4;
        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, healthSucked, dramatic: false, dot: false);
        player.statLife -= healthSucked;
        if (player.statLife <= 0)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} forgot their blood is necessary."), healthSucked, 1, false, true, false, -1, false);
        }
        SoundEngine.PlaySound(note, player.Center);

        return true;
    }
    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        velocity = velocity.RotatedByRandom(0.17);
    }
}
