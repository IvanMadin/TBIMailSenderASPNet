using EmailManager.Data.Entities.BaseProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Entities
{
    public class LoanApplication : ModifierEntityProperties
    {
        //TODO: Have to add one more property or enum we will see to check if the application is approved or rejected. Both situations it is going to Closed!
        public string Id { get; set; }
        public decimal Amount { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string ClientDataId { get; set; }
        public ClientData ClientData { get; set; }

        public string EmailId { get; set; }
        public ClientEmail Email { get; set; }

        public string StatusApplicationId { get; set; }
        public StatusApplication StatusApplication { get; set; }
    }
}
