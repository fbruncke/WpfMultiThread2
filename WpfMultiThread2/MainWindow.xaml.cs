using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfMultiThread2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Non Anon thread method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(theMaxMethod);
            t.Start();
            Console.WriteLine(" Mainthread work");
        }

        private void theMaxMethod()
        {
            Console.WriteLine("I sit in the front row");
        }


        /// <summary>
        /// Anon thread method call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Thread t = new Thread(() =>  Console.WriteLine("I sit in the front row") );
           // t.Start();

            (new Thread(() => Console.WriteLine("I sit in the front row"))).Start();
            Console.WriteLine(" Mainthread work");



        }

        /// <summary>
        /// Anon method thread with shared variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string myVar = "I sit in the front row";
            int no = 10;
            Thread t = new Thread(() => {
                Console.WriteLine(myVar + "hashvalue: " + myVar.GetHashCode());
                myVar = "another value";
                no = 12;
            });
            t.Start();
            Thread.Sleep(500);
            Console.WriteLine("Main thread: "+ myVar + myVar.GetHashCode() + " no: " + no);
        }

        /// <summary>
        /// method thread with a parameter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string davidString = "Coding makes me happy!";
            //ParameterizedThreadStart pts = new ParameterizedThreadStart(NoahSays);
            Thread t = new Thread(NoahSays);
            t.Start(davidString);
        }

        private void NoahSays(Object parameter)
        {
            Console.WriteLine("I heard Jakub saying: " + parameter.ToString());
        }

        /// <summary>
        /// careful example!, method thread with shared variable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; ++i)
                new Thread(() => Console.WriteLine("i is: "+i)).Start();
        }



        /// <summary>
        /// Method thread with shared variable, dirty fix with lexical closure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < 10; i++)
            {
                int tmp = i;
                new Thread(() => Console.WriteLine("i is: " + tmp)).Start();
            }
        }

        



        /// <summary>
        /// Misc method thread
        /// Setting some thread properties
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(TommysNamedMethod);
            t.Name = "Mads method";  
            t.IsBackground = true;  //the default is foreground thread
            t.Priority = ThreadPriority.Highest;    //Adjusting priority can lead to stavation
            t.Start();
        }

        private void TommysNamedMethod()
        {
            Console.WriteLine("I am named: " + Thread.CurrentThread.Name );
        }
    }
}
