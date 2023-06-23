namespace Diplomna.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string GroupNumber { get; set; }

        public List<User> Students { get; set; }
    }
}
