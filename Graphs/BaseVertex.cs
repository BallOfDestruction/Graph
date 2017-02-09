using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Базовый класс-дженерик для вершины
    /// </summary>
    /// <typeparam name="T">Тип ребра</typeparam>
    public class BaseVertex<T>
    {
        public string Name { get;protected set; }
        /// <summary>
        /// Метка, используется в поисках
        /// </summary>
        internal bool IsSeen { get; set; }

        public int Depth { get; set; } = 0;
        /// <summary>
        /// Степень вершины
        /// </summary>
        public List<T> NextVertex { get; protected set; }

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
