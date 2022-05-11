using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query_Engine
{
    /*
     PDS to allow filtering effectively
     */
    public struct Data
    {
        public List<User> Users;
        public List<Order> Orders;

        public override string ToString()
        {
            string ans = "";
            if (Users != null)
            {
                foreach (User user in Users)
                {
                   ans += user.ToString() + "\n";
                }
            }
            if (Orders != null)
            {
                foreach (Order order in Orders)
                {
                    ans += order.ToString() + "\n";
                }
            }
            return ans;
        }
    }
}
