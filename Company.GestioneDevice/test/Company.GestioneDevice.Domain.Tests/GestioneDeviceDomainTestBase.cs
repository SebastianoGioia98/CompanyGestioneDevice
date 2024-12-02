﻿using Volo.Abp.Modularity;

namespace Company.GestioneDevice;

/* Inherit from this class for your domain layer tests. */
public abstract class GestioneDeviceDomainTestBase<TStartupModule> : GestioneDeviceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
