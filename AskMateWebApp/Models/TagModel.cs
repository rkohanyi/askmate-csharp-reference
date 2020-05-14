using AskMate.Common.Domain;

namespace AskMate.Web.Models
{
    public class TagModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public TagModel(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
        }
    }
}
