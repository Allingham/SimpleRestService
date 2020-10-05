using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class FilterClass
    {
        public FilterClass(){
        }

        public FilterClass(int highQuantity, int lowQuantity){
            HighQuantity = highQuantity;
            LowQuantity = lowQuantity;
        }

        public int HighQuantity{ get; set; }
        public int LowQuantity{ get; set; }
        
    }
}
