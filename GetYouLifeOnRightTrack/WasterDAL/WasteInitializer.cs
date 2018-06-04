using System.Data.Entity;
using WasterDAL.Model;

namespace WasterDAL
{
    public class WasteInitializer : DropCreateDatabaseIfModelChanges<WasteContext>
    {
        //Do inicjalizacji danych testowych - ja nie potrzebuje
        protected override void Seed(WasteContext context)
        {
            var identity = new Identity
                {
                    Id = 1, Email = "artur@mail.me", Login = "arturnow", Password = "password", Salt = "123"
                };

            context.Identities.Add(identity);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}