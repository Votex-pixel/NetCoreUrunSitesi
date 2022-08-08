using DAL;

namespace BL
{
    public class AppUserManager : Repository<Entities.AppUser>
    {
        public AppUserManager(DatabaseContext context) : base(context)
        {
        }
    }
}
