using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
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
using System.Windows.Threading;

namespace Drawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static char c1 = 'A';
        int ticketNum = 5001;
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PrepareData();
        }

        private async void PrepareData()
        {
            //10 x 200 = 2000 tickets
            int[,] array = new int[10, 200];
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int y = 10 * i + 1 + j;
                    array[j, i] = y;
                }
            }

            //2000 tickets divide by 4 will have 500 pages
            int[,] newArray = new int[500, 4];
            int k = 0;
            int newIndex = 0;
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < 200; i++)
                {
                    newArray[newIndex, k] = array[j, i];
                    k++;
                    if ((i + 1) % 4 == 0)
                    {
                        k = 0;
                        newIndex++;
                    }
                }
            }

            //Sorted so that when cutting 10 pages at once they are in their own pack
            //Sorted list is cool
            SortedList sl = new SortedList();

            for (int n = 0; n < newIndex; n++)
            {
                string temp = "";
                string sortedTemp = "";
                for (int m = 0; m < 4; m++)
                {
                    if (m < 3)
                        temp += newArray[n, m].ToString("d4") + ",";
                    else
                        temp += newArray[n, m].ToString("d4");
                    if (m == 0)
                        sortedTemp = newArray[n, m].ToString("d4") + "";
                }
                sl.Add(sortedTemp, temp);

                temp = "";
                sortedTemp = "";
            }

            ICollection key = sl.Keys;
            foreach (string s in key)
            {
                //Console.WriteLine(s + ": " + sl[s]);

                string myStr = sl[s].ToString();
                List<int> TagIds = myStr.Split(',').Select(int.Parse).ToList();

                foreach (int ch in TagIds)
                {
                    int NewTicketNo = ch + 5000;
                    //Console.WriteLine(ch.ToString("d4"));
                    //Print out 200 tickets at the time
                    //if (ticketNum > 6600 && ticketNum <= 7000)
                    if (NewTicketNo == 5500 || NewTicketNo == 5780 ||
                        NewTicketNo == 6613 || NewTicketNo == 6920)
                    //if (NewTicketNo == 6466)
                    {
                        await Task.Run(() => PrintingTicket(NewTicketNo));
                        await Task.Run(() => ConfirmTicket(NewTicketNo));
                        await Task.Run(() => SaveTicket(NewTicketNo));
                        //System.Threading.Thread.Sleep(50);
                        //System.Threading.Thread.Sleep(10);
                    }
                    ticketNum++;
                }
            }
        }
        private async void PrintingTicket(int ticketNo)
        {
            // Ticket numbers
            string tickNo = String.Format("No. {0}", ticketNo.ToString("D4"));
            lblTicketNoLeft.Dispatcher.Invoke(() =>
            {
                // UI operation goes inside of Invoke
                lblTicketNoLeft.Content = tickNo;
            });

            lblTicketNoRight.Dispatcher.Invoke(() =>
            {
                // UI operation goes inside of Invoke
                lblTicketNoRight.Content = tickNo;
            });

            await Task.Delay(10);
            // CPU-bound or I/O-bound operation goes outside of Invoke

            //lblTicketNoLeft.Content = tickNo;
            //lblTicketNoLeft.Refresh();
            //lblTicketNoRight.Content = tickNo;
            //lblTicketNoRight.Refresh();

            //// Table numbers
            //string tableNo = TableNumbering(ticketNo);
            //string tableNo = ticketNo.ToString();
            ////Console.WriteLine(tableNo);
            //lblTableNoLeft.Content = String.Format("Số bàn: {0}", tableNo);
            //lblTableNoLeft.Refresh();
            //lblTableNoRight.Content = String.Format("Số bàn: {0}", tableNo);
            //lblTableNoRight.Refresh();



        }

        private void ConfirmTicket(int ticketNo)
        {
            bool leftLabelCorrect = true;
            bool rightLabelCorrect = true;
            // Ticket numbers
            string tickNo = String.Format("No. {0}", ticketNo.ToString("D4"));
            lblTicketNoLeft.Dispatcher.Invoke(() =>
            {
                // UI operation goes inside of Invoke
                if (lblTicketNoLeft.Content.ToString() != tickNo)
                    leftLabelCorrect = false;
            });

            lblTicketNoRight.Dispatcher.Invoke(() =>
            {
                // UI operation goes inside of Invoke
                if (lblTicketNoRight.Content.ToString() != tickNo)
                    rightLabelCorrect = false;
            });

            if ((!leftLabelCorrect) || (!rightLabelCorrect))
            {
                MessageBox.Show("Something wrong with " + tickNo);
                return ;
            }
        }

            private async void SaveTicket(int ticketNo)
        {
            this.Dispatcher.Invoke(() =>
            {

                RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap(Convert.ToInt32(this.Width), Convert.ToInt32(this.Height), 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(this);
                PngBitmapEncoder pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
                //using (System.IO.Stream fileStream = File.Create(@"C:\Users\QC\Documents\Kangen Water\Tickets\Ticket" + i.ToString("D4") + ".png"))
                using (System.IO.Stream fileStream = File.Create(@"C:\Temp\Tickets\Ticket" + ticketNum.ToString("D4") + " - " + ticketNo.ToString("D4") + ".png"))
                {
                    pngImage.Save(fileStream);
                }
            });
            await Task.Delay(10);
        }

        private static string TableNumbering(int ticketNo)
        {
            // Ticket numbers
            // Table numbers
            string tableNo = "";
            if (ticketNo <= 50) // M1 - M5 50 tickets
            {
                //tableNo = string.Format("{0}-M{1}", ticketNo.ToString("D3"), ((ticketNo + 9) / 10).ToString()); //"1A-R";
                tableNo = string.Format("M{0}", ((ticketNo + 9) / 10).ToString()); //"1A-R";
            }
            else if (ticketNo <= 100) // 1A-R to 1E-R -> 50 tickets
            {
                //tableNo = string.Format("{0}-1{1}-R", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("1{0}-R", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 100)
                    c1 = 'A';
            }
            else if (ticketNo <= 150) // 1A-L to 1E-L -> 50 tickets
            {
                //tableNo = string.Format("{0}-1{1}-L", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("1{0}-L", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 150)
                    c1 = 'A';
            }
            else if (ticketNo <= 210) // 2A-R to 2F-R -> 60 tickets
            {
                //tableNo = string.Format("{0}-2{1}-R", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("2{0}-R", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 210)
                    c1 = 'A';

            }
            else if (ticketNo <= 270) // 2A-L to 2F-L -> 60 tickets
            {
                //tableNo = string.Format("{0}-2{1}-L", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("2{0}-L", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 270)
                    c1 = 'A';

            }
            else if (ticketNo <= 350) // 3A-R to 3H-R -> 80 tickets
            {
                //tableNo = string.Format("{0}-3{1}-R", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("3{0}-R", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 350)
                    c1 = 'A';
            }
            else if (ticketNo <= 430) // 3A-L to 3H-L -> 80 tickets
            {
                //tableNo = string.Format("{0}-3{1}-L", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("3{0}-L", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 430)
                    c1 = 'A';
            }
            else if (ticketNo <= 500) // 4A-R to 4G-R -> 70 tickets
            {
                //tableNo = string.Format("{0}-4{1}-R", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("4{0}-R", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 500)
                    c1 = 'A';
            }
            else if (ticketNo <= 540) // 4A-L to 4D-L -> 40 tickets
            {
                //tableNo = string.Format("{0}-4{1}-L", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("4{0}-L", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 540)
                    c1 = 'A';
            }
            else if (ticketNo <= 610) // 5A-R to 5G-R -> 70 tickets
            {
                //tableNo = string.Format("{0}-5{1}-R", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("5{0}-R", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 610)
                    c1 = 'A';
            }
            else if (ticketNo <= 650) // 5A-L to 5D-L -> 40 tickets
            {
                //tableNo = string.Format("{0}-5{1}-L", ticketNo.ToString("D3"), c1.ToString()); //"1A-R";
                tableNo = string.Format("5{0}-L", c1.ToString()); //"1A-R";
                if (ticketNo % 10 == 0)
                    c1++; // c1 is 'B' now

                if (ticketNo == 650)
                    c1 = 'A';
            }

            return tableNo;
        }
    }


    public static class ExtensionMethods
    {

        private static Action EmptyDelegate = delegate () { };


        public static void Refresh(this UIElement uiElement)
        {
            uiElement.Dispatcher.Invoke(DispatcherPriority.Render, EmptyDelegate);
        }
    }

}
