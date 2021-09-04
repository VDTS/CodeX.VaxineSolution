using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VaxineApp.Models;
using VaxineApp.MVVMHelper;
using VaxineApp.StaticData;
using VaxineApp.Views.Home.Family;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using VaxineApp.AndroidNativeApi;

namespace VaxineApp.ViewModels.Home.Family
{
    public class FamilyListViewModel : ViewModelBase, IDataCrud, IVMUtils
    {


        private ObservableCollection<FamilyModel> families;
        public ObservableCollection<FamilyModel> Families
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

        private FamilyModel selectedFamily;
        public FamilyModel SelectedFamily
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
        public ICommand CancelSelectionCommand { private set; get; }
        public ICommand PullRefreshCommand { private set; get; }
        public ICommand SaveAsPDFCommand { private set; get; }

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
                var data = JsonConvert.DeserializeObject<Dictionary<string, FamilyModel>>(jData);
                foreach (KeyValuePair<string, FamilyModel> item in data)
                {
                    StaticDataStore.FamilyNumbers.Add(item.Value.HouseNo);
                    Families.Add(
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
                StaticDataStore.Families = Families;
                StaticDataStore.TeamStats.TotalHouseholds = Families.Count;
            }
        }

        public void Put()
        {
            throw new NotImplementedException();
        }

        public void Post()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            Families.Clear();
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
