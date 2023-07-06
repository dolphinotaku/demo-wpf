using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Entity
{
    public class CentralBookingPagination : INotifyPropertyChanged
    {
        private int _currentPage;
        private int _totalPage;
        private int _defaultPageRowCount;
        private LinkedList<CentralBooking> _tupleList;
        private Dictionary<int, List<CentralBooking>> _centralBookingColumnDisplay;
        protected CentralBooking firstDisplayBookingNode;
        protected CentralBooking lastDisplayBookingNode;

        public CentralBookingPagination()
        {
            this._currentPage = -1;
            this._totalPage = -1;
            this._defaultPageRowCount = 6;
            this._tupleList = new LinkedList<CentralBooking>();
            this._centralBookingColumnDisplay = new Dictionary<int, List<CentralBooking>>();
            this.firstDisplayBookingNode = new CentralBooking();
            this.lastDisplayBookingNode = new CentralBooking();


        }
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }
        public int TotalPage
        {
            get { return _totalPage; }
            set
            {
                _totalPage = value;
                OnPropertyChanged("TotalPage");
            }
        }
        public int DefaultPageRowCount
        {
            get { return _defaultPageRowCount; }
            set
            {
                _defaultPageRowCount = value;
                OnPropertyChanged("DefaultPageRowCount");
            }
        }
        public LinkedList<CentralBooking> TupleList
        {
            get { return _tupleList; }
            set
            {
                _tupleList = value;
                OnPropertyChanged("TupleList");
            }
        }
        public Dictionary<int, List<CentralBooking>> CentralBookingColumnDisplay
        {
            get { return _centralBookingColumnDisplay; }
            set
            {
                _centralBookingColumnDisplay = value;
                OnPropertyChanged("CentralBookingColumnDisplay");
            }
        }
        public int TotalBooking
        {
            get { if (this._tupleList != null) { return this._tupleList.Count; } else { return -1; } }
        }

        public void AddBooking(CentralBooking _booking)
        {
            if (!this._tupleList.Contains(_booking))
            {
                this._tupleList.AddFirst(_booking);
                this.RefreshDisplayColumn();
            }
        }
        public void AddBookingRange(List<CentralBooking> _bookingList)
        {
            foreach(CentralBooking _booking in _bookingList)
            {
                if (!this._tupleList.Contains(_booking))
                    this._tupleList.AddFirst(_booking);
            }

            this.RefreshDisplayColumn();
            //this._tupleList.AddRange(_bookingList);
        }
        public void RemoveBooking(CentralBooking _booking)
        {
            this._tupleList.Remove(_booking);
        }
        public void ClearBookingList()
        {
            this._tupleList.Clear();
        }

        public void RefreshDisplayColumn()
        {
            //this.CentralBookingColumnDisplay.Clear();
            List<CentralBooking> column1BookList = new List<CentralBooking>();
            List<CentralBooking> column2BookList = new List<CentralBooking>();

            if (this.CentralBookingColumnDisplay.ContainsKey(1)) column1BookList = this.CentralBookingColumnDisplay[1];
            if (this.CentralBookingColumnDisplay.ContainsKey(2)) column2BookList = this.CentralBookingColumnDisplay[2];

            // if queryList is empty
            // add empty to the list, to expand the block on the layout
            if (this.TupleList == null || this.TupleList.Count == 0)
            {
                for(int i = 0; i<6; i++)
                {
                    CentralBooking _booking1 = new CentralBooking();
                    CentralBooking _booking2 = new CentralBooking();
                    column1BookList.Add(_booking1);
                    column2BookList.Add(_booking2);
                }
            }
            else
            {
                column1BookList.Clear();
                column2BookList.Clear();

                for (int i = 0; i < this.DefaultPageRowCount * 2; i++)
                {
                    LinkedListNode<CentralBooking> linkedListNode = this.TupleList.Find(this.lastDisplayBookingNode);
                        if (linkedListNode == null)
                            linkedListNode = this.TupleList.First;
                        else 
                            linkedListNode = linkedListNode.Next;

                    CentralBooking _booking = linkedListNode.Value;
                    // mark the first displaying node
                    if (i == 0) this.firstDisplayBookingNode = _booking;

                    // the node is repeat, means the list was looped through
                    // break the display for loop
                    if (i!=0 && _booking == this.firstDisplayBookingNode) break; 

                    if (column1BookList.Count < this.DefaultPageRowCount) {
                        column1BookList.Add(_booking);
                    } else if (column2BookList.Count < this.DefaultPageRowCount) {
                        column2BookList.Add(_booking);
                    }

                    // mark the last displaying node
                    lastDisplayBookingNode = _booking;
                }
            }

            //this.CentralBookingColumnDisplay.Add(1, column1BookList);
            //this.CentralBookingColumnDisplay.Add(2, column2BookList);

            this.CentralBookingColumnDisplay[1] = column1BookList;
            this.CentralBookingColumnDisplay[2] = column2BookList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
