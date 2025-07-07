using DiffCode.DOM.Core.Extensions;
using DiffCode.DOM.Core.Models.Docums;
using DiffCode.DOM.Core.Models.Params;
using DiffCode.DOM.Examples.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DiffCode.DOM.Examples;


public class PoACourtAdvocating(NameFactories nameFactories, PoACourtAdvocating.ParamsFactory paramsFactory)
{



  public delegate PoACourtAdvocating Factory();

  public delegate Parameters ParamsFactory();




  public NameFactories Factories { get; } = nameFactories;


  public Parameters Prms { get; } = paramsFactory();


  public Docum MainDoc
  {
    get
    {
      var ret = MainDocBuilder();
      ret.Content.SetNumberings();
      return ret;
    }
  }



  public Func<Docum> MainDocBuilder => () => new Docum()
    .With(
      Para(
        $"Настоящей доверенностью ", 
        Prms.SelfName.Nom, 
        $" (далее - ", 
        Prms.SelfPartyName.Nom, 
        $"), в лице ",
        Prms.SelfPosition.Gen, 
        $" ", 
        Prms.SelfSigner.Gen, 
        $", действующего на основании ", 
        Prms.SelfAuth.Gen,
        $", уполномачивает"
        ),
      Para(
        Prms.OtherSigner.Acc, 
        $", дата рождения ", 
        Prms.OtherBirthDate, 
        $", место рождения ", 
        Prms.OtherBirthPlace,
        $", паспорт ", 
        Prms.OtherPassportNum
        )
      .WithLines(1,1)
      )

    .With(
      Para(
        $"представлять интересы и вести дела ", Prms.SelfName.Gen, $" во всех судах общей юрисдикции, арбитражных и других судах, " +
        $"у мировых судей, со всеми правами, какие предоставлены законом истцу и/или административному истцу, ответчику " +
        $"и/или административному ответчику, третьему лицу, заявителю, потерпевшему или лицу, в отношении которого ведется " +
        $"производство по делу об административном правонарушении, в том числе с правом подписания и подачи жалоб, исковых " +
        $"заявлений и/или административных исковых заявлений, отзыва на исковое заявление и/или административное " +
        $"исковое заявление, заявление об обеспечении иска и/или административного иска, заявления о применении мер " +
        $"предварительной защиты по административному иску, подачу встречного иска и/или административного искового " +
        $"заявления, заявления о пересмотре судебных актов по вновь открывшимся обстоятельствам, передачи дела " +
        $"в третейский суд, признания или отказа полностью или частично от исковых требований и/или административных " +
        $"исковых требований, уменьшения и увеличения их размера, изменения предмета и оснований иска и/или " +
        $"административного иска, с правом заключения и подписания мирового соглашения, соглашения сторон по " +
        $"фактическим обстоятельствам административного дела, обжалования решений суда, с правом подписания и подачи " +
        $"частной, кассационной, апелляционной жалоб и жалоб в порядке надзора, обжалования постановления по делу " +
        $"об административном правонарушении, заявления отводов и ходатайств, с правом получения решений, определений, " +
        $"судебных приказов, исполнительных документов, предъявления последних ко взысканию, открытию исполнительного " +
        $"производства, участия в нем, отзыва исполнительных документов, с правом подписания акта описи ареста имущества, " +
        $"с правом принятия имущества на ответственное хранение, с правом подписания любых процессуальных документов " +
        $"в исполнительном производстве от имени ", Prms.SelfPartyName.Gen, $" обжалования действий (бездействия) " +
        $"судебного пристава-исполнителя, обжалование актов судебного пристава-исполнителя, заключения мирового " +
        $"соглашения и соглашения по фактическим обстоятельствам, а также с правами, предусмотренными КАС РФ:"
        )
      .WithLines(1, 1)
      )

    .With(
      Numbered1(
        $"На подписание административного искового заявления и возражений на административное исковое заявление, подачу их в суд."
        ),
      Numbered1(
        $"На заявление о применении мер предварительной защиты по административному иску."
        ),
      Numbered1(
        $"На подачу встречного административного искового заявления."
        ),
      Numbered1(
        $"На заключение соглашения о примирении сторон или соглашения сторон по фактическим обстоятельствам административного дела."
        ),
      Numbered1(
        $"На полный либо частичный отказ от административного иска или на признание административного иска."
        ),
      Numbered1(
        $"На изменение предмета или основания административного иска."
        ),
      Numbered1(
        $"На подписание заявления о пересмотре судебных актов по вновь открывшимся обстоятельствам."
        ),
      Numbered1(
        $"На обжалование судебного акта."
        ),
      Numbered1(
        $"На предъявление исполнительного документа ко взысканию."
        ),

      Para(
        $"А также быть представителем ", Prms.SelfName.Gen, $" в деле о банкротстве и участвовать в арбитражном процессе " +
        $"по делу о банкротстве, для чего предоставляю право подавать и подписывать от имени ", Prms.SelfName.Gen, $" " +
        $"заявление о признании должника банкротом; участвовать во всех процедурах, применяемых в деле о банкротстве; заявлять " +
        $"требования для включения в реестр требований кредиторов; участвовать в заседаниях арбитражного суда по делу о банкротстве; " +
        $"заявлять ходатайства, в том числе о назначении экспертизы в целях выявления признаков преднамеренного или фиктивного " +
        $"банкротства; представлять в арбитражный суд предусмотренные Федеральным законом о банкротстве документы в электронной форме, " +
        $"заполнять формы документов, размещенных на официальном сайте арбитражного суда в информационно-телекоммуникационной сети " +
        $"«Интернет», в порядке, установленном в пределах своих полномочий Верховным Судом Российской Федерации; обжаловать судебные " +
        $"акты арбитражного суда; участвовать в первом собрании кредиторов; заключать мировое соглашение и совершать все иные " +
        $"предусмотренные Федеральным законом о банкротстве процессуальные действия в арбитражном процессе по делу о банкротстве, " +
        $"необходимые для реализации предоставленных прав,"
        )
      .WithLines(1,1),

      Para(
        $"для чего предоставляю право подавать все необходимые справки, удостоверения и иные документы, в том числе получать " +
        $"копии документов, делать заявления, знакомиться с материалами дела, представлять доказательства по делу, давать " +
        $"объяснения, уплачивать обязательные сборы и пошлины, расписываться в случае необходимости и совершать все действия, " +
        $"связанные с выполнением данных поручений."
        )
      .WithLines(1,1),

      Para(
        $"Доверенность выдана без права передоверия и действительна по ",
        Prms.PoAValidTill
        )
      .WithLines(1,1)

      )

    ;





  public class Parameters
  {

    /// <summary>
    /// Название документа-доверенности.
    /// </summary>
    public StringParam MainDocName { get; } = new StringParam();

    /// <summary>
    /// Город выдачи документа-доверенности.
    /// </summary>
    public StringParam MainDocPlace { get; } = new StringParam();

    /// <summary>
    /// Номер документа-доверенности.
    /// </summary>
    public StringParam MainDocNumber { get; } = new StringParam();

    /// <summary>
    /// Дата выдачи документа-доверенности.
    /// </summary>
    public DateOnlyParam MainDocDate { get; } = new DateOnlyParam();

    /// <summary>
    /// Дата окончания срока действия документа-доверенности.
    /// </summary>
    public DateOnlyParam PoAValidTill { get; } = new DateOnlyParam();


    public LegalEntityNameParam SelfName { get; } = new LegalEntityNameParam();


    public PositionNameParam SelfPosition { get; } = new PositionNameParam();


    public PartyNameParam SelfPartyName { get; } = new PartyNameParam();


    public PersonNameParam SelfSigner { get; } = new PersonNameParam();


    public AuthorityNameParam SelfAuth { get; } = new AuthorityNameParam();


    public PersonNameParam OtherSigner { get; } = new PersonNameParam();


    public DateOnlyParam OtherBirthDate { get; } = new DateOnlyParam();


    public StringParam OtherBirthPlace { get; } = new StringParam();


    public StringParam OtherPassportNum { get; } = new StringParam();
  }

}








public static class IServiceExtensions
{



  public static IServiceCollection AddPoA(this IServiceCollection scoll) =>
    scoll

    .AddScoped(sp => new NameFactories(sp))
    
    .AddScoped<PoACourtAdvocating.ParamsFactory>(
      sp => () =>
      {
        var ret = new PoACourtAdvocating.Parameters();
        var factories = sp.GetRequiredService<NameFactories>();

        ret.MainDocName.With("Доверенность");
        ret.MainDocNumber.With("____");
        ret.MainDocPlace.With("г. Москва");
        ret.MainDocDate.With(DateOnly.FromDateTime(DateTime.Today));
        ret.PoAValidTill.With(ret.MainDocDate.Value.AddMonths(6));

        ret.SelfName.SetValueFactory(factories.LegalEntityFactory).Set("Общество с ограниченной ответственностью «Общество»", "ООО «Общество»");
        ret.SelfPosition.SetValueFactory(factories.PositionFactory).Set("генеральный директор");
        ret.SelfPartyName.SetValueFactory(factories.PartyFactory).Set("Доверитель");
        ret.SelfSigner.SetValueFactory(factories.PersonNameFactory).Set("Сергей Васильевич Кузнецов");
        ret.SelfAuth.SetValueFactory(factories.AuthorityFactory).Set("Устав");
        ret.OtherSigner.SetValueFactory(factories.PersonNameFactory).Set("Баканова Анна Игоревна");
        ret.OtherBirthDate.With(DateOnly.FromDateTime(DateTime.Today).AddYears(-40));
        ret.OtherBirthPlace.With("г. Москва");
        ret.OtherPassportNum.With("серия 7777 номер 444555");

        return ret;
      })

    .AddScoped(
      sp => new PoACourtAdvocating(
        sp.GetRequiredService<NameFactories>(), 
        sp.GetRequiredService<PoACourtAdvocating.ParamsFactory>()
        )
      )

    ;


}