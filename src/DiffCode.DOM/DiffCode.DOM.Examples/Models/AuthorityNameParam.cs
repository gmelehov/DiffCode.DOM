using DiffCode.CommonEntities;
using DiffCode.CommonEntities.Enums;
using DiffCode.DOM.Core.Abstractions.Params;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Examples.Models;

/// <summary>
/// Параметр, типизированный основанием полномочий подписанта.
/// </summary>
public class AuthorityNameParam : BaseParamWithFactory<AuthorityName, AuthorityName.Factory>
{
  public AuthorityNameParam([CallerMemberName] string memberName = "") : base(memberName)
  {

  }



  public string Nom => Value[GCase.NOM];


  public string Gen => Value[GCase.GEN];


  public string Dat => Value[GCase.DAT];


  public string Acc => Value[GCase.ACC];


  public string Ins => Value[GCase.INS];


  public string Loc => Value[GCase.LOC];
}



