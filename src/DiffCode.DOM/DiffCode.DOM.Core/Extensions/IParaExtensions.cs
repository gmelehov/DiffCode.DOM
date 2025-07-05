using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Core.Models.Paragraphs;
using DiffCode.DOM.Core.Models.Params;
using DiffCode.DOM.Core.Models.Texts;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;
using System.Linq.Expressions;
using System.Reflection;


namespace DiffCode.DOM.Core.Extensions;

/// <summary>
/// Методы расширения для абзаца.
/// </summary>
public static class IParaExtensions
{


  /// <summary>
  /// Добавляет новое разнородное содержимое к абзацу.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац.</param>
  /// <param name="objects">Список добавляемых объектов.</param>
  /// <returns></returns>
  public static T With<T>(this T para, params object[] objects) where T : IPara => para.FluentAction(() =>
  {
    foreach (var obj in objects)
    {
      if (obj is IPara pp)
      {
        para.AddRange(pp);
      }
      else if (obj is IText tt)
      {
        para.AddRange(tt);
      }
      else if (obj is string ss)
      {
        para.AddRange(new Text(ss));
      }
      else if (obj is IParam prm)
      {
        para.AddRange(new Text(prm));
      }
      else if (obj is DateOnlyParam prmDate)
      {
        para.AddRange(new Text(prmDate));
      }
      else if (obj is Expr expr)
      {
        para.SetActiveOn<T>(expr);
      }
    }
    ;
  });


  /// <summary>
  /// Добавляет новые дочерние абзацы.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац, к которому необходимо добавить новые дочерние абзацы.</param>
  /// <param name="elems">Список добавляемых абзацев.</param>
  /// <returns></returns>
  public static T With<T>(this T para, params IPara[] elems) where T : IPara => (T)para.AddRange(elems);


  /// <summary>
  /// Добавляет новые текстовые фрагменты.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац, к которому необходимо добавить новые текстовые фрагменты.</param>
  /// <param name="elems">Список добавляемых текстовых фрагментов.</param>
  /// <returns></returns>
  public static T With<T>(this T para, params IText[] elems) where T : IPara => (T)para.AddRange(elems);


  /// <summary>
  /// Создает новые текстовые фрагменты из указанного списка строк и добавляет их к абзацу.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац, к которому необходимо добавить новые текстовые фрагменты.</param>
  /// <param name="elems">Список строк.</param>
  /// <returns></returns>
  public static T With<T>(this T para, params string[] elems) where T : IPara => (T)para.AddRange(elems.ToArray());


  /// <summary>
  /// Добавляет условие для вычисления признака активного/видимого абзаца.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац.</param>
  /// <param name="expr">Условие для вычисления признака активного/видимого абзаца.</param>
  /// <returns></returns>
  public static T With<T>(this T para, Expr expr) where T : IPara => (T)para.SetActiveOn<T>(expr);


  /// <summary>
  /// Задает новое количество пустых строк до и после абзаца.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац.</param>
  /// <param name="before">Кол-во пустых строк до абзаца.</param>
  /// <param name="after">Кол-во пустых строк после абзаца.</param>
  /// <returns></returns>
  public static T WithLines<T>(this T para, int before, int after) where T : IPara => para.FluentAction(() => para.Lines = new BeforeAfter<int>(before, after));


  /// <summary>
  /// Задает правила выравнивание содержимого абзаца.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац.</param>
  /// <param name="align">Правила выравнивания содержимого в виде набора флагов <see cref="AlignEnum"/>.</param>
  /// <returns></returns>
  public static T WithAlign<T>(this T para, AlignEnum align) where T : IPara => para.FluentAction(() => para.Align = align);


  /// <summary>
  /// Задает новые значения для вертикальных отступов до и после абзаца.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац.</param>
  /// <param name="before">Отступ до абзаца.</param>
  /// <param name="after">Отступ после абзаца.</param>
  /// <returns></returns>
  public static T WithSpacing<T>(this T para, int before, int after) where T : IPara => para.FluentAction(() => para.Spacing = new BeforeAfter<int>(before, after));


  /// <summary>
  /// Создает абзац-таблицу, содержащую сведения о типе и значении/значениях указанного параметра.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="prm">Параметр.</param>
  /// <returns></returns>
  public static Grid ToGrid<T>(this IParam<T> prm)
  {

    bool isEnumerable = TypeHelper.FindIEnumerable(prm.Value.GetType()) != null;
    var prmValueType = TypeHelper.GetElementType(typeof(T));

    var properties =
      prmValueType
      .GetProperties(BindingFlags.Instance | BindingFlags.Public)
      ;

    List<BaseCell> cells = [];
    Grid ret = new Grid();

    cells.AddRange(properties.Select((s, i) => new Cell(new Text(s.Name)).SetInnerCell(i + 1, 1)));


    if (TypeHelper.FindIEnumerable(prm.Value.GetType()) == null)
      cells.AddRange(properties.Select((s, i) => new Cell(new Text(s.GetValue(prm.Value).ToString())).SetInnerCell(i + 1, 2)));


    if (isEnumerable)
    {
      var method = prm.Value.GetType().GetProperty("Item");
      var countMethod = prm.Value.GetType().GetMethods().FirstOrDefault(w => w.Name.Contains("Count"));
      int count = (int)countMethod.Invoke(prm.Value, []);
      var first = method.GetValue(prm.Value, [0]);
      var enumRange = Enumerable.Range(0, count);

      foreach (var ss in enumRange)
      {
        var item = properties.Select((s, i) => new Cell(new Text(s.GetValue(method.GetValue(prm.Value, [ss])).ToString())).SetInnerCell(i + 1, ss + 2));
        cells.AddRange(item);
      }

      ret
        .SetInnerGrid(properties.Count(), count + 1, true, true)
        .SetInnerGridWidths(Enumerable.Range(1, properties.Count()).Select(s => 100 / (decimal)properties.Count()).Cast<decimal>().ToArray())
        .SetInnerGridMargins(4)
        .With(cells.ToArray())
        ;
    }
    else
    {
      ret
        .SetInnerGrid(properties.Count(), 2, true, true)
        .SetInnerGridWidths(Enumerable.Range(1, properties.Count()).Select(s => 100 / (decimal)properties.Count()).Cast<decimal>().ToArray())
        .SetInnerGridMargins(4)
        .With(cells.ToArray())
        ;
    }

    return ret;
  }


  /// <summary>
  /// Добавляет к текущему абзацу новый текстовый фрагмент, содержимое которого
  /// формируется из значения, полученного из указанного параметра по указанному выражению.
  /// </summary>
  /// <typeparam name="T">.NET-тип абзаца.</typeparam>
  /// <typeparam name="TVal">Тип значения параметра.</typeparam>
  /// <param name="para">Текущий абзац, к которому добавляется новый текстовый фрагмент.</param>
  /// <param name="prm">Ссылка на типизированный параметр.</param>
  /// <param name="expression">
  /// Выражение, применяемое к типизированному параметру <paramref name="prm"/>. 
  /// Если равно <see langword="null"/>, то в качестве содержимого будет использовано все значение параметра (<see cref="IParam{T}.Value"/>)
  /// </param>
  /// <returns></returns>
  public static T With<T, TVal>(this T para, IParam<TVal> prm, Expression<Func<IParam<TVal>, object>> expression = null) where T : IPara
    => para.FluentAction(() => para.AddRange(new Text(prm, expression)));





  /// <summary>
  /// Добавляет новые дочерние абзацы-ячейки таблицы к абзацу-таблице.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="para">Абзац-таблица.</param>
  /// <param name="cells">Список добавляемых абзацев-ячеек таблицы.</param>
  /// <returns></returns>
  public static T WithCells<T>(this T para, params BaseCell[] cells) where T : IParentOfCell =>
    para.FluentAction(() => para.AddRange(cells));


  /// <summary>
  /// Добавляет дочерний абзац-таблицу к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="grid">Добавляемый абзац-таблица.</param>
  /// <returns></returns>
  public static T WithGrid<T>(this T para, BaseGrid grid) where T : IParentOfGrid => para.FluentAction(() => para.AddRange(grid));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="INumHeader1"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithNumHeader1<T>(this T para, INumHeader1 elem) where T : IParentOfNumHeader1 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="IHeader1"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithHeader1<T>(this T para, IHeader1 elem) where T : IParentOfHeader1 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="func"></param>
  /// <returns></returns>
  public static T WithHeader1<T>(this T para, Func<IHeader1> func) where T : IParentOfHeader1 => para.FluentAction(() => para.AddRange(func()));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="IHeader2"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithHeader2<T>(this T para, IHeader2 elem) where T : IParentOfHeader2 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="IHeader3"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithHeader3<T>(this T para, IHeader3 elem) where T : IParentOfHeader3 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="IHeader4"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithHeader4<T>(this T para, IHeader4 elem) where T : IParentOfHeader4 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="IHeader5"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithHeader5<T>(this T para, IHeader5 elem) where T : IParentOfHeader5 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="INumbered1"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithNumbered1<T>(this T para, INumbered1 elem) where T : IParentOfNumbered1 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="INumbered2"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithNumbered2<T>(this T para, INumbered2 elem) where T : IParentOfNumbered2 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="INumbered3"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithNumbered3<T>(this T para, INumbered3 elem) where T : IParentOfNumbered3 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="INumbered4"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithNumbered4<T>(this T para, INumbered4 elem) where T : IParentOfNumbered4 => para.FluentAction(() => para.AddRange(elem));

  /// <summary>
  /// Добавляет дочерний абзац типа <see cref="ISimplePara"/> к возможному абзацу-родителю.
  /// </summary>
  /// <typeparam name="T">Тип абзаца, являющегося возможным родителем для добавляемого абзаца.</typeparam>
  /// <param name="para">Возможный абзац-родитель.</param>
  /// <param name="elem">Добавляемый дочерний абзац.</param>
  /// <returns></returns>
  public static T WithSimplePara<T>(this T para, ISimplePara elem) where T : IParentOfSimplePara => para.FluentAction(() => para.AddRange(elem));

}
