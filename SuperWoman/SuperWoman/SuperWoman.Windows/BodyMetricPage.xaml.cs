using SuperWoman.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace SuperWoman
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BodyMetricPage : Page
    {
        BodyMetrics metric = new BodyMetrics();
        //Grid.DataContext = metric;
       // GridView.DataContext = BodyMetrics;
        DataTransferManager myDataManager =
        DataTransferManager.GetForCurrentView();
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }


        public BodyMetricPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;
        }



        private async void BtnBMI_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBXheight.Text) && (!string.IsNullOrEmpty(txtBXweight.Text)))
            {
                Button curr = (Button)sender;
                Canvas Currcvs = FindName(curr.Tag.ToString()) as Canvas;
                try
                {
                    if (Currcvs.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                    {
                        Leancvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;//collapse other canvses
                        BodyFatcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        setZIndexForAllChildren();
                        disappearEveryone();
                        Currcvs.SetValue(Canvas.ZIndexProperty, 10);
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        resultBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        resultBlck.Text = metric.BMIresult.ToString();
                        await ReadFile();
                    }
                    else // collapse as it is already visible.
                    {
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
                catch
                {
                }
            }
            else
            {
                ErrorBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                ErrorBlck.Text = "Please enter your weight and height";
            }
        }


        private async void BtnBodyFat_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBXheight.Text) && (!string.IsNullOrEmpty(txtBXweight.Text) && (!string.IsNullOrEmpty(txtBXage.Text))))
            {//adult body fat % = (1.20 × BMI) + (0.23 × Age) − (10.8 × 0) − 5.4
                Button curr = (Button)sender;
                Canvas Currcvs = FindName(curr.Tag.ToString()) as Canvas;
                try
                {
                    if (Currcvs.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                    {
                        BMIcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Leancvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        setZIndexForAllChildren();
                        disappearEveryone();
                        Currcvs.SetValue(Canvas.ZIndexProperty, 10);
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        BFresultBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        BFresultBlck.Text = metric.bodyFat.ToString();
                        BFtxtDisplayBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        await ReadFileBodyFat();
                    }
                    else // collapse as it is already visible.
                    {
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
                catch
                {
                }
            }
            else
            {
                if (ErrorBlck.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                {
                    ErrorBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    ErrorBlck.Text = "Please enter your weight and height and Age";
                }
            }
        }
        private async void BtnLeanMass_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button curr = (Button)sender;
            Canvas Currcvs = FindName(curr.Tag.ToString()) as Canvas;
            if (!string.IsNullOrEmpty(txtBXheight.Text) && (!string.IsNullOrEmpty(txtBXweight.Text) && (!string.IsNullOrEmpty(txtBXage.Text))))
            {
                try
                {
                    if (Currcvs.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                    {
                        BMIcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        BodyFatcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        setZIndexForAllChildren();
                        disappearEveryone();
                        Currcvs.SetValue(Canvas.ZIndexProperty, 10);
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        LeanResultBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        LeanResultBlck.Text = metric.LeanBodyMass.ToString();
                        LeantxtDisplayBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        await ReadFileLean();
                    }
                    else // collapse as it is already visible.
                    {
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
                catch
                {
                }
            }
            else
            {
                if (ErrorBlck.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                {
                    ErrorBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    ErrorBlck.Text = "Please enter your weight and height and Age";
                }
            }
        }

        private async void BtnHipToWaist_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button curr = (Button)sender;
            Canvas Currcvs = FindName(curr.Tag.ToString()) as Canvas;
            if (!string.IsNullOrEmpty(txtBXwaist.Text) && (!string.IsNullOrEmpty(txtBXhips.Text)))
            {
                 try
                {
                    if (Currcvs.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                    {
                        BMIcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        BodyFatcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Leancvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        setZIndexForAllChildren();
                        disappearEveryone();
                        Currcvs.SetValue(Canvas.ZIndexProperty, 10);
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        HipresultBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        HipresultBlck.Text = metric.HipWaistRatio.ToString();
                        HiptxtDisplayBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                        await ReadFileHip();
                    }
                    else // collapse as it is already visible.
                    {
                        Currcvs.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
                 catch
                 {
                 }
            }
            else
            {
                if (ErrorBlck.Visibility == Windows.UI.Xaml.Visibility.Collapsed)
                {
                    ErrorBlck.Visibility = Windows.UI.Xaml.Visibility.Visible;
                    ErrorBlck.Text = "Please enter your waist and hip measurements";
                }
            }
        }


        private async Task ReadFile()
        {
            String fileContent;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Common/textFiles/BMI.txt"));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            fileContent = await sRead.ReadToEndAsync();
            BMItxtDisplayBlck.Text = fileContent;
        }

        private async Task ReadFileBodyFat()
        {
            String fileContent;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Common/textFiles/BodyFat.txt"));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            fileContent = await sRead.ReadToEndAsync();
            BFtxtDisplayBlck.Text = fileContent;
        }
        private async Task ReadFileWaist()
        {
            string fileContent;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Common/textFiles/WeightWaist.txt"));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            fileContent = await sRead.ReadToEndAsync();
            //txtDisplayBlck.Text = fileContent;

        }
        private async Task ReadFileHip()
        {
            String fileContent;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Common/textFiles/hipWaistRatio.txt"));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            fileContent = await sRead.ReadToEndAsync();
            HiptxtDisplayBlck.Text = fileContent;
        }
        private async Task ReadFileLean()
        {
            String fileContent;
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Common/textFiles/Lean.txt"));
            using (StreamReader sRead = new StreamReader(await file.OpenStreamForReadAsync()))
            fileContent = await sRead.ReadToEndAsync();
            LeantxtDisplayBlck.Text = fileContent;
        }

        private void setZIndexForAllChildren()
        {
            // use  a loop to find all children of the base canvas
            // and set zindex to zero
            foreach (Canvas child in cvsBaseCanvas.Children)
            {
                if (child is Canvas)
                    child.SetValue(Canvas.ZIndexProperty, 0);
            }

        }

        private void disappearEveryone()
        {
            foreach (Canvas child in cvsBaseCanvas.Children)
            {
                if (child is Canvas)
                    child.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }


        private void BtnMeasure_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Measure));
        }








        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.



        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            await ReadFile();
            //await ReadFileBodyFat();
            //await ReadFileWaist();
            //await ReadFileHip();
            myDataManager.DataRequested += myDataManager_DataRequested;
        }



        //private Task ReadFilehipWaistRatio()
        //{
        //    throw new NotImplementedException();
        //}

        private void myDataManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataPackage myData = args.Request.Data;
            myData.Properties.Title = "Hello Sharing World!";
            string localImage = "ms-appx:///Assets/selfexam.jpg";
            string htmlExample = "<p>Here is a local image: <img src=\"" + localImage + "\">.</p>";
            string htmlFormat = HtmlFormatHelper.CreateHtmlFormat(htmlExample);
            myData.SetHtmlFormat(htmlFormat);
            myData.Properties.Description = "This is the description for sharing";
            myData.SetText("Share this valuable information!");
            myData.SetWebLink(new Uri("http://www.google.com"));
            RandomAccessStreamReference streamRef =
                RandomAccessStreamReference.CreateFromUri(new Uri(localImage));
            myData.ResourceMap[localImage] = streamRef;
        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
            // Unregister the current page as a share source.

            myDataManager.DataRequested -= myDataManager_DataRequested;
        }

        #endregion

        private void BtnSave_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}




      





       


       

      

       



        


