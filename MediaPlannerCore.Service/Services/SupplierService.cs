using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Service.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public void AddSupplier(Supplier supplier)
        {
            this.supplierRepository.Insert(supplier);
        }
        public void UpdateSupplier(Supplier supplier)
        {
            this.supplierRepository.Update(supplier);
        }

        public void DeleteSupplier(int? id)
        {
            this.supplierRepository.Delete(id);
        }

        public Supplier GetSupplierById(int? id)
        {
            return this.supplierRepository.GetByID(id);
        }

        public IEnumerable<Supplier> GetSuppliers(string filter, string includeProperties)
        {
            IEnumerable<Supplier> suppliers = null;
            if (filter != null)
            {
                suppliers = this.supplierRepository.Get(s => s.SupplierName.Contains(filter), includeProperties).AsEnumerable();

            }
            else
            {
                suppliers = this.supplierRepository.Get(null, includeProperties).AsEnumerable();
            }
            return suppliers;
        }
    }
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetSuppliers(string filter, string includeProperties);
        Supplier GetSupplierById(int? id);
        void AddSupplier(Supplier supplier);
        void UpdateSupplier(Supplier supplier);
        void DeleteSupplier(int? id);

    }
}
