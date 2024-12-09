﻿using Company.GestioneDevice.Devices.SoftwareVersions;
using Company.GestioneDevice.Devices.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Company.GestioneDevice.Devices.DeviceFeatures;

namespace Company.GestioneDevice.Devices;

public class Device : AuditedAggregateRoot<Guid>
{
	public string Name { get; private set; }
	public DeviceType Type { get; set; }
    public string Model { get; set; }


    //Navigation Properties
    public  List<SoftwareVersion> SoftwareVersions { get; set; } = new List<SoftwareVersion>();
    public List<DeviceFeature> DeviceFeatures { get; set; } = new List<DeviceFeature>();

    public Guid UserId { get; set; }






    //   --- properties control ---
    //NAME (UNIQUE)
    internal Device ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: GestioneDeviceSharedConsts.MaxNameLength
        );
    }



    //constructors
    public Device()
    {

    }

    public Device(Guid id, string name, DeviceType type, string model, SoftwareVersion softwareVersion) : base(id)
    {
        SetName(name);
        Type = type;
        Model = model;
        SoftwareVersions.Add(softwareVersion);
    }


}
