using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Exchange.Models.Entities.Base;

namespace Exchange.Models.Entities
{
    public class Members : EntityBase
    {
        public Members()
        {
            this.Currencies = new HashSet<Currencies>();
        }

        //[Key]
        //public int User_Id { get; set; }
        public int? Parent { get; set; }

        public virtual Users Users { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<Currencies> Currencies { get; set; }
        public virtual ICollection<LogsSales> LogsSales { get; set; }
        public virtual ICollection<Members> Childeren { get; set; }
        public virtual Members Parents { get; set; }


    }
}