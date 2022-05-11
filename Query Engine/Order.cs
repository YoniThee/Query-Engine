using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query_Engine
{
    /*
     * The instructions did not address what contains in this struct, so I decided for myself
     */
    public struct Order
    {
        public string Sender;
        public string Target;

        public override string ToString()
        {
            return $"Sender: {Sender}, Target: {Target}";
        }
    }
}
