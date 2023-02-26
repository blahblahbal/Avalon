using ExxoAvalonOrigins.Common;
using ExxoAvalonOrigins.UI;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;
using Terraria.UI;

namespace ExxoAvalonOrigins.Hooks
{
    public class UIChanges : ModHook
    {
        protected override void Apply()
        {
            On_UIElement.Remove += OnUIElementRemove;
            IL_UIElement.Recalculate += ILUIElementRecalculate;
        }

        /// <summary>
        ///     ExxoUIElements use a better removal system that removes children after updating, this allows children to remove
        ///     themselves during update.
        /// </summary>
        /// <param name="orig">Delegate that calls the original method.</param>
        /// <param name="self">The calling UIElement instance.</param>
        private static void OnUIElementRemove(On_UIElement.orig_Remove orig, Terraria.UI.UIElement self)
        {
            if (self.Parent is ExxoUIElement exxoParent)
            {
                exxoParent.ElementsForRemoval.Enqueue(self);
            }
            else
            {
                orig(self);
            }
        }

        /// <summary>
        ///     Some trickery that changes default behaviour of Recalculate to only recalculate self when element is ExxoUIElement.
        /// </summary>
        /// <param name="il">The ILContext of the original method.</param>
        private static void ILUIElementRecalculate(ILContext il)
        {
            var c = new ILCursor(il);
            c.GotoNext(i => i.MatchCallvirt(typeof(Terraria.UI.UIElement).GetMethod(
                nameof(Terraria.UI.UIElement.RecalculateChildren),
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, Type.EmptyTypes, null)));
            c.Index--;

            ILLabel label = c.DefineLabel();

            c.Emit(OpCodes.Ldarg_0);
            c.EmitDelegate<Func<Terraria.UI.UIElement, bool>>(element => element is ExxoUIElement);
            c.Emit(OpCodes.Brtrue, label)
                .GotoNext(i => i.MatchRet())
                .MarkLabel(label);
        }
    }
}
