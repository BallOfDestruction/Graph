using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    /// <summary>
    /// Базовый класс для графа
    /// </summary>
    /// <typeparam name="T">Тип вершины</typeparam>
    /// <typeparam name="E">Тип ребра</typeparam>
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
        public Dictionary<string, T> NameVertex => Vertex.ToDictionary(wde => wde.Name);
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
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <returns>Есть ли путь от начальной к кконечной вершине</returns>
        public bool DFS(string startVertex, string endVertex)
        {
            if (this.GetPathDFS(startVertex, endVertex) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Поиск в глубину
        /// </summary>
        /// <returns>Путь от начальной к конечной</returns>
        public List<T> GetPathDFS(string startVertexName, string endVertexName)
        {
            NullebleNow();
            if (!NameVertex.ContainsKey(startVertexName))
            {
                throw new Exception("Вершины " + startVertexName + "в графе нет");
            }
            if (!NameVertex.ContainsKey(endVertexName))
            {
                throw new Exception("Вершины " + endVertexName + "в графе нет");
            }
            T startVertex = this.NameVertex[startVertexName];
            T endVertex = this.NameVertex[endVertexName];
            Stack<T> stack = new Stack<T>();
            stack.Push(startVertex);
            bool end = false;
            while (stack.Count != 0)
            {
                T bufferVertex = stack.Pop();
                if (bufferVertex.IsSeen == false)
                {
                    bufferVertex.IsSeen = true;
                    if (bufferVertex != endVertex)
                    {
                        foreach (T buf in bufferVertex.NextVertex.Select(w => w.NextVertex))
                        {
                            if ((!buf.IsSeen) && !stack.Contains(buf))
                            {
                                stack.Push(buf);
                            }
                        }
                    }
                    else
                    {
                        end = true;
                        break;
                    }
                }
            }
            if (end)
            {
                Stack<T> answer = new Stack<T>();
                T buffer = endVertex;
                answer.Push(endVertex);
                while (buffer != startVertex)
                {
                    var r = Vertex.Where(w => w.NextVertex.Where(ww => (ww.NextVertex == buffer) && (ww.Previous.IsSeen)).Count() == 1).ToArray();
                    answer.Push(r[0]);
                    buffer.IsSeen = false;
                    buffer = r[0];
                }
                NullebleNow();
                return answer.ToList();
            }
            else
            {
                NullebleNow();
                return null;
            }
        }
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <returns>Есть ли путь от начальной к кконечной вершине</returns>
        public bool BFS(string startVertex, string endVertex)
        {
            if (this.GetPathBFS(startVertex, endVertex) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Поиск в ширину
        /// </summary>
        /// <returns>Путь от начальной к конечной</returns>
        public List<T> GetPathBFS(string startVertexName, string endVertexName)
        {
            NullebleNow();
            if (!NameVertex.ContainsKey(startVertexName))
            {
                throw new Exception("Вершины " + startVertexName + "в графе нет");
            }
            if (!NameVertex.ContainsKey(endVertexName))
            {
                throw new Exception("Вершины " + endVertexName + "в графе нет");
            }
            T startVertex = this.NameVertex[startVertexName];
            T endVertex = this.NameVertex[endVertexName];
            Queue<T> queue = new Queue<T>();
            queue.Enqueue(startVertex);
            bool end = false;
            while (queue.Count != 0)
            {
                T bufferVertex = queue.Dequeue();
                if (bufferVertex.IsSeen == false)
                {
                    bufferVertex.IsSeen = true;
                    if (bufferVertex != endVertex)
                    {
                        foreach (T buf in bufferVertex.NextVertex.Select(w => w.NextVertex))
                        {
                            if ((!buf.IsSeen) && !queue.Contains(buf))
                            {
                                queue.Enqueue(buf);
                            }
                        }
                    }
                    else
                    {
                        end = true;
                        break;
                    }
                }
            }
            if (end)
            {
                Stack<T> answer = new Stack<T>();
                T buffer = endVertex;
                answer.Push(endVertex);
                while (buffer != startVertex)
                {
                    var r = Vertex.Where(w => w.NextVertex.Where(ww => (ww.NextVertex == buffer) && (ww.Previous.IsSeen)).Count() == 1).ToArray();
                    answer.Push(r[0]);
                    buffer.IsSeen = false;
                    buffer = r[0];
                }
                NullebleNow();
                return answer.ToList();
            }
            else
            {
                NullebleNow();
                return null;
            }
        }
        /// <summary>
        /// Обнуляет все вершины
        /// </summary>
        protected void NullebleNow()
        {
            foreach (T edge in Vertex)
            {
                edge.IsSeen = false;
            }
        }
        #endregion
    }
}
