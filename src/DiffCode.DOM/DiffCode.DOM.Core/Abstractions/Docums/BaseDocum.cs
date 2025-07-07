using DiffCode.DOM.Core.Ids;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;
using System.Diagnostics;
using System.Linq;

namespace DiffCode.DOM.Core.Abstractions.Docums;

/// <summary>
/// Базовая модель документа.
/// </summary>
[DebuggerDisplay("{AsText}")]
public abstract class BaseDocum : BaseValidatable<IDocum>, IDocum, IWithFluentAction
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Expr _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isActiveOnLocked = false;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected IDocum _parent;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected List<IDocum> _documents;




  protected BaseDocum()
  {

  }





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="docums"></param>
  /// <returns></returns>
  public IDocum AddRange(params IDocum[] docums) => this.FluentForEachAction(docums, it =>
  {
    it.SetParent(this);
    Documents.Add(it);
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="elements"></param>
  /// <returns></returns>
  public IDocum AddRange(params IPara[] elements) => this.FluentAction(elements, elems =>
  {
    foreach (var elem in elems)
      elem.SetParent(Content);

    Content.AddRange(elems);
  });

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public void LockActiveOn() => _isActiveOnLocked = true;

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="expr"></param>
  /// <returns></returns>
  public T SetActiveOn<T>(IExpr expr) where T : IActiveState
  {
    if (!_isActiveOnLocked)
    {
      _isActiveOn = (Expr)expr;
    }

    return (T)(IActiveState)this;
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IEnumerable<IDocum> FindMany(Func<IDocum, bool> predicate) => this.Documents.Where(predicate).Concat(this.Documents.SelectMany(s => s.FindMany(predicate))).OrderBy(ord => ord.Id);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IEnumerable<IDocum> GetAll() => FindMany(t => true).Prepend(this).OrderBy(ord => ord.Id);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IEnumerable<IDocum> GetAll(Func<IDocum, bool> predicate) => predicate(this) ? FindMany(predicate).Prepend(this).OrderBy(ord => ord.Id) : FindMany(predicate);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected int FindMaxChildHeight()
  {
    int max = 0;
    Documents.ForEach(f =>
    {
      max = Math.Max(max, f.GetHeight());
    });

    return max;
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public bool HasChildren() => Documents.Count > 0;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public bool HasParent() => Parent != null && Parent.HasChildren();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public int GetHeight() => Documents.Count == 0 ? 0 : 1 + FindMaxChildHeight();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public ushort GetOrder() => (ushort)(Parent?.Documents.IndexOf(this) + 1 ?? 0);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public IEnumerable<IDocum> GetParentsAxis()
  {
    var ret = new List<IDocum>(10);
    IDocum curr = this;

    while (curr != null && curr.HasParent())
    {
      ret.Add(curr);
      curr = curr.Parent;
    }

    return ret;
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="parent"></param>
  /// <returns></returns>
  public IDocum SetParent(IDocum parent) => this.FluentAction(parent, p =>
  {
    _parent = p;
    Documents.ForEach(f => f.SetParent(this));
  });





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public DocumId Id =>
    GetParentsAxis().Count() == 0
    ?
    DocumId.New(0)
    :
    DocumId.New(new ushort[] { 0 }.Concat(GetParentsAxis().Reverse()?.Select(s => s.GetOrder())).ToArray());

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  ITypedId<string> IDocum.Id => Id;


  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public abstract IPara Content { get; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public IDocum Parent => _parent;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public List<IDocum> Documents => _documents;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public virtual string AsText => Content?.AsText;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public virtual string Numbering { get; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public virtual bool IsActive => IsActiveOn.Result ?? true;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool IsActiveOnLocked => _isActiveOnLocked;

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public Expr IsActiveOn => _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IExpr IActiveState.IsActiveOn => IsActiveOn;
}
