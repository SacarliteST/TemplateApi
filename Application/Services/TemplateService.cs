using Domain;
using Microsoft.Extensions.Logging;
using TemplateApi.Application.Services.Interface;

namespace TemplateApi.Application.Services;

public class TemplateService : ITemplateService
{
    private readonly ITemplateRepository templateRepository;
    private readonly ILogger logger;

    public TemplateService(
        ITemplateRepository templateRepository,
        ILogger<TemplateService> logger)
    {
        this.templateRepository = templateRepository;
        this.logger = logger;
    }

    public async Task<PageResult<TemplateObject>> GetWithPaging(
        Pagination pagination,
        CancellationToken cancellationToken)
        => await templateRepository.GetAllAsync(pagination, cancellationToken);

    public async Task<TemplateObject> CreateAsync(
        CreateOrUpdateTemplateCommand createCommand,
        CancellationToken cancellationToken)
    {
        var templateObject = TemplateObject.Create(
            createCommand.TemplateName);

        await templateRepository.CreateAsync(templateObject, cancellationToken);

        logger.LogInformation("Шаблонный объект с Id: {pallet.Id} добавлена в БД", templateObject.Id);
        return templateObject;
    }

    public async Task<TemplateObject> GetAsync(
        Guid id,
        CancellationToken cancellationToken)
        => await templateRepository.GetByIdAsync(id, cancellationToken)
           ?? throw new KeyNotFoundException($"Шаблонный объект  с Id: {id} не найдена");

    public async Task<TemplateObject> UpdateAsync(
        Guid id,
        CreateOrUpdateTemplateCommand updateCommand,
        CancellationToken cancellationToken)
    {
        var palletToUpdate = await templateRepository.GetByIdAsync(id, cancellationToken)
                             ?? throw new KeyNotFoundException($"Шаблонный объект с id = {id} не найден");

        palletToUpdate.Update(updateCommand.TemplateName);

        await templateRepository.UpdateAsync(palletToUpdate, cancellationToken);

        logger.LogInformation("Шаблонный объект с Id: {palletToUpdate} изменён", palletToUpdate.Id);

        return palletToUpdate;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var pallet = await templateRepository.GetByIdAsync(id, cancellationToken)
                     ?? throw new KeyNotFoundException($"Не удалось удалить. Шаблонный объект с Id = {id} не найден");

        await templateRepository.DeleteAsync(pallet, cancellationToken);

        logger.LogInformation("Шаблонный объект с Id: {BoxId} удален из БД", pallet.Id);
    }
}
