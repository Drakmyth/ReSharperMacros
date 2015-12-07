using System.Collections.Generic;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Hotspots;
using JetBrains.ReSharper.Feature.Services.LiveTemplates.Macros;
using JetBrains.Util;

namespace ReSharperMacros
{
    [MacroDefinition("ReSharperMacros.ControllerResourceFromClassName",
        ShortDescription = "Determines a Web API's resource's name based on the enclosing type.",
        LongDescription = "Trims 'Controller' off the end of the type name or returns the type name if it does not end with 'Controller'.")]
    public class ControllerResourceFromClassNameDefinition : IMacroDefinition
    {
        public string GetPlaceholder(IDocument document, IEnumerable<IMacroParameterValue> parameters)
        {
            return "a";
        }

        public ParameterInfo[] Parameters => EmptyArray<ParameterInfo>.Instance;
    }

    [MacroImplementation]
    public class ControllerResourceFromClassNameImplementation : IMacroImplementation
    {
        public bool HandleExpansion(IHotspotContext context)
        {
            return false;
        }

        public HotspotItems GetLookupItems(IHotspotContext context)
        {
            return null;
        }

        public string EvaluateQuickResult(IHotspotContext context)
        {
            return null;
        }
    }
}