using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using System.Linq;
using System.Runtime.InteropServices;

namespace ReSharperMacros
{
    [MacroDefinition("addSuffix",
        ShortDescription = "Value of {#0:another variable} with `{#1:suffix}` added",
        LongDescription = "Adds a suffix")]
    public class AddSuffixMacroDef : SimpleMacroDefinition
    {
        public override ParameterInfo[] Parameters => new[]
        {
            new ParameterInfo(ParameterType.VariableReference),
            new ParameterInfo(ParameterType.String)
        };
    }

    [MacroImplementation(Definition = typeof(AddSuffixMacroDef))]
    public class AddSuffixMacroImpl : SimpleMacroImplementation
    {
        private readonly IMacroParameterValueNew _variableRef;
        private readonly IMacroParameterValueNew _suffix;

        public AddSuffixMacroImpl([Optional] MacroParameterValueCollection arguments)
        {
            _variableRef = arguments?.ElementAt(0);
            _suffix = arguments?.ElementAt(1);
        }

        public override HotspotItems GetLookupItems(IHotspotContext context)
        {
            return MacroUtil.SimpleEvaluateResult(Evaluate(_variableRef, _suffix));
        }

        private static string Evaluate(IMacroParameterValueNew variableRef, IMacroParameterValueNew suffix)
        {
            return variableRef.GetValue() + suffix.GetValue();
        }
    }
}