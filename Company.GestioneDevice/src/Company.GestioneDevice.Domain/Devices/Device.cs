using Company.GestioneDevice.Devices.SoftwareVersions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Company.GestioneDevice.Devices.DeviceFeatures;
using Company.GestioneDevice.Users.UserPolicies;
using Company.GestioneDevice.Users;

namespace Company.GestioneDevice.Devices;

public class Device : AuditedAggregateRoot<Guid>
{
	public string Name { get; private set; }
	public DeviceType Type { get; set; }
    public string Model { get; set; }


    //Navigation Properties
    public  List<SoftwareVersion> SoftwareVersions { get; private set; } = new List<SoftwareVersion>();
    public List<DeviceFeature> DeviceFeatures { get; private set; } = new List<DeviceFeature>();

    public Guid UserId { get; private set; }




    //constructors
    public Device()
    {
    }

    public Device(Guid id, string name, DeviceType type, string model) : base(id)
    {
        SetName(name);
        Type = type;
        Model = model;
    }



    //   --- properties control ---
    public void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(
            name,
            nameof(name),
            maxLength: GestioneDeviceSharedConsts.MaxNameLength
        );
    }


    //--------USER Methods
    public void AssignUser(Guid userId)
    {
        Check.NotNull(userId, nameof(userId));
        UserId = userId;
    }

    //--------FEATURE Methods
    public void AssignFeature(Guid featureId)
    {
        Check.NotNull(featureId, nameof(featureId));

        if (IsAlreadyInDeviceFeatures(featureId))
        {
            return;
        }

        this.DeviceFeatures.Add(new DeviceFeature(this.Id, featureId));
    }

    public void RemoveFeature(Guid featureId)
    {
        Check.NotNull(featureId, nameof(featureId));

        if (!IsAlreadyInDeviceFeatures(featureId))
        {
            return;
        }

        this.DeviceFeatures.RemoveAll(x => x.FeatureId == featureId);
    }

    public void RemoveAllFeaturesExceptGivenIds(List<Guid> featureIds)
    {
        Check.NotNullOrEmpty(featureIds, nameof(featureIds));

        this.DeviceFeatures.RemoveAll(x => !featureIds.Contains(x.FeatureId));
    }

    public void RemoveAllFeatures()
    {
        this.DeviceFeatures.RemoveAll(x => x.DeviceId == this.Id);
    }

    private bool IsAlreadyInDeviceFeatures(Guid featureId)
    {
        return this.DeviceFeatures.Any(x => x.FeatureId == featureId);
    }


    //--------SoftwareVersion Methods
    public void UpdateSoftwareVersion(SoftwareVersion softwareVersion)
    {
        Check.NotNull(softwareVersion, nameof(softwareVersion));

        this.SoftwareVersions.Add(softwareVersion);
    }

}
