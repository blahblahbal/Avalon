using Avalon.ModSupport.MLL.Dusts;
using Avalon.ModSupport.MLL.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModLiquidLib.ModLoader;
using ModLiquidLib.Utils.Structs;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.ModSupport.MLL.Liquids;

internal class Blood : ModLiquid
{
	//SetStaticDefaults are the defaults added when the game initially loads.
	//Here we set a few settings that this liquid will have.
	//SetStaticDefaults is only ever run once just after all the content from mods are added to the game.
	public override void SetStaticDefaults()
	{
		//This is the viscosity of the liquid, only used visually.
		//Lava usually has this set to 200, while honey has this set to 240. All other liquids set this to 0 by default.
		//In Vanilla this property can be found at "Terraria.GameContent.Liquid.LiquidRenderer.VISCOSITY_MASK"
		VisualViscosity = 160;

		//This is the length the liquid will visually have when flowing/falling downwards or if there is a slope underneath.
		//In Vanilla This property can be found at "Terraria.GameContent.Liquid.LiquidRenderer.WATERFALL_LENGTH"
		LiquidFallLength = 6;

		//This is the opacity of the liquid. How well you can see objects in the liquid.
		//The SlopeOpacity property is different, as slopes do not render the same as a normal liquid tile
		//DefaultOpacity in vanilla, can be found at "Terraria.GameContent.Liquid.LiquidRenderer.DEFAULT_OPACITY"
		DefaultOpacity = 1f;
		SlopeOpacity = 1f;
		//To change the old liquid rendering opacity, please see the RetroDrawEffects override.

		//For the Waves Quality setting, when set to Medium, waves are set to be the same distance no matter the liquid type.
		//To do this, the game applied a multiplier to make them all consistant between liquids. Here we set our own multiplier to make the waves the same distance.
		WaterRippleMultiplier = 0.6f;

		//This is used to specify what dust is used when splashing in this liquid.
		//Normally, when returning false in each OnSplash hook/method, this property is used in the mod liquid's default splash code
		//It returns -1 normally, which prevents the liquid from doing any splash dust
		//Here we set it, as we use the property in our OnSplash hooks to have one central variable that controls which dust ID is used in our custom splash
		SplashDustType = ModContent.DustType<BloodLiquidSplash>();

		//This is used to specify what sound is played when an entity enters a liquid
		//Normally this property is used in the mod liquid's default splash code and returns null as no sound is played normally.
		//Similarly to SplashDustType, we use this to have 1 central place for the splash sound used accross each OnSplash hooks.
		SplashSound = SoundID.SplashWeak;

		FallDelay = 2; //The delay when liquids are falling. Liquids will wait this extra amount of frames before falling again.

		ChecksForDrowning = true; //If the player can drown in this liquid
		PlayersEmitBreathBubbles = false; //Bubbles will come out of the player's mouth normally when drowning, here we can stop that by setting it to false.

		//For modders who don't want to reimplement the entire player movement for this liquid, this multiplier is used in the default mod liquid player movement.
		//Here we make our liquid slow the player down by half what honey would allow the player to move at.

		//Heres the defaults for each liquid:
		//Water/Lava/Regular modded liquid = 0.5f
		//Honey = 0.25f
		//Shimmer = 0.375f
		PlayerMovementMultiplier = 0.375f;
		StopWatchMPHMultiplier = PlayerMovementMultiplier; //We set stopwatch to the same multiplier as we don't want a different between whats felt and what the player can read their movement as.
		NPCMovementMultiplierDefault = PlayerMovementMultiplier; //NPCs have a similar modifier but as a field, here we set the default value as some other NPCs set this multiplier to 0. We set this to PlayerMovementMultiplier as we need them to all be the same.
		ProjectileMovementMultiplier = PlayerMovementMultiplier; //Simiarly to Players, Projectiles have this property for easy editing of a projectile velocity multiplier without needing to reimplement all of the projectile liquid movement code.

		FishingPoolSizeMultiplier = 2f; //The multiplier used for calculating the size of a fishing pool of this liquid. Here, each liquid tile counts as 2 for every tile in a fished pool.

		//We can add a map entry to our liquid, by doing so we can show where our liquid is on the map.
		//Unlike vanilla, we can also add a map entry name, which will display a name if the liquid is being selected on the map.
		AddMapEntry(new Color(200, 0, 0));
	}

	//Here with LiquidMerge, we are able to decide when the liquid generates with a different tile.
	//Using the otherLiquid param, we are able to select which liquid that collides with ours creates a specific tile.
	public override int LiquidMerge(int i, int j, int otherLiquid)
	{
		if (otherLiquid == LiquidID.Water)
		{
			return ModContent.TileType<CoagulatedBlood>(); //When the liquid collides with water. Blue team block is created
		}
		else if (otherLiquid == LiquidID.Lava)
		{
			return ModContent.TileType<BoiledBlood>(); //When the liquid collides with lava. Red team block is created
		}
		else if (otherLiquid == LiquidID.Honey)
		{
			return ModContent.TileType<BloodClot>(); //When the liquid collides with honey. Yellow team block is created
		}
		else if (otherLiquid == LiquidID.Shimmer)
		{
			return ModContent.TileType<Plasma>(); //When the liquid collides with shimmer. Pink team block is created
		}
		//The base return is what the liquid generates by default. This is useful for when this liquid collides with another modded liquid that this liquid has no support for.
		//usually by default, this method returns TIleID.Stone, and generates a stone tile if it cannot recognize any predetermined tile type to generate with
		return TileID.Stone;

		//NOTE: for custom collisions/tile creation, please see PreLiquidMerge to determine whether the liquid should do its normal tile merging,
		//or if you want to do other effects when this liquid merges with another liquid.
	}

	//LiquidMergeSound is played only on clients, and serves as editing the sound of when a liquid merges with another liquid.
	//Here we set a few custom sounds to play when this liquid merges with other liquids.
	public override void LiquidMergeSound(int i, int j, int otherLiquid, ref SoundStyle? collisionSound)
	{
		collisionSound = SoundID.LiquidsHoneyWater; //by default, we set the sound to be the glass shattering sound when merging with a liquid.
		if (otherLiquid == LiquidID.Water)
		{
			collisionSound = SoundID.LiquidsHoneyWater; //...but if the liquid being merged is water, then we play Item 2 (Eating sound)
		}
		else if (otherLiquid == LiquidID.Lava)
		{
			collisionSound = SoundID.LiquidsHoneyLava;
		}
		else if (otherLiquid == LiquidID.Shimmer)
		{
			collisionSound = SoundID.LiquidsWaterLava; //...but if the liquid being merged is shimmer, then we play the maximum mana sound
		}
	}

	//ChooseWaterfallStyle allows for the selection of what waterfall style this liquid chooses when next to a slope.
	public override int ChooseWaterfallStyle(int i, int j)
	{
		return ModContent.GetInstance<BloodFall>().Slot;
	}

	//LiquidLightMaskMode is how the game decides what lightMaskMode to use when this liquid is over a tile
	//We set this to none, this is due to the liquid emitting light, needing no special lightMaskMode for its interaction with light.
	public override LightMaskMode LiquidLightMaskMode(int i, int j)
	{
		return LightMaskMode.None;
	}

	//ModifyLight allows the liquid to emit light similarly to any tile or wall.
	//You can use this to emit light similarly to lava or shimmer.
	//public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	//{
	//	//Here we make the liquid just emit a bright white light by setting R, G, and B to 1.
	//	r = 1f;
	//	g = 1f;
	//	b = 1f;
	//}

	//Using EvaporatesInHell, we are able to choose whether this liquid evaporates in hell, based on a condition.
	//For custom evaporation, use UpdateLiquid override.
	public override bool EvaporatesInHell(int i, int j)
	{
		return false;
	}

	//Using RetroDrawEffects, we can do stuff only during the rendering of liquids in the retro lighting style.
	//Here we set the opacity we want during retro lighting so that its consistant with the opacity of the liquid when not in the retro lighting style
	//NOTE: Despite being having RETRO in the name, this also applies to the "Trippy" Lighting style as well.
	public override void RetroDrawEffects(int i, int j, SpriteBatch spriteBatch, ref RetroLiquidDrawInfo drawData, float liquidAmountModified, int liquidGFXQuality)
	{
		drawData.liquidAlphaMultiplier *= 1.8f;
		if (drawData.liquidAlphaMultiplier > 1f)
		{
			drawData.liquidAlphaMultiplier = 1f;
		}
	}

	public override void ItemLiquidMovement(Item item, ref Vector2 wetVelocity, ref float gravity, ref float maxFallSpeed)
	{
		// same as shimmer values
		gravity = 0.065f;
		maxFallSpeed = 4f;
		wetVelocity = item.velocity * 0.375f;
	}

	//The following region contains all the logic for what this liquid does when being entered and exited by different entities.
	#region Splash Effects

	//Each hook/method is used to execute what would happen when something enters/exits a liquid.
	//There is a hook for the following
	// * Players
	// * NPCs
	// * Projectiles
	// * Items
	//Each hook has a "isEnter" param, which is true whenever entity is entering the liquid.
	//This is usually used to do different effects when entering a liquid rather than exiting one

	//The following hooks/methods have adapted code from vanilla's splash code for honey as the splash dusts themselves are based on the honey splash dust.
	public override bool OnPlayerSplash(Player player, bool isEnter)
	{
		for (int i = 0; i < 20; i++)
		{
			int dust = Dust.NewDust(new Vector2(player.position.X - 6f, player.position.Y + (player.height / 2) - 8f), player.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= 1f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, player.position);
		return false;
	}

	public override bool OnNPCSplash(NPC npc, bool isEnter)
	{
		for (int i = 0; i < 10; i++)
		{
			int dust = Dust.NewDust(new Vector2(npc.position.X - 6f, npc.position.Y + (npc.height / 2) - 8f), npc.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= 1f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		//only play the sound if the npc isnt a slime, mouse, tortoise, or if it has no gravity
		if (npc.aiStyle != NPCAIStyleID.Slime &&
				npc.type != NPCID.BlueSlime && npc.type != NPCID.MotherSlime && npc.type != NPCID.IceSlime && npc.type != NPCID.LavaSlime &&
				npc.type != NPCID.Mouse &&
				npc.aiStyle != NPCAIStyleID.GiantTortoise &&
				!npc.noGravity)
		{
			SoundEngine.PlaySound(SplashSound, npc.position);
		}
		return false;
	}

	public override bool OnProjectileSplash(Projectile proj, bool isEnter)
	{
		for (int i = 0; i < 10; i++)
		{
			int dust = Dust.NewDust(new Vector2(proj.position.X - 6f, proj.position.Y + (proj.height / 2) - 8f), proj.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= 1f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, proj.position);
		return false;
	}

	public override bool OnItemSplash(Item item, bool isEnter)
	{
		for (int i = 0; i < 5; i++)
		{
			int dust = Dust.NewDust(new Vector2(item.position.X - 6f, item.position.Y + (item.height / 2) - 8f), item.width + 12, 24, SplashDustType);
			Main.dust[dust].velocity.Y -= 1f;
			Main.dust[dust].velocity.X *= 2.5f;
			Main.dust[dust].scale = 1.3f;
			Main.dust[dust].alpha = 100;
			Main.dust[dust].noGravity = true;
		}
		SoundEngine.PlaySound(SplashSound, item.position);
		return false;
	}
	#endregion
}
