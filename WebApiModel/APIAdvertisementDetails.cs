public class APIAdvertisementDetails
{
    public int AdvId { get; set; }
    public int EmpId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public System.DateTime PostedDate { get; set; }
    public int CreatedBy { get; set; }
    public System.DateTime CreatedDate { get; set; }
    public int ModifiedBy { get; set; }
    public System.DateTime ModifiedDate { get; set; }
    public string Location { get; set; }
    public int AdvCategoryId { get; set; }
    public bool IsApproved { get; set; }
    public string MediaPath { get; set; }
    public bool IsRejected { get; set; }
    public bool Interested { get; set; }
    
   
}

