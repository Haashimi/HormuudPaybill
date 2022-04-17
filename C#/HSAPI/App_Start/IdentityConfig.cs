using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using HSAPI.Models;
using System.Web;
using System.Data.Entity;

namespace HSAPI
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    namespace AspNetIdentity2WebApiCustomize
    {
        public class ApplicationUserManager : UserManager<ApplicationUser, string>
        {
            public ApplicationUserManager(IUserStore<ApplicationUser, string> store)
                : base(store)
            {
            }

            public static ApplicationUserManager Create(
                IdentityFactoryOptions<ApplicationUserManager> options,
                IOwinContext context)
            {
                var manager = new ApplicationUserManager(
                    new UserStore<ApplicationUser, ApplicationRole, string,
                        ApplicationUserLogin, ApplicationUserRole,
                        ApplicationUserClaim>(context.Get<ApplicationDbContext>()));

                // Configure validation logic for usernames
                manager.UserValidator = new UserValidator<ApplicationUser>(manager)
                {
                    AllowOnlyAlphanumericUserNames = false,
                    RequireUniqueEmail = true
                };

                // Configure validation logic for passwords
                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 6,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                };
                var dataProtectionProvider = options.DataProtectionProvider;
                if (dataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<ApplicationUser>(
                            dataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }
        }


        public class ApplicationRoleManager : RoleManager<ApplicationRole>
        {
            public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
                : base(roleStore)
            {
            }

            public static ApplicationRoleManager Create(
                IdentityFactoryOptions<ApplicationRoleManager> options,
                IOwinContext context)
            {
                return new ApplicationRoleManager(
                    new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
            }
        }

        //DropCreateDatabaseAlways
        public class ApplicationDbInitializer
            : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                InitializeIdentityForEF(context);
                InitializeIdentityForUser(context);
                base.Seed(context);
            }

            //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
            public static void InitializeIdentityForEF(ApplicationDbContext db)
            {
                var userManager = HttpContext.Current
                    .GetOwinContext().GetUserManager<ApplicationUserManager>();

                var roleManager = HttpContext.Current
                    .GetOwinContext().Get<ApplicationRoleManager>();

                const string name = "admin@example.com";
                const string password = "Admin@123456";

                // Some initial values for custom properties:
                const string address = "1234 Sesame Street";
                const string city = "Portland";
                const string state = "OR";
                const string postalCode = "97209";

                const string roleName = "Admin";
                const string roleDescription = "All access pass";

                //Create Role Admin if it does not exist
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole(roleName);

                    // Set the new custom property:
                    role.Description = roleDescription;
                    var roleresult = roleManager.Create(role);
                }

                var user = userManager.FindByName(name);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = name, Email = name };

                    // Set the new custom properties:
                    user.Address = address;
                    user.City = city;
                    user.State = state;
                    user.PostalCode = postalCode;

                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var result = userManager.AddToRole(user.Id, role.Name);
                }
            }

            //Create User=online@hormuud.com with password=Somn1433# in the User role        
            public static void InitializeIdentityForUser(ApplicationDbContext db)
            {
                var userManager = HttpContext.Current
                    .GetOwinContext().GetUserManager<ApplicationUserManager>();

                var roleManager = HttpContext.Current
                    .GetOwinContext().Get<ApplicationRoleManager>();

                const string name = "online@hormuud.com";
                const string password = "Somn1433#";

                // Some initial values for custom properties:
                const string address = "1234 Sesame Street";
                const string city = "Portland";
                const string state = "OR";
                const string postalCode = "97209";

                const string roleName = "User";
                const string roleDescription = "User access pass";

                //Create Role Admin if it does not exist
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new ApplicationRole(roleName);

                    // Set the new custom property:
                    role.Description = roleDescription;
                    var roleresult = roleManager.Create(role);
                }

                var user = userManager.FindByName(name);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = name, Email = name };

                    // Set the new custom properties:
                    user.Address = address;
                    user.City = city;
                    user.State = state;
                    user.PostalCode = postalCode;

                    var result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    var result = userManager.AddToRole(user.Id, role.Name);
                }
            }

        }

        //public class ApplicationUserManager : UserManager<ApplicationUser>
        //{
        //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        //        : base(store)
        //    {
        //    }

        //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //    {
        //        var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
        //        // Configure validation logic for usernames
        //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
        //        {
        //            AllowOnlyAlphanumericUserNames = false,
        //            RequireUniqueEmail = true
        //        };
        //        // Configure validation logic for passwords
        //        manager.PasswordValidator = new PasswordValidator
        //        {
        //            RequiredLength = 6,
        //            RequireNonLetterOrDigit = true,
        //            RequireDigit = true,
        //            RequireLowercase = true,
        //            RequireUppercase = true,
        //        };
        //        var dataProtectionProvider = options.DataProtectionProvider;
        //        if (dataProtectionProvider != null)
        //        {
        //            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        //        }
        //        return manager;
        //    }
        //}
    }
}
