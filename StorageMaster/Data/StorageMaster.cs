using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StorageMaster.Entity.Products;
using StorageMaster.Entity.Storages;
using StorageMaster.Entity.Vehicleses;
using StorageMaster.Factories;

namespace StorageMaster.Data
{
    public class StorageMaster
    {
        private Dictionary<string,Storage> storages;
        private Dictionary<string,Stack<Product>> products;
        private ProductFactory productFactory;
        private StorageFactory storageFactory;
        private Vehicle currentVehicle;

        public StorageMaster()
        {
            this.storages = new Dictionary<string, Storage>();
            this.products = new Dictionary<string, Stack<Product>>();
            this.productFactory = new ProductFactory();
            this.storageFactory = new StorageFactory();
        }


        public string AddProduct(string type, double price)
        {
            if (!this.products.ContainsKey(type))
            {
                this.products[type] = new Stack<Product>();
            }
            Product product = productFactory.CreateProduct(type, price);
            this.products[type].Push(product);
            return $"Added {type} to pool";
        }

        public string RegisterStorage(string type, string name)
        {
            Storage storage = storageFactory.CreateStorage(type, name);
            this.storages[storage.Name] = storage;
            return $"Registered {storage.Name}";
        }

        public string SelectVehicle(string storageName, int garageSlot)
        {

            Storage storage = this.storages[storageName];
            Vehicle vehicle = storage.GetVehicle(garageSlot);
            
            this.currentVehicle = vehicle;
            return $"Selected {vehicle.GetType().Name}";
        }

        public string LoadVehicle(IEnumerable<string> productNames)
        {
            int loadedProductsCount = 0;
            
            Vehicle vehicle = this.currentVehicle;

            foreach (var productName in productNames)
            {
                if (this.currentVehicle.IsFull)
                {
                    break;
                }
                if (!this.products.ContainsKey(productName) || this.products[productName].Any())
                {
                    throw new InvalidOperationException($"{productName} is out of stock!");
                }
                var product = this.products[productName].Pop();
                this.currentVehicle.LoadProducts(product);
                loadedProductsCount++;
            }

            int productCount = productNames.Count();
            return $"Loaded {loadedProductsCount}/{productCount} products into {vehicle.GetType().Name}";
        }

        public string SendVehicleTo(string sourceName, int sourceGarageSlot, string destinationName)
        {
            Storage storageToGO = storages.FirstOrDefault(c => c.Name == sourceName);
            Storage destStorage = storages.FirstOrDefault(c => c.Name == destinationName);
            if (storageToGO == null)
            {
                throw new InvalidOperationException("Invalid source storage!");
            }
            if (destStorage == null)
            {
                throw new InvalidOperationException("Invalid destination storage!");
            }

            var destinationGarageSlot = storageToGO.SendVehicleTo(sourceGarageSlot, destStorage);

            Vehicle vehicle = storageToGO.GetVehicle(sourceGarageSlot);
            return $"Sent {vehicle.GetType().Name} to {destinationName} (slot {destinationGarageSlot})";

        }

        public string UnloadVehicle(string storageName, int garageSlot)
        {
            Storage storage = storages.FirstOrDefault(c => c.Name == storageName);
            Vehicle vehicle = storage.GetVehicle(garageSlot);
            var vehCount = vehicle.Trunk.Count;
            var unloadedProductsCount = storage.UnloadVehicle(garageSlot);

            return $"Unloaded {unloadedProductsCount}/{vehCount} products at {storageName}";
        }

        public string GetStorageStatus(string storageName)
        {
            throw new NotImplementedException();
        }

        public string GetSummary()
        {
            throw new NotImplementedException();
        }

    }
}
