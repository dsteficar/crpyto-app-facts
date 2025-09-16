using Domain.Entity.Authentication;

namespace Domain.Entity.Trading.Graphs
{
    public class ParallelChannel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price1 { get; set; }
        public decimal Price2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Timestamp1 { get; set; }
        public decimal Timestamp2 { get; set; }
        public decimal Timestamp3 { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
