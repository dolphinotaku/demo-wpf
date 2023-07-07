using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class CentralBooking : INotifyPropertyChanged
    {
        private string _description;
        private string _name;
        private string _ticketNum;
        private string _roomNum;
        private int _priority;
        private TaskType _type;

        public CentralBooking()
        {
        }

        public CentralBooking(string ticketNum, string roomNum, int priority, TaskType type)
        {
            _ticketNum = ticketNum;
            _roomNum = roomNum;
            _priority = priority;
            _type = type;
        }

        public string TicketNum
        {
            get { return _ticketNum; }
            set
            {
                _ticketNum = value;
                OnPropertyChanged("TaskName");
            }
        }

        public string RoomNum
        {
            get { return _roomNum; }
            set
            {
                _roomNum = value;
                OnPropertyChanged("Description");
            }
        }

        public int Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }

        public TaskType TaskType
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("TaskType");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString() => String.Format("Ticket:{0}, Room:{1}", this.TicketNum, this.RoomNum);

        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
