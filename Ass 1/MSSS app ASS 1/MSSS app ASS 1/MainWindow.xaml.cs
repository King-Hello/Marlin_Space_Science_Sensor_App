using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
        /// while the calling code argument is the linkedlist name. 
        /// The method code must follow the pseudo code supplied below in the Appendix. 
        /// The return type is Boolean.
        /// </4.7>
        private bool SelectionSort(LinkedList<double> values)
        {
            bool ifsorted = false;
            if (values.Count == 400)
            {
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
                    ifsorted = true;

                }
                return ifsorted;
            }
            else
            {
                return ifsorted;
            }
        }

        /// <4.8>
        /// 4Create a method called “InsertionSort” which has a single parameter of type LinkedList, 
        /// while the calling code argument is the linkedlist name. 
        /// The method code must follow the pseudo code supplied below in the Appendix. 
        /// The return type is Boolean.
        /// </4.8>
        private bool InsertionSort(LinkedList<double> values)
        {
            bool ifSorted = false;
            if (values.Count == 400)
            {
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
                            ifSorted = true;
                        }
                    }
                }
                return ifSorted;
            }
            else
            {
                return ifSorted;
            }
        }

        /// <4.9>
        /// Create a method called “BinarySearchIterative” which has the following four parameters: LinkedList, SearchValue, Minimum and Maximum. 
        /// This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value. 
        /// The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list. 
        /// The method code must follow the pseudo code supplied below in the Appendix.
        /// </4.9>
        private int BinarySearchIterative(LinkedList<double> LinkedList, int SearchValue, int Minimum, int Maxium)
        {
            while (Minimum <= Maxium - 1)
            {
                int middle = (Minimum + Maxium) / 2;
                if (SearchValue == LinkedList.ElementAt(middle))
                {
                    return ++middle;
                }
                else if (SearchValue < LinkedList.ElementAt(middle))
                {
                    Maxium = middle - 1;
                }
                else
                {
                    Minimum = middle + 1;
                }
            }
            return Minimum;

        }

        /// <4.10>
        /// Create a method called “BinarySearchRecursive” which has the following four parameters: 
        /// LinkedList, SearchValue, Minimum and Maximum.
        /// This method will return an integer of the linkedlist element from a successful search or the nearest neighbour value. 
        /// The calling code argument is the linkedlist name, search value, minimum list size and the number of nodes in the list. 
        /// The method code must follow the pseudo code supplied below in the Appendix.
        /// </4.10>
        private int BinarySearchRecursive(LinkedList<double> LinkedList, int SearchValue, int Minimum, int Maxium)
        {
            if (Minimum <= Maxium - 1)
            {
                int middle = (Minimum + Maxium) / 2;
                if (SearchValue == LinkedList.ElementAt(middle))
                {
                    return middle;
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

        #region UI Button Methods (questions 4.11 - 4.14)
        /// <4.11>
        /// Create button click methods that will search the LinkedList for an integer value entered into a textbox on the form. 
        /// The four methods are:
        /// 1.	Method for Sensor A and Binary Search Iterative
        /// 2.	Method for Sensor A and Binary Search Recursive
        /// 3.	Method for Sensor B and Binary Search Iterative
        /// 4.	Method for Sensor B and Binary Search Recursive
        /// The search code must check to ensure the data is sorted, then start a stopwatch before calling the search method.
        /// Once the search is complete the stopwatch will stop, and the number of ticks will be displayed in a read only textbox.
        /// Finally, the code/method will call the “DisplayListboxData” method and highlight the search target number and two values on each side.
        /// </4.11>>

        private void HighLightSelectedItems(int foundAt, ListBox listBox, LinkedList<double> linikedlist)
        {
            if (foundAt != -1)
            {
                listBox.SelectedItems.Clear();

                int startIndex = Math.Max(0, foundAt - 2);
                int endIndex = Math.Min(linikedlist.Count - 1, foundAt + 2);

                if (endIndex - startIndex < 4)
                {
                    if (startIndex == 0)
                    {
                        endIndex = Math.Min(sensorA.Count - 1, startIndex + 4);
                    }
                    else if (endIndex == linikedlist.Count - 1)
                    {
                        startIndex = Math.Max(0, endIndex - 1);
                    }
                }

                for (int i = startIndex; i <= endIndex; i++)
                {
                    listBox.SelectedItems.Add(listBox.Items[i]);
                }

                listBox.ScrollIntoView(listBox.SelectedItems[0]);
            }
        }

        private void Iterative_SensorA(object sender, RoutedEventArgs e)
        {
            if (SelectionSort(sensorA))
            {
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    int foundAt = BinarySearchIterative(sensorA, Convert.ToInt32(Search_TextBox_SensorA.Text), 0, 400);
                    stopwatch.Stop();
                    BS_Iterative_Timer_sensorA.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();
                    HighLightSelectedItems(foundAt, SensorA_Listbox, sensorA);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please sort the list first");
            }
        }


        private void Iterative_SensorB(object sender, RoutedEventArgs e)
        {
            if (SelectionSort(sensorB))
            {
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    int foundAt = BinarySearchIterative(sensorB, Convert.ToInt32(Search_TextBox_SensorB.Text), 0, 400);
                    stopwatch.Stop();
                    BS_Iterative_Timer_sensorB.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();
                    HighLightSelectedItems(foundAt, SensorB_Listbox, sensorB);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please sort the list first");
            }
        }


        private void Recursive_SensorA(object sender, RoutedEventArgs e)
        {
            if (SelectionSort(sensorA))
            {
                try
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    int foundAt = BinarySearchRecursive(sensorA, Convert.ToInt32(Search_TextBox_SensorA.Text), 0, 400);
                    stopwatch.Stop();
                    BS_Iterative_Timer_sensorA.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();

                    HighLightSelectedItems(foundAt, SensorA_Listbox, sensorA);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
            else
            {
                MessageBox.Show("Please sort the list first");
            }
        }
        private void Recursive_SensorB(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            if (SelectionSort(sensorB))
            {
                try
                {
                    stopwatch.Start();
                    int foundAt = BinarySearchRecursive(sensorB, Convert.ToInt32(Search_TextBox_SensorB.Text), 0, 400);
                    stopwatch.Stop();
                    BS_Recursive_Timer_sensorB.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();

                    HighLightSelectedItems(foundAt, SensorB_Listbox, sensorA);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please sort the list first");
            }
        }

        /// <4.12>
        /// Create button click methods that will sort the LinkedList using the Selection and Insertion methods. The four methods are:
        /// 1.	Method for Sensor A and Selection Sort
        /// 2.	Method for Sensor A and Insertion Sort
        /// 3.	Method for Sensor B and Selection Sort
        /// 4.	Method for Sensor B and Insertion Sort
        /// The button method must start a stopwatch before calling the sort method.
        /// Once the sort is complete the stopwatch will stop, and the number of milliseconds will be displayed in a read only textbox.
        /// Finally, the code/method will call the “ShowAllSensorData” method and “DisplayListboxData” for the appropriate sensor.

        /// </4.12>>
        private void Selection_SensorA(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SelectionSort(sensorA);
            stopwatch.Stop();
            Selection_Sort_timer_SensorA.Clear();
            Selection_Sort_timer_SensorA.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();
            ShowAllSensorData();
            DisplayListboxData(sensorA, SensorA_Listbox);
        }

        private void Selection_SensorB(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SelectionSort(sensorB);
            stopwatch.Stop();
            Selection_Sort_timer_SensorB.Clear();
            Selection_Sort_timer_SensorB.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();
            ShowAllSensorData();
            DisplayListboxData(sensorB, SensorB_Listbox);
        }

        private void Insertion_SensorA(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            InsertionSort(sensorA);
            stopwatch.Stop();
            Insertion_Sort_timer_SensorA.Clear();
            Insertion_Sort_timer_SensorA.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();
            ShowAllSensorData();
            DisplayListboxData(sensorA, SensorA_Listbox);
        }

        private void Insertion_SensorB(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            InsertionSort(sensorB);
            stopwatch.Stop();
            Insertion_Sort_timer_SensorB.Clear();
            Insertion_Sort_timer_SensorB.Text = stopwatch.Elapsed.TotalMilliseconds.ToString();
            ShowAllSensorData();
            DisplayListboxData(sensorB, SensorB_Listbox);
        }


        /// <4.14>
        /// Add textboxes for the search value; one for each sensor, ensure only numeric integer values can be entered.
        /// </4.14>>
        private void Number_Validation_TextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        
        #endregion
        
    }
}