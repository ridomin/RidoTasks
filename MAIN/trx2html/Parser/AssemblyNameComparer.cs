using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace trx2html.Parser
{
    class AssemblyNameComparer : IEqualityComparer<AssemblyName>
    {
        public bool Equals(AssemblyName x, AssemblyName y)
        {
            return x.FullName == y.FullName;
        }

        public int GetHashCode(AssemblyName obj)
        {
            return base.GetHashCode();
        }
    }
}
