using Exchange.Models;
using Exchange.Models.Entities;
using Exchange.Models.ViewModels;
using Exchange.Services.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Services
{
    public class WalletService : IWalletService
    {
        public bool SaveWallet(Users user, WalletViewModel model)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    user = db.Users.Find(user.Id);
                    Members member = new Members() { Users = user };
                        foreach (var currencyId in model.SelectedCurrencyId)
                        {
                            var currency = db.Currencies.Find(currencyId);
                            member.Currencies.Add(currency);
                        }
                    Wallet wallet = new Wallet() { Cash = model.Cash, Members = member };
                    member.Wallet = wallet;
                    db.Members.Add(member);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public WalletViewModel findWallet(Users user, WalletViewModel model)
        {
            using (var db = new ApplicationDbContext())
            {
                var member = db.Members.Include("Users").FirstOrDefault(m => m.Id == user.UsersId);

            }
            return model;
        }
    }
}

// await Task.Run(() => { /* do DataContext stuff here */ })
