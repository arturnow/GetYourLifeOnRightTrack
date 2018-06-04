namespace WasterDAL.Model
{
    public class Relation : AuditableEntity<long>
    {
        public long ParentId { get; set; }

        public SocialProfile Parent { get; set; }

        public long ChildId { get; set; }

        public SocialProfile Child { get; set; }

        public RelationshipType Type { get; set; }
    }
}