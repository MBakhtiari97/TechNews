using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

using TechNews.Utility.Generators;
using TechNews.Utility.Security;

namespace TechNews.Core.Services
{
    public class UserRepository : IUserRepository
    {

        #region Injection

        private TechNewsContext _context;

        public UserRepository(TechNewsContext context)
        {
            _context = context;
        }

        #endregion

        public bool ActivateUser(string email, string identifier)
        {
            var user = _context.Users.SingleOrDefault(u => u.EmailAddress == email);
            if (user == null)
            {
                return false;
            }
            else
            {
                if (user.IdentifierCode == identifier)
                {
                    user.IsActive = true;
                    user.IdentifierCode = StringGenerator.GenerateUniqueString();
                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }
               
            }

            return true;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users
                .SingleOrDefault(u => u.EmailAddress == email);
        }

        public User GetUserByIdentifierCode(string identifierCode, string emailAddress)
        {
            return _context.Users.SingleOrDefault(u =>
                u.EmailAddress == emailAddress && u.IdentifierCode == identifierCode);
        }

        public bool ChangeUserPassword(ChangePasswordViewModel newPassword)
        {
            //Hashing old password that user input
            newPassword.OldPassword = PasswordHelper.EncodePasswordMd5(newPassword.OldPassword);

            //Checking if existed user with these credentials or not
            if (_context.Users.Any(u => u.UserId == newPassword.UserId && u.Password == newPassword.OldPassword))
            {
                //Hashing new password for saving in database
                newPassword.NewPassword = PasswordHelper.EncodePasswordMd5(newPassword.NewPassword);

                //getting user for changing password and changing password
                var user = _context.Users.Single(u =>
                    u.UserId == newPassword.UserId && u.Password == newPassword.OldPassword);
                user.Password = newPassword.NewPassword;

                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUserByUserId(int userId)
        {
            return _context.Users.AsNoTracking().SingleOrDefault(u=>u.UserId==userId);
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return true;
        }

        public User GetUserForLogin(string email, string password)
        {
            return _context.Users.SingleOrDefault(u => u.EmailAddress == email && u.Password == password);
        }

        public bool IsEmailExisted(string email)
        {
            if (_context.Users.Any(u => u.EmailAddress == email))
                return false;


            return true;

        }

        public void RegisterUser(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
