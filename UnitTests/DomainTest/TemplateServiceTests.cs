using Domain;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using TemplateApi.Application.Services;
using TemplateApi.TestsCommon;

namespace TemplateApi.UnitTests.DomainTest;

/// <summary>
/// Тест сервиса шаблонов
/// </summary>
/// <see cref="TemplateService"/>
public sealed class TemplateServiceTests
{
    private static readonly Guid ExampleTemplateId = Guid.NewGuid();
    private readonly Mock<ILogger<TemplateService>> loggerMock = new();
    private readonly Mock<ITemplateRepository> templateRepositoryMock = new();
    private TemplateService templateService;

    public TemplateServiceTests()
    {
        templateService = new TemplateService(templateRepositoryMock.Object, loggerMock.Object);
    }

    [Fact(DisplayName = "Получение данных не существующего шаблона")]
    public async Task Get_IdNotFound_ShouldThrow()
    {
        // Arrange
        templateRepositoryMock
            .Setup(templateRepository => templateRepository.GetByIdAsync(ExampleTemplateId, CancellationToken.None))
            .ReturnsAsync(() => null);

        // Act
        var act = async () => await templateService.GetAsync(ExampleTemplateId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact(DisplayName = "Создание шаблона")]
    public async Task Createtemplate_ShouldBeAdded()
    {
        // Arrange
        var templateObject = DataGenerator.GenerateTemplate();
        var templateCommand = DataGenerator.GenerateValidCreateOrUpdateTemplateCommand(templateObject);

        templateRepositoryMock
            .Setup(p => p.CreateAsync(templateObject, CancellationToken.None));

        // Act
        await templateService.CreateAsync(templateCommand, CancellationToken.None);

        // Assert
        templateRepositoryMock.Verify(p => p.CreateAsync(It.IsAny<TemplateObject>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Удаление несуществующей шаблона")]
    public async Task Delete_templateNonExisted_ShouldThrow()
    {
        // Arrange
        templateRepositoryMock
            .Setup(p => p.GetByIdAsync(ExampleTemplateId, CancellationToken.None))
            .ReturnsAsync(() => null);

        // Act
        var template = async () => await templateService.DeleteAsync(ExampleTemplateId, CancellationToken.None);

        // Assert
        await template.Should().ThrowAsync<KeyNotFoundException>();
    }

    [Fact(DisplayName = "Удаление существующего шаблона")]
    public async Task Delete_templateExisted_ShouldCallGetRepository()
    {
        // Arrange
        var templateObject = DataGenerator.GenerateTemplate();
        var templateCommand = DataGenerator.GenerateValidCreateOrUpdateTemplateCommand(templateObject);

        templateRepositoryMock
            .Setup(p => p.GetByIdAsync(ExampleTemplateId, CancellationToken.None))
            .ReturnsAsync(templateObject);

        // Act
        await templateService.DeleteAsync(ExampleTemplateId, CancellationToken.None);

        // Assert
        templateRepositoryMock.Verify(templateRepositoryMock => templateRepositoryMock.GetByIdAsync(ExampleTemplateId, CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Успешное изменение шаблона")]
    public async Task Update_ReturnsCorrecttemplate()
    {
        // Arrange
        var templateObject = DataGenerator.GenerateTemplate();

        templateRepositoryMock
            .Setup(b => b.GetByIdAsync(ExampleTemplateId, CancellationToken.None))
            .ReturnsAsync(templateObject);

        var templateCommand = DataGenerator.GenerateValidCreateOrUpdateTemplateCommand(templateObject);

        // Act
        var updatedTemplateObject = await templateService.UpdateAsync(ExampleTemplateId, templateCommand, CancellationToken.None);

        // Assert
        templateRepositoryMock.Verify(b => b.UpdateAsync(templateObject, CancellationToken.None), Times.Once);
        updatedTemplateObject.Should().BeSameAs(templateObject);
    }

    [Fact(DisplayName = "Изменение несуществующей шаблона")]
    public async Task Update_templateIdNonExisted_ShouldThrow()
    {
        // Arrange
        templateRepositoryMock
            .Setup(p => p.GetByIdAsync(ExampleTemplateId, CancellationToken.None))
            .ReturnsAsync(() => null);

        var templateObject = DataGenerator.GenerateTemplate();
        var templateCommand = DataGenerator.GenerateValidCreateOrUpdateTemplateCommand(templateObject);

        // Act
        var updatedTemplateObject = async () => await templateService.UpdateAsync(ExampleTemplateId, templateCommand, CancellationToken.None);

        // Assert
        await updatedTemplateObject.Should().ThrowAsync<KeyNotFoundException>();
    }
}
