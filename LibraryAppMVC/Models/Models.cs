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

        public class ReturnModel
        {
            public String Msg { get; set; }
        }

        public class NewBook
        {
            public String Title { get; set; } = "No title";
            public int PageCount { get; set; } = 0;
            public List<int> AuthorIDs { get; set; } = new List<int>();
            public String ISBN { get; set; } = "000-0000000000";
            public String Authors { get; set; }
            public String FicID { get; set; }
            public String DeweyDecimal { get; set; }
            public String Description { get; set; } = "No description";
            public String ImagePath { get; set; } = "noimage.bmp";
        }

        public class NewAuthor
        {
            public String Name { get; set; }
            public String AuthorType { get; set; }
        }
    }
}
