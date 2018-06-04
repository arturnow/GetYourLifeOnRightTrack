namespace WasterDAL.Model
{
    public class Identity : AuditableEntity<int>// BaseEntity<int>
    {

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        //TODO Pattern and TrackRecord collections
    }
}