using System.Collections.ObjectModel;

namespace DataAccessLib.Models
{
    public class FeedbackModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Collection<string> Labels { get; set; }
    }
}
