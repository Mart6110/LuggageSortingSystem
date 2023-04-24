using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSortingSystem
{
    class Luggage
    {
        private string destination;

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        public Luggage(string destination)
        {
            this.destination = destination;
        }
    }
}
