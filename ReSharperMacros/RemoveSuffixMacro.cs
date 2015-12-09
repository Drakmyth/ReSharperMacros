using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using System.Linq;
using System.Runtime.InteropServices;

namespace ReSharperMacros
{
    [MacroDefinition("removeSuffix",
        ShortDescription = "Value of {#0:another variable} with `{#1:suffix}` removed",
        LongDescription = "Removes a suffix")]
    public class RemoveSuffixMacroDef : SimpleMacroDefinition
    {
        public override ParameterInfo[] Parameters => new[]
        {
            new ParameterInfo(ParameterType.VariableReference),
            new ParameterInfo(ParameterType.String)
        };
    }

    [MacroImplementation(Definition = typeof(RemoveSuffixMacroDef))]
    public class RemoveSuffixMacroImpl : SimpleMacroImplementation
    {
        private readonly IMacroParameterValueNew _variableRef;
        private readonly IMacroParameterValueNew _suffix;

        public RemoveSuffixMacroImpl([Optional] MacroParameterValueCollection arguments)
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

            string variableValue = variableRef.GetValue();
            string suffixValue = suffix.GetValue();
            string retVal = variableValue;

            if (variableValue.EndsWith(suffix.GetValue()))
            {
                retVal = variableValue.Substring(0, variableValue.Length - suffixValue.Length);
            }

            return retVal;
        }
    }
}