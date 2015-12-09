using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using System.Linq;
using System.Runtime.InteropServices;

namespace ReSharperMacros
{
	[MacroDefinition("uppercase",
		ShortDescription = "Value of {#0:another variable} in all upper case",
		LongDescription = "Uppercases a string value")]
	public class UppercaseMacroDef : SimpleMacroDefinition
	{
		public override ParameterInfo[] Parameters => new[]
		{
			new ParameterInfo(ParameterType.VariableReference)
		};
	}

	[MacroImplementation(Definition = typeof(UppercaseMacroDef))]
	public class UppercaseMacroImpl : SimpleMacroImplementation
	{
		private readonly IMacroParameterValueNew _variableRef;

		public UppercaseMacroImpl([Optional] MacroParameterValueCollection arguments)
		{
			_variableRef = arguments?.ElementAt(0);
		}

		public override HotspotItems GetLookupItems(IHotspotContext context)
		{
			return MacroUtil.SimpleEvaluateResult(Evaluate(_variableRef));
		}

		private static string Evaluate(IMacroParameterValueNew variableRef)
		{
			return variableRef.GetValue().ToUpper();
		}
	}
}