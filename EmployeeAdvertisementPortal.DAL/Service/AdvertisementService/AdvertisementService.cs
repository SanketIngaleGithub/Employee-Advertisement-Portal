using EmployeeAdvertisementPortal.DAL.Data;
using EmployeeAdvertisementPortal.DAL.IService.IAdvertisementService;
using EmployeeAdvertisementPortal.Model.AdvertisementViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace EmployeeAdvertisementPortal.DAL.Service.AdvertisementService
{
    public class AdvertisementService : IAdvertisementService
    {
        Employee_Advertisement_PortalEntity db = new Employee_Advertisement_PortalEntity();
        public List<AdvertisementDetails_tbl> GetApprovedList(int pageNumber, int pageSize)
        {
            int skipCount = pageNumber * pageSize;
            List<AdvertisementDetails_tbl> listOfAdvertisement = db.AdvertisementDetails_tbl.Include("AdvertInterestMapper_tbl").Where(a => a.IsApproved == true).OrderBy(a => a.AdvId).Skip(skipCount).Take(pageSize).ToList();

            List<AdvertisementDetails_tbl> modifiedListOfAdvertisement = new List<AdvertisementDetails_tbl>();
            foreach (AdvertisementDetails_tbl item in listOfAdvertisement)
            {
                AdvertisementDetails_tbl modifiedAdvertisement = new AdvertisementDetails_tbl();

                modifiedAdvertisement.AdvId = item.AdvId;
                modifiedAdvertisement.EmpId = item.EmpId;
                modifiedAdvertisement.Title = item.Title;
                modifiedAdvertisement.Description = item.Description;
                modifiedAdvertisement.Price = item.Price;
                modifiedAdvertisement.PostedDate = item.PostedDate;
                modifiedAdvertisement.CreatedBy = item.CreatedBy;
                modifiedAdvertisement.CreatedDate = item.CreatedDate;
                modifiedAdvertisement.ModifiedBy = item.ModifiedBy;
                modifiedAdvertisement.ModifiedDate = item.ModifiedDate;
                modifiedAdvertisement.Location = item.Location;
                modifiedAdvertisement.AdvCategoryId = item.AdvCategoryId;
                modifiedAdvertisement.IsApproved = item.IsApproved;
                modifiedAdvertisement.IsRejected = item.IsRejected;
                modifiedAdvertisement.MediaPath = item.MediaPath;
                modifiedAdvertisement.AdvertInterestMapper_tbl = item.AdvertInterestMapper_tbl;
                
                modifiedListOfAdvertisement.Add(modifiedAdvertisement);
            }
            return modifiedListOfAdvertisement;
        }

        public List<AdvertisementDetails_tbl> GetAllAdvertisement()
        {
            List<AdvertisementDetails_tbl> listOfAdvertisement = db.AdvertisementDetails_tbl.ToList();
            List<AdvertisementDetails_tbl> modifiedListOfAdvertisement = new List<AdvertisementDetails_tbl>();
            foreach (AdvertisementDetails_tbl item in listOfAdvertisement)
            {
                AdvertisementDetails_tbl modifiedAdvertisement = new AdvertisementDetails_tbl();
                modifiedAdvertisement.AdvId = item.AdvId;
                modifiedAdvertisement.EmpId = item.EmpId;
                modifiedAdvertisement.Title = item.Title;
                modifiedAdvertisement.Description = item.Description;
                modifiedAdvertisement.Price = item.Price;
                modifiedAdvertisement.PostedDate = item.PostedDate;
                modifiedAdvertisement.CreatedBy = item.CreatedBy;
                modifiedAdvertisement.CreatedDate = item.CreatedDate;
                modifiedAdvertisement.ModifiedBy = item.ModifiedBy;
                modifiedAdvertisement.ModifiedDate = item.ModifiedDate;
                modifiedAdvertisement.Location = item.Location;
                modifiedAdvertisement.AdvertisementCategory_tbl = new AdvertisementCategory_tbl();
                modifiedAdvertisement.AdvertisementCategory_tbl.Category = item.AdvertisementCategory_tbl.Category;
                modifiedAdvertisement.AdvCategoryId = item.AdvCategoryId;
                modifiedAdvertisement.IsApproved = item.IsApproved;
                modifiedAdvertisement.IsRejected = item.IsRejected;
                modifiedAdvertisement.MediaPath = item.MediaPath;

                modifiedListOfAdvertisement.Add(modifiedAdvertisement);
            }
            return modifiedListOfAdvertisement;
        }

        public AdvertisementDetails_tbl GetAdvertisementById(int id)
        {
            var item  =  db.AdvertisementDetails_tbl.Include("EmployeeDetails_tbl").FirstOrDefault(x => x.AdvId == id);
            return item;
        }

        public List<AdvertisementCategory_tbl> GetAllCategory()
        {
            var category = db.AdvertisementCategory_tbl.ToList();
            return category;
        }

        public bool CreateAdvertisement(AdvertisementDetails_tbl model)
        {
            try
            {
                model.AdvId = 0;
                db.AdvertisementDetails_tbl.Add(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw ;
            }
        }

        public bool UpdateAdvertisement(AdvertisementDetails_tbl model)
        {
            try
            {
                db.SP_UpdateAdvDetail(model.AdvId, model.EmpId, model.Title, model.Description, model.Price, model.PostedDate, model.CreatedBy, model.CreatedDate, model.ModifiedBy, model.ModifiedDate, model.Location, model.AdvCategoryId, model.IsApproved, model.IsRejected, model.MediaPath);

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteAdvertisement(int id)
        {
            try
            {
                AdvertisementDetails_tbl advertisement = db.AdvertisementDetails_tbl.FirstOrDefault(x => x.AdvId == id);
                if (advertisement != null)
                {
                    db.AdvertisementDetails_tbl.Remove(advertisement);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AdvertisementViewModel> GetAdvertisementsByEmpId(int id)
        {
            List<AdvertisementDetails_tbl> listOfAdvertisements = db.AdvertisementDetails_tbl.Where(a => a.EmpId == id).ToList();
            List<AdvertisementViewModel> advertisementsByEmpId = AdvertisementDetails(listOfAdvertisements);
            return advertisementsByEmpId;
        }

        public List<AdvertisementViewModel> AdvertisementDetails(List<AdvertisementDetails_tbl> advertisementDetails)
        {
            return advertisementDetails.Select(x => new AdvertisementViewModel
            {
                AdvId = x.AdvId,
                EmpId = x.EmpId,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                PostedDate = x.PostedDate,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,
                Location = x.Location,
                AdvCategoryId = x.AdvCategoryId,
                IsApproved = x.IsApproved,
                IsRejected = x.IsRejected,
                MediaPath = x.MediaPath
                
            }).ToList();
        }

        public string GetEmailByEmpId(int EmpId)
        {
            try
            {
                using (Employee_Advertisement_PortalEntity context = new Employee_Advertisement_PortalEntity())
                {
                    var user = context.EmployeeDetails_tbl.FirstOrDefault(x => x.EmpId == EmpId);
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

        public bool CheckAdvIdExists(int id)
        {
            return  db.AdvertisementDetails_tbl.Any(x => x.AdvId == id); 
        }

        public bool SaveInterest(int AdvId, int userId, bool isInterested)
        {
            try
            {
                var existingInterest = db.AdvertInterestMapper_tbl.FirstOrDefault(i => i.AdvId == AdvId && i.EmpId == userId);

                if (isInterested)
                {
                    if (existingInterest == null)
                    {
                        AdvertInterestMapper_tbl interest = new AdvertInterestMapper_tbl
                        {
                            AdvId = AdvId,
                            EmpId = userId
                        };
                        db.AdvertInterestMapper_tbl.Add(interest);
                    }
                }
                else
                {
                    if (existingInterest != null)
                    {
                        db.AdvertInterestMapper_tbl.Remove(existingInterest);
                    }
                }
               
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<AdvertInterestMapper_tbl> GetInterestMappingsByUserId(int userId)
        {
            return db.AdvertInterestMapper_tbl.Where(i => i.EmpId == userId).ToList();
        }

        public AdvertisementDetails_tbl GetAdvertisementDetailsById(int id)
        {
            return db.AdvertisementDetails_tbl.FirstOrDefault(x => x.AdvId == id);
        }

        public List<string> GetEmailByAdvId(int advId)
        {
            var emails = (from mp in db.AdvertInterestMapper_tbl
                          join ep in db.EmployeeDetails_tbl on mp.EmpId equals ep.EmpId
                          where mp.AdvId == advId
                          select ep.Email).ToList();

            return emails;
        }
    }
}