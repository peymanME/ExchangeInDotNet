using Exchange.Controllers.Base;
using Exchange.Models.Entities;
using Exchange.Models.Entities.Virtual;
using Exchange.Models.ViewModels;
using Exchange.Services;
using Exchange.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Exchange.Controllers
{
    public class ExchangeController : BaseController
    {
        private IExchangeService _exchangeService;

        public ExchangeController()
        {
            _exchangeService = new ExchangeService();
        }

        // GET: Exchange
        [Authorize]
        public async Task<ActionResult> Index()
        {
            ExchangeViewModel exchangeViewModel = new ExchangeViewModel();
            CurrenciesPrice currenciesPrice = _exchangeService.GetResponseText();
            Users user = await UserManager.FindByNameAsync(User.Identity.Name);
            exchangeViewModel.currenciesPrice = _exchangeService.GetUserCurrencies(user, currenciesPrice);
            exchangeViewModel.FullNameUser = user.getFullName();
            Session["WalletContent"] = exchangeViewModel;

           return PartialView(exchangeViewModel);
        }

        [Authorize]
        public ActionResult Currencies()
        {
            return PartialView("~/Views/Exchange/Currencies.cshtml");
        }

        [Authorize]
        public ActionResult MyCurrencies()
        {
            return PartialView("~/Views/Exchange/MyCurrencies.cshtml");
        }
    }

    
}