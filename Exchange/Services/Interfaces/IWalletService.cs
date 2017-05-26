using Exchange.Models.Entities;
using Exchange.Models.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Exchange.Services.Interfaces
{
    interface IWalletService
    {
        bool SaveWallet(Users user, WalletViewModel model);
        WalletViewModel findWallet(Users user, WalletViewModel model);
    }
}
