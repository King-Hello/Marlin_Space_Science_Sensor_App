using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using Galileo6;

namespace Marlin_Space_Science_Sensor_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    class displayInfo
    {
        public double DisplaySensorA { get; set; }
        public double DisplaySensorB { get; set; }
    }

    public partial class MainWindow : Window
    {
        LinkedList<double> sensorA = new LinkedList<double>();
        LinkedList<double> sensorB = new LinkedList<double>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            var test = new ReadData();
            double mu = Convert.ToDouble(Mu.Text);
            double sigma = Convert.ToDouble(Sigma.Text);
            var test2 = test.SensorA(mu, sigma);
            var test3 = test.SensorB(mu, sigma);
            while (sensorA.Count != 400) 
            {
                test2 = test.SensorA(mu, sigma);
                sensorA.AddFirst(test2);
            }

            while (sensorB.Count != 400)
            {
                test3 = test.SensorA(mu, sigma);
                sensorB.AddFirst(test3);
            }
            Display();

        }

        public void Display()
        {
            int index = 0;
            foreach (double sensor in sensorA)
            {
                double sensor2 = sensorB.ElementAt(index);
                CombinedA_B_Listview.Items.Add(new displayInfo { DisplaySensorA = sensor, DisplaySensorB = sensor2 });
                index++;
            }

            foreach (double sensor in sensorA)
            {
                SensorA_Listbox.Items.Add(sensor);
            }

            foreach (double sensor in sensorB)
            {
                SensorB_Listbox.Items.Add(sensor);
            }
        }

        private void Load_Button(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
    
}