using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf
{

    public class PersonObject : INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string id;
        private DateTime date;
        public string ID
        {
            get { return id; }
            set { id = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }


        // Declare the event
        public event PropertyChangedEventHandler PropertyChanged;

        public PersonObject()
        {
        }

        public PersonObject(string id, DateTime date)
        {
            this.id = id;
            this.date = date;
        }
        public PersonObject(string id, DateTime date, string firstName, string lastName)
        {
            this.id = id;
            this.date = date;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        // Create the OnPropertyChanged method to raise the event
        // The calling member's name will be used as the parameter.
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
