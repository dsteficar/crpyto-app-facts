namespace WebApplicationUI.Models.Trading.Graphs
{
    public class UserParallelChannelModel
    {
        public int UserId { get; set; }

        public ParallelChannelPointModel[] Points { get; set; } = new ParallelChannelPointModel[3];
    }
}
