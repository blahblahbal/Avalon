using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Avalon.Items.Tools;

class ShadowShellphone : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphoneHome>());
        }
        else
        {
            player.Shellphone_Spawn();
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}

class ShadowShellphoneDungeon : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override string Texture => ModContent.GetInstance<ShadowShellphone>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphoneJungleTropics>());
        }
        else
        {
            DungeonPort(player);
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }

    public void DungeonPort(Player player)
    {
        //Vector2 newPos2 = new Vector2(Main.dungeonX, Main.dungeonY - 2.5f) * 16;
        bool canSpawn = false;
        int num = Main.dungeonX;
        Main.NewText(num);
        int num2 = 100;
        int num3 = num2 / 2;
        int teleportStartY = Main.dungeonY - 3;
        int teleportRangeY = 0;
        Player.RandomTeleportationAttemptSettings settings = new Player.RandomTeleportationAttemptSettings
        {
            mostlySolidFloor = true,
            avoidAnyLiquid = true,
            avoidLava = true,
            avoidHurtTiles = true,
            avoidWalls = true,
            attemptsBeforeGivingUp = 1000,
            maximumFallDistanceFromOrignalPoint = 30
        };
        Vector2 vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num3, num2, teleportStartY, teleportRangeY, settings);
        if (!canSpawn)
        {
            vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num2, num3, teleportStartY, teleportRangeY, settings);
        }
        if (!canSpawn)
        {
            vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num + num3, num3, teleportStartY, teleportRangeY, settings);
        }
        if (canSpawn)
        {
            Vector2 newPos = vector;
            player.Teleport(newPos, 7);
            player.velocity = Vector2.Zero;
            if (Main.netMode == NetmodeID.Server)
            {
                RemoteClient.CheckSection(player.whoAmI, player.position);
                NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos.X, newPos.Y, 7);
            }
        }
        else
        {
            Vector2 newPos2 = player.position;
            player.Teleport(newPos2, 7);
            player.velocity = Vector2.Zero;
            if (Main.netMode == NetmodeID.Server)
            {
                RemoteClient.CheckSection(player.whoAmI, player.position);
                NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos2.X, newPos2.Y, 7, 1);
            }
        }
    }

    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}

class ShadowShellphoneOcean : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override string Texture => ModContent.GetInstance<ShadowShellphone>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphoneHell>());
        }
        else
        {
            player.MagicConch();
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}

class ShadowShellphoneHell : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override string Texture => ModContent.GetInstance<ShadowShellphone>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphoneRandom>());
        }
        else
        {
            player.DemonConch();
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }

    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}

class ShadowShellphoneJungleTropics : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override string Texture => ModContent.GetInstance<ShadowShellphone>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }

    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphoneOcean>());
        }
        else
        {
            JungleTropicsPort(player);
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public void JungleTropicsPort(Player player)
    {
        bool canSpawn = false;
        int num = AvalonWorld.JungleLocationX;
        Main.NewText(num);
        int num2 = 100;
        int num3 = num2 / 2;
        int teleportStartY = 250;
        int teleportRangeY = 80;
        Player.RandomTeleportationAttemptSettings settings = new Player.RandomTeleportationAttemptSettings
        {
            mostlySolidFloor = true,
            avoidAnyLiquid = true,
            avoidLava = true,
            avoidHurtTiles = true,
            avoidWalls = true,
            attemptsBeforeGivingUp = 1000,
            maximumFallDistanceFromOrignalPoint = 30
        };
        Vector2 vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num3, num2, teleportStartY, teleportRangeY, settings);
        if (!canSpawn)
        {
            vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num - num2, num3, teleportStartY, teleportRangeY, settings);
        }
        if (!canSpawn)
        {
            vector = player.CheckForGoodTeleportationSpot(ref canSpawn, num + num3, num3, teleportStartY, teleportRangeY, settings);
        }
        if (canSpawn)
        {
            Vector2 newPos = vector;
            player.Teleport(newPos, 7);
            player.velocity = Vector2.Zero;
            if (Main.netMode == NetmodeID.Server)
            {
                RemoteClient.CheckSection(player.whoAmI, player.position);
                NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos.X, newPos.Y, 7);
            }
        }
        else
        {
            Vector2 newPos2 = player.position;
            player.Teleport(newPos2, 7);
            player.velocity = Vector2.Zero;
            if (Main.netMode == NetmodeID.Server)
            {
                RemoteClient.CheckSection(player.whoAmI, player.position);
                NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, player.whoAmI, newPos2.X, newPos2.Y, 7, 1);
            }
        }
    }

    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}

class ShadowShellphoneRandom : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override string Texture => ModContent.GetInstance<ShadowShellphone>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphone>());
        }
        else
        {
            player.TeleportationPotion();
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }

    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}

class ShadowShellphoneHome : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
    }
    public override string Texture => ModContent.GetInstance<ShadowShellphone>().Texture;
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Red;
        Item.width = dims.Width;
        Item.useTime = 30;
        Item.useTurn = true;
        Item.value = 500000;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 30;
        Item.height = dims.Height;
    }
    public override bool AltFunctionUse(Player player)
    {
        return true;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ItemID.Shellphone)
            .AddIngredient(ItemID.FallenStar, 40)
            .AddIngredient(ItemID.Diamond, 20)
            .AddIngredient(ItemID.ChlorophyteBar, 7)
            .AddIngredient(ItemID.Ectoplasm, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.ShellphoneSpawn)
            .AddIngredient(ItemID.FallenStar, 40)
            .AddIngredient(ItemID.Diamond, 20)
            .AddIngredient(ItemID.ChlorophyteBar, 7)
            .AddIngredient(ItemID.Ectoplasm, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.ShellphoneOcean)
            .AddIngredient(ItemID.FallenStar, 40)
            .AddIngredient(ItemID.Diamond, 20)
            .AddIngredient(ItemID.ChlorophyteBar, 7)
            .AddIngredient(ItemID.Ectoplasm, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        Recipe.Create(Type)
            .AddIngredient(ItemID.ShellphoneHell)
            .AddIngredient(ItemID.FallenStar, 40)
            .AddIngredient(ItemID.Diamond, 20)
            .AddIngredient(ItemID.ChlorophyteBar, 7)
            .AddIngredient(ItemID.Ectoplasm, 10)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
    }
    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.SetDefaults(ModContent.ItemType<ShadowShellphoneDungeon>());
        }
        else
        {
            player.Spawn(PlayerSpawnContext.RecallFromItem);
            SoundEngine.PlaySound(SoundID.Item6, player.position);
        }
        return true;
    }

    public override void UpdateInventory(Player player)
    {
        player.accThirdEye = player.accFishFinder = player.accWeatherRadio = player.accCalendar = player.accCritterGuide = player.accDreamCatcher =
            player.accJarOfSouls = player.accStopwatch = player.accOreFinder = true;
        player.accWatch = 3;
        player.accDepthMeter = 1;
        player.accCompass = 1;
    }
}
