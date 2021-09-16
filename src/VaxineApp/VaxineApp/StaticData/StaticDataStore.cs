using System.Collections.Generic;
using System.Collections.ObjectModel;
using VaxineApp.Models;

namespace VaxineApp.StaticData
{
    public static class StaticDataStore
    {
        public static List<int> FamilyNumbers { get; set; } = new List<int>();

        public static ObservableCollection<FamilyModel> Families { get; set; }
        public static TeamModel TeamStats { get; set; } = new TeamModel();
    }
}
