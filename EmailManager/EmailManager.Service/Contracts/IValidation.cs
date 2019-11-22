using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Contracts
{
    public interface IValidation
    {
        void IsNameInRange(string name);
        void IsNameNullOrEmpty(string name);
        void IsEGNInRange(string egn);
        void IsEGNNullOrEmpty(string egn);
        void IsPhoneInRange(string phone);
        void IsPhoneNullOrEmpty(string phone);
    }
}
