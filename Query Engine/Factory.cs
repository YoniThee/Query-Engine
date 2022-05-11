using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query_Engine
{
    static public class Factory
    {/*
      This class is implement "Factory" method for more safe using at thr program, this object can only be created 
      here in this class, and all the program get only this object
      */
        static public interfaceFunctions GetObject()
        {
            return initilaizeObject.Instance;
        }
    }
}