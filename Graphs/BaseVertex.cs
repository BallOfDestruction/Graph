using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class BaseVertex<T>
    {
        public string Name { get; set; }

        public List<T> NextVertex { get; set; }

        public BaseVertex(string name)
        {
            this.Name = name;
            NextVertex = new List<T>();
        }

        public BaseVertex(string name, params T[] vertex): this(name)
        {
            NextVertex.AddRange(vertex);
        }
    }
}
