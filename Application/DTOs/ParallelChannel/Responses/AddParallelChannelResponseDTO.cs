namespace Application.DTOs.ParallelChannel.Responses
{
    public class AddParallelChannelResponseDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Price1 { get; set; }
        public decimal Timestamp1 { get; set; }
        public decimal Price2 { get; set; }
        public decimal Timestamp2 { get; set; }
        public decimal Price3 { get; set; }
        public decimal Timestamp3 { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
