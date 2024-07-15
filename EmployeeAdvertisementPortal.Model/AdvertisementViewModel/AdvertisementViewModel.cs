using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace EmployeeAdvertisementPortal.Model.AdvertisementViewModel
{
    public class AdvertisementViewModel
    {

        public int AdvId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int EmpId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Price must be a positive number")]
        public int Price { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Posted Date")]
        [DataType(DataType.Date)]
        public DateTime PostedDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int CreatedBy { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public int ModifiedBy { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Modified Date")]
        [DataType(DataType.Date)]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Location { get; set; }

        [DisplayName("Category")]

        [Required(ErrorMessage = "This field is required")]
        public int AdvCategoryId { get; set; }

        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }

        [DisplayName("Image")]
        public string MediaPath { get; set; }

        //[Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase AdvertisementFile { get; set; }

        public IEnumerable<SelectListItem> Category { get; set; }

        public string CreatedByEmail { get; set; }

        public bool Interested { get; set; }
        public string InterestedEmails { get; set; }
        public int EmailCount { get; set; }
    }
}