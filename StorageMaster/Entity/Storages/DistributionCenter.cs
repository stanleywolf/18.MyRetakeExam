using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Entity.Vehicleses;

namespace StorageMaster.Entity.Storages
{
   public class DistributionCenter:Storage
    {
        public DistributionCenter(string name) : base(name, 2, 5, new Vehicle[] { new Van(), new Van(), new Van() })
        {
           
        }
    }
}
