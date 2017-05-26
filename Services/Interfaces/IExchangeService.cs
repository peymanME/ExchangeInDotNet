using Exchange.Models.Entities;
using Exchange.Models.Entities.Virtual;

namespace Exchange.Services.Interfaces
{
    interface IExchangeService
    {
        CurrenciesPrice GetResponseText();
        CurrenciesPrice GetUserCurrencies(Users user, CurrenciesPrice currenciesPrice);

        Members getMember(Users user);
    }
}
