using System;

namespace WasterDAL.Model
{
    //TODO: Zaimplementować dla wzorców
    public interface ISoftDeleteEntity<T> where T: struct
    {
        bool IsDeleted { get; set; }
    }

    public interface IDeleteableEntity<T> where T: struct
    {
        
    }

    public class BaseEntity<T> where T : struct
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }

    public class AuditableEntity<T> : BaseEntity<T>
        where T : struct
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    }
}