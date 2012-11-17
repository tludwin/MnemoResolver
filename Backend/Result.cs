using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Backend
{
    public sealed class Result
    {
        public string Number { get; set; }
        public IEnumerable<string> Words { get; set; }
    }
}
