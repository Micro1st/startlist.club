﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FlightJournal.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FlightJournal.Web.Configuration
{
    public class Demo
    {
        public struct DemoMembership
        {
            private ApplicationUser _applicationUser;
            public string RoleName;
            public string Name;
            public string Password;
            public string PhoneNumber;

            public ApplicationUser GetApplicationUser
            {
                get
                {
                    if (_applicationUser != null)
                        return _applicationUser;
                    
                    var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    _applicationUser = userManager.FindByName(Name);

                    return _applicationUser;
                }
            }

            public DemoMembership(string roleName, string name, string password, string phoneNumber)
            {
                this.RoleName = roleName;
                this.Name = name;
                this.Password = password;
                this.PhoneNumber = phoneNumber;
                this._applicationUser = null;
            }
        }

        public static List<DemoMembership> GetDemoMembershipTemplates
        {
            get
            {
                return new List<DemoMembership>
                {
                    new DemoMembership("Admin", "admin@demo.startlist.club", "admin", "+4500000000"),
                    new DemoMembership("Manager", "manager@demo.startlist.club", "manager", "+4500000001"),
                    new DemoMembership("Editor", "editor@demo.startlist.club", "editor", "+4500000002"),
                    new DemoMembership(string.Empty, "pilot@demo.startlist.club", "pilot", "+4500000003"),
                    // Anonymous account with no binding
                    new DemoMembership(string.Empty, "user-with-no-pilot@demo.startlist.club", "nopilot", "+4500000004"),
                    // Users related to another club
                    new DemoMembership("Manager", "manager-of-other-club@demo.startlist.club", "manager", "+4500000005"),
                    new DemoMembership("Editor", "editor-of-other-club@demo.startlist.club", "editor", "+4500000006"),
                    new DemoMembership(string.Empty, "pilot-of-other-club@demo.startlist.club", "pilot", "+4500000007")
                };
            }
        }

        public static List<DemoMembership> GetLiveDemoMemberships()
        {
            return GetDemoMembershipTemplates.Where(d => d.GetApplicationUser != null) as List<DemoMembership>;
        }
            
    }
}
