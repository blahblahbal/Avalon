using Avalon.Common;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    internal class Heartsick : ModHook
    {
        protected override void Apply()
        {
            On_Player.PickupItem += OnPickupItem;
        }
        private static Item OnPickupItem(On_Player.orig_PickupItem orig, Player self, int playerIndex, int worldItemArrayIndex, Item itemToPickUp)
        {
            if (self.GetModPlayer<AvalonPlayer>().Heartsick || self.GetModPlayer<AvalonPlayer>().HeartsickElixir)
            {
                if (itemToPickUp.type == ItemID.Heart || itemToPickUp.type == ItemID.CandyApple || itemToPickUp.type == ItemID.CandyCane)
                {
                    SoundEngine.PlaySound(SoundID.Item7, self.position);
                    int amt = 25;
                    if (self.GetModPlayer<AvalonPlayer>().HeartsickElixir) amt = 30;
                    self.Heal(amt);
                    itemToPickUp = new Item();
                    //return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
                }
                // Gold/Platinum hearts LATER
                /*
                if (itemToPickUp.type == ModContent.ItemType<PlatinumHeart>() || itemToPickUp.type == ItemID.CandyApple || itemToPickUp.type == ItemID.CandyCane)
                {
                    SoundEngine.PlaySound(SoundID.Item7, self.position);
                    self.Heal(25);
                    itemToPickUp = new Item();
                    //return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
                }*/
            }
            return orig(self, playerIndex, worldItemArrayIndex, itemToPickUp);
        }
    }
}
