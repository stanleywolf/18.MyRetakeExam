using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Entity.Vehicleses;

namespace StorageMaster.Entity.Storages
{
   public class AutomatedWarehouse:Storage
    {
        private static readonly Vehicle[] DefaultVehicles =
        {
            new Truck()
        };
        public AutomatedWarehouse(string name) : base(name,1,2,DefaultVehicles)
        {
           
        }
    }
}
