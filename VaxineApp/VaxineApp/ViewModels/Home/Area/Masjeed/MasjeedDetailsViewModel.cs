using System;
using System.Collections.Generic;
using System.Text;
using VaxineApp.Models;

namespace VaxineApp.ViewModels.Home.Area.Masjeed
{
    public class MasjeedDetailsViewModel
    {
        public MasjeedModel Masjeed { get; }
        public MasjeedDetailsViewModel(MasjeedModel masjeed)
        {
            Masjeed = masjeed;
        }

    }
}
