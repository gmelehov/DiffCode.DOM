using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;


namespace DiffCode.DOM.Core.Extensions;

public static class IParamExtensions
{


  public static T With<T, TVal>(this T prm, TVal val) where T : IParam<TVal> => prm.FluentAction(() => prm.Set(val));



}
