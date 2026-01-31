using Shouldly;
using TemplateApi.IntegrationsTest.infrastructure;
using TemplateApi.TestsCommon;

namespace TemplateApi.IntegrationsTest.Template;

/// Тест контроллера коробок
/// </summary>
public sealed class TemplateControllerTests : ApiTestBase
{
    public TemplateControllerTests(TestApplication testApplication) : base(testApplication) { }

    [Fact(DisplayName = "Получение шаблона по Id")]
    public async Task GetTemplate_ByValidId_ShouldReturnTemplate()
    {
        // Arrange
        var template = DataGenerator.GenerateTemplate();
        var createTemplateRequest = DataGenerator.GenerateValidCreateOrUpdateTemplateRequest(template);
        var createdTemplateResponse = await TemplateClient.CreateAsync(createTemplateRequest);

        // Act
        var actualTemplate = await TemplateClient.GetByIdAsync(createdTemplateResponse.Id.ShouldNotBeNull(), default);

        // Assert
        actualTemplate
            .ShouldNotBeNull();
    }

    [Fact(DisplayName = "Добавление шаблона")]
    public async Task CreateTemplate_ShouldReturnCreatedTemplate()
    {
        // Arrange
        var template = DataGenerator.GenerateTemplate();
        var createTemplateRequest = DataGenerator.GenerateValidCreateOrUpdateTemplateRequest(template);

        // Act
        var createdTemplateResponse = await TemplateClient.CreateAsync(createTemplateRequest);

        // Assert
        var actualTemplate = await TemplateClient.GetByIdAsync(createdTemplateResponse.Id.ShouldNotBeNull(), default);

        actualTemplate
            .ShouldNotBeNull();
    }

    [Fact(DisplayName = "Изменение данных шаблона")]
    public async Task UpdateTemplate_ShouldReturnUpdatedTemplate()
    {
        // Arrange
        var template = DataGenerator.GenerateTemplate();
        var createTemplateRequest = DataGenerator.GenerateValidCreateOrUpdateTemplateRequest(template);
        var createdTemplateResponse = await TemplateClient.CreateAsync(createTemplateRequest);
        var newTemplate = DataGenerator.GenerateTemplate();
        var updateTemplateRequest = DataGenerator.GenerateValidCreateOrUpdateTemplateRequest(newTemplate);

        // Act
        await TemplateClient.UpdateAsync(createdTemplateResponse.Id.ShouldNotBeNull(), updateTemplateRequest);

        // Assert
        var actualTemplate = await TemplateClient.GetByIdAsync(createdTemplateResponse.Id.ShouldNotBeNull(), default);

        actualTemplate
            .ShouldNotBeNull();
    }

    [Fact(DisplayName = "Удаление шаблона")]
    public async Task DeleteTemplate_ShouldNotThrow()
    {
        // Arrange
        var template = DataGenerator.GenerateTemplate();
        var createTemplateRequest = DataGenerator.GenerateValidCreateOrUpdateTemplateRequest(template);
        var createdTemplateResponse = await TemplateClient.CreateAsync(createTemplateRequest);

        // Act
        var act = async () => await TemplateClient.DeleteAsync(createdTemplateResponse.Id.ShouldNotBeNull());

        // Assert
        await act.ShouldNotThrowAsync();
    }
}
