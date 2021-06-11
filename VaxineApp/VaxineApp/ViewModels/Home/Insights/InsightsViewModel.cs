using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using VaxineApp.Models;
using VaxineApp.ViewModels.Base;
using Xamarin.Essentials;
using Entry = Microcharts.ChartEntry;

namespace VaxineApp.ViewModels.Home.Insights
{
    public class InsightsViewModel : BaseViewModel
    {
        private PieChart _femaleVsMaleChart;
        public PieChart FemaleVsMaleChart
        {
            get => _femaleVsMaleChart;
            set
            {
                _femaleVsMaleChart = value;
                RaisedPropertyChanged(nameof(FemaleVsMaleChart));
            }
        }
        public List<Entry> Entries;
        public string[] ColourValues = new string[] {
            "FFB900",  "E74856", "0078D7",   "0099BC",  "7A7574",  "767676",
            "FF8C00",  "E81123",  "0063B1",  "2D7D9A",  "5D5A58",  "4C4A48",
            "F7630C",  "EA005E",  "8E8CD8",  "00B7C3",  "68768A",  "69797E",
            "CA5010",  "C30052",  "6B69D6",  "038387",  "515C6B",  "4A5459",
            "DA3B01",  "E3008C",  "8764B8",  "00B294",  "567C73",  "647C64",
            "EF6950",  "BF0077",  "744DA9",  "018574",  "486860",  "525E54",
            "D13438",  "C239B3",  "B146C2",  "00CC6A",  "498205",  "847545"
        };
        private List<FemaleVsMaleChildModel> _femaleVsMaleData;
        public List<FemaleVsMaleChildModel> FemaleVsMaleData
        {
            get => _femaleVsMaleData;
            set
            {
                _femaleVsMaleData = value;
                RaisedPropertyChanged(nameof(FemaleVsMaleData));
            }
        }
        public InsightsViewModel()
        {
            Entries = new List<Entry>();
            FemaleVsMaleData = new List<FemaleVsMaleChildModel>();
            LoadData();
        }

        private async void LoadData()
        {
            var data = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
            int _maleChildren = 0;
            int _femaleChildren = 0;
            foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
            {
                var data2 = await DataService.Get($"Child/{item.Value.Id}");
                var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                {
                    if(item2.Value.Gender == "Female")
                    {
                        _femaleChildren++;
                    }
                    else
                    {
                        _maleChildren++;
                    }
                }
            }
            FemaleVsMaleData.Add(
                    new FemaleVsMaleChildModel
                    {
                        Indicator = "Male",
                        Counts = _maleChildren
                    }
                ) ;
            FemaleVsMaleData.Add(
                    new FemaleVsMaleChildModel
                    {
                        Indicator = "Female",
                        Counts = _femaleChildren
                    }
                );

            foreach (var item in FemaleVsMaleData)
            {
                Random rd = new Random();
                int c = rd.Next(1, 42);
                Entries.Add(new Entry(item.Counts)
                {
                    Label = item.Indicator,
                    ValueLabel = item.Counts.ToString(),
                    Color = SKColor.Parse($"#{ColourValues[c]}")
                });
            }
            FemaleVsMaleChart = new PieChart()
            {
                Entries = Entries
            };
            FemaleVsMaleChart.LabelTextSize = 40;
        }
    }
}
