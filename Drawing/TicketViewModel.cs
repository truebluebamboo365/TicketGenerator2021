using Microsoft.VisualStudio.PlatformUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Drawing
{
    public class TicketViewModel: INotifyPropertyChanged
    {
        public TicketViewModel()
        {
            //for (int i = 1; i <= 120; i++)
            //{
            //    TicketNo = i;
            //    SaveTicket(i);
            //}
        }

        private void SaveTicket(int ticketNo)
        {
            //MainWindow window = new MainWindow();

            //window.Dispatcher.Invoke(() =>
            //{

            //    RenderTargetBitmap renderTargetBitmap =
            //    new RenderTargetBitmap(Convert.ToInt32(window.Width), Convert.ToInt32(window.Height), 96, 96, PixelFormats.Pbgra32);
            //    renderTargetBitmap.Render(window);
            //    PngBitmapEncoder pngImage = new PngBitmapEncoder();
            //    pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            //    //using (System.IO.Stream fileStream = File.Create(@"C:\Users\QC\Documents\Kangen Water\Tickets\Ticket" + i.ToString("D4") + ".png"))
            //    using (System.IO.Stream fileStream = File.Create(@"C:\Temp\ViewModel\Ticket" + " - " +
            //        //ticketPrintingSequence.ToString("D3") + " - " +
            //        //tableNumber.ToString("D2") + " - " +
            //        ticketNo.ToString("D3") + ".png"))
            //    {
            //        pngImage.Save(fileStream);
            //    }
            //});
        }

        public void MouseLeftButtonClick(object sender, MouseButtonEventArgs e)
        {
            //for (int i = 1; i <= 20; i++)
            //{
            //    TicketNo = i;
            //}
        }
        //public ICommand MyButtonClickCommand
        //{
        //    get { return new DelegateCommand<object>(FuncToCall, FuncToEvaluate); }
        //}

        //private void FuncToCall(object context)
        //{
        //    //this is called when the button is clicked
        //}

        //private bool FuncToEvaluate(object context)
        //{
        //    //this is called to evaluate whether FuncToCall can be called
        //    //for example you can return true or false based on some validation logic
        //    return true;
        //}
        private ICommand _clickCommand;
        public ICommand PrintTicketCommand
        {
            get
            {
                return _clickCommand ?? (_clickCommand = new CommandHandler(() => PrintAction(), true));
            }
        }
        public void PrintAction()
        {
            //for (int i = 1; i <= 120; i++)
            //{
            //    TicketNo = i;
            //}
        }

        private class PrintTicket : ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                // Code implementation for execution
            }
        }
        private string ticketNo;
        private string tableNo;

        public string TicketNo
        {
            get
            {
                return ticketNo;
            }
            set
            {
                ticketNo = value;
                OnPropertyChanged("TicketNo");
            }
        }

        public string TableNo
        {
            get
            {
                return tableNo;
            }
            set
            {
                tableNo = value;
                OnPropertyChanged("TableNo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            _action();
        }
    }
}
