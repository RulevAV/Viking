using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viking.Models.Sports;

namespace Viking.Repositories
{
    public class Base
    {
        public readonly conViking_Sports _conVikingSports;

        public Base(conViking_Sports conVikingSports)
        {
            _conVikingSports = conVikingSports;
        }
    }
}
