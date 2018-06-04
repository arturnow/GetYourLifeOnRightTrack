using System;
using WasterDAL.Model;

namespace WasterDAL.Repositories
{
    public class EFPatternRepository : BaseRepositry<Pattern, Guid>, IPatternRepository
    {
        private readonly WasteContext _context;

        private readonly string _userName;

        public EFPatternRepository(WasteContext context): base(context)
        {
        }

        //TODO: Trochę nie podoba mi sie sposób implementacji Delete. Potrzebne inne ropozytorium
        public override void Delete(Guid id)
        {
            var entity = this.Get(id);

            entity.IsEnabled = false;
            entity.UpdateDate = DateTime.Now;

            _context.SaveChanges();
        }


        protected override Guid GenerateId()
        {
            return Guid.NewGuid();
        }
    }
}