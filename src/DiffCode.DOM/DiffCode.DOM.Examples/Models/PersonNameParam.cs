using DiffCode.CommonEntities.Abstractions;
using DiffCode.CommonEntities.Enums;
using DiffCode.DOM.Core.Abstractions.Params;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Examples.Models;

/// <summary>
/// Параметр, типизированный моделью ФИО.
/// </summary>
public class PersonNameParam : BaseParamWithFactory<BasePersonName, BasePersonName.Factory>
{
  public PersonNameParam([CallerMemberName] string memberName = "") : base(memberName)
  {

  }
  

  


  public string Nom => Value.GetFullForm(GCase.NOM);


  public string Gen => Value.GetFullForm(GCase.GEN);


  public string Dat => Value.GetFullForm(GCase.DAT);


  public string Acc => Value.GetFullForm(GCase.ACC);


  public string Ins => Value.GetFullForm(GCase.INS);


  public string Loc => Value.GetFullForm(GCase.LOC);
}
