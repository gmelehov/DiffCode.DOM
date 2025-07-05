using DiffCode.DOM.Common.Enums;
using DiffCode.Validating.Interfaces;
using System.Diagnostics;


namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс абзаца.
/// </summary>
public interface IPara : IActiveState, IWithFluentAction, IValidatable<IPara>
{



  /// <summary>
  /// 
  /// </summary>
  void SetNumberings();


  #region МЕТОДЫ ДЛЯ ОПРЕДЕЛЕНИЯ ПОЗИЦИИ АБЗАЦА ВНУТРИ ИЕРАРХИИ АБЗАЦЕВ /////////////////////////////////////////////////////////


  /// <summary>
  /// Устанавливает родительский абзац для текущего абзаца.
  /// </summary>
  /// <param name="parent">Абзац, который необходимо установить в качестве родительского.</param>
  /// <returns></returns>
  IPara SetParent(IPara parent);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerable<IPara> GetParentsAxis();

  /// <summary>
  /// Возвращает порядок следования текущего абзаца в списке дочерних абзацев своего родителя.
  /// </summary>
  /// <returns></returns>
  ushort GetOrder();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  int GetHeight();

  /// <summary>
  /// Возвращает признак наличия родителя у текущего абзаца.
  /// </summary>
  /// <returns></returns>
  bool HasParent();

  /// <summary>
  /// Возвращает признак наличия дочерних абзацев у текущего абзаца.
  /// </summary>
  /// <returns></returns>
  bool HasChildren();


  #endregion



  #region МЕТОДЫ ДЛЯ ДОБАВЛЕНИЯ СОДЕРЖИМОГО В АБЗАЦ /////////////////////////////////////////////////////////////////////////////


  /// <summary>
  /// Добавляет коллекцию дочерних абзацев к текущему абзацу.
  /// </summary>
  /// <param name="items"></param>
  /// <returns></returns>
  IPara AddRange(params IPara[] items);

  /// <summary>
  /// Добавляет коллекцию дочерних абзацев к текущему абзацу, непосредственно после
  /// дочернего абзаца, на который указывает предикат.
  /// </summary>
  /// <param name="predicate">Предикат для поиска дочернего абзаца, после которого нужно добавить новые элементы.</param>
  /// <param name="items">Коллекция дочерних абзацев, которые нужно добавить к этому абзацу.</param>
  /// <returns></returns>
  IPara InsertAfter(Func<IPara, bool> predicate, params IPara[] items);

  /// <summary>
  /// Добавляет коллекцию текстовых фрагментов к текущему абзацу.
  /// </summary>
  /// <param name="texts"></param>
  /// <returns></returns>
  IPara AddRange(params IText[] texts);

  /// <summary>
  /// Добавляет к текущему абзацу коллекцию текстовых фрагментов, 
  /// сформированных из указанного списка строк.
  /// </summary>
  /// <param name="texts"></param>
  /// <returns></returns>
  IPara AddRange(params string[] texts);

  #endregion



  #region МЕТОДЫ ДЛЯ ВЫЧИСЛЕНИЯ ТЕКСТОВОГО ПРЕДСТАВЛЕНИЯ АБЗАЦА /////////////////////////////////////////////////////////////////


  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  string GetFragmentsText();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  string GetChildrenText();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  string GetFullText();

  #endregion



  #region МЕТОДЫ ДЛЯ ПОИСКА СОДЕРЖИМОГО ВНУТРИ АБЗАЦА ///////////////////////////////////////////////////////////////////////////


  /// <summary>
  /// Возвращает все вложенные в этот абзац дочерние абзацы,
  /// соответствующие указанному условию.
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  IEnumerable<IPara> FindMany(Func<IPara, bool> predicate);

  /// <summary>
  /// Возвращает первый найденный в этом абзаце дочерний абзац,
  /// соответствующий указанному условию.
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  IPara FindOne(Func<IPara, bool> predicate);

  /// <summary>
  /// Возвращает первый найденный в этом абзаце дочерний абзац,
  /// тип которого равен указанному.
  /// </summary>
  /// <typeparam name="T">Тип искомого абзаца.</typeparam>
  /// <returns></returns>
  T FindOne<T>() where T : IPara;

  /// <summary>
  /// Возвращает текущий абзац и все его дочерние абзацы единым списком.
  /// </summary>
  /// <returns></returns>
  IEnumerable<IPara> GetAll();

  /// <summary>
  /// Возвращает все вложенные в этот абзац дочерние абзацы, соответствующие
  /// указанному условию.
  /// </summary>
  /// <remarks>
  /// Этот абзац также проверяется на соответствие указанному условию и,
  /// в случае соответствия, добавляется в начало возвращаемого списка.
  /// </remarks>
  /// <param name="predicate"></param>
  /// <returns></returns>
  IEnumerable<IPara> GetAll(Func<IPara, bool> predicate);

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  IEnumerable<T> FindText<T>() where T : IText;

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="predicate"></param>
  /// <returns></returns>
  IEnumerable<T> FindText<T>(Func<T, bool> predicate) where T : IText;

  #endregion



  /// <summary>
  /// 
  /// </summary>
  /// <param name="enums"></param>
  /// <returns></returns>
  bool HasAllParaTypes(params ParaTypeEnum[] enums);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="enums"></param>
  /// <returns></returns>
  bool HasAnyParaTypes(params ParaTypeEnum[] enums);










  /// <summary>
  /// Идентификатор (сегментный адрес) абзаца.
  /// </summary>
  ITypedId<string> Id { get; }

  /// <summary>
  /// Ссылка на вышестоящий (родительский) абзац.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IPara Parent { get; }

  /// <summary>
  /// Ссылка на список текстовых фрагментов абзаца.
  /// </summary>
  List<IText> Texts { get; }

  /// <summary>
  /// Ссылка на список дочерних абзацев.
  /// </summary>
  List<IPara> Paragraphs { get; }

  /// <summary>
  /// Кол-во пустых строк, выводимых до и после абзаца.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IBeforeAfter<int> Lines { get; set; }

  /// <summary>
  /// Вертикальные отступы до и после абзаца.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IBeforeAfter<int> Spacing { get; set; }

  /// <summary>
  /// Текстовое представление нумерации.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  string Numbering { get; set; }

  /// <summary>
  /// Комплексный тип абзаца.
  /// </summary>
  ParaTypeEnum ParaType { get; }

  /// <summary>
  /// Тип выравнивания абзаца внутри своего родительского абзаца.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  AlignEnum Align { get; set; }

  /// <summary>
  /// Полное текстовое представление абзаца.
  /// </summary>
  string AsText { get; }

}
