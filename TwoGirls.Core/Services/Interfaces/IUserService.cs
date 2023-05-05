using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public User GetUserByEmailAndPassword(string email, string password);
        public int AddUser(User user);
        public void UpdateUser(EditProfileViewModel user);
        #endregion

        #region User Address   
        public IEnumerable<Address> GetUserAddresses(int id);
        public int AddUserAddress(Address address);
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

    }
}
