using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Multithreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int queueLength = 20;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MyQueue<int> secondThreadQueue = new MyQueue<int>();

        private void StartFirstThread_Click(object sender, RoutedEventArgs e)
        {
            Thread firstThread = new Thread(GenerateNumbersByFirstThread);
            
            if (StartFirstThread.Content.ToString() == "StartFirstThread")
            {
                StartFirstThread.Content = "StopFirstThread";
                firstThread.Start();
            }
            else
            {
                StartFirstThread.Content = "StartFirstThread";
                firstThread.Abort();
            }
        }

        private void GenerateNumbersByFirstThread()
        {
            for (int i = 0; ; i++)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    FirstThreadListBox.Items.Add(i);
                    if (i >= queueLength)
                    {
                        secondThreadQueue.Dequeue();
                        SecondThreadListBox.Items.Add(i);
                        SecondThreadListBox.Items.Remove(i-20);
                        secondThreadQueue.Enqueue(i);
                    }
                    else
                    {
                       secondThreadQueue.Enqueue(i);
                       SecondThreadListBox.Items.Add(secondThreadQueue.Last);
                    }
                }));
                Thread.Sleep(1000);
            }
        }  

        private void GetOldestValueFromQueue()
        {
            for(; ; )
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    ThirdThreadListBox.Items.Add(secondThreadQueue.Last);
                }));
                Thread.Sleep(1000);
            }
        }

        private void StartThirdThread_Click(object sender, RoutedEventArgs e)
        {
            Thread thirdThread = new Thread(GetOldestValueFromQueue);
               
              
            if (StartThirdThread.Content.ToString() == "StartThirdThread")
            {
            StartThirdThread.Content = "StopThirdThread";
                thirdThread.Start();
            }
            else
            {

             StartThirdThread.Content = "StartThirdThread";
                thirdThread.Abort();
            }
        }
    }
}
