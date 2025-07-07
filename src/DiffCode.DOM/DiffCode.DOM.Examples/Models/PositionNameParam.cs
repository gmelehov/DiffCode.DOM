using DiffCode.CommonEntities;
using DiffCode.CommonEntities.Enums;
using DiffCode.CommonEntities.Units.Currency;
using DiffCode.DOM.Core.Abstractions.Params;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Core.Models.Params;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Examples.Models;

/// <summary>
/// Параметр, типизированный названием должности подписанта.
/// </summary>
public class PositionNameParam : BaseParamWithFactory<PositionName, PositionName.Factory>
{
  public PositionNameParam([CallerMemberName] string memberName = "") : base(memberName)
  {

  }




  public string Nom => Value[GCase.NOM];


  public string Gen => Value[GCase.GEN];


  public string Dat => Value[GCase.DAT];


  public string Acc => Value[GCase.ACC];


  public string Ins => Value[GCase.INS];


  public string Loc => Value[GCase.LOC];
}
