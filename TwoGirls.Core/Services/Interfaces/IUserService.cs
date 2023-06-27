using TwoGirls.Core.DTOs;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services.Interfaces
{
    public interface IUserService
    {
        #region User
        public bool IsUserExistByEmail(string email);
        public bool IsUserExistByEmailAndPassword(string email, string password);
        public bool ActiveUserAccount(string activeCode);
        public bool ResetPassword(string activeCode, string password);
        public bool ChangePassword(int id, string oldPassword, string password);
        public User GetUserByEmail(string email);
        public User GetUserById(int userId);
        public User GetUserByIdIncludeRole(int userId);
        public User GetUserByIdIncludeFavorites(int userId);
        public User GetUserByIdIgnoreQueryFilters(int userId);
        public User GetUserByEmailAndPassword(string email, string password);
        public int AddUser(User user);
        public void UpdateUser(EditProfileViewModel user);
        #endregion

        #region User Favorites
        public List<Favorite> GetUserFavorites(int userId);
        public bool IsInUserFavorites(int userId, int productId);
        public void AddFavorite(Favorite favorite);
        public void DeleteFavorite(int userId, int productId);

        #endregion

        #region User Address   
        public List<Address> GetUserAddresses(int id);
        public int AddUserAddress(Address address);
        public bool CheckAddressId(int userId, int addressId);
        public bool RemoveUserAddress(int userId, int addressId);
        #endregion

        #region User CreditCard
        public IEnumerable<CreditCard> GetUserCreditCards(int id);
        public int AddUserCreditCard(CreditCard creditCard);
        public bool RemoveUserCreditCard(int userId, int creditCardId);
        #endregion

        #region User Avatar
        public UserAvatarViewModel ChangeAvatar(int id, string userAvatar);
        #endregion

        #region User Wallet
        public decimal GetAccountBalance(int UserId);
        public List<Transaction> GetAllTransactions(int UserId);
        public void AddTransaction(Transaction transaction);
        #endregion

        #region User for Admin
        public UsersForAdminViewModel GetUsersByFilterForAdminViewModel(int pageId = 1, string filter = "");
        public UsersForAdminViewModel GetDeletedUsersByFilterForAdminViewModel(int pageId = 1, string filter = "");
        public bool EditUserForAdmin(User user);
        public bool DeleteUserForAdmin(int userId);
        public bool RecoverUserForAdmin(int userId);
        public List<Role> GetAllRoles();
        #endregion
    }
}
