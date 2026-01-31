using AutoFixture;
using Contracts.Template;
using Domain;
using TemplateApi.Application.Services;

namespace TemplateApi.TestsCommon;

public static class DataGenerator
{
    private static readonly IFixture AutoFixture = new Fixture();

    public static CreateOrUpdateTemplateRequest GenerateValidCreateOrUpdateTemplateRequest(TemplateObject template)
    {
        return new CreateOrUpdateTemplateRequest() { TemplateName = template.TemplateName };
    }

    public static TemplateObject GenerateTemplate()
    {
        return TemplateObject.Create(
            AutoFixture.Create<string>());
    }

    public static CreateOrUpdateTemplateCommand GenerateValidCreateOrUpdateTemplateCommand(TemplateObject template)
    {
        return new CreateOrUpdateTemplateCommand(TemplateName: template.TemplateName!);
    }
}
