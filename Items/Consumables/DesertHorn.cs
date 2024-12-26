using Avalon.Common.Players;
using Avalon.NPCs.Bosses.PreHardmode;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Items.Consumables;

class DesertHorn : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.ResearchUnlockCount = 3;
    }
    public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
    {
        itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossSpawners;
    }
    public override void SetDefaults()
    {
        //Rectangle dims = this.GetDims();
        Item.rare = ItemRarityID.Blue;
        Item.consumable = true;
        Item.width = 32;
        Item.maxStack = 9999;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.height = 28;
        Item.useAnimation = Item.useTime = 18;
    }
    public override void AddRecipes()
    {
        Recipe.Create(Type)
            .AddIngredient(ModContent.ItemType<Material.Beak>(), 6)
            .AddIngredient(ItemID.SandBlock, 30)
            .AddIngredient(ItemID.FossilOre, 10)
            .AddTile(TileID.Hellforge)
            .Register();
    }
    public override bool CanUseItem(Player player)
    {
        return !NPC.AnyNPCs(ModContent.NPCType<NPCs.Bosses.PreHardmode.DesertBeak>()) && player.ZoneDesert && Main.dayTime;
    }
    public override void UseAnimation(Player player)
    {
        SoundEngine.PlaySound(new SoundStyle($"{nameof(Avalon)}/Sounds/Item/DesertHorn"), player.position);
    }
	public override bool? UseItem(Player player)
	{
		if (player.whoAmI == Main.myPlayer && player.itemAnimation == 1)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				// If the player is not in multiplayer, spawn directly
				NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DesertBeak>());
			}
			else
			{
				// If the player is in multiplayer, request a spawn
				// This will only work if NPCID.Sets.MPAllowedEnemies[type] is true
				NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: ModContent.NPCType<DesertBeak>());
			}
			SoundEngine.PlaySound(SoundID.Roar, player.position);
		}
		return base.UseItem(player);
	}
	public override void HoldItem(Player player)
    {
        if (player.ItemAnimationJustStarted)
        {
            player.inventory[player.selectedItem].stack--;
            if (player.inventory[player.selectedItem].stack <= 0)
            {
                player.inventory[player.selectedItem].SetDefaults();
            }
        }
    }
}
