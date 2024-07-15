namespace WebApiModel.EmployeeDetail
{

    public class EmployeeDetailAPIModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public System.DateTime DOB { get; set; }
        public string Address { get; set; }
        public int RoleId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Password { get; set; }
    }
}