using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;


namespace DiffCode.DOM.Core.Extensions;

public static class ITextExtensions
{


  public static T WithFormat<T>(this T text, TextFormat format) where T : IText => text.FluentAction(() => text.Format = format);


}