using SuperWoman.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class DepressPage : Page
    {
        public int count = 0;

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


        public DepressPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
           
        }
     
        //if radio button is in column 1 = 1point col2 2pt col3 3pt col4 4pt and 5 5pt
        //10-15 =low
        //16-29 = moderate
        //30-44 =high
        //45 - 50 severe

        
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
            
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
             
           CheckBox curr = (CheckBox)sender;
           CheckBox CurrCheckBox = FindName(curr.Tag.ToString()) as CheckBox;
           try
           {
              
               count += 1;
           }
            catch{}

        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            //CheckBox CurrCheckBox = new CheckBox(); 
            CheckBox curr = (CheckBox)sender;
           CheckBox Curr = FindName(curr.Tag.ToString()) as CheckBox;              
               count += 2;
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            CheckBox curr = (CheckBox)sender;
            CheckBox CurrCheckBox = FindName(curr.Tag.ToString()) as CheckBox;
            count += 3;

        }
        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            CheckBox curr = (CheckBox)sender;
            CheckBox CurrCheckBox = FindName(curr.Tag.ToString()) as CheckBox;
          
                count += 4;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if (count > 0 && count < 50)
            {
                this.Frame.Navigate(typeof(SurveyResultsPage));
            }
            else
            {
                txtBoxError.Text = "Please fill in the Qustionnaire ";
            }
        }

        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
            CheckBox curr = (CheckBox)sender;
            CheckBox CurrCheckBox = FindName(curr.Tag.ToString()) as CheckBox;
            count += 3;
          

        }

     }
}
