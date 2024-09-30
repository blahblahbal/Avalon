using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Avalon.Common;
using Avalon.Common.Players;

namespace Avalon.Items.Other;

class SkyInsignia : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ItemNoGravity[Type] = true;
    }
    public override void SetDefaults()
    {
        Rectangle dims = this.GetDims();
        Item.width = dims.Width;
        Item.height = dims.Height;
    }
    public override void GrabRange(Player player, ref int grabRange)
    {
        grabRange = 78;
    }
    public override bool CanPickup(Player player)
    {
        return true;
    }

    public override bool OnPickup(Player player)
    {
        player.GetModPlayer<AvalonPlayer>().LevelUpSkyBlessing();
        SoundEngine.PlaySound(SoundID.Grab, player.position);
        if (Main.netMode != NetmodeID.SinglePlayer)
        {
            Network.SyncSkyBlessing.SendPacket((byte)player.whoAmI, player.position);
			//foreach (var p in Main.ActivePlayers)
			//{
			//	if (p.whoAmI != player.whoAmI && Vector2.Distance(p.position, player.position) < 43.75f * 16)
			//	{
			//		p.AddBuff(ModContent.BuffType<Buffs.SkyBlessing>(), 60 * 7);
			//	}
			//}
		}
        return false;
    }
}
public class SkyBlessingPickupHook : ModHook
{
    protected override void Apply()
    {
        On_Player.PickupItem += OnPickupItem;
    }
    private static Item OnPickupItem(On_Player.orig_PickupItem orig, Player self, int playerIndex, int worldItemArrayIndex, Item itemToPickUp)
    {
        if (itemToPickUp.type == ModContent.ItemType<SkyInsignia>())
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                Network.SyncSkyBlessing.SendPacket((byte)self.whoAmI, self.position);
            }
            else self.GetModPlayer<AvalonPlayer>().LevelUpSkyBlessing();
            return new Item();
        }
        return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
    }
}
