using AskMateCommon.Domain;

namespace AskMateWebApp.Models
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
