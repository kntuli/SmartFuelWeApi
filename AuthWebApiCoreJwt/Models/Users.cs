using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebApiCoreJwt.Models
{
    public partial class Users
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Permissions { get; set; }
        public int Activated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Mobile { get; set; }

        public string RefreshToken { get; set; }
        public int Status { get; set; }
        public int RoleId { get; set; }
        public DateTime? CreatedOnDate { get; set; }
    }
}
