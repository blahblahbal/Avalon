using System.Linq.Expressions;
using System.Reflection;
using Terraria.UI;

namespace Avalon.UI.Next;

public class FakeEventTarget : UIElement {
    public readonly ExxoUIElement LinkedElement;

    public FakeEventTarget(ExxoUIElement linkedElement) {
        LinkedElement = linkedElement;
    }

    private delegate void ParentSetterDelegate(UIElement instance, UIElement value);

    private static ParentSetterDelegate CacheParentSetter() {
        PropertyInfo propertyInfo =
            typeof(UIElement).GetProperty(nameof(UIElement.Parent), BindingFlags.Instance | BindingFlags.Public)!;

        ParameterExpression instanceParam = Expression.Parameter(typeof(UIElement), "instance");
        ParameterExpression valueParam = Expression.Parameter(typeof(UIElement), "value");

        MethodCallExpression setterCall = Expression.Call(instanceParam, propertyInfo.SetMethod!, valueParam);

        return Expression.Lambda<ParentSetterDelegate>(setterCall, instanceParam, valueParam).Compile();
    }

    private static readonly ParentSetterDelegate ParentSetter = CacheParentSetter();

    public new UIElement Parent {
        get => base.Parent;
        set => ParentSetter(this, value);
    }
}
