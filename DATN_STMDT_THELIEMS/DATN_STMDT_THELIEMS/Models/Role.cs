namespace DATN_STMDT_THELIEMS.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Role_Permission> Role_Permissions { get; set; }
        public ICollection<Users> Users { get; set; }

    }
    //public class AssignPermissionViewModel
    //{
    //    public int RoleId { get; set; }
    //    public string RoleName { get; set; }
    //    public List<PermissionViewModel> Permissions { get; set; }
    //    public List<int> SelectedPermissionIds { get; set; } // Danh sách quyền được chọn
    //}

    //public class PermissionViewModel
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public bool IsAssigned { get; set; }
    //}
}
