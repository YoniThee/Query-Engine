using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query_Engine
{
    /*
     * PDS to allow filtering effectively
     */
    public struct User
    {
        public string Email;
        public string FullName;
        public int Age;

        public override string ToString()
        {
            return $"Email: {Email}, Name: {FullName}";
        }
    }
}
