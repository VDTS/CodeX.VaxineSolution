using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Models
{
    public class AreaModel
    {
        public string ClusterName { get; set; }
        public string TeamNo { get; set; }
        public int SocialMobilizerId { get; set; }
        public string CHWName { get; set; }
        public int TotalHouseholds { get; set; }
        public int TotalMasjeeds { get; set; }
        public int TotalChildren { get; set; }
    }
}
