using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using langfvn.Common;
using langfvn.Models;

namespace langfvn.Models
{
    public class UserDao
    {
        LangfvnContext db = new LangfvnContext();

        public UserDao()
        {
            LangfvnContext db = new LangfvnContext();
        }
     
        public Account GetAccountByEmail(String Email)
        {
            return db.Accounts.SingleOrDefault(x => x.Email == Email);
        }

        public Account GetAccountById(int id)
        {
            return db.Accounts.SingleOrDefault(x => x.UserID == id);
        }

        public bool Login(String userName, String pass)
        {
            var result = db.Accounts.Count(x=>x.Email == userName && x.Password == pass); 
            if(result>0)
            {
                return true;
            }
            return false;

        }

        public bool CheckEmail(String Email)
        {
            return db.Accounts.Count(x => x.Email == Email) > 0;
        }

        public bool CheckPhone(String Phone)
        {
            return db.Accounts.Count(x => x.Phone == Phone) > 0;
        }

        public bool Insert(Account newAccount)
        {
            db.Accounts.Add(newAccount);
            db.SaveChanges();
            return true;
        }

        public bool UpdateInformation(Account accountUpdate)
        {
            var account = db.Accounts.Single(a => a.UserID == accountUpdate.UserID);

            account.FullName = accountUpdate.FullName;
            account.Email = accountUpdate.Email;
            account.Address = accountUpdate.Address;
            account.Phone = accountUpdate.Phone;
            account.Unit = accountUpdate.Unit;
            db.SaveChanges();
            return true;
        }
    }
}