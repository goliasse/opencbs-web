namespace OpenCBS.Web.Model
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Id == ((EntityBase) obj).Id;
        }
    }
}
