using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Core.Models.Texts;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;

namespace DiffCode.DOM.Core.Extensions;

/// <summary>
/// Методы расширения для документа.
/// </summary>
public static class IDocumExtensions
{


  /// <summary>
  /// Добавляет новое разнородное содержимое к корневому абзацу документа.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="docum">Документ.</param>
  /// <param name="objects">Список добавляемых объектов.</param>
  /// <returns></returns>
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


  /// <summary>
  /// Добавляет новые дочерние абзацы к корневому абзацу документа.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="doc">Документ.</param>
  /// <param name="elems">Список добавляемых абзацев.</param>
  /// <returns></returns>
  public static T With<T>(this T doc, params IPara[] elems) where T : IDocum => doc.FluentAction(() => doc.Content.AddRange(elems));


  /// <summary>
  /// Добавляет новые текстовые фрагменты к корневому абзацу документа.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="doc">Документ.</param>
  /// <param name="elems">Список добавляемых текстовых фрагментов.</param>
  /// <returns></returns>
  public static T With<T>(this T doc, params IText[] elems) where T : IDocum => doc.FluentAction(() => doc.Content.AddRange(elems));


  /// <summary>
  /// Создает новые текстовые фрагменты из указанного списка строк и 
  /// добавляет их к корневому абзацу документа.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="doc">Документ.</param>
  /// <param name="elems">Список строк.</param>
  /// <returns></returns>
  public static T With<T>(this T doc, params string[] elems) where T : IDocum => doc.FluentAction(() => doc.Content.AddRange(elems.Select(s => new Text(s)).ToArray()));


  /// <summary>
  /// Добавляет условие для вычисления признака активного/видимого документа.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="doc">Документ.</param>
  /// <param name="expr">Условие для вычисления признака активного/видимого документа.</param>
  /// <returns></returns>
  public static T With<T>(this T doc, Expr expr) where T : IDocum => doc.FluentAction(() => doc.SetActiveOn<T>(expr));


}
