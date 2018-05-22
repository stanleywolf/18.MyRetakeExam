using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StorageMaster.Entity.Vehicleses
{
    using StorageMaster.Entity.Products;
    public abstract class Vehicle
    {
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
           protected set { capacity = value; }
        }
        private readonly List<Product> trunk;

        public IReadOnlyCollection<Product> Trunk => this.trunk.AsReadOnly();


        public bool IsFull => this.Trunk.Sum(c => c.Weight) >= this.Capacity;
        public bool IsEmpty => !this.Trunk.Any();

        protected Vehicle(int capacity)
        {
            this.Capacity = capacity;
            this.trunk = new List<Product>();
        }

        public void LoadProducts(Product product)
        {
            if (IsFull)
            {
                throw new InvalidOperationException("Vehicle is full!");
            }
            this.trunk.Add(product);
        }

        public Product Unload()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("No products left in vehicle!");
            }
            var product = this.trunk.Last();
            this.trunk.RemoveAt(this.Trunk.Count -1);
            return product;
        }
        
    }
}
