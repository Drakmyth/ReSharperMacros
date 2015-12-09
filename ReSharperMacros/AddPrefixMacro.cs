using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using System.Linq;
using System.Runtime.InteropServices;

namespace ReSharperMacros
{
    [MacroDefinition("addPrefix",
        ShortDescription = "Value of {#0:another variable} with `{#1:prefix}` added",
        LongDescription = "Adds a prefix")]
    public class AddPrefixMacroDef : SimpleMacroDefinition
    {
        public override ParameterInfo[] Parameters => new[]
        {
            new ParameterInfo(ParameterType.VariableReference),
            new ParameterInfo(ParameterType.String)
        };
    }

    [MacroImplementation(Definition = typeof(AddPrefixMacroDef))]
    public class AddPrefixMacroImpl : SimpleMacroImplementation
    {
        private readonly IMacroParameterValueNew _variableRef;
        private readonly IMacroParameterValueNew _prefix;

        public AddPrefixMacroImpl([Optional] MacroParameterValueCollection arguments)
        {
            _variableRef = arguments?.ElementAt(0);
            _prefix = arguments?.ElementAt(1);
        }

        public override HotspotItems GetLookupItems(IHotspotContext context)
        {
            return MacroUtil.SimpleEvaluateResult(Evaluate(_variableRef, _prefix));
        }

        private static string Evaluate(IMacroParameterValueNew variableRef, IMacroParameterValueNew prefix)
        {
            return prefix.GetValue() + variableRef.GetValue();
        }
    }
}