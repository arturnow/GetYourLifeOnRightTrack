namespace WasterDAL.Model
{
    public class UserInGroup : AuditableEntity<long>
    {
        public SocialProfile User { get; set; }

        public long UserId { get; set; }

        public Group Group { get; set; }

        public long GroupId { get; set; }

        public UserInGroupType Type { get; set; }
    }
}