using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo_mah_wpf
{
    public class NameList : ObservableCollection<PersonObject>
    {
        public NameList() : base()
        {
            Add(new PersonObject("1", DateTime.Now, "Willa", "Cather"));
            Add(new PersonObject("2", DateTime.Now, "Isak", "Dinesen"));
            Add(new PersonObject("3", DateTime.Now, "Victor", "Hugo"));
            Add(new PersonObject("4", DateTime.Now, "Jules", "Verne"));
        }
    }

    
}
