using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwoGirls.Core.Convertors;
using TwoGirls.Core.DTOs;
using TwoGirls.Core.Generator;
using TwoGirls.Core.Security;
using TwoGirls.Core.Services.Interfaces;
using TwoGirls.DataLayer.Context;
using TwoGirls.DataLayer.Entities;

namespace TwoGirls.Core.Services
{
    public class UserService : IUserService
    {
        TwogirsContext _context;
        public UserService(TwogirsContext context)
        {
            _context = context;
        }

        #region User
        public int AddUser(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
        public void UpdateUser(EditProfileViewModel user)
        {
            var _user = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            _user.FirstName = user.FirstName;
            _user.LastName = user.LastName;
            _user.Email = user.Email;
            _user.PhoneNumber = user.PhoneNumber;
            _context.Update(_user);
            _context.SaveChanges();
        }
        public bool ActiveUserAccount(string activeCode)
        {
            var user = _context.Users.FirstOrDefault(x => x.ActiveCode == activeCode);
            if (user == null || user.IsActive)
            {
                return false;
            }

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqueCode();
            _context.SaveChanges();
            return true;
        }
        public bool IsUserExistByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == FixedText.FixedEmail(email));
        }
        public bool IsUserExistByEmailAndPassword(string email, string password)
        {
            try
            {
                var user = _context.Users
                     .FirstOrDefault(u => u.Email == FixedText.FixedEmail(email));


                if (HashPassword.Verify(password, user.Password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool ResetPassword(string activeCode, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.ActiveCode == activeCode);
            if (user == null)
            {
                return false;
            }
            user.Password = HashPassword.Hash(password);
            user.ActiveCode = NameGenerator.GenerateUniqueCode();
            _context.SaveChanges();
            return true;
        }
        public bool ChangePassword(int id, string oldPassword, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return false;
            }
            if (HashPassword.Verify(oldPassword, user.Password))
            {
                user.Password = HashPassword.Hash(password);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users
                     .FirstOrDefault(u => u.Email == FixedText.FixedEmail(email));
        }
        public User GetUserById(int userId)
        {
            return _context.Users
                               .FirstOrDefault(u => u.Id == userId);
        }
        public User GetUserByEmailAndPassword(string email, string password)
        {
            try
            {
                var user = _context.Users
                     .FirstOrDefault(u => u.Email == FixedText.FixedEmail(email));


                if (HashPassword.Verify(password, user.Password.ToString()))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region User Address
        public IEnumerable<Address> GetUserAddresses(int id)
        {
            return _context.Addresses.Where(x => x.UserId == id).ToList();

        }
        public int AddUserAddress(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
            return address.Id;
        }
        public bool RemoveUserAddress(int userId, int addressId)
        {
            var address = _context.Addresses.Where(x => x.UserId == userId).FirstOrDefault(x => x.Id == addressId);
            if (address == null)
            {
                return false;
            }
            _context.Addresses.Remove(address);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region User CreditCard
        public int AddUserCreditCard(CreditCard creditCard)
        {
            _context.CreditCards.Add(creditCard);
            _context.SaveChanges();
            return creditCard.Id;
        }
        public IEnumerable<CreditCard> GetUserCreditCards(int id)
        {
            return _context.CreditCards.Where(x => x.UserId == id).ToList();
        }
        public bool RemoveUserCreditCard(int userId, int creditCardId)
        {
            var creditCard = _context.CreditCards.Where(x => x.UserId == userId).FirstOrDefault(x => x.Id == creditCardId);
            if (creditCard == null)
            {
                return false;
            }
            _context.CreditCards.Remove(creditCard);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region User Avatar
        public UserAvatarViewModel ChangeAvatar(int id, string userAvatar)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            user.ImagePath = userAvatar;
            _context.SaveChanges();
            return new UserAvatarViewModel
            {
                ImagePath = user.ImagePath,
                FullName = user.FirstName + " " + user.LastName,
                Email = user.Email
            };
        }
        #endregion

        #region User Wallet
        public decimal GetAccountBalance(int UserId)
        {
            var Deposit = _context.Transactions.Where(t => t.UserId == UserId && t.TypeId == 1&& t.Finaly==true).Select(t => t.Amount).ToList();
            var Withdraw = _context.Transactions.Where(t => t.UserId == UserId && t.TypeId == 2 && t.Finaly == true).Select(t => t.Amount).ToList();

            return Deposit.Sum() - Withdraw.Sum();
        }
        public List<Transaction> GetAllTransactions(int UserId)
        {
            return  _context.Transactions.Where(t => t.UserId == UserId && t.Finaly == true).Include(x=>x.TransactionType).ToList();
        }
        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
        #endregion
    }
}

