using System;
using System.Collections.Generic;
using System.Text;
using StorageMaster.Entity.Vehicleses;

namespace StorageMaster.Entity.Storages
{
   public class Warehouse:Storage
    {
        public Warehouse(string name) : base(name, 10, 10, new Vehicle[]{new Semi(),new Semi(),new Semi()})
        {
            
        }
    }
}
