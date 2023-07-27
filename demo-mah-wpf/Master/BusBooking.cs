using System;

namespace demo_mah_wpf
{
    public class BusBooking : Booking
    {
        protected DateTime _bookDateTime;
        protected string _bookDateTimeStr;
        protected int _rowHeight;
        public DateTime BookDateTime
        {
            get { return _bookDateTime; }
            set
            {
                _bookDateTime = value;
                OnPropertyChanged("TaskName");
            }
        }
        public string BookDateTimeStr
        {
            get
            {
                if (this.BookDateTime != null && this.BookDateTime != DateTime.MinValue)
                {
                    return this.BookDateTime.ToString("hh:mm");
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                _bookDateTimeStr = value;
                OnPropertyChanged("TaskName");
            }
        }
        public int RowHeight
        {
            get { return this._rowHeight; }
            set { this._rowHeight = value; }
        }

        public BusBooking() : base()
        {
        }

        public BusBooking(DateTime bookDateTime, string description, string roomNum, int priority, TaskType type) : base()
        {
            _bookDateTime = bookDateTime;
            _description = description;
            _roomNum = roomNum;
            _priority = priority;
            _type = type;

            this.RowHeight = 0;
        }

        public override bool IsEmpty() { return string.IsNullOrEmpty(this.Description); }
        public override string ToString() => String.Format("Ticket:{0}, Room:{1}", this.TicketNum, this.RoomNum);
    }
}
