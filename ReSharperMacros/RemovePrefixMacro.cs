using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using System.Linq;
using System.Runtime.InteropServices;

namespace ReSharperMacros
{
    [MacroDefinition("removePrefix",
        ShortDescription = "Value of {#0:another variable} with `{#1:prefix}` removed",
        LongDescription = "Removes a prefix")]
    public class RemovePrefixMacroDef : SimpleMacroDefinition
    {
        public override ParameterInfo[] Parameters => new[]
        {
            new ParameterInfo(ParameterType.VariableReference),
            new ParameterInfo(ParameterType.String)
        };
    }

    [MacroImplementation(Definition = typeof(RemovePrefixMacroDef))]
    public class RemovePrefixMacroImpl : SimpleMacroImplementation
    {
        private readonly IMacroParameterValueNew _variableRef;
        private readonly IMacroParameterValueNew _prefix;

        public RemovePrefixMacroImpl([Optional] MacroParameterValueCollection arguments)
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

            string variableValue = variableRef.GetValue();
            string prefixValue = prefix.GetValue();
            string retVal = variableValue;

            if (variableValue.StartsWith(prefix.GetValue()))
            {
                retVal = variableValue.Substring(prefixValue.Length);
            }

            return retVal;
        }
    }
}