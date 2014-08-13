using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using JSONSample.Resources;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;

namespace JSONSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            myButton.Click += new RoutedEventHandler(myButton_Click);

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        void myButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient webClient = new WebClient();
                Uri uri = new Uri("http://docs.blackberry.com/sampledata.json");
                webClient.OpenReadCompleted += new OpenReadCompletedEventHandler(webClient_OpenReadCompleted);
                webClient.OpenReadAsync(uri);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "first error");
            }
        }

        void webClient_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ObservableCollection<RootObject>));
                ObservableCollection<RootObject> employees = ser.ReadObject(e.Result) as ObservableCollection<RootObject>;
                foreach (RootObject em in employees)
                {
                    string id = em.vehicleColor + "|" + em.vehicleType + "|" + em.fuel + "|" + em.treadType + "|";
                    //string nm = em.GetRoot.Customer.CustomerID;
                    lstEmployee.Items.Add("<=" + id + "=>");
                    foreach (var error in em.approvedOperators)
                    {
                        lstEmployee.Items.Add("**" + error.experiencePoints + "|" + error.name);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "second error" + ex.StackTrace);
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }

    public class ApprovedOperator
    {
        public string name { get; set; }
        public int experiencePoints { get; set; }
    }

    public class RootObject
    {
        public string vehicleType { get; set; }
        public string vehicleColor { get; set; }
        public string fuel { get; set; }
        public List<ApprovedOperator> approvedOperators { get; set; }
        public string treadType { get; set; }
    }

}