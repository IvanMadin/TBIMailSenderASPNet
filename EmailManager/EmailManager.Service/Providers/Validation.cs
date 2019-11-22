using EmailManager.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Service.Providers
{
    public class Validation : IValidation
    {
        public void IsNameInRange(string name)
        {
            if (name.Length < 3 || name.Length > 50)
            {
                throw new ArgumentException("Name must be between 3 and 50 symbols.");
            }
        }

        public void IsNameNullOrEmpty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty string.");
            }
        }

        public void IsEGNInRange(string egn)
        {
            if (egn.Length != 10)
            {
                throw new ArgumentException("EGN must be 10 symbols.");
            }
        }

        public void IsEGNNullOrEmpty(string egn)
        {
            if (string.IsNullOrEmpty(egn))
            {
                throw new ArgumentException("EGN cannot be null or empty string.");
            }
        }

        public void IsPhoneInRange(string phone)
        {
            if (phone.Length < 10 || phone.Length > 13)
            {
                throw new ArgumentException("Phone must be between 10 and 13 symbols.");
            }
        }

        public void IsPhoneNullOrEmpty(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                throw new ArgumentException("Phone cannot be null or empty string.");
            }
        }
    }
}
