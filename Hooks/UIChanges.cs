using System;
using System.Reflection;
using Avalon.Common;
using Avalon.UI.Next;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.UI;

namespace Avalon.Hooks;

public class UIChanges : ModHook {
    protected override void Apply() {
        On_UIElement.Remove += OnUIElementRemove;
        On_UIElement.GetElementAt += OnUIElementGetElementAt;
        IL_UIElement.Recalculate += ILUIElementRecalculate;
    }

    /// <summary>
    ///     ExxoUIElements use a better removal system that removes children after updating, this allows children to remove
    ///     themselves during update.
    /// </summary>
    /// <param name="orig">Delegate that calls the original method.</param>
    /// <param name="self">The calling UIElement instance.</param>
    private static void OnUIElementRemove(On_UIElement.orig_Remove orig, UIElement self) {
        if (self.Parent is UI.ExxoUIElement exxoParent) {
            exxoParent.ElementsForRemoval.Enqueue(self);
        }
        else {
            orig(self);
        }
    }

    /// <summary>
    ///     Some trickery that changes default behaviour of Recalculate to only recalculate self when element is ExxoUIElement.
    /// </summary>
    /// <param name="il">The ILContext of the original method.</param>
    private static void ILUIElementRecalculate(ILContext il) {
        var c = new ILCursor(il);
        c.GotoNext(i => i.MatchCallvirt(typeof(UIElement).GetMethod(
            nameof(UIElement.RecalculateChildren),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, Type.EmptyTypes, null)));
        c.Index--;

        ILLabel label = c.DefineLabel();

        c.Emit(OpCodes.Ldarg_0);
        c.EmitDelegate<Func<UIElement, bool>>(element => element is UI.ExxoUIElement);
        c.Emit(OpCodes.Brtrue, label)
            .GotoNext(i => i.MatchRet())
            .MarkLabel(label);
    }

    private static UIElement? OnUIElementGetElementAt(On_UIElement.orig_GetElementAt orig, UIElement self,
                                                      Vector2 point) {
        if (self is not ExxoToVanillaUIAdapter exxoToVanillaUIAdapter) {
            return orig(self, point);
        }

        ExxoUIElement? exxoTarget = exxoToVanillaUIAdapter.ExxoUIElement.GetElementAt(point.ToPoint());
        if (exxoTarget is VanillaToExxoUIAdapter vanillaToExxoUIAdapter) {
            return vanillaToExxoUIAdapter.VanillaElement.GetElementAt(point);
        }

        return exxoTarget != null ? new FakeEventTarget(exxoTarget) { Parent = self } : orig(self, point);
    }
}
