using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.AndroidNativeApi;
using VaxineApp.MobilizerShell.Views.Home.Family;
using VaxineApp.Core.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VaxineApp.MobilizerShell.ViewModels.Home.Family
{
    public class FamilyListViewModel : ViewModelBase
    {


        private ObservableCollection<FamilyModel>? families;
        public ObservableCollection<FamilyModel>? Families
        {
            get
            {
                return families;
            }
            set
            {
                families = value;
                OnPropertyChanged();
            }
        }

        private FamilyModel? selectedFamily;
        public FamilyModel? SelectedFamily
        {
            get
            {
                return selectedFamily;
            }
            set
            {
                selectedFamily = value;
                OnPropertyChanged();
            }
        }


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

        public ICommand GoToPostPageCommand { private set; get; }
        public ICommand SelectionCommand { private set; get; }
        //public ICommand CancelSelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }
        public ICommand AddFromQRCodeCommand { private set; get; }

        public FamilyListViewModel()
        {
            // Property
            Families = new ObservableCollection<FamilyModel>();

            // Get
            Get();

            // Commands
            SaveAsPDFCommand = new Command(SaveAsPDF);
            SelectionCommand = new Command(GoToDetailsPage);
            PullRefreshCommand = new Command(Refresh);
            GoToPostPageCommand = new Command(GoToPostPage);
            AddFromQRCodeCommand = new Command(AddFromQRCode);
        }

        private async void AddFromQRCode(object obj)
        {
            await Scanner();
            await Post();
        }

        string s;
        public async Task Scanner()
        {
#if __ANDROID__
// Initialize the scanner first so it can track the current context
        MobileBarcodeScanner.Initialize (Application);
#endif

            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.PossibleFormats = new List<ZXing.BarcodeFormat>() {
            ZXing.BarcodeFormat.QR_CODE, ZXing.BarcodeFormat.QR_CODE
            };

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan(options);

            if (result != null)
            {
                s = result.Text;
            }
        }

        public async Task Post()
        {
            var m = JsonConvert.DeserializeObject<FamilyWithChildrenModel>(s);
            if (m != null)
            {
                var jData = JsonConvert.SerializeObject(m.Family);

                string postResponse = await DataService.Post(jData, $"Family/{Preferences.Get("TeamId", "")}");
                if (postResponse == "ConnectionError")
                {
                    StandardMessagesDisplay.NoConnectionToast();
                }
                else if (postResponse == "Error")
                {
                    StandardMessagesDisplay.Error();
                }
                else if (postResponse == "ErrorTracked")
                {
                    StandardMessagesDisplay.ErrorTracked();
                }
                else
                {
                    _ = await DataService.Put((++StaticDataStore.TeamStats.TotalHouseholds).ToString(), $"Team/{Preferences.Get("ClusterId", "")}/{Preferences.Get("TeamFId", "")}/TotalHouseholds");
                    StandardMessagesDisplay.AddDisplayMessage($"{m.Family.ParentName}'s Family ");

                    var route = "..";
                    await Shell.Current.GoToAsync(route);
                }
            }
        }


        public async void Get()
        {
            var jData = await DataService.Get($"Family/{Preferences.Get("TeamId", "")}");


            if (jData == "ConnectionError")
            {
                StandardMessagesDisplay.NoConnectionToast();
            }
            else if (jData == "null")
            {
                StandardMessagesDisplay.NoDataDisplayMessage();
            }
            else if (jData == "Error")
            {
                StandardMessagesDisplay.Error();
            }
            else if (jData == "ErrorTracked")
            {
                StandardMessagesDisplay.ErrorTracked();
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Dictionary<string, FamilyModel>>(jData);

                    if (data != null)
                        foreach (KeyValuePair<string, FamilyModel> item in data)
                        {
                            StaticDataStore.FamilyNumbers.Add(item.Value.HouseNo);
                            Families?.Add(
                                new FamilyModel
                                {
                                    FId = item.Key.ToString(),
                                    Id = item.Value.Id,
                                    ParentName = item.Value.ParentName,
                                    PhoneNumber = item.Value.PhoneNumber,
                                    HouseNo = item.Value.HouseNo
                                }
                                );
                        }

                    if (Families != null)
                    {
                        StaticDataStore.Families = Families;
                        StaticDataStore.TeamStats.TotalHouseholds = Families.Count;
                    }
                }
                catch (Exception ex)
                {
                    Crashes.TrackError(ex);
                    StandardMessagesDisplay.InputToast(ex.Message);
                }
            }
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Families?.Clear();
        }

        public void CancelSelection()
        {
            throw new NotImplementedException();
        }

        public void SaveAsPDF()
        {
            // Synfusion.PDF for save as pdf
            //Create a new PDF document
            PdfDocument document = new PdfDocument();

            //Add a page to the document
            PdfPage page = document.Pages.Add();

            //Create PDF graphics for the page
            //PdfGraphics graphics = page.Graphics;

            //Set the standard font
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            int x = 0;
            int y = 0;

            // Syncfusion.PDF ends
            if(Families != null)
            foreach (var value in Families)
            {
                string str = $"{value.ParentName} : {value.PhoneNumber}";

                //Draw string in the PDF page
                page.Graphics.DrawString(str, font, PdfBrushes.Black, new PointF(x, y));

                y += 30;
            }

            //Save the document to the stream
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document
            document.Close(true);

            stream.Position = 0;

            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Sample.pdf", "application/pdf", stream);
        }

        public async void Refresh()
        {
            IsBusy = true;

            Clear();
            Get();
            await Task.Delay(2000);

            IsBusy = false;
        }

        public async void GoToPostPage()
        {
            var route = $"{nameof(AddFamilyPage)}";
            await Shell.Current.GoToAsync(route);
        }

        public void GoToPutPage()
        {
            throw new NotImplementedException();
        }

        public async void GoToDetailsPage()
        {
            if (SelectedFamily == null)
            {
                return;
            }
            else
            {
                var JsonSelectedFamily = JsonConvert.SerializeObject(SelectedFamily);
                var route = $"{nameof(FamilyDetailsPage)}?Family={JsonSelectedFamily}";
                await Shell.Current.GoToAsync(route);

                SelectedFamily = null;
            }
        }

        public void GoToMapPage()
        {
            throw new NotImplementedException();
        }
    }
}
