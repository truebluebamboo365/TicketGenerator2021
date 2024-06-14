using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawing
{
    public class Ticket : INotifyPropertyChanged
    {
        private int ticketNo;
        private int tableNo;

        public int TicketNo
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
        
        public int TableNo
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

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
