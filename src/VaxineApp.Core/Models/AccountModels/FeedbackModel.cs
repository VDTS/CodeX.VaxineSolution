using System.Collections.ObjectModel;

namespace VaxineApp.Core.Models.AccountModels
{
    public class FeedbackModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Collection<string> Labels { get; set; }
    }
}
