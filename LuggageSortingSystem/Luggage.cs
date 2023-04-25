using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSortingSystem
{
    class Luggage
    {
        // Feilds
        private string destination;

        // Properies
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        // Constructor, The constructor has a parameter string destination.
        public Luggage(string destination)
        {
            this.destination = destination;
        }
    }
}
