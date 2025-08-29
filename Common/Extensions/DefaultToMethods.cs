using Avalon.Items.Ammo;
using Avalon.Items.Material;
using Avalon.Tiles;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Common.Extensions
{
	public enum PotionCorkType : int
	{
		None = ItemRarityID.White,
		Default = ItemRarityID.Blue,
		Obsidian = ItemRarityID.Green,
		Elixir = ItemRarityID.Lime
	}
	public enum TreasureBagRarities : int
	{
		EyeTier = ItemRarityID.Blue,
		EvilTier = ItemRarityID.Green,
		SkeleTier = ItemRarityID.Orange,
		WofTier = ItemRarityID.LightRed,
		MechTier = ItemRarityID.Pink,
		PlantTier = ItemRarityID.LightPurple,
		GolemTier = ItemRarityID.Lime,
		LunarTier = ItemRarityID.Yellow,
		// below are maybe temp values, change them if you think they should use custom rarities
		WosTier = ItemRarityID.Cyan,
		earlySHMTier = ItemRarityID.Red,
		ArmaTier = ItemRarityID.Purple
	}

	internal static class DefaultToMethods
	{
		/// <summary>
		/// This method sets a variety of Item values common to armor items.<br/>
		/// Specifically: <code>
		/// width = 16;
		/// height = 16;
		/// defense = <paramref name="defense"/>;
		/// </code>
		/// </summary>
		public static void DefaultToArmor(this Item item, int defense)
		{
			item.width = 16;
			item.height = 16;
			item.defense = defense;
		}

		/// <summary>
		/// This method sets a variety of Item values common to arrow items.<br/>
		/// Specifically: <code>
		/// width = 10;
		/// height = 28;
		/// ammo = <see cref="AmmoID.Arrow"/>;
		/// DamageType = <see cref="DamageClass.Ranged"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// damage = <paramref name="damage"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// consumable = <paramref name="consumable"/>;
		/// </code>
		/// </summary>
		public static void DefaultToArrow(this Item item, int damage, int projectile, float shootSpeed, float knockback, bool consumable = true)
		{
			item.width = 10;
			item.height = 28;
			item.ammo = AmmoID.Arrow;
			item.DamageType = DamageClass.Ranged;
			item.maxStack = Item.CommonMaxStack;
			item.damage = damage;
			item.shoot = projectile;
			item.shootSpeed = shootSpeed;
			item.knockBack = knockback;
			item.consumable = consumable;
		}
		/// <summary>
		/// This method sets a variety of Item values common to axe items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// axe = <paramref name="axePowerTimes5"/> / 5;
		/// autoReuse = true;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// scale = <paramref name="scale"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>;
		/// useTime = <paramref name="miningSpeed"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToAxe(this Item item, int axePowerTimes5, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.axe = axePowerTimes5 / 5;
			item.autoReuse = true;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.scale = scale;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useTurn = useTurn;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to bar items.<br/>
		/// Specifically:<code>
		/// createTile = <see cref="ModContent.TileType"/>; (Where <typeparamref name="T"/> is <see cref="PlacedBars"/>)
		/// placeStyle = <paramref name="tileStyleToPlace"/>;
		/// width = 20;
		/// height = 20;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// useAnimation = 15;
		/// useTime = 10;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// useTurn = true;
		/// autoReuse = true;
		/// consumable = true;
		/// </code>
		/// </summary>
		public static void DefaultToBar(this Item item, int tileStyleToPlace)
		{
			item.DefaultToPlaceableTile(ModContent.TileType<PlacedBars>(), tileStyleToPlace);
			item.width = 20;
			item.height = 20;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to blowpipes:
		/// <code>
		/// shoot = <see cref="ProjectileID.PurificationPowder"/>; (vanilla sets it to this for blowpipes, functionally should be no difference to if it were set to <see cref="ProjectileID.Seed"/>)
		/// useAmmo = <see cref="AmmoID.Dart"/>;
		/// UseSound = <see cref="SoundID.Item63"/>; (note that all vanilla dart weapons use a unique sound; this is just a placeholder)
		/// </code>
		/// </summary>
		public static void DefaultToBlowpipe(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, int reuseDelay = 0, int crit = 0, int width = 38, int height = 6)
		{
			item.DefaultToRangedWeapon(width, height, ProjectileID.PurificationPowder, AmmoID.Dart, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
			item.UseSound = SoundID.Item63;
		}
		/// <summary>
		/// This method sets a variety of Item values common to boomerang weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = <paramref name="autoReuse"/>;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// useTime = <paramref name="singleUseTime"/>;
		/// useAnimation = <paramref name="singleUseTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToBoomerang(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, bool autoReuse = false, int crit = 0, int width = 14, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.autoReuse = autoReuse;
			item.crit = crit;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shoot = projectile;
			item.shootSpeed = shootSpeed;
			item.useTime = singleUseTime;
			item.useAnimation = singleUseTime;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to boss mask items.<br/>
		/// Specifically: <code>
		/// width = 18;
		/// height = 18;
		/// rare = <see cref="ItemRarityID.Blue"/>;
		/// value = 3 gold 75 silver;
		/// vanity = true;
		/// </code>
		/// </summary>
		public static void DefaultToBossMask(this Item item)
		{
			item.DefaultToVanity();
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 75);
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to bows:
		/// <code>
		/// shoot = <see cref="ProjectileID.WoodenArrowFriendly"/>;
		/// useAmmo = <see cref="AmmoID.Arrow"/>;
		/// UseSound = <see cref="SoundID.Item5"/>;
		/// </code>
		/// </summary>
		public static void DefaultToBow(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int reuseDelay = 0, int crit = 0, int width = 14, int height = 30)
		{
			item.DefaultToRangedWeapon(width, height, ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
			item.UseSound = SoundID.Item5;
		}
		/// <summary>
		/// This method sets a variety of Item values common to buff potion items.<br/>
		/// Specifically: <code>
		/// width = 14;
		/// height = 24;
		/// UseSound = <see cref="SoundID.Item3"/>;
		/// useStyle = <see cref="ItemUseStyleID.DrinkLiquid"/>;
		/// useTurn = true;
		/// useAnimation = 17;
		/// useTime = 17;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// consumable = true;
		/// buffType = <paramref name="buffType"/>;
		/// buffTime = <paramref name="buffDuration"/>;
		/// rare = (<see cref="int"/>)<paramref name="cork"/>;
		/// if (<paramref name="cork"/> == <see cref="PotionCorkType.None"/>) value = 0;
		/// if (<paramref name="cork"/> == <see cref="PotionCorkType.Default"/>) value = 10 silver;
		/// if (<paramref name="cork"/> == <see cref="PotionCorkType.Obsidian"/>) value = 20 silver;
		/// if (<paramref name="cork"/> == <see cref="PotionCorkType.Elixir"/>) value = 50 silver;
		/// </code>
		/// </summary>
		public static void DefaultToBuffPotion(this Item item, int buffType, int buffDuration, PotionCorkType cork = PotionCorkType.Default)
		{
			item.width = 14;
			item.height = 24;
			item.UseSound = SoundID.Item3;
			item.useStyle = ItemUseStyleID.DrinkLiquid;
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = Item.CommonMaxStack;
			item.consumable = true;
			item.buffType = buffType;
			item.buffTime = buffDuration;
			item.rare = (int)cork;
			item.value = cork switch
			{
				PotionCorkType.None => 0,
				PotionCorkType.Default => Item.sellPrice(silver: 2),
				PotionCorkType.Obsidian => Item.sellPrice(silver: 4),
				PotionCorkType.Elixir => Item.sellPrice(silver: 10),
				_ => 0,
			};
		}
		/// <summary>
		/// This method sets a variety of Item values common to bullet items.<br/>
		/// Specifically: <code>
		/// width = 8;
		/// height = 8;
		/// ammo = <see cref="AmmoID.Bullet"/>;
		/// DamageType = <see cref="DamageClass.Ranged"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// damage = <paramref name="damage"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// consumable = <paramref name="consumable"/>;
		/// </code>
		/// </summary>
		public static void DefaultToBullet(this Item item, int damage, int projectile, float shootSpeed, float knockback, bool consumable = true)
		{
			item.width = 8;
			item.height = 8;
			item.ammo = AmmoID.Bullet;
			item.DamageType = DamageClass.Ranged;
			item.maxStack = Item.CommonMaxStack;
			item.damage = damage;
			item.shoot = projectile;
			item.shootSpeed = shootSpeed;
			item.knockBack = knockback;
			item.consumable = consumable;
		}
		/// <summary>
		/// This method sets a variety of Item values common to rhotuka spinner items.<br/>
		/// Specifically: <code>
		/// width = 10;
		/// height = 12;
		/// ammo = <see cref="ModContent.ItemType"/>; (Where <typeparamref name="T"/> is <see cref="Canister"/>)
		/// DamageType = <see cref="DamageClass.Ranged"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// damage = <paramref name="damage"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = 0f;
		/// knockBack = 0f;
		/// consumable = <paramref name="consumable"/>;
		/// </code>
		/// </summary>
		public static void DefaultToCanister(this Item item, int damage, int projectile, bool consumable = true)
		{
			item.width = 10;
			item.height = 12;
			item.ammo = ModContent.ItemType<Canister>();
			item.DamageType = DamageClass.Ranged;
			item.maxStack = Item.CommonMaxStack;
			item.damage = damage;
			item.shoot = projectile;
			item.shootSpeed = 0f;
			item.knockBack = 0f;
			item.consumable = consumable;
		}
		/// <summary>
		/// This method sets a variety of Item values common to chainsaw items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// axe = <paramref name="axePowerTimes5"/> / 5;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>; (Defaults to -1 if <see cref="ItemID.Sets.IsChainsaw"></see>[Item.type] == true)
		/// useTime = <paramref name="miningSpeed"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsChainsaw"></see>[Item.type] == true)
		/// useAnimation = <paramref name="useAnimation"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsChainsaw"></see>[Item.type] == true)
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// UseSound = <see cref="SoundID.Item23"/>;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// channel = true;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shootSpeed = <paramref name="projectile"/>;
		/// </code>
		/// </summary>
		public static void DefaultToChainsaw(this Item item, int projectile, int axePowerTimes5, int damage, float knockback, int miningSpeed, int tileRangeModifier = 0, int useAnimation = 15, float shootSpeed = 40f, int width = 20, int height = 12)
		{
			item.width = width;
			item.height = height;
			item.axe = axePowerTimes5 / 5;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useStyle = ItemUseStyleID.Shoot;
			item.UseSound = SoundID.Item23;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
		}
		/// <summary>
		/// This method sets a variety of Item values common to consumable items.<br/>
		/// Specifically:<code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// consumable = <paramref name="consumable"/>;
		/// useAnimation = <paramref name="useAnim"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useTime = <paramref name="useTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.HoldUp"/>;
		/// </code>
		/// </summary>
		public static void DefaultToConsumable(this Item item, bool consumable = true, int useAnim = 30, int useTime = 30, bool useTurn = false, int width = 18, int height = 18)
		{
			item.width = width;
			item.height = height;
			item.maxStack = Item.CommonMaxStack;
			item.consumable = consumable;
			item.useAnimation = useAnim;
			item.useTurn = useTurn;
			item.useTime = useTime;
			item.useStyle = ItemUseStyleID.HoldUp;
		}
		/// <summary>
		/// This method sets a variety of Item values common to drill items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// pick = <paramref name="pickPower"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>; (Defaults to -1 if <see cref="ItemID.Sets.IsDrill"></see>[Item.type] == true)
		/// useTime = <paramref name="miningSpeed"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsDrill"></see>[Item.type] == true)
		/// useAnimation = <paramref name="useAnimation"/>; (Multiplied by 0.6 if <see cref="ItemID.Sets.IsDrill"></see>[Item.type] == true)
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// UseSound = <see cref="SoundID.Item23"/>;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// channel = true;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shootSpeed = <paramref name="projectile"/>;
		/// </code>
		/// </summary>
		public static void DefaultToDrill(this Item item, int projectile, int pickPower, int damage, int miningSpeed, int tileRangeModifier = 0, int useAnimation = 15, float shootSpeed = 32f, float knockback = 0.5f, int width = 20, int height = 12)
		{
			item.width = width;
			item.height = height;
			item.pick = pickPower;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useStyle = ItemUseStyleID.Shoot;
			item.UseSound = SoundID.Item23;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
		}
		/// <summary>
		/// This method sets a variety of Item values common to fish items.<br/>
		/// Specifically:<code>
		/// width = 26;
		/// height = 26;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// value = 25 silver;
		/// </code>
		/// </summary>
		public static void DefaultToFish(this Item item)
		{
			item.width = 26;
			item.height = 26;
			item.maxStack = Item.CommonMaxStack;
			item.value = Item.sellPrice(0, 0, 5);
		}
		/// <summary>
		/// This method sets a variety of Item values common to fishing pole items.<br/>
		/// Specifically: <code>
		/// width = 24;
		/// height = 28;
		/// fishingPole = <paramref name="fishingPower"/>
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shoot = <paramref name="projectile"/>;
		/// useTime = 8;
		/// useAnimation = 8;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToFishingPole(this Item item, int projectile, int fishingPower, float shootSpeed)
		{
			item.width = 24;
			item.height = 28;
			item.fishingPole = fishingPower;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
			item.useTime = 8;
			item.useAnimation = 8;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to flail weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// channel = true;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// scale = <paramref name="scale"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// useTime = <paramref name="singleUseTime"/>;
		/// useAnimation = <paramref name="singleUseTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// </code>
		/// </summary>
		public static void DefaultToFlail(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, int crit = 0, float scale = 1.1f, int width = 28, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.channel = true;
			item.crit = crit;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.scale = scale;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
			item.useTime = singleUseTime;
			item.useAnimation = singleUseTime;
			item.useStyle = ItemUseStyleID.Shoot;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to flamethrowers:
		/// <code>
		/// consumeAmmoOnFirstShotOnly = <paramref name="consumeAmmoOnFirstShotOnly"/>;
		/// shoot = <paramref name="projectile"/>;
		/// useAmmo = <see cref="AmmoID.Gel"/>;
		/// UseSound = <see cref="SoundID.Item34"/>;
		/// </code>
		/// </summary>
		/// <param name="consumeAmmoOnFirstShotOnly">Set this to false if you have custom logic inside <see cref="ModItem.CanConsumeAmmo"/></param>
		public static void DefaultToFlamethrower(this Item item, int projectile, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool consumeAmmoOnFirstShotOnly = true, int crit = 0, int width = 50, int height = 18)
		{
			item.DefaultToRangedWeapon(width, height, projectile, AmmoID.Gel, damage, knockback, shootSpeed, useTime, useAnimation, true, 0, crit);
			item.consumeAmmoOnFirstShotOnly = consumeAmmoOnFirstShotOnly;
			item.UseSound = SoundID.Item34;
		}
		/// <summary>
		/// This method sets a variety of Item values common to genie items. (Genies are planned to be pet-like, hence all the redundant values being set)<br/>
		/// Specifically:<code>
		/// width = 16;
		/// height = 30;
		/// accessory = true;
		/// damage = 0
		/// noMelee = true;
		/// useAnimation = 20;
		/// useTime = 20;
		/// buffType = 0;
		/// shoot = 0;
		/// rare = <see cref="ItemRarityID.Green"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item2"/>;
		/// value = 20 gold;<br/>
		/// <see cref="Item.GetGlobalItem"/>.Genie = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
		/// </code>
		/// </summary>
		public static void DefaultToGenie(this Item item)
		{
			item.DefaultToVanitypet(0, 0);
			item.accessory = true;
			item.rare = ItemRarityID.Green;
			item.value = Item.buyPrice(0, 20);
			item.GetGlobalItem<AvalonGlobalItemInstance>().Genie = true;
		}
		/// <summary>
		/// This method sets a variety of Item values common to grappling hook items.<br/>
		/// Specifically: <code>
		/// width = 18;
		/// height = 28;
		/// damage = 0;
		/// knockBack = 7f;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shootSpeed = <paramref name="projectile"/>;
		/// useTime = 20;
		/// useAnimation = 20;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToGrapplingHook(this Item item, int projectile, float shootSpeed)
		{
			item.width = 18;
			item.height = 28;
			item.damage = 0;
			item.knockBack = 7f;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
			// don't listen to example mod's lies and slander, these next three values don't actually need to be set to anything in particular (but these are the vanilla values for them)
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.Shoot;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to guns:
		/// <code>
		/// shoot = <see cref="ProjectileID.Bullet"/>;
		/// useAmmo = <see cref="AmmoID.Bullet"/>;
		/// UseSound = <see cref="SoundID.Item11"/>;
		/// </code>
		/// </summary>
		public static void DefaultToGun(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int reuseDelay = 0, int crit = 0, int width = 50, int height = 14)
		{
			item.DefaultToRangedWeapon(width, height, ProjectileID.Bullet, AmmoID.Bullet, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
			item.UseSound = SoundID.Item11;
		}
		/// <summary>
		/// This method sets a variety of Item values common to hammer items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// hammer = <paramref name="hammerPower"/>;
		/// axe = <paramref name="hammerPower"/> / 5;
		/// autoReuse = true;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// scale = <paramref name="scale"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>;
		/// useTime = <paramref name="miningSpeed"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToHamaxe(this Item item, int hammerPower, int axePowerTimes5, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.hammer = hammerPower;
			item.axe = axePowerTimes5 / 5;
			item.autoReuse = true;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.scale = scale;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useTurn = useTurn;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to hammer items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// hammer = <paramref name="hammerPower"/>;
		/// autoReuse = true;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// scale = <paramref name="scale"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>;
		/// useTime = <paramref name="miningSpeed"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToHammer(this Item item, int hammerPower, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.hammer = hammerPower;
			item.autoReuse = true;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.scale = scale;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useTurn = useTurn;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to herb items.<br/>
		/// Specifically:<code>
		/// width = 12;
		/// height = 14;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// value = 20 copper;
		/// </code>
		/// </summary>
		public static void DefaultToHerb(this Item item)
		{
			item.width = 12;
			item.height = 14;
			item.maxStack = Item.CommonMaxStack;
			item.value = Item.sellPrice(0, 0, 0, 20);
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to launchers:
		/// <code>
		/// shoot = <see cref="ProjectileID.RocketI"/>;
		/// useAmmo = <see cref="AmmoID.Rocket"/>;
		/// UseSound = <see cref="SoundID.Item11"/>;
		/// </code>
		/// </summary>
		public static void DefaultToLauncher(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, int reuseDelay = 0, int crit = 0, int width = 50, int height = 20)
		{
			item.DefaultToRangedWeapon(width, height, ProjectileID.RocketI, AmmoID.Rocket, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
			item.UseSound = SoundID.Item11;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to longbows:
		/// <code>
		/// channel = true;
		/// noUseGraphic = true;
		/// shoot = <see cref="ProjectileID.WoodenArrowFriendly"/>;
		/// useAmmo = <see cref="AmmoID.Arrow"/>;
		/// </code>
		/// </summary>
		public static void DefaultToLongbow(this Item item, int damage, float knockback, float shootSpeed, int singleUseTime, int crit = 0, int width = 16, int height = 50)
		{
			item.DefaultToRangedWeapon(width, height, ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, damage, knockback, shootSpeed, singleUseTime, singleUseTime, false, 0, crit);
			item.channel = true;
			item.noUseGraphic = true;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToSword"/>
		/// Additional values specific to maces:
		/// <code>
		/// noMelee = true;
		/// noUseGraphic = true;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = 6;
		/// useTurn = false;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// </code>
		/// </summary>
		public static void DefaultToMace(this Item item, int projectile, int damage, float knockback, float scale, int singleUseTime, bool autoReuse = true, int crit = 6, int width = 40, int height = 40)
		{
			item.DefaultToSword(damage, knockback, singleUseTime, autoReuse, crit, scale, false, width, height);
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shoot = projectile;
			item.shootSpeed = 6f;
			item.useStyle = ItemUseStyleID.Shoot;
		}
		/// <summary>
		/// This method sets a variety of Item values common to magic weapons.<br/>
		/// Specifically: <code>width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = <paramref name="autoReuse"/>;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Magic"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// mana = <paramref name="manaUsed"/>;
		/// noMelee = true;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shoot = <paramref name="projectile"/>;
		/// useTime = <paramref name="useTime"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// </code>
		/// </summary>
		public static void DefaultToMagicWeapon(this Item item, int width, int height, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int crit = 0)
		{
			item.width = width;
			item.height = height;
			item.autoReuse = autoReuse;
			item.crit = crit;
			item.damage = damage;
			item.DamageType = DamageClass.Magic;
			item.knockBack = knockback;
			item.mana = manaUsed;
			item.noMelee = true;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
			item.useTime = useTime;
			item.useAnimation = useAnimation;
			item.useStyle = ItemUseStyleID.Shoot;
		}
		/// <summary>
		/// <inheritdoc	cref="DefaultToMagicWeapon"/>
		/// Additional values specific to channeling:
		/// <code>
		/// autoReuse = false;
		/// channel = true;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// </code>
		/// </summary>
		public static void DefaultToMagicWeaponChanneled(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int singleUseTime, int crit = 0, int width = 26, int height = 28)
		{
			item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, singleUseTime, singleUseTime, false, crit);
			item.channel = true;
			item.useStyle = ItemUseStyleID.Swing;
		}
		/// <summary>
		/// <inheritdoc	cref="DefaultToMagicWeapon"/>
		/// Additional values specific to swung weapons:
		/// <code>
		/// noUseGraphic = <paramref name="noUseGraphic"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// </code>
		/// </summary>
		public static void DefaultToMagicWeaponSwing(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int singleUseTime, bool autoReuse = false, int crit = 0, bool noUseGraphic = false, int width = 26, int height = 28)
		{
			item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, singleUseTime, singleUseTime, autoReuse, crit);
			item.noUseGraphic = noUseGraphic;
			item.useStyle = ItemUseStyleID.Swing;
		}
		/// <summary>
		/// This method sets a variety of Item values common to minion weapons.<br/>
		/// Specifically: <code>width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = true;
		/// buffType = <paramref name="buff"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Summon"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// mana = <paramref name="manaUsed"/>;
		/// noMelee = true;
		/// reuseDelay = 2;
		/// shootSpeed = 10;
		/// shoot = <paramref name="projectile"/>;
		/// useTime = <paramref name="singleUseTime"/>;
		/// useAnimation = <paramref name="singleUseTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// </code>
		/// </summary>
		public static void DefaultToMinionWeapon(this Item item, int projectile, int buff, int damage, float knockback, int singleUseTime = 36, int manaUsed = 10, int width = 26, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.autoReuse = true;
			item.buffType = buff;
			item.damage = damage;
			item.DamageType = DamageClass.Summon;
			item.knockBack = knockback;
			item.mana = manaUsed;
			item.noMelee = true;
			item.reuseDelay = 2;
			item.shootSpeed = 10f;
			item.shoot = projectile;
			item.useTime = singleUseTime;
			item.useAnimation = singleUseTime;
			item.useStyle = ItemUseStyleID.Swing;
		}
		/// <summary>
		/// <inheritdoc	cref="DefaultToMinionWeapon"/>
		/// Additional values specific to upgradeable minion weapons:
		/// <code>
		/// buffType = 0; (buff added in the counter projectile's AI)
		/// shoot = 0; (based on how our prime staff currently works, if it calls ModItem.Shoot, the arms will disappear and reappear on alternating uses after exceeding the player's max minion count)
		/// </code>
		/// </summary>
		public static void DefaultToMinionWeaponUpgradeable(this Item item, int damage, float knockback, int singleUseTime = 36, int manaUsed = 10, int width = 26, int height = 28)
		{
			item.DefaultToMinionWeapon(0, 0, damage, knockback, singleUseTime, manaUsed, width, height);
		}
		/// <summary>
		/// This method sets a variety of Item values common to miscellaneous items.<br/>
		/// Specifically:<code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// </code>
		/// </summary>
		public static void DefaultToMisc(this Item item, int width = 16, int height = 16)
		{
			item.width = width;
			item.height = height;
			item.maxStack = Item.CommonMaxStack;
		}
		/// <summary>
		/// This method sets a variety of Item values common to monster banner items.<br/>
		/// Specifically:<code>
		/// createTile = <see cref="ModContent.TileType"/>; (Where <typeparamref name="T"/> is <see cref="MonsterBanner"/>)
		/// placeStyle = <paramref name="tileStyleToPlace"/>;
		/// width = 10;
		/// height = 24;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// useAnimation = 15;
		/// useTime = 10;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// useTurn = true;
		/// autoReuse = true;
		/// consumable = true;
		/// rare = <see cref="ItemRarityID.Blue"/>;
		/// value = 10 silver;
		/// </code>
		/// </summary>
		public static void DefaultToMonsterBanner(this Item item, int tileStyleToPlace)
		{
			item.DefaultToPlaceableTile(ModContent.TileType<MonsterBanner>(), tileStyleToPlace);
			item.width = 10;
			item.height = 24;
			item.rare = ItemRarityID.Blue;
			item.value = Item.buyPrice(silver: 10);
		}
		/// <summary>
		/// This method sets a variety of Item values common to painting items.<br/>
		/// Specifically:<code>
		/// createTile = <paramref name="tileIDToPlace"/>;
		/// placeStyle = <paramref name="tileStyleToPlace"/>;
		/// width = 30;
		/// height = 30;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// useAnimation = 15;
		/// useTime = 10;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// useTurn = true;
		/// autoReuse = true;
		/// consumable = true;
		/// value = 50 silver;
		/// </code>
		/// </summary>
		public static void DefaultToPainting(this Item item, int tileIDToPlace, int tileStyleToPlace)
		{
			item.DefaultToPlaceableTile(tileIDToPlace, tileStyleToPlace);
			item.width = 30;
			item.height = 30;
			item.value = Item.sellPrice(0, 0, 10);
		}
		/// <summary>
		/// This method sets a variety of Item values common to pickaxe items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// pick = <paramref name="pickaxePower"/>;
		/// autoReuse = true;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// scale = <paramref name="scale"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>;
		/// useTime = <paramref name="miningSpeed"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToPickaxe(this Item item, int pickaxePower, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.pick = pickaxePower;
			item.autoReuse = true;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.scale = scale;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useTurn = useTurn;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to pickaxe axe items.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// pick = <paramref name="pickaxePower"/>;
		/// axe = <paramref name="axePowerTimes5"/> / 5;
		/// autoReuse = true;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// scale = <paramref name="scale"/>;
		/// tileBoost = <paramref name="tileRangeModifier"/>;
		/// useTime = <paramref name="miningSpeed"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToPickaxeAxe(this Item item, int pickaxePower, int axePowerTimes5, int damage, float knockback, int miningSpeed, int useAnimation, int tileRangeModifier = 0, float scale = 1f, bool useTurn = true, int width = 24, int height = 28)
		{
			item.width = width;
			item.height = height;
			item.pick = pickaxePower;
			item.axe = axePowerTimes5 / 5;
			item.autoReuse = true;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.scale = scale;
			item.tileBoost = tileRangeModifier;
			item.useTime = miningSpeed;
			item.useAnimation = useAnimation;
			item.useTurn = useTurn;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToSword"/>
		/// Additional values specific to beam swords:
		/// <code>
		/// noMelee = <paramref name="noMelee"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shootsEveryUse = <paramref name="shootsEveryUse"/>;
		/// useTime = <paramref name="useTime"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// </code>
		/// </summary>
		public static void DefaultToProjectileSword(this Item item, int projectile, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, bool shootsEveryUse = false, bool noMelee = false, int crit = 0, float scale = 1f, bool useTurn = false, int width = 40, int height = 40)
		{
			item.DefaultToSword(damage, knockback, useAnimation, autoReuse, crit, scale, useTurn, width, height);
			item.noMelee = noMelee;
			item.shoot = projectile;
			item.shootSpeed = shootSpeed;
			item.shootsEveryUse = shootsEveryUse;
			item.useTime = useTime;
		}
		/// <summary>
		/// This method sets a variety of Item values common to ranged weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = <paramref name="autoReuse"/>;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Ranged"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// noMelee = true;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shoot = <paramref name="baseProjType"/>;
		/// reuseDelay = <paramref name="reuseDelay"/>;
		/// useTime = <paramref name="useTime"/>;
		/// useAnimation = <paramref name="useAnimation"/>;
		/// useAmmo = <paramref name="ammoID"/>;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// </code>
		/// </summary>
		public static void DefaultToRangedWeapon(this Item item, int width, int height, int baseProjType, int ammoID, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int reuseDelay = 0, int crit = 0)
		{
			item.DefaultToRangedWeapon(baseProjType, ammoID, useTime, shootSpeed, autoReuse);
			item.width = width;
			item.height = height;
			item.crit = crit;
			item.damage = damage;
			item.knockBack = knockback;
			item.reuseDelay = reuseDelay;
			item.useAnimation = useAnimation;
		}
		/// <summary>
		/// <inheritdoc cref="DefaultToRangedWeapon"/>
		/// Additional values specific to repeaters:
		/// <code>
		/// shoot = <see cref="ProjectileID.WoodenArrowFriendly"/>;
		/// useAmmo = <see cref="AmmoID.Arrow"/>;
		/// UseSound = <see cref="SoundID.Item5"/>;
		/// </code>
		/// </summary>
		public static void DefaultToRepeater(this Item item, int damage, float knockback, float shootSpeed, int useTime, int useAnimation, bool autoReuse = true, int reuseDelay = 0, int crit = 0, int width = 50, int height = 18)
		{
			item.DefaultToRangedWeapon(width, height, ProjectileID.WoodenArrowFriendly, AmmoID.Arrow, damage, knockback, shootSpeed, useTime, useAnimation, autoReuse, reuseDelay, crit);
			item.UseSound = SoundID.Item5;
		}
		/// <summary>
		/// This method sets a variety of Item values common to shard items.<br/>
		/// Specifically:<code>
		/// createTile = <see cref="ModContent.TileType"/>; (Where <typeparamref name="T"/> is <paramref name="tier2"/> ? <see cref="ShardsTier2"/> : <see cref="Shards"/>)
		/// placeStyle = <paramref name="tileStyleToPlace"/>;
		/// width = 20;
		/// height = 20;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// useAnimation = 15;
		/// useTime = 10;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// useTurn = true;
		/// autoReuse = true;
		/// consumable = true;
		/// rare = <paramref name="tier2"/> ? <see cref="ItemRarityID.Lime"/> : <see cref="ItemRarityID.Green"/>;
		/// value = <paramref name="tier2"/> ? 60 silver : 30 silver;
		/// </code>
		/// </summary>
		public static void DefaultToShard(this Item item, int tileStyleToPlace, bool tier2 = false)
		{
			item.DefaultToPlaceableTile(tier2 ? ModContent.TileType<ShardsTier2>() : ModContent.TileType<Shards>(), tileStyleToPlace);
			item.width = 20;
			item.height = 20;
			item.rare = tier2 ? ItemRarityID.Lime : ItemRarityID.Green;
			item.value = Item.sellPrice(0, 0, tier2 ? 12 : 6);
		}
		/// <summary>
		/// <inheritdoc	cref="DefaultToSpear"/>
		/// Additional values specific to shortswords:
		/// <code>
		/// useStyle = <see cref="ItemUseStyleID.Rapier"/>;
		/// </code>
		/// </summary>
		public static void DefaultToShortsword(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, bool autoReuse = false, int crit = 0, float scale = 1f, int width = 24, int height = 28)
		{
			item.DefaultToSpear(projectile, damage, knockback, singleUseTime, shootSpeed, autoReuse, crit, scale, width, height);
			item.useStyle = ItemUseStyleID.Rapier;
		}
		/// <summary>
		/// This method sets a variety of Item values common to summoning items.<br/>
		/// Specifically:<code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// consumable = <paramref name="consumable"/>;
		/// useAnimation = <paramref name="useAnim"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useTime = <paramref name="useTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.HoldUp"/>;
		/// </code>
		/// </summary>
		public static void DefaultToSpawner(this Item item, bool consumable = true, int useAnim = 45, int useTime = 45, bool useTurn = false, int width = 22, int height = 14)
		{
			item.DefaultToConsumable(consumable, useAnim, useTime, useTurn, width, height);
		}
		/// <summary>
		/// This method sets a variety of Item values common to spear weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = <paramref name="autoReuse"/>;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// scale = <paramref name="scale"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// useTime = <paramref name="singleUseTime"/>;
		/// useAnimation = <paramref name="singleUseTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToSpear(this Item item, int projectile, int damage, float knockback, int singleUseTime, float shootSpeed, bool autoReuse = false, int crit = 0, float scale = 1f, int width = 40, int height = 40)
		{
			item.DefaultToSpear(projectile, shootSpeed, singleUseTime);
			item.width = width;
			item.height = height;
			item.autoReuse = autoReuse;
			item.crit = crit;
			item.damage = damage;
			item.knockBack = knockback;
			item.scale = scale;
		}
		/// <summary>
		/// <inheritdoc	cref="DefaultToMagicWeapon"/>
		/// Additional values specific to spell books:
		/// <code>
		/// autoReuse = true;
		/// scale = <paramref name="scale"/>
		/// </code>
		/// </summary>
		public static void DefaultToSpellBook(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int useTime, int useAnimation, float scale = 0.9f, int crit = 0, int width = 24, int height = 28)
		{
			item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, useTime, useAnimation, true, crit);
			item.scale = scale;
		}
		/// <summary>
		/// This method sets a variety of Item values common to rhotuka spinner items.<br/>
		/// Specifically: <code>
		/// width = 26;
		/// height = 26;
		/// ammo = <see cref="ModContent.ItemType"/>; (Where <typeparamref name="T"/> is <see cref="RhotukaSpinner"/>)
		/// DamageType = <see cref="DamageClass.Ranged"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// damage = <paramref name="damage"/>;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// consumable = <paramref name="consumable"/>;
		/// </code>
		/// </summary>
		public static void DefaultToSpinner(this Item item, int damage, int projectile, float shootSpeed, float knockback, bool consumable = true)
		{
			item.width = 26;
			item.height = 26;
			item.ammo = ModContent.ItemType<RhotukaSpinner>();
			item.DamageType = DamageClass.Ranged;
			item.maxStack = Item.CommonMaxStack;
			item.damage = damage;
			item.shoot = projectile;
			item.shootSpeed = shootSpeed;
			item.knockBack = knockback;
			item.consumable = consumable;
		}
		/// <summary>
		/// This method sets a variety of Item values common to magic staff weapons.<br/>
		/// Specifically: <code>
		/// <inheritdoc	cref="DefaultToMagicWeapon"/>
		/// </code>
		/// </summary>
		public static void DefaultToStaff(this Item item, int projectile, int damage, float knockback, int manaUsed, float shootSpeed, int useTime, int useAnimation, bool autoReuse = false, int crit = 0, int width = 40, int height = 40)
		{
			item.DefaultToMagicWeapon(width, height, projectile, damage, knockback, manaUsed, shootSpeed, useTime, useAnimation, autoReuse, crit);
		}
		/// <summary>
		/// This method sets a variety of Item values common to stamina potion items.<br/>
		/// Specifically: <code>
		/// width = 14;
		/// height = 24;
		/// UseSound = <see cref="SoundID.Item3"/>;
		/// useStyle = <see cref="ItemUseStyleID.DrinkLiquid"/>;
		/// useTurn = true;
		/// useAnimation = 17;
		/// useTime = 17;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// consumable = true;<br></br>
		/// <see cref="Item.GetGlobalItem"/>.HealStamina = <paramref name="staminaAmount"/>; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
		/// </code>
		/// </summary>
		public static void DefaultToStaminaPotion(this Item item, int staminaAmount)
		{
			item.width = 14;
			item.height = 24;
			item.UseSound = SoundID.Item3;
			item.useStyle = ItemUseStyleID.DrinkLiquid;
			item.useTurn = true;
			item.useAnimation = 17;
			item.useTime = 17;
			item.maxStack = Item.CommonMaxStack;
			item.consumable = true;
			item.GetGlobalItem<AvalonGlobalItemInstance>().HealStamina = staminaAmount;
		}
		/// <summary>
		/// This method sets a variety of Item values common to stamina scroll items.<br/>
		/// Specifically:<code>
		/// width = 20;
		/// height = 20;
		/// accessory = true;
		/// rare = <see cref="ItemRarityID.Green"/>;
		/// useStyle = <see cref="ItemUseStyleID.HoldUp"/>;
		/// UseSound = new <see cref="SoundStyle"/>("Avalon/Sounds/Item/Scroll");<br></br>
		/// <see cref="Item.GetGlobalItem"/>.StaminaScroll = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
		/// </code>
		/// </summary>
		public static void DefaultToStaminaScroll(this Item item)
		{
			item.CloneDefaults(ModContent.ItemType<BlankScroll>());
			item.maxStack = 1;
			item.accessory = true;
			item.rare = ItemRarityID.Green;
			item.GetGlobalItem<AvalonGlobalItemInstance>().StaminaScroll = true;
		}
		/// <summary>
		/// This method sets a variety of Item values common to sword weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = <paramref name="autoReuse"/>;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// scale = <paramref name="scale"/>;
		/// useTime = <paramref name="singleUseTime"/>;
		/// useAnimation = <paramref name="singleUseTime"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToSword(this Item item, int damage, float knockback, int singleUseTime, bool autoReuse = true, int crit = 0, float scale = 1f, bool useTurn = true, int width = 40, int height = 40)
		{
			item.width = width;
			item.height = height;
			item.autoReuse = autoReuse;
			item.crit = crit;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.scale = scale;
			item.useTime = singleUseTime;
			item.useAnimation = singleUseTime;
			item.useTurn = useTurn;
			item.useStyle = ItemUseStyleID.Swing;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to thrown weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// autoReuse = <paramref name="autoReuse"/>;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Ranged"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// maxStack = <paramref name="consumable"/> ? <see cref="Item.CommonMaxStack"/> : 1;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// shoot = <paramref name="projectile"/>;
		/// reuseDelay = <paramref name="reuseDelay"/>;
		/// useTime = <paramref name="singleUseTime"/>;
		/// useAnimation = <paramref name="singleUseTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToThrownWeapon(this Item item, int projectile, int damage, float knockback, float shootSpeed, int singleUseTime, bool autoReuse = true, bool consumable = true, int reuseDelay = 0, int crit = 0, int width = 18, int height = 20)
		{
			item.DefaultToThrownWeapon(projectile, singleUseTime, shootSpeed, autoReuse);
			item.width = width;
			item.height = height;
			item.consumable = consumable;
			item.crit = crit;
			item.damage = damage;
			item.knockBack = knockback;
			item.maxStack = consumable ? Item.CommonMaxStack : 1;
			item.noUseGraphic = true;
			item.reuseDelay = reuseDelay;
			item.UseSound = SoundID.Item1;
		}
		/// <summary>
		/// This method sets a variety of Item values common to tome items.<br/>
		/// Specifically: <code>
		/// width = 24;
		/// height = 24;
		/// rare = Math.Min(<paramref name="grade"/> + <paramref name="rarityBonus"/>, <see cref="ItemRarityID.Purple"/>);<br></br>
		/// <see cref="Item.GetGlobalItem"/>.Tome = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)<br></br>
		/// <see cref="Item.GetGlobalItem"/>.TomeGrade = <paramref name="grade"/>; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
		/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> == 1) value = 1 gold 50 silver;
		/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> == 2) value = 3 gold;
		/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> &gt;= 3 and &lt;= 7) value = 10 gold;
		/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> &gt;= 8) value = 15 gold;
		/// if (<paramref name="grade"/> + <paramref name="rarityBonus"/> &gt;= 9) value = 25 gold;
		/// </code>
		/// </summary>
		public static void DefaultToTome(this Item item, int grade, int rarityBonus = 0)
		{
			item.width = 24;
			item.height = 24;
			item.rare = Math.Min(grade + rarityBonus, ItemRarityID.Purple);
			item.GetGlobalItem<AvalonGlobalItemInstance>().Tome = true;
			item.GetGlobalItem<AvalonGlobalItemInstance>().TomeGrade = grade;
			item.value = item.rare switch
			{
				1 => Item.sellPrice(silver: 30),
				2 => Item.sellPrice(silver: 60),
				>= 3 and <= 7 => Item.sellPrice(gold: 2),
				8 => Item.sellPrice(gold: 3),
				>= 9 => Item.sellPrice(gold: 5),
				_ => 0,
			};
		}
		/// <summary>
		/// This method sets a variety of Item values common to tome material items.<br/>
		/// Specifically:<code>
		/// width = 16;
		/// height = 20;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// value = 10 silver;<br/>
		/// <see cref="Item.GetGlobalItem"/>.TomeMaterial = true; (Where <typeparamref name="T"/> is <see cref="AvalonGlobalItemInstance"/>)
		/// </code>
		/// </summary>
		public static void DefaultToTomeMaterial(this Item item)
		{
			item.width = 16;
			item.height = 20;
			item.maxStack = Item.CommonMaxStack;
			item.value = Item.sellPrice(0, 0, 2);
			item.GetGlobalItem<AvalonGlobalItemInstance>().TomeMaterial = true;
		}
		/// <summary>
		/// This method sets a variety of Item values common to treasure bag items.<br/>
		/// Specifically:<code>
		/// width = 24;
		/// height = 24;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// consumable = true;
		/// rare = (<see cref="int"/>)<paramref name="rarity"/>;
		/// expert = true;
		/// </code>
		/// </summary>
		public static void DefaultToTreasureBag(this Item item, TreasureBagRarities rarity)
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = Item.CommonMaxStack;
			item.consumable = true;
			item.rare = (int)rarity;
			item.expert = true;
		}
		/// <summary>
		/// This method sets a variety of Item values common to useable items.<br/>
		/// Specifically:<code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// maxStack = <see cref="Item.CommonMaxStack"/>;
		/// autoReuse = true;
		/// useTurn = true;
		/// consumable = <paramref name="consumable"/>;
		/// useAnimation = <paramref name="useAnim"/>;
		/// useTurn = <paramref name="useTurn"/>;
		/// useTime = <paramref name="useTime"/>;
		/// useStyle = <see cref="ItemUseStyleID.Swing"/>;
		/// </code>
		/// </summary>
		public static void DefaultToUseable(this Item item, bool consumable = true, int useAnim = 15, int useTime = 15, bool useTurn = false, int width = 20, int height = 20)
		{
			item.width = width;
			item.height = height;
			item.maxStack = Item.CommonMaxStack;
			item.autoReuse = true;
			item.useTurn = true;
			item.consumable = consumable;
			item.useAnimation = useAnim;
			item.useTurn = useTurn;
			item.useTime = useTime;
			item.useStyle = ItemUseStyleID.Swing;
		}
		/// <summary>
		/// This method sets a variety of Item values common to vanity items.<br/>
		/// Specifically: <code>
		/// width = 18;
		/// height = 18;
		/// vanity = true;
		/// </code>
		/// </summary>
		public static void DefaultToVanity(this Item item)
		{
			item.width = 18;
			item.height = 18;
			item.vanity = true;
		}
		/// <summary>
		/// This method sets a variety of Item values common to yoyo weapons.<br/>
		/// Specifically: <code>
		/// width = <paramref name="width"/>;
		/// height = <paramref name="height"/>;
		/// channel = true;
		/// crit = <paramref name="crit"/>;
		/// damage = <paramref name="damage"/>;
		/// DamageType = <see cref="DamageClass.Melee"/>;
		/// knockBack = <paramref name="knockback"/>;
		/// noMelee = true;
		/// noUseGraphic = true;
		/// shoot = <paramref name="projectile"/>;
		/// shootSpeed = <paramref name="shootSpeed"/>;
		/// useTime = 25;
		/// useAnimation = 25;
		/// useStyle = <see cref="ItemUseStyleID.Shoot"/>;
		/// UseSound = <see cref="SoundID.Item1"/>;
		/// </code>
		/// </summary>
		public static void DefaultToYoyo(this Item item, int projectile, int damage, float knockback, float shootSpeed, int crit = 0, int width = 24, int height = 24)
		{
			item.width = width;
			item.height = height;
			item.channel = true;
			item.crit = crit;
			item.damage = damage;
			item.DamageType = DamageClass.Melee;
			item.knockBack = knockback;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shootSpeed = shootSpeed;
			item.shoot = projectile;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.Shoot;
			item.UseSound = SoundID.Item1;
		}
	}
}
