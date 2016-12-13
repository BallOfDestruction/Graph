using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public abstract class BaseGraph<T, E>
        where T : BaseVertex<E>
        where E : BaseEdge<E>
    {
        /// <summary>
        /// Список ребер графа
        /// </summary>
        public List<E> Edges => Vertex.SelectMany(w => w.NextVertex).ToList();
        /// <summary>
        /// Словарь вершин графа по их именам
        /// </summary>
        public Dictionary<string, T> NameVertex => Vertex.ToDictionary(wde=> wde.Name);
        /// <summary>
        /// Вершины графа
        /// </summary>
        public List<T> Vertex { get; } = new List<T>();

        #region Vertex
        /// <summary>
        /// Добавление новой вершины 
        /// </summary>
        public void AddVertex(T vertex)
        {
            if (this.Vertex.Contains(vertex))
            {
                throw new Exception("Вершина " + vertex.Name + " уже присутствует в графе");
            }
            this.Vertex.Add(vertex);
        }
        /// <summary>
        /// Добавление любого перечисляемого множества вершин в граф
        /// </summary>
        public void AddVertex(IEnumerable<T> vertex)
        {
            var buffer = vertex.Where(w => this.Vertex.Contains(w));
            if (buffer.Count() != 0)
            {
                throw new Exception("Некоторые вершины уже есть в графе: " + buffer.Select(w => w.Name + " "));
            }
            foreach (T vert in vertex)
            {
                AddVertex(vert);
            }
        }
        public void AddVertex(params T[] vertex)
        {
            AddVertex(vertex.AsEnumerable());
        }
        /// <summary>
        /// При удалении вершины также удаляются все ребра
        /// </summary>
        public void RemoveVertex(string name)
        {
            if (NameVertex.ContainsKey(name))
            {
                var vertex = NameVertex[name];
                this.Vertex.Remove(vertex);
                var neibour = this.Vertex.Where(w => (w.NextVertex.Select(ww => ww.NextVertex).Contains(vertex)));
                foreach (var neib in neibour)
                {
                    var edge = neib.NextVertex.Where(w => w.NextVertex == vertex);
                    if (edge.Count() == 1)
                    {
                        neib.NextVertex.Remove(edge.ToArray()[0]);
                    }
                    else
                    {
                        throw new Exception(@"Вообще не парю в каком случае может так сломаться, но проверить надо,
                            а то точно не будет понятно где крякнулась");
                    }
                }
            }
            else
            {
                throw new Exception("Нет такой вершины");
            }
        }
        /// <summary>
        /// При удалении вершины также удаляются все ребра
        /// </summary>
        public void RemoveVertex(T vertex)
        {
            this.RemoveVertex(vertex.Name);
        }
        #endregion

        #region Edge
        /// <summary>
        /// Добавление вершины в одну сторону
        /// </summary>
        protected void AddEdge(E edge)
        {
            var c = (this.Edges.Where(w => (edge.Previous == w.Previous) && (w.NextVertex == edge.NextVertex))).ToArray();
            c.Count();
            if (this.Edges.Contains(edge) || c.Count() > 0)
            {
                throw new Exception("Ребро уже присутствует в графе " + edge.Previous.Name + " -> " + edge.NextVertex.Name);
            }
            if (Vertex.Contains(edge.Previous) && Vertex.Contains(edge.NextVertex))
            {
                NameVertex[edge.Previous.Name].NextVertex.Add(edge);
            }
            else
            {
                if ((!Vertex.Contains(edge.Previous) && !Vertex.Contains(edge.NextVertex)))
                {
                    throw new Exception("Вершины: " + edge.Previous.Name + ", " + edge.NextVertex.Name + " не существуют в графе");
                }
                else
                {
                    if (!Vertex.Contains(edge.Previous))
                    {
                        throw new Exception("Вершина " + edge.Previous.Name + " не существует в графе");
                    }
                    else
                    {
                        if (!Vertex.Contains(edge.NextVertex))
                        {
                            throw new Exception("Вершина " + edge.NextVertex.Name + " не существует в графе");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// удаление существующего ребра
        /// </summary>
        protected void RemoveEdge(string previous, string nextVertex)
        {
            var edges = this.Edges.Where(w => w.NextVertex.Name.Equals(nextVertex) && w.Previous.Name.Equals(previous)).ToArray();
            if (edges.Count() == 0)
            {
                throw new Exception("Такого ребра не существует");
            }
            else
            {
                if (edges.Count() > 1)
                {
                    throw new Exception("Я снова не парю такой случай, вроде все предусмотрел, надеюсь, вы этого не видите");
                }
                else
                {
                    this.RemoveEdge(edges[0]);
                }
            }
        }
        /// <summary>
        /// Удаление существующего ребра
        /// </summary>
        protected void RemoveEdge(E edge)
        {
            if (!Edges.Contains(edge))
            {
                throw new Exception("Такого ребра не существует");
            }
            else
            {
                edge.Previous.NextVertex.Remove(edge);
            }
        }
        #endregion

        #region Searched



        #endregion
    }
}
