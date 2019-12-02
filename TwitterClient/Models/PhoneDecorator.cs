using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    class PhoneDecorator : OptionalFieldsDecorator
    {
        private readonly User user;

        public PhoneDecorator(User user, string phone)
        {
            user.Phone = phone;
            this.user = user;
        }

        public override string GetBio()
        {
            return user.Bio;
        }

        public override string GetPhone()
        {
            return user.Phone;
        }

        public User GetDecoratedUser()
        {
            return user;
        }
    }
}
