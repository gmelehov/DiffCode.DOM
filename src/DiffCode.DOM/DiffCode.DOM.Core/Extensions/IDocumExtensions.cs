using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Core.Models.Texts;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;


namespace DiffCode.DOM.Core.Extensions;

public static class IDocumExtensions
{


  public static T With<T>(this T docum, params object[] objects) where T : IDocum => docum.FluentAction(() =>
  {
    foreach (var obj in objects)
    {
      if (obj is IPara pp)
      {
        docum.Content.AddRange(pp);
      }
      else if (obj is IText tt)
      {
        docum.Content.AddRange(tt);
      }
      else if (obj is string ss)
      {
        docum.Content.AddRange(new Text(ss));
      }
      else if (obj is IParam prm)
      {
        docum.Content.AddRange(new Text(prm));
      }
      else if (obj is Expr expr)
      {
        docum.SetActiveOn<T>(expr);
      }
    }
    ;
  });

  public static T With<T>(this T doc, params IPara[] elems) where T : IDocum => doc.FluentAction(() => doc.Content.AddRange(elems));


  public static T With<T>(this T doc, params IText[] elems) where T : IDocum => doc.FluentAction(() => doc.Content.AddRange(elems));


  public static T With<T>(this T doc, params string[] elems) where T : IDocum => doc.FluentAction(() => doc.Content.AddRange(elems.Select(s => new Text(s)).ToArray()));


  public static T With<T>(this T doc, Expr expr) where T : IDocum => doc.FluentAction(() => doc.SetActiveOn<T>(expr));


}
