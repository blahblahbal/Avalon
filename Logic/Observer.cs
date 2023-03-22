using System;

namespace Avalon.Logic;

internal class Observer<T> where T : IEquatable<T>
{
    private readonly Func<T> provider;
    private T? oldValue;
    public Observer(Func<T> provider) => this.provider = provider;

    public bool Check()
    {
        bool changed = !provider().Equals(oldValue);
        oldValue = provider();
        return changed;
    }
}
