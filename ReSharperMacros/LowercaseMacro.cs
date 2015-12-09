using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using System.Linq;
using System.Runtime.InteropServices;

namespace ReSharperMacros
{
    [MacroDefinition("lowercase",
        ShortDescription = "Value of {#0:another variable} in all lower case",
        LongDescription = "Lowercases a string value")]
    public class LowercaseMacroDef : SimpleMacroDefinition
    {
        public override ParameterInfo[] Parameters => new[]
        {
            new ParameterInfo(ParameterType.VariableReference)
        };
    }

    [MacroImplementation(Definition = typeof(LowercaseMacroDef))]
    public class LowercaseMacroImpl : SimpleMacroImplementation
    {
        private readonly IMacroParameterValueNew _variableRef;

        public LowercaseMacroImpl([Optional] MacroParameterValueCollection arguments)
        {
            _variableRef = arguments?.ElementAt(0);
        }

        public override HotspotItems GetLookupItems(IHotspotContext context)
        {
            return MacroUtil.SimpleEvaluateResult(Evaluate(_variableRef));
        }

        private static string Evaluate(IMacroParameterValueNew variableRef)
        {
            return variableRef.GetValue().ToLower();
        }
    }
}