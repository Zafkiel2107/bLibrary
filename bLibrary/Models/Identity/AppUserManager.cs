using bLibrary.App_Start;
using bLibrary.DBContext;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace bLibrary.Models.Identity
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store, IIdentityMessageService emailService)
            : base(store)
        {
            this.EmailService = emailService;
        }
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            BLibraryContext db = context.Get<BLibraryContext>();
            return new AppUserManager(new UserStore<User>(db), new EmailService());
        }
    }
}
