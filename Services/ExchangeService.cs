using Exchange.Models.Entities;
using System;
using System.Net;
using System.Linq;
using System.IO;
using System.Web.Script.Serialization;
using Exchange.Models.Entities.Virtual;
using Exchange.Services.Interfaces;

namespace Exchange.Services
{
    public class ExchangeService : IExchangeService
    {
        public CurrenciesPrice GetResponseText()
        {
            var url = "http://webtask.future-processing.com:8068/currencies";
            string responseText = String.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                responseText = sr.ReadToEnd();
            }
            return jsonConvertToStruct(responseText);
        }

        private CurrenciesPrice jsonConvertToStruct(string jsonString)
        {
            var json_serializer = new JavaScriptSerializer();
            return json_serializer.Deserialize<CurrenciesPrice>(jsonString);
        }

        public Members getMember(Users user)
        {
            Members member = new Members();
            using (var db = new Models.ApplicationDbContext())
            {
               member = db.Members
                    .Include("Currencies")
                    .Include("Wallet")
                    .Include("LogsSales")
                    .First(u => u.Users.Id == user.Id);
            }

            return member;

        }

        public CurrenciesPrice GetUserCurrencies(Users user, CurrenciesPrice currenciesPrice)
        {
            var member = getMember(user);

            var currencies = member.Currencies;
            currenciesPrice.cash = member.Wallet.Cash;
            var items = currenciesPrice.Items.Join(
                currencies,
                i=>i.code , 
                c=>c.Code,
                (i,c)=> i
                );
            currenciesPrice.Items = items;
            return currenciesPrice;
        }


    }
}