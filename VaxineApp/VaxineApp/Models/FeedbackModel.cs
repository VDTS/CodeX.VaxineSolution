using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VaxineApp.Models
{
    public class FeedbackModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public Collection<string> Labels { get; set; }
        public FeedbackModel()
        {
            Labels = new Collection<string>();
        }
    }
}
