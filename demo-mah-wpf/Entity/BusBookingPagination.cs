using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf.Entity
{
    public class BusBookingPagination : INotifyPropertyChanged
    {
        private int _currentPage;
        private int _totalPage;
        private int _defaultPageRowCount;
        private int _defaultPageColumnCount;
        private LinkedList<BusBooking> _tupleList;
        private ObservableConcurrentDictionary<int, List<BusBooking>> _busBookingColumnDisplay;
        protected BusBooking firstDisplayBookingNode;
        protected BusBooking lastDisplayBookingNode;

        public BusBookingPagination()
        {
            this._currentPage = -1;
            this._totalPage = -1;
            this._defaultPageRowCount = 2;
            this._defaultPageColumnCount = 1;
            this._tupleList = new LinkedList<BusBooking>();
            this._busBookingColumnDisplay = new ObservableConcurrentDictionary<int, List<BusBooking>>();
            this.firstDisplayBookingNode = new BusBooking();
            this.lastDisplayBookingNode = new BusBooking();


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
        public int DefaultPageColumnCount
        {
            get { return _defaultPageColumnCount; }
            set
            {
                _defaultPageColumnCount = value;
                OnPropertyChanged("DefaultPageRowCount");
            }
        }
        public LinkedList<BusBooking> TupleList
        {
            get { return _tupleList; }
            set
            {
                _tupleList = value;
                OnPropertyChanged("TupleList");
            }
        }
        public ObservableConcurrentDictionary<int, List<BusBooking>> BusBookingColumnDisplay
        {
            get { return _busBookingColumnDisplay; }
            set
            {
                _busBookingColumnDisplay = value;
                OnPropertyChanged("BusBookingColumnDisplay");
            }
        }
        public int TotalBooking
        {
            get { if (this._tupleList != null) { return this._tupleList.Count; } else { return -1; } }
        }

        public void AddBooking(BusBooking _booking)
        {
            if (!this._tupleList.Contains(_booking))
            {
                this._tupleList.AddFirst(_booking);
                //this.RefreshDisplayColumn();
            }
        }
        public void AddBookingRange(List<BusBooking> _bookingList)
        {
            foreach (BusBooking _booking in _bookingList)
            {
                if (!this._tupleList.Contains(_booking))
                    this._tupleList.AddFirst(_booking);
            }

            //this.RefreshDisplayColumn();
        }
        public void RemoveBooking(BusBooking _booking)
        {
            this._tupleList.Remove(_booking);
        }
        public void ClearBookingList()
        {
            this._tupleList.Clear();
            this.firstDisplayBookingNode = new BusBooking();
            this.lastDisplayBookingNode = new BusBooking();
        }

        public void RefreshDisplayColumn()
        {
            List<BusBooking> column1BookList = new List<BusBooking>();

            if (this.BusBookingColumnDisplay.ContainsKey(1)) column1BookList = this.BusBookingColumnDisplay[1];

            // if queryList is empty
            // add empty to the list, to expand the block on the layout
            if (this.TupleList == null || this.TupleList.Count == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    BusBooking _booking1 = new BusBooking();
                    column1BookList.Add(_booking1);
                }
            }
            else
            {
                column1BookList.Clear();

                for (int i = 0; i < this.DefaultPageRowCount * this.DefaultPageColumnCount; i++)
                {
                    LinkedListNode<BusBooking> linkedListNode = null;
                    if (!this.lastDisplayBookingNode.IsEmpty())
                    {
                        linkedListNode = this.TupleList.FindLast(this.lastDisplayBookingNode);
                    }
                    if (linkedListNode == null)
                    {
                        linkedListNode = this.TupleList.First;
                    }
                    else
                    {
                        if (linkedListNode == this.TupleList.Last && i == 0)
                        {
                            linkedListNode = this.TupleList.First;
                        }
                        else if (linkedListNode == this.TupleList.Last && i >= 0)
                        {
                            break;
                        }
                        else
                        {
                            linkedListNode = linkedListNode.Next;
                        }
                    }

                    BusBooking _booking = linkedListNode.Value;
                    // mark the first displaying node
                    if (i == 0) this.firstDisplayBookingNode = _booking;

                    // the node is repeat, means the list was looped through
                    // break the display for loop
                    if (i != 0 && _booking == this.firstDisplayBookingNode) break;

                    if (!string.IsNullOrEmpty(_booking.Description))
                    {
                        if (_booking.Description.Length < 45)
                        {
                            _booking.RowHeight = 1;
                        }
                        else if (_booking.Description.Length >= 45)
                        {
                            _booking.RowHeight = 2;
                        }
                    }

                    if (column1BookList.Count < this.DefaultPageRowCount)
                    {
                        column1BookList.Add(_booking);
                    }

                    // mark the last displaying node
                    this.lastDisplayBookingNode = _booking;
                }

                // calculate row height
                while (column1BookList.Select(node => node.RowHeight).ToList().Sum() > this.DefaultPageRowCount)
                {
                    column1BookList.RemoveAt(column1BookList.Count - 1);
                }

                this.lastDisplayBookingNode = column1BookList[column1BookList.Count - 1];

            }

            this.BusBookingColumnDisplay[1] = column1BookList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
