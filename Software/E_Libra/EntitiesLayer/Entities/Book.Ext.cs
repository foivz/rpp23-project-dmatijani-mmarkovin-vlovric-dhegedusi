using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntitiesLayer {
    public partial class Book {
        public override string ToString() {
            return name;
        }
        public string AuthorsString {
            get { return string.Join(", ", Authors.Select(a => a.name + " " + a.surname)); }
        }
    }
}
