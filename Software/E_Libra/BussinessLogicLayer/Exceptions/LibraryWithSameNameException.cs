using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions {
    public class LibraryWithSameNameException : LibraryException {
        public LibraryWithSameNameException(string message) : base(message) { }
    }
}
