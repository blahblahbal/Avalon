using Avalon.Common;
using Avalon.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace Avalon.Hooks
{
    public class UpdateMusic : ModHook
    {
        protected override void Apply()
        {
            On_Main.UpdateAudio_DecideOnNewMusic += On_Main_UpdateAudio_DecideOnNewMusic;
        }

        private void On_Main_UpdateAudio_DecideOnNewMusic(On_Main.orig_UpdateAudio_DecideOnNewMusic orig, Main self)
        {
            orig(self);
            //if (Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrack && Main.musicBox2 == 0)
            //{
            //    Main.musicBox2 = Main.LocalPlayer.GetModPlayer<AvalonJukeboxPlayer>().PlayingATrackID;
            //}
        }
    }
}
