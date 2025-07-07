using DiffCode.CommonEntities;
using DiffCode.CommonEntities.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace DiffCode.DOM.Examples.Models;


public class NameFactories(IServiceProvider sp)
{


  public LegalEntityName.Factory LegalEntityFactory { get; } = sp.GetRequiredService<LegalEntityName.Factory>();


  public PositionName.Factory PositionFactory { get; } = sp.GetRequiredService<PositionName.Factory>();


  public PartyName.Factory PartyFactory { get; } = sp.GetRequiredService<PartyName.Factory>();


  public BasePersonName.Factory PersonNameFactory { get; } = sp.GetRequiredService<BasePersonName.Factory>();


  public AuthorityName.Factory AuthorityFactory { get; } = sp.GetRequiredService<AuthorityName.Factory>();

}