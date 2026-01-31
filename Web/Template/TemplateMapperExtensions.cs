using Contracts.Template;
using Domain;
using TemplateApi.Application.Services;

namespace TemplateApi.Web;

internal static class TemplateMapperExtensions
{
    public static TemplateResponse ToTemplateResponse(this TemplateObject templateObject)
    {
        var response = new TemplateResponse { Id = templateObject.Id, TemplateName = templateObject.TemplateName };

        return response;
    }

    public static CreateOrUpdateTemplateCommand ToCreateOrUpdateTemplateCommand(this CreateOrUpdateTemplateRequest request)
    {
        return new CreateOrUpdateTemplateCommand(
            TemplateName: request.TemplateName!);
    }
}
