using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAppMVC.Models
{
    public class Models
    {
        public class LoginModel
        {
            public String Username { get; set; }
            public String Password { get; set; }
        }

        public class UserModel
        {
            public String Name { get; set; }
            public String StudentID { get; set; }
            public int TokenVersion { get; set; }
        }

        public class TransactionRequest
        {
            public int BookID { get; set; }
        }

        public class NewUser
        {
            public String FullName { get; set; }
            public String Username { get; set; }
            public String Password { get; set; }
            public int UserTypeInt { get; set; }
        }

        public class SearchRequest
        {
            public int BookID { get; set; }
            public String Author { get; set; }
            public String Title { get; set; }
            public String Category { get; set; }

        }
    }
}
