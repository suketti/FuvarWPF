using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Ride> rides = new List<Ride>();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            InitializeComboBox();
        }


        public void LoadData()
        {
            StreamReader sr = new StreamReader("fuvar.csv");
            string line = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                var ls = line.Split(';');
                rides.Add(new Ride(Convert.ToInt32(ls[0]), Convert.ToDateTime(ls[1]), Convert.ToInt32(ls[2]), float.Parse(ls[3]), float.Parse(ls[4]), float.Parse(ls[5]), ls[6]));
            }

            rides = rides.OrderBy(x => x.TaxiId).ToList();
            sr.Close();
        }

        public int CountRides()
        {
            return rides.Count();
        }

        private void InitializeComboBox()
        {
           List<Ride> ridesDistinct = rides.DistinctBy(x => x.TaxiId).ToList();
           foreach (var ride in ridesDistinct) {
                cbTaxi.Items.Add(ride.TaxiId);
           }
        }


        private void Task3_Click(object sender, RoutedEventArgs e)
        {
           MessageBox.Show($"{CountRides().ToString()} utazas kerult feljegyezsre az allomanyban");
        }

        private void Task4_Click(object sender, RoutedEventArgs e)
        {
            float income = 0;
            int rideCount = 0;
            int taxiID = 0;
            foreach (var r in rides)
            {
                if (r.TaxiId == rides[cbTaxi.SelectedIndex].TaxiId)
                {
                    taxiID = r.TaxiId;
                    income += r.DeliveryCost;
                    rideCount++;
                }
            }

            MessageBox.Show($"A {taxiID} szamu taxisnak a bevetele {income} volt {rideCount} fuvarbol");
        }

        private void Task5_Click(object sender, RoutedEventArgs e)
        {
            int cashTransactions = rides.FindAll(x => x.PaymentInfo == "készpénz").Count();
            int cardTransactions = rides.FindAll(x => x.PaymentInfo == "bankkártya").Count();
            int disputedTransactions = rides.FindAll(x => x.PaymentInfo == "vitatott").Count();
            int freeTransactions = rides.FindAll(x => x.PaymentInfo == "ingyenes").Count();
            int typeUnknownTransactions = rides.FindAll(x => x.PaymentInfo == "ismeretlen").Count();

            MessageBox.Show($"Keszpenzes tranzakciok szama: {cashTransactions}, kartyas tranzakciok szama: {cardTransactions}, vitatott tranzakciok szama: {disputedTransactions}, ingyenes fuvarok szama: {freeTransactions}, ismeretlen fuvarok szama: {typeUnknownTransactions}");
        }

        private void Task6_Click(object sender, RoutedEventArgs e)
        {
            double allMiles = 0;

            foreach (var m in rides)
            {
                allMiles += m.Distance;
            }

            double allKilometers = allMiles * 1.6;
            allKilometers = Math.Round(allKilometers, 2);

            MessageBox.Show($"Osszes megtett kilometer: {allKilometers}");
            
        }

        private void Task7_Click(object sender, RoutedEventArgs e)
        {
            Ride ride = rides.OrderByDescending(x => x.RideTime).ToList()[0];
            MessageBox.Show($"Leghosszabb fuvar: \n Fuvar hossza: {ride.RideTime} masodperc\n Taxi azonosito: {ride.TaxiId} \n Megtett tavolsag: {Math.Round(ride.Distance * 1.6, 1)}, Viteldij: {ride.DeliveryCost}$");
        }

        private void Task8_Click(object sender, RoutedEventArgs e)
        {
            List<Ride> invalidRides = new List<Ride>();

            foreach (var r in rides)
            {
                if (r.RideTime > 0 && r.DeliveryCost > 0 && r.Distance == 0)
                {
                    invalidRides.Append(r);
                }
            }

            invalidRides = invalidRides.OrderBy(x => x.StartTime).ToList();

            StreamWriter sw = new StreamWriter("hibak.txt");
            sw.WriteLine("taxi_id;indulás,időtartam;távo1sag;viteldíj;borravaló;fizetés-módja");
            foreach (var r in invalidRides)
            {
                sw.WriteLine($"{r.TaxiId},{r.StartTime},{r.Distance}, {r.DeliveryCost},{r.Tip}, {r.PaymentInfo}");
            }
            sw.Close();
        }
    }
}
