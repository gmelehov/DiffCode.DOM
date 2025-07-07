using DiffCode.CommonEntities.GrammarServices.Extensions;
using DiffCode.DOM.Examples;
using DiffCode.DOM.Rendering.Extensions;
using DiffCode.DOM.Rendering.Services;
using Microsoft.Extensions.DependencyInjection;


var scoll = new ServiceCollection()
  .AddAllGrammars()
  .AddMigraDocRendering()
  .AddPoA()
  ;

var builtSp = scoll.BuildServiceProvider();
using var scope = builtSp.GetRequiredService<IServiceScopeFactory>().CreateScope();
var sp = scope.ServiceProvider;

var migra = sp.GetRequiredService<MigraDocRenderingService>();
var poa = sp.GetRequiredService<PoACourtAdvocating>();

poa.Prms.OtherSigner.Set("Остапенко Игнат Васильевич");



var migraDoc = migra.MakePDF("../../../test.pdf", poa.MainDoc).Result;




Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");
