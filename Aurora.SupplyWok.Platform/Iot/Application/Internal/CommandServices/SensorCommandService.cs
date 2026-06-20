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
/// Sensor command service
/// </summary>
/// <param name="sensorRepository">Sensor repository</param>
/// <param name="unitOfWork">Unit of work</param>
/// <param name="localizer">Error message localizer</param>
public class SensorCommandService(ISensorRepository sensorRepository,
    IAlertRepository alertRepository,
    IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessages> localizer):
    ISensorCommandService
{
    private readonly IStringLocalizer<ErrorMessages> _localizer = localizer;
    
    // <inheritdoc />
    public async Task<Result<Sensor>> Handle(CreateSensorCommand command, CancellationToken cancellationToken)
    {
        var sensor = new Sensor(command);
        try
        {
            await sensorRepository.AddAsync(sensor, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sensor>.Success(sensor);
        }
        catch (OperationCanceledException)
        {
            return Result<Sensor>.Failure(SensorsError.OperationCancelled,
                _localizer[nameof(SensorsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Sensor>.Failure(SensorsError.DatabaseError,
                _localizer[nameof(SensorsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Sensor>.Failure(SensorsError.InternalServerError,
                _localizer[nameof(SensorsError.InternalServerError)]);
        }
    }

    // <inheritdoc />
    public async Task<Result<Sensor>> Handle(UpdateSensorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sensor = await sensorRepository.GetSensorByIdAsync(command.Id, cancellationToken);
            if (sensor == null)
            {
                return Result<Sensor>.Failure(SensorsError.SensorNotFound,
                    string.Format(_localizer[nameof(SensorsError.SensorNotFound)], command.Id));
            }

            sensor.Update(command.Name, command.MinValue, command.MaxValue, command.Enabled, command.LastValue, command.Type);
            
            if (sensor.LastValue < sensor.MinValue || sensor.LastValue > sensor.MaxValue)
            {
                var detail = $"Sensor '{sensor.Name}' value ({sensor.LastValue}) is out of allowed range [{sensor.MinValue}, {sensor.MaxValue}].";
                var alert = new AlertRestaurant(
                    EAlertSeverity.High,
                    detail,
                    DateTimeOffset.UtcNow,
                    EAlertStatus.Pending,
                    sensor.Id
                )
                {
                    Sensor = sensor
                };
                await alertRepository.AddAsync(alert, cancellationToken);
            }
            
            sensorRepository.Update(sensor);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Sensor>.Success(sensor);
        }
        catch (ArgumentException ex)
        {
            return Result<Sensor>.Failure(SensorsError.InternalServerError, ex.Message);
        }
        catch (OperationCanceledException)
        {
            return Result<Sensor>.Failure(SensorsError.OperationCancelled,
                _localizer[nameof(SensorsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Sensor>.Failure(SensorsError.DatabaseError,
                _localizer[nameof(SensorsError.DatabaseError)]);
        }
        catch (Exception ex)
        {
            return Result<Sensor>.Failure(SensorsError.InternalServerError,
                ex.Message);
        }
    }

    // <inheritdoc />
    public async Task<Result<bool>> Handle(DeleteSensorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var sensor = await sensorRepository.GetSensorByIdAsync(command.Id, cancellationToken);
            if (sensor == null)
            {
                return Result<bool>.Failure(SensorsError.SensorNotFound,
                    string.Format(_localizer[nameof(SensorsError.SensorNotFound)], command.Id));
            }

            sensorRepository.Remove(sensor);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (OperationCanceledException)
        {
            return Result<bool>.Failure(SensorsError.OperationCancelled,
                _localizer[nameof(SensorsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<bool>.Failure(SensorsError.DatabaseError,
                _localizer[nameof(SensorsError.DatabaseError)]);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(SensorsError.InternalServerError,
                ex.Message);
        }
    }
}