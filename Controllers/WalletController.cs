using Exchange.Controllers.Base;
using Exchange.Models.ViewModels;
using Exchange.Services;
using Exchange.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Exchange.Controllers
{
    public class WalletController : BaseController
    {
        private IWalletService _walletService;

        public WalletController()
        {
            _walletService = new WalletService();
        }

        // GET: Wallet
        [Authorize]
        public ActionResult Wallet()
        {
            return PartialView();
        }

        // GET: Currencies
        [Authorize]
        public async Task<ActionResult> Currencies()
        {
            WalletViewModel walletViewModel = new WalletViewModel();
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            _walletService.findWallet(user, walletViewModel);

            return PartialView(walletViewModel);
        }

        // POST: Currencies
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> Currencies(WalletViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView(model);
            }
            var user = await UserManager.FindByNameAsync(User.Identity.Name );

            _walletService.SaveWallet(user, model);
           

            return PartialView(model);
        }

    }
}