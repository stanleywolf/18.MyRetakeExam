using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorageMaster.Entity.Products;
using StorageMaster.Entity.Vehicleses;

namespace StorageMaster.Entity.Storages
{
    public abstract class Storage
    {
        private string name;

        public string Name
        {
            get { return name; }
           protected set { name = value; }
        }
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
            protected set { capacity = value; }
        }
        private int garageSlots;

        public int GarageSlots
        {
            get { return garageSlots; }
           protected set { garageSlots = value; }
        }

        public bool IsFull => this.Products.Sum(c => c.Weight) >= this.Capacity;

        private  Vehicle[] garage;
        public IReadOnlyCollection<Vehicle> Garage => Array.AsReadOnly(this.garage);

        private  List<Product> products;
        public IReadOnlyCollection<Product> Products => this.products.AsReadOnly();

        protected Storage(string name,int capacity, int garageSlots,IEnumerable<Vehicle> vehicles)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.GarageSlots = garageSlots;

            this.garage = new Vehicle[garageSlots];
            this.products = new List<Product>();

            this.InitializeGarage(vehicles);
        }

        private void InitializeGarage(IEnumerable<Vehicle> vehicles)
        {
            var garageSlot = 0;
            foreach (var vehicle in vehicles)
            {
                this.garage[garageSlot++] = vehicle;
            }
        }
        public Vehicle GetVehicle(int garageSlot)
        {
            if (garageSlot >= this.garage.Length)
            {
                throw new InvalidOperationException("Invalid garage slot!");
            }
            if (this.garage[garageSlot] == null)
            {
                throw new InvalidOperationException("No vehicle in this garage slot!");
            }
            return this.garage[garageSlot];
        }

        public int SendVehicleTo(int garageSlot, Storage deliveryLocation)
        {
            Vehicle vehicle = this.GetVehicle(garageSlot);
            var delyGaraHasFreeSlot = deliveryLocation.Garage.Any(c => c == null);
            if(!delyGaraHasFreeSlot)
            {
                throw new InvalidOperationException("No room in garage!");
            }

            this.garage[garageSlot] = null;

            var addedSlot = deliveryLocation.AddVehicle(vehicle);
            return addedSlot;
        }

        private int AddVehicle(Vehicle vehicle)
        {
            var freeGarageSlot = Array.IndexOf(this.garage, null);
            this.garage[freeGarageSlot] = vehicle;

            return freeGarageSlot;
        }

        public int UnloadVehicle(int garageSlot)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Storage is full!");
            }
            Vehicle vehicle = this.GetVehicle(garageSlot);
            int unloadeedProduct = 0;
            while (!vehicle.IsEmpty && !this.IsFull)
            {
                var product = vehicle.Unload();
                this.products.Add(product);
                
                unloadeedProduct++;
            }


            return unloadeedProduct;

            
        }

    }
}
