using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.Models
{
    class BioDecorator : OptionalFieldsDecorator
    {
        private readonly User user;

        public BioDecorator(User user, string bio)
        {
            user.Bio = bio;
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
