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
    public class WalkInBookingPagination : INotifyPropertyChanged
    {
        private int _currentPage;
        private int _totalPage;
        private int _defaultPageRowCount;
        private int _defaultPageColumnCount;
        private LinkedList<WalkInBooking> _tupleList;
        private ObservableConcurrentDictionary<int, List<WalkInBooking>> _walkInBookingColumnDisplay;
        protected WalkInBooking firstDisplayBookingNode;
        protected WalkInBooking lastDisplayBookingNode;

        public WalkInBookingPagination()
        {
            this._currentPage = -1;
            this._totalPage = -1;
            this._defaultPageRowCount = 6;
            this._defaultPageColumnCount = 1;
            this._tupleList = new LinkedList<WalkInBooking>();
            this._walkInBookingColumnDisplay = new ObservableConcurrentDictionary<int, List<WalkInBooking>>();
            this.firstDisplayBookingNode = new WalkInBooking();
            this.lastDisplayBookingNode = new WalkInBooking();


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
        public LinkedList<WalkInBooking> TupleList
        {
            get { return _tupleList; }
            set
            {
                _tupleList = value;
                OnPropertyChanged("TupleList");
            }
        }
        public ObservableConcurrentDictionary<int, List<WalkInBooking>> WalkInBookingColumnDisplay
        {
            get { return _walkInBookingColumnDisplay; }
            set
            {
                _walkInBookingColumnDisplay = value;
                OnPropertyChanged("WalkInBookingColumnDisplay");
            }
        }
        public int TotalBooking
        {
            get { if (this._tupleList != null) { return this._tupleList.Count; } else { return -1; } }
        }

        public void AddBooking(WalkInBooking _booking)
        {
            if (!this._tupleList.Contains(_booking))
            {
                this._tupleList.AddFirst(_booking);
                this.RefreshDisplayColumn();
            }
        }
        public void AddBookingRange(List<WalkInBooking> _bookingList)
        {
            foreach(WalkInBooking _booking in _bookingList)
            {
                if (!this._tupleList.Contains(_booking))
                    this._tupleList.AddFirst(_booking);
            }

            this.RefreshDisplayColumn();
        }
        public void RemoveBooking(WalkInBooking _booking)
        {
            this._tupleList.Remove(_booking);
        }
        public void ClearBookingList()
        {
            this._tupleList.Clear();
        }

        public void RefreshDisplayColumn()
        {
            //this.WalkInBookingColumnDisplay.Clear();
            List<WalkInBooking> column1BookList = new List<WalkInBooking>();
            //List<WalkInBooking> column2BookList = new List<WalkInBooking>();

            if (this.WalkInBookingColumnDisplay.ContainsKey(1)) column1BookList = this.WalkInBookingColumnDisplay[1];
            //if (this.WalkInBookingColumnDisplay.ContainsKey(2)) column2BookList = this.WalkInBookingColumnDisplay[2];

            // if queryList is empty
            // add empty to the list, to expand the block on the layout
            if (this.TupleList == null || this.TupleList.Count == 0)
            {
                for(int i = 0; i<6; i++)
                {
                    WalkInBooking _booking1 = new WalkInBooking();
                    WalkInBooking _booking2 = new WalkInBooking();
                    column1BookList.Add(_booking1);
                    //column2BookList.Add(_booking2);
                }
            }
            else
            {
                column1BookList.Clear();
                //column2BookList.Clear();

                for (int i = 0; i < this.DefaultPageRowCount * this.DefaultPageColumnCount; i++)
                {
                    LinkedListNode<WalkInBooking> linkedListNode = this.TupleList.Find(this.lastDisplayBookingNode);
                        if (linkedListNode == null)
                            linkedListNode = this.TupleList.First;
                        else 
                            linkedListNode = linkedListNode.Next;

                    WalkInBooking _booking = linkedListNode.Value;
                    // mark the first displaying node
                    if (i == 0) this.firstDisplayBookingNode = _booking;

                    // the node is repeat, means the list was looped through
                    // break the display for loop
                    if (i!=0 && _booking == this.firstDisplayBookingNode) break; 

                    if (column1BookList.Count < this.DefaultPageRowCount) {
                        column1BookList.Add(_booking);
                    } 
                    //else if (column2BookList.Count < this.DefaultPageRowCount) {
                    //    column2BookList.Add(_booking);
                    //}

                    // mark the last displaying node
                    lastDisplayBookingNode = _booking;
                }
            }

            //this.WalkInBookingColumnDisplay.Add(1, column1BookList);
            //this.WalkInBookingColumnDisplay.Add(2, column2BookList);

            this.WalkInBookingColumnDisplay[1] = column1BookList;
            //this.WalkInBookingColumnDisplay[2] = column2BookList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(info));
        }
    }
}
