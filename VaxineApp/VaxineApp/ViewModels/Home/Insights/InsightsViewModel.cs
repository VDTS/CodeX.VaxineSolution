using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using VaxineApp.ViewModels.Base;
using Entry = Microcharts.ChartEntry;

namespace VaxineApp.ViewModels.Home.Insights
{
    public class InsightsViewModel : BaseViewModel
    {
        private BarChart _femaleVsMaleChart;
        public BarChart FemaleVsMaleChart
        {
            get => _femaleVsMaleChart;
            set
            {
                _femaleVsMaleChart = value;
                RaisedPropertyChanged(nameof(FemaleVsMaleChart));
            }
        }
        public List<Entry> Entries;
        private int c = 0;
        public string[] ColourValues = new string[] {
            "FF0000", "00FF00", "0000FF", "FFFF00", "FF00FF", "00FFFF", "000000",
            "800000", "008000", "000080", "808000", "800080", "008080", "808080",
            "C00000", "00C000", "0000C0", "C0C000", "C000C0", "00C0C0", "C0C0C0",
            "400000", "004000", "000040", "404000", "400040", "004040", "404040",
            "200000", "002000", "000020", "202000", "200020", "002020", "202020",
            "600000", "006000", "000060", "606000", "600060", "006060", "606060",
            "A00000", "00A000", "0000A0", "A0A000", "A000A0", "00A0A0", "A0A0A0",
            "E00000", "00E000", "0000E0", "E0E000", "E000E0", "00E0E0", "E0E0E0",
        };

        public class Data
        {
            public string FullName { get; set; }
            public int Counts { get; set; }
        }
        public InsightsViewModel()
        {
            Entries = new List<Entry>();
            List<Data> data = new List<Data>();
            data.Add(
                    new Data
                    {
                        FullName = "Ahmad",
                        Counts = 123
                    }
                );
            data.Add(
                new Data
                {
                    FullName = "Rafiq",
                    Counts = 143
                }
                );
            data.Add(
                    new Data
                    {
                        FullName = "Numan",
                        Counts = 453
                    }
                );
            foreach (var item in data)
            {
                Entries.Add(new Entry(item.Counts)
                {
                    Label = item.FullName,
                    ValueLabel = item.Counts.ToString(),
                    Color = SKColor.Parse($"#{ColourValues[c++]}")
                });
            }
            FemaleVsMaleChart = new BarChart()
            {
                Entries = Entries
            };
            FemaleVsMaleChart.LabelTextSize = 40;
            FemaleVsMaleChart.PointSize = 40;
        }

    }
}
