using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Avalon.UI.StatDisplay;

public class StatDisplayUIThing : ExxoUIPanelWrapper<ExxoUIList>
{
    public ExxoUITextPanel StatText { get; }

    public StatDisplayUIThing() : base(new ExxoUIList())
    {
        Height.Set(0, 1);

        InnerElement.FitWidthToContent = true;
        InnerElement.Justification = Justification.Center;
        InnerElement.ContentHAlign = UIAlign.Center;

        StatText = new ExxoUITextPanel("");
        InnerElement.Append(StatText);


    }

    protected override void UpdateSelf(GameTime gameTime)
    {
        base.UpdateSelf(gameTime);

        Player player = Main.LocalPlayer;
        StatText.TextElement.SetText("         Endurance: " + (int)(player.endurance * 100) + "%\n" +
                                     " Magic crit chance: " + player.GetCritChance(DamageClass.Magic) + "%\n" +
                                     " Melee crit chance: " + player.GetCritChance(DamageClass.Melee) + "%\n" +
                                     "Ranged crit chance: " + player.GetCritChance(DamageClass.Melee) + "%\n");
    }
}
