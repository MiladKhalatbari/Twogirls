using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.DTOs
{
    #region User
    public class UsersForAdminViewModel
    {
        public List<User> Users { get; set; }
        public string Filter { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }

    }
    public class CreateOrEditUserForAdmin
    {
        public User User { get; set; }
        public List<Role>? Roles { get; set; }
        public IFormFile? formFile { get; set; }
        [MinLength(8)]
        public string? Password { get; set; }
    }
    #endregion

    #region Product
    public class ProductsForAdminViewModel
    {
        public List<Product> Products { get; set; }
        public string Filter { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }

    public class CreateOrEditProductForAdmin
    {
        public Product Product { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public ICollection<ProductType>? ProductTypes { get; set; }
        [BindProperty]
        public List<int> SelectedCategories { get; set; }
        [BindProperty]
        public List<IFormFile>? Files { get; set; }
    }
    #endregion

    #region Role Permission 
    public class CreateOrEditRoleForAdmin
    {
        public Role Role { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
        [BindProperty]
        public List<int> SelectedRoles { get; set; }
    }
    #endregion

    #region Order 
    public class OrdersForAdminViewModel
    {
        public List<Order> Orders { get; set; }
        public string Filter { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    #endregion
    public class DiscountsForAdminViewModel
    {
        public List<DiscountCode> Discounts { get; set; }
        public string Filter { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    #region  
    #endregion TransactionsForAdminViewModels
    public class TransactionsForAdminViewModel
    {
        public List<Transaction> Transactions { get; set; }
        public string Filter { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
    #region  
    #endregion
}
