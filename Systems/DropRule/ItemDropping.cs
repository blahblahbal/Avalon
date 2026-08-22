using Avalon.Reflection;
using System.Reflection;
using Terraria.GameContent.ItemDropRules;

namespace Avalon.Systems.DropRule;
//MIT License

//Copyright(c) 2024
//Tyfyter

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
public class ItemDropping
{
	delegate ItemDropAttemptResult Del_ResolveRule(IItemDropRule rule, DropAttemptInfo info);
	private static readonly Del_ResolveRule Cached_Del_ResolveRule =
		Utilities.CacheInstanceMethod<Del_ResolveRule>(typeof(ItemDropResolver).GetMethod("ResolveRule", BindingFlags.Instance | BindingFlags.NonPublic)!);
	public static ItemDropAttemptResult ResolveRule(IItemDropRule rule, DropAttemptInfo info) => Cached_Del_ResolveRule(rule, info);
}