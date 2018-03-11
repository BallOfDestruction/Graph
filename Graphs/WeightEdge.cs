namespace Graphs
{
    public class WeightEdge : BaseEdge<WeightEdge>
    {
        public double Weight { get; set; }

        public WeightEdge(WeightVertex previousVertex, WeightVertex nextVertex , double weight) : base(previousVertex, nextVertex)
        {
            Weight = weight;
        }
        public override string ToString()
        {
            return base.ToString() + "|" + Weight;
        }
    }
}
