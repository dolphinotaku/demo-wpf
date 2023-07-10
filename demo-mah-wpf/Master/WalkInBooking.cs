using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class WalkInBooking : Booking
    {

        public WalkInBooking() : base()
        {
        }

        public WalkInBooking(string ticketNum, string roomNum, int priority, TaskType type) : base()
        {
            _ticketNum = ticketNum;
            _roomNum = roomNum;
            _priority = priority;
            _type = type;
        }

        public override string ToString() => String.Format("Ticket:{0}, Room:{1}", this.TicketNum, this.RoomNum);
    }
}
