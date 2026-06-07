using Aurora.SupplyWok.Platform.Iot.Domain.Model.ValueObjects;

namespace Aurora.SupplyWok.Platform.Iot.Interfaces.Rest.Resources;

public record UpdateSensorResource(
    string Name,
    double MinValue,
    double MaxValue,
    bool Enabled,
    double LastValue,
    ESensorType Type);
