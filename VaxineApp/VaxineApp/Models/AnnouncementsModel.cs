using System;
using System.Collections.Generic;
using System.Text;
using Xam.Forms.Markdown;

namespace VaxineApp.Models
{
    public class AnnouncementsModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }
        public string FIdDate { get; set; }
        public string ActiveTill { get; set; }
    }
}
