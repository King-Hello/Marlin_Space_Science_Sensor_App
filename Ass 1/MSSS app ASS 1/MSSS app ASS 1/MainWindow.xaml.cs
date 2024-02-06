using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Galileo6;

namespace MSSS_app_ASS_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    class displayInfo
    {
        public double DisplaySensorA { get; set; }
        public double DisplaySensorB { get; set; }
    }

    public partial class MainWindow : Window
    {
        /// <4.1>
        ///Create two data structures using the LinkedList<T> class from the C# Systems.Collections.Generic namespace. 
        ///The data must be of type “double”; you are not permitted to use any additional classes, nodes, pointers or data 
        ///structures (array, list, etc) in the implementation of this application. The two LinkedLists of type double are 
        ///to be declared as global within the “public partial class”.
        /// </4.1>>
        LinkedList<double> sensorA = new LinkedList<double>();
        LinkedList<double> sensorB = new LinkedList<double>();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Global Methods (questions 4.1 - 4.4)
        /// <4.2>
        /// Copy the Galileo.DLL file into the root directory of your solution folder and add the appropriate reference in the solution explorer. 
        /// Create a method called “LoadData” which will populate both LinkedLists. Declare an instance of the Galileo library in the method and create 
        /// the appropriate loop construct to populate the two LinkedList; the data from Sensor A will populate the first LinkedList, 
        /// while the data from Sensor B will populate the second LinkedList. The LinkedList size will be hardcoded inside the method and must be equal to
        /// 400. The input parameters are empty, and the return type is void.
        /// </4.2>
        private void LoadData()
        {
            sensorA.Clear();
            sensorB.Clear();
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
        }
        /// <4.3>
        /// Create a custom method called “ShowAllSensorData” which will display both LinkedLists in a ListView. 
        /// Add column titles “Sensor A” and “Sensor B” to the ListView. The input parameters are empty, and the return type is void.
        /// </4.3>
        private void ShowAllSensorData()
        {
            CombinedA_B_Listview.Items.Clear();
            int index = 0;
            foreach (double sensor in sensorA)
            {
                double sensor2 = sensorB.ElementAt(index);
                CombinedA_B_Listview.Items.Add(new displayInfo { DisplaySensorA = sensor, DisplaySensorB = sensor2 });
                index++;
            }
            DisplayListboxData(sensorB, SensorB_Listbox);
            DisplayListboxData(sensorA, SensorA_Listbox);
        }

        /// <4.4>
        /// Create a button and associated click method that will call the LoadData and ShowAllSensorData methods.
        /// The input parameters are empty, and the return type is void.
        /// </4.4>
        private void Load_Button(object sender, RoutedEventArgs e)
        {
            LoadData();
            ShowAllSensorData();
        }
        #endregion

        #region Utility Methods (questions 4.5 and 4.6)
        /// <4.5>
        /// Create a method called “NumberOfNodes” that will return an integer which is the number of nodes(elements) in a LinkedList. 
        /// The method signature will have an input parameter of type LinkedList, and the calling code argument is the linkedlist name.
        /// </4.5>>
        private int NumberOfNodes(LinkedList<double> values)
        {
            return values.Count;
        }
        /// <4.6>
        /// Create a method called “DisplayListboxData” that will display the content of a LinkedList inside the appropriate ListBox. 
        /// The method signature will have two input parameters; a LinkedList, and the ListBox name.  
        /// The calling code argument is the linkedlist name and the listbox name.
        /// </4.6>>
        private void DisplayListboxData(LinkedList<double> values, ListBox listBox)
        {
            listBox.Items.Clear();
            if (listBox.Items.Count <= 0)
            {
                foreach (double sensor in values)
                {
                    listBox.Items.Add(sensor);
                }
            }
        }
        #endregion

        #region Sort and Search Methods (questions 4.7 - 4.10)
        /// <4.7>
        /// Create a method called “SelectionSort” which has a single input parameter of type LinkedList,
        /// while the calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in the Appendix. 
        /// The return type is Boolean.
        /// </4.7>
        private Boolean SelectionSort(LinkedList<double> values)
        {
            if (CheckIfAlreadySorted(values) == true)
            {
                return false;
            }

            int counter = 0;
            int min = 0;
            int max = NumberOfNodes(values);
            for (int i = 0; i < max - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < max; j++)
                {
                    if (values.ElementAt(j) < values.ElementAt(min))
                    {
                        min = j;
                    }
                }
                LinkedListNode<double> currentMin = values.Find(values.ElementAt(min));
                LinkedListNode<double> currentI = values.Find(values.ElementAt(i));
                double temp = currentMin.Value;
                currentMin.Value = currentI.Value;
                currentI.Value = temp;

            }
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (values.ElementAt(j - 1) > values.ElementAt(j))
                    {
                        counter++;
                    }
                }
            }
            if (counter == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <4.8>
        /// 4Create a method called “InsertionSort” which has a single parameter of type LinkedList, 
        /// while the calling code argument is the linkedlist name. The method code must follow the pseudo code supplied below in the Appendix. 
        /// The return type is Boolean.
        /// </4.8>
        private Boolean InsertionSort(LinkedList<double> values)
        {
            if (CheckIfAlreadySorted(values) == true)
            {
                return false;
            }

            int max = NumberOfNodes(values);

            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (values.ElementAt(j - 1) > values.ElementAt(j))
                    { 
                        LinkedListNode<double> current = values.Find(values.ElementAt(j));
                        LinkedListNode<double> currentI = values.Find(values.ElementAt(j - 1));
                        double temp = values.ElementAt(j - 1);
                        values.Remove(currentI);
                        values.AddAfter(current, temp);
                    }
                }
            }
            return true;
        }

        private Boolean CheckIfAlreadySorted(LinkedList<double> values)
        {
            int counter = 0;
            int max = NumberOfNodes(values);
            for (int i = 0; i < max - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (values.ElementAt(j - 1) > values.ElementAt(j))
                    {
                        counter++;
                    }
                }
            }

            if (counter == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <4.9>
        /// Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum. 
        /// This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value. 
        /// The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list. 
        /// The method code must follow the pseudo code supplied below in the Appendix.
        /// </4.9>
        private int BinarySearchIterative(LinkedList<double> LinkedList, double SearchValue, int Minimum, int Maxium)
        {
            while (Minimum <= Maxium - 1)
            {
                int middle = (Minimum + Maxium) / 2;
                if (SearchValue == LinkedList.ElementAt(middle))
                {
                    return ++middle;
                }
                else if (SearchValue > middle - 1)
                {
                    Maxium = middle - 1;
                }
                else
                {
                    Maxium = middle + 1;
                }
            }
            return Minimum;
        }

        /// <4.10>
        /// Create a method called “BinarySearchRecursive” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum.
        /// This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value. 
        /// The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list. 
        /// The method code must follow the pseudo code supplied below in the Appendix.
        /// </4.10>
        private int BinarySearchRecursive(LinkedList<double> LinkedList, double SearchValue, int Minimum, int Maxium)
        {
            if (Minimum <= Maxium - 1)
            {
                int middle = (Minimum + Maxium) / 2;
                if (SearchValue == LinkedList.ElementAt(middle))
                {
                    return ++middle;
                }
                else if (SearchValue < LinkedList.ElementAt(middle))
                {
                    return BinarySearchRecursive(LinkedList, SearchValue, Minimum, middle - 1);
                }
                else
                {
                    return BinarySearchRecursive(LinkedList, SearchValue, middle + 1, Maxium);
                }
            }
            return Minimum;
        }


        #endregion

        #region UI Button Methods
        private void Iterative_SensorA(object sender, RoutedEventArgs e)
        {
            int value = BinarySearchIterative(sensorA, Convert.ToDouble(Binary_Search_Iterative_sensorA.Text), 0, 400);
        }

        private void Iterative_SensorB(object sender, RoutedEventArgs e)
        {
            DisplayListboxData(sensorB, SensorB_Listbox);
            
        }

        private void Recursive_SensorA(object sender, RoutedEventArgs e)
        {
            BinarySearchRecursive(sensorA, Convert.ToDouble(Binary_Search_Iterative_sensorA.Text), 0, 400);
        }

        private void Recursive_SensorB(object sender, RoutedEventArgs e)
        {
            BinarySearchRecursive(sensorB, Convert.ToDouble(Binary_Search_Iterative_sensorA.Text), 0, 400);
        }

        private void Selection_SensorA(object sender, RoutedEventArgs e)
        {
            if (SelectionSort(sensorA) == true)
            {
                ShowAllSensorData();
                DisplayListboxData(sensorA, SensorA_Listbox);
            }
            else
            {
                MessageBox.Show("List already sorted!");
            }
        }

        private void Selection_SensorB(object sender, RoutedEventArgs e)
        {
            if (SelectionSort(sensorB) == true)
            {
                ShowAllSensorData();
                DisplayListboxData(sensorB, SensorB_Listbox);
            }
            else
            {
                MessageBox.Show("List already sorted!");
            }
        }

        private void Insertion_SensorA(object sender, RoutedEventArgs e)
        {
            if (InsertionSort(sensorA) == true)
            {
                ShowAllSensorData();
                DisplayListboxData(sensorA, SensorA_Listbox);
            }
            else
            {
                MessageBox.Show("List already sorted!");
            }
        }

        private void Insertion_SensorB(object sender, RoutedEventArgs e)
        {
            if (InsertionSort(sensorB) == true)
            {
                ShowAllSensorData();
                DisplayListboxData(sensorB, SensorB_Listbox);
            }
            else
            {
                MessageBox.Show("List already sorted!");
            }
        }

        /// <4.>
        /// 
        /// </4.>>
        #endregion

    }
}