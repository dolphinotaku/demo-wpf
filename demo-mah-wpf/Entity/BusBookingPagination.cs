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
                this.RefreshDisplayColumn();
            }
        }
        public void AddBookingRange(List<BusBooking> _bookingList)
        {
            foreach(BusBooking _booking in _bookingList)
            {
                if (!this._tupleList.Contains(_booking))
                    this._tupleList.AddFirst(_booking);
            }

            this.RefreshDisplayColumn();
        }
        public void RemoveBooking(BusBooking _booking)
        {
            this._tupleList.Remove(_booking);
        }
        public void ClearBookingList()
        {
            this._tupleList.Clear();
        }

        public void RefreshDisplayColumn()
        {
            //this.BusBookingColumnDisplay.Clear();
            List<BusBooking> column1BookList = new List<BusBooking>();
            //List<BusBooking> column2BookList = new List<BusBooking>();

            if (this.BusBookingColumnDisplay.ContainsKey(1)) column1BookList = this.BusBookingColumnDisplay[1];
            //if (this.BusBookingColumnDisplay.ContainsKey(2)) column2BookList = this.BusBookingColumnDisplay[2];

            // if queryList is empty
            // add empty to the list, to expand the block on the layout
            if (this.TupleList == null || this.TupleList.Count == 0)
            {
                for(int i = 0; i<6; i++)
                {
                    BusBooking _booking1 = new BusBooking();
                    column1BookList.Add(_booking1);
                }
            }
            else
            {
                column1BookList.Clear();

                LinkedListNode<BusBooking> linkedListNode = this.TupleList.Find(this.lastDisplayBookingNode);
                if (linkedListNode == null)
                    linkedListNode = this.TupleList.First;
                else
                    linkedListNode = linkedListNode.Next;

                BusBooking _booking1 = linkedListNode.Value;
                BusBooking _booking2 = new BusBooking();

                if (this.firstDisplayBookingNode != _booking1)
                {
                    this.firstDisplayBookingNode = _booking1;

                    column1BookList.Add(_booking1);
                    // mark the last displaying node
                    this.lastDisplayBookingNode = _booking1;

                    if (!string.IsNullOrEmpty(_booking1.Description) && _booking1.Description.Length < 30)
                    {
                        _booking2 = linkedListNode.Next.Value;
                        if(!string.IsNullOrEmpty(_booking2.Description) && _booking2.Description.Length < 30)
                        {
                            column1BookList.Add(_booking2);
                            // mark the last displaying node
                            this.lastDisplayBookingNode = _booking2;
                        }

                    }
                }
                else
                {
                    // the node is repeat, means the list was looped through
                    // break the display for loop
                }

                //for (int i = 0; i < this.DefaultPageRowCount * this.DefaultPageColumnCount; i++)
                //{
                //    LinkedListNode<BusBooking> linkedListNode = this.TupleList.Find(this.lastDisplayBookingNode);
                //        if (linkedListNode == null)
                //            linkedListNode = this.TupleList.First;
                //        else 
                //            linkedListNode = linkedListNode.Next;

                //    BusBooking _booking = linkedListNode.Value;
                //    // mark the first displaying node
                //    if (i == 0) this.firstDisplayBookingNode = _booking;

                //    // the node is repeat, means the list was looped through
                //    // break the display for loop
                //    if (i!=0 && _booking == this.firstDisplayBookingNode) break; 

                //    if (column1BookList.Count < this.DefaultPageRowCount) {
                //        column1BookList.Add(_booking);
                //    } 

                //    // mark the last displaying node
                //    this.lastDisplayBookingNode = _booking;
                //}
            }

            //this.BusBookingColumnDisplay.Add(1, column1BookList);
            //this.BusBookingColumnDisplay.Add(2, column2BookList);

            this.BusBookingColumnDisplay[1] = column1BookList;
            //this.BusBookingColumnDisplay[2] = column2BookList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
