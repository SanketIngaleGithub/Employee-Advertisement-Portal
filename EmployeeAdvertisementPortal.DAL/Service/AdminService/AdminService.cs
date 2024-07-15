using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdminService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeAdvertisementPortal.DAL.Service.AdminService
{
    public class AdminService : IAdminService 
    {
        public Employee_Advertisement_PortalEntity db = new Employee_Advertisement_PortalEntity();
        public List<AdvertisementDetails_tbl> GetAdvertisementRequests()
        {
            List<AdvertisementDetails_tbl> listOfAdvertisements = db.AdvertisementDetails_tbl.ToList();
            List<AdvertisementDetails_tbl> modifiedListOfAdvertisements = new List<AdvertisementDetails_tbl>();
            foreach (AdvertisementDetails_tbl item in listOfAdvertisements)
            {
                AdvertisementDetails_tbl modifiedAdvertisements = new AdvertisementDetails_tbl();
                modifiedAdvertisements.AdvId = item.AdvId;
                modifiedAdvertisements.EmpId = item.EmpId;
                modifiedAdvertisements.Title = item.Title;
                modifiedAdvertisements.Description = item.Description;
                modifiedAdvertisements.Price = item.Price;
                modifiedAdvertisements.PostedDate = item.PostedDate;
                modifiedAdvertisements.CreatedBy = item.CreatedBy;
                modifiedAdvertisements.CreatedDate = item.CreatedDate;
                modifiedAdvertisements.ModifiedBy = item.ModifiedBy;
                modifiedAdvertisements.ModifiedDate = item.ModifiedDate;
                modifiedAdvertisements.ModifiedBy = item.ModifiedBy;
                modifiedAdvertisements.ModifiedDate = item.ModifiedDate;
                modifiedAdvertisements.Location = item.Location;
                modifiedAdvertisements.AdvCategoryId = item.AdvCategoryId;
                modifiedAdvertisements.IsApproved = item.IsApproved;
                modifiedAdvertisements.IsRejected = item.IsRejected;
                modifiedAdvertisements.MediaPath = item.MediaPath;

                modifiedListOfAdvertisements.Add(modifiedAdvertisements);
            }
            return modifiedListOfAdvertisements;
        }
        public bool ApproveAdvertisement(int id)
        {
            AdvertisementDetails_tbl advertisement = db.AdvertisementDetails_tbl.Find(id);
            if (advertisement != null)
            {
                advertisement.IsApproved = true;
                advertisement.IsRejected = false;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool RejectAdvertisement(int id)
        {
            AdvertisementDetails_tbl advertisement = db.AdvertisementDetails_tbl.Find(id);
            if (advertisement != null)
            {
                advertisement.IsRejected = true;
                advertisement.IsApproved = false;
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool DeleteAdvertisement(int id)
        {
            try
            {
                AdvertisementDetails_tbl advertisemnt = db.AdvertisementDetails_tbl.Find(id);
                if (advertisemnt != null)
                {
                    db.AdvertisementDetails_tbl.Remove(advertisemnt);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
               throw;
            }
        }
        public List<AdvertisementCategory_tbl> GetCategoryName()
        {
            List<AdvertisementCategory_tbl> category = db.AdvertisementCategory_tbl.ToList();
            return category;
        }
        public int? GetEmpIdByAdvId(int advId)
        {
            try
            {
                using (Employee_Advertisement_PortalEntity context = new Employee_Advertisement_PortalEntity())
                {
                    var advertisement = context.AdvertisementDetails_tbl.FirstOrDefault(x => x.AdvId == advId);
                    if (advertisement != null)
                    {
                        return advertisement.EmpId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        public string GetEmailByEmpId(int empId)
        {
            try
            {
                using (Employee_Advertisement_PortalEntity context = new Employee_Advertisement_PortalEntity())
                {
                    var user = context.EmployeeDetails_tbl.FirstOrDefault(x => x.EmpId == empId);
                    if (user != null)
                    {
                        return user.Email;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public string GetTitleByAdvId(int advId)
        {
            try
            {
                using (Employee_Advertisement_PortalEntity context = new Employee_Advertisement_PortalEntity())
                {
                    var advertisement = context.AdvertisementDetails_tbl.FirstOrDefault(x => x.AdvId == advId);
                    if (advertisement != null)
                    {
                        return advertisement.Title;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public int GetPriceByAdvId(int advId)
        {
            try
            {
                using (Employee_Advertisement_PortalEntity context = new Employee_Advertisement_PortalEntity())
                {
                    var advertisement = context.AdvertisementDetails_tbl.FirstOrDefault(x => x.AdvId == advId);
                    if (advertisement != null)
                    {
                        return advertisement.Price;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }
    }
}