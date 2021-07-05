using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;
using Entry = Microcharts.ChartEntry;

namespace VaxineApp.ViewModels.Home.Insights
{
    public class InsightsViewModel : ViewModelBase
    {
        // Property
        private PieChart femaleVsMaleChart;
        public PieChart FemaleVsMaleChart
        {
            get
            {
                return femaleVsMaleChart;
            }
            set
            {
                femaleVsMaleChart = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Entry> entries;
        public ObservableCollection<Entry> Entries
        {
            get
            {
                return entries;
            }
            set
            {
                entries = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<FemaleVsMaleChildModel> femaleVsMaleData;
        public ObservableCollection<FemaleVsMaleChildModel> FemaleVsMaleData
        {
            get
            {
                return femaleVsMaleData;
            }
            set
            {
                femaleVsMaleData = value;
                OnPropertyChanged();
            }
        }

        public string[] ColourValues = new string[] {
            "FFB900",  "E74856", "0078D7",   "0099BC",  "7A7574",  "767676",
            "FF8C00",  "E81123",  "0063B1",  "2D7D9A",  "5D5A58",  "4C4A48",
            "F7630C",  "EA005E",  "8E8CD8",  "00B7C3",  "68768A",  "69797E",
            "CA5010",  "C30052",  "6B69D6",  "038387",  "515C6B",  "4A5459",
            "DA3B01",  "E3008C",  "8764B8",  "00B294",  "567C73",  "647C64",
            "EF6950",  "BF0077",  "744DA9",  "018574",  "486860",  "525E54",
            "D13438",  "C239B3",  "B146C2",  "00CC6A",  "498205",  "847545"
        };

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }


        // Command
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        // ctor
        public InsightsViewModel()
        {
            // Property
            Entries = new ObservableCollection<Entry>();
            FemaleVsMaleData = new ObservableCollection<FemaleVsMaleChildModel>();
            Entries = new ObservableCollection<Entry>();

            // Get
            Get();

            // Command
            SaveAsPDFCommand = new Command(SaveAsPDF);
            PullRefreshCommand = new Command(Refresh);
        }

        private async void SaveAsPDF(object obj)
        {
            StandardMessagesDisplay.FeatureUnderConstructionTitleDisplayMessage();
        }

        private async void Get()
        {
            int _maleChildren = 0;
            int _femaleChildren = 0;
            var data = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");
            if (data != "null" && data != "Error")
            {
                var clinic = JsonConvert.DeserializeObject<Dictionary<string, GetFamilyModel>>(data);
                foreach (KeyValuePair<string, GetFamilyModel> item in clinic)
                {
                    var data2 = await DataService.Get($"Child/{item.Value.Id}");
                    if (data2 != "null" && data2 != "Error")
                    {
                        var clinic2 = JsonConvert.DeserializeObject<Dictionary<string, ChildModel>>(data2);
                        foreach (KeyValuePair<string, ChildModel> item2 in clinic2)
                        {
                            if (item2.Value.Gender == "Female")
                            {
                                _femaleChildren++;
                            }
                            else
                            {
                                _maleChildren++;
                            }
                        }
                    }
                }
            }
            else
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            FemaleVsMaleData.Add(
                    new FemaleVsMaleChildModel
                    {
                        Indicator = "Male",
                        Counts = _maleChildren
                    }
                );
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

        public async void Refresh()
        {
            IsBusy = true;

            await Task.Delay(2000);
            Clear();
            Get();

            IsBusy = false;
        }

        public void Clear()
        {
            FemaleVsMaleData.Clear();
            Entries.Clear();
            FemaleVsMaleChart = null;
        }
    }
}
