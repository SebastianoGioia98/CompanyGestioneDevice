using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.GestioneDevice.Devices;

public class DeviceDetailedDto : DeviceDto
{

    public SoftwareVersionLookupDto LastSoftwareVersion { get; set; }

    public List<FeatureDto> Features { get; set; }

}
