﻿using ReportApp_Models;

namespace ReportApp_Repositories.Shippers
{
    public interface IShipperRepository
    {
        IEnumerable<Shipper> GetAll();
        Shipper GetShipperById(int id);
        void AddShipper(Shipper shipper);
        void UpdateShipper(Shipper shipper);
        void DeleteShipper(int id);
    }
}
