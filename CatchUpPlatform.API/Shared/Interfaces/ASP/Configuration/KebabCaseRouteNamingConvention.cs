using CatchUpPlatform.API.Shared.Interfaces.ASP.Configuration.Entensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CatchUpPlatform.API.Shared.Interfaces.ASP.Configuration;

public class KebabCaseRouteNamingConvention:IControllerModelConvention
{
    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?.Replace("[controller]", name.ToKebabCase()),

            }
            : null;
    }
}