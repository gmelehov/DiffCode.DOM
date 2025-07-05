using DiffCode.Validating.Interfaces;

namespace DiffCode.DOM.Core.Abstractions;

/// <summary>
/// Базовая модель объекта, поддерживающего валидацию своего состояния.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseValidatable<T> : IValidatable<T> where T : IValidatable<T>
{

}
