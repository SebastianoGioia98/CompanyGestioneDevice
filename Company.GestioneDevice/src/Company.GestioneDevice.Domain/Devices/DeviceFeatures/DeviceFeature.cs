using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Company.GestioneDevice.Devices.DeviceFeatures;

public class DeviceFeature: AuditedEntity<Guid>
{
    public Guid DeviceId { get; private set; }
    public Guid FeatureId { get; private set; }

    public DeviceFeature() { } 

    public DeviceFeature(Guid id, Guid deviceId, Guid featureId):base(id)
    {
        DeviceId = deviceId;
        FeatureId = featureId;
    }
}
