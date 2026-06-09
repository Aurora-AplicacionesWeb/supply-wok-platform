using Aurora.SupplyWok.Platform.Iot.Application.CommandServices;
using Aurora.SupplyWok.Platform.Iot.Domain.Model;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Aggregate;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Entities;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Iot.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Aurora.SupplyWok.Platform.Iot.Application.Internal.CommandServices;

/// <summary>
/// Alert command service implementation.
/// </summary>
public class AlertCommandService(
    IAlertRepository alertRepository,
    ISensorRepository sensorRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer) : IAlertCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;

    // <inheritdoc />
    public async Task<Result<Alert>> Handle(CreateAlertRestaurantCommand command, CancellationToken cancellationToken)
    {
        var sensor = await sensorRepository.GetSensorByIdAsync(command.SensorId, cancellationToken);
        if (sensor == null)
        {
            return Result<Alert>.Failure(AlertsError.SensorNotFound,
                string.Format(_localizer[nameof(AlertsError.SensorNotFound)] ?? "Sensor with ID {0} not found.", command.SensorId));
        }

        var alert = new AlertRestaurant(
            command.Severity,
            command.Detail,
            DateTimeOffset.UtcNow,
            EAlertStatus.Pending,
            command.SensorId
        );

        try
        {
            await alertRepository.AddAsync(alert, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Alert>.Success(alert);
        }
        catch (OperationCanceledException)
        {
            return Result<Alert>.Failure(AlertsError.OperationCancelled,
                _localizer[nameof(AlertsError.OperationCancelled)] ?? "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Alert>.Failure(AlertsError.DatabaseError,
                _localizer[nameof(AlertsError.DatabaseError)] ?? "Database error occurred.");
        }
        catch (Exception ex)
        {
            return Result<Alert>.Failure(AlertsError.InternalServerError,
                ex.Message);
        }
    }

    // <inheritdoc />
    public async Task<Result<Alert>> Handle(AcknowledgeAlertCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var alert = await alertRepository.GetAlertByIdAsync(command.AlertId, cancellationToken);
            if (alert == null)
            {
                return Result<Alert>.Failure(AlertsError.AlertNotFound,
                    string.Format(_localizer[nameof(AlertsError.AlertNotFound)] ?? "Alert with ID {0} not found.", command.AlertId));
            }

            alert.Acknowledge();
            alertRepository.Update(alert);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Alert>.Success(alert);
        }
        catch (OperationCanceledException)
        {
            return Result<Alert>.Failure(AlertsError.OperationCancelled,
                _localizer[nameof(AlertsError.OperationCancelled)] ?? "Operation was cancelled.");
        }
        catch (DbUpdateException)
        {
            return Result<Alert>.Failure(AlertsError.DatabaseError,
                _localizer[nameof(AlertsError.DatabaseError)] ?? "Database error occurred.");
        }
        catch (Exception ex)
        {
            return Result<Alert>.Failure(AlertsError.InternalServerError,
                ex.Message);
        }
    }
}
