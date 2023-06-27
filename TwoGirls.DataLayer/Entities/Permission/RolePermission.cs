namespace TwoGirls.DataLayer.Entities
{
    public class RolePermission
    {

        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }


        #region Relation
        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
        #endregion
    }
}
