using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.Business.Facades.ParsLogic
{
    class PMICase : PMIHelper
    {
        public PMICase(string a_address)
            : base(a_address)
        {
            base.address = a_address;
        }
      }
}
