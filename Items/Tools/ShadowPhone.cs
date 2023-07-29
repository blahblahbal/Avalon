using Avalon.Common;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Tools;

class ShadowPhone : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
        }
        return false;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneHome>());
        }
    }
    public override bool? UseItem(Player player)
    {
        player.Shellphone_Spawn();
        SoundEngine.PlaySound(SoundID.Item6, player.position);
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

class ShadowPhoneDungeon : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneJungleTropics>());
        }
        return false;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneJungleTropics>());
        }
    }
    public override bool? UseItem(Player player)
    {
        DungeonPort(player);
        SoundEngine.PlaySound(SoundID.Item6, player.position);
        return true;
    }

    public void DungeonPort(Player player)
    {
        bool canSpawn = false;
        int num = Main.dungeonX;
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

class ShadowPhoneOcean : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneHell>());
        }
        return false;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneHell>());
        }
    }
    public override bool? UseItem(Player player)
    {
        player.MagicConch();
        SoundEngine.PlaySound(SoundID.Item6, player.position);
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

class ShadowPhoneHell : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneRandom>());
        }
        return false;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneRandom>());
        }
    }
    public override bool? UseItem(Player player)
    {
        player.DemonConch();
        SoundEngine.PlaySound(SoundID.Item6, player.position);
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

class ShadowPhoneJungleTropics : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneOcean>());
        }
        return false;
    }
    public override bool? UseItem(Player player)
    {
        JungleTropicsPort(player);
        SoundEngine.PlaySound(SoundID.Item6, player.position);
        return true;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneOcean>());
        }
    }
    public void JungleTropicsPort(Player player)
    {
        bool canSpawn = false;
        int num = AvalonWorld.JungleLocationX;
        if (AvalonWorld.JungleLocationX == 0)
        {
            num = Main.maxTilesX - Main.dungeonX;
        }
        int num2 = 100;
        int num3 = num2 / 2;
        int teleportStartY = 300;
        int teleportRangeY = 50;
        Player.RandomTeleportationAttemptSettings settings = new Player.RandomTeleportationAttemptSettings
        {
            mostlySolidFloor = true,
            avoidAnyLiquid = false,
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

class ShadowPhoneRandom : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhone>());
        }
        return false;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhone>());
        }
    }
    public override bool? UseItem(Player player)
    {
        player.TeleportationPotion();
        SoundEngine.PlaySound(SoundID.Item6, player.position);
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

class ShadowPhoneHome : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 1;
        ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
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
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = 0;
    }
    public override bool CanRightClick()
    {
        if (Main.mouseRightRelease && Main.mouseRight)
        {
            SoundEngine.PlaySound(SoundID.Unlock, Main.LocalPlayer.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneDungeon>());
        }
        return false;
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
        player.Spawn(PlayerSpawnContext.RecallFromItem);
        SoundEngine.PlaySound(SoundID.Item6, player.position);
        return true;
    }
    public override void HoldItem(Player player)
    {
        if (Main.mouseRight && Main.mouseRightRelease && !Main.mapFullscreen)
        {
            SoundEngine.PlaySound(SoundID.Unlock, player.position);
            Item.ChangeItemType(ModContent.ItemType<ShadowPhoneDungeon>());
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
