namespace TrelloConnector.Interfaces
{
	public interface ICard
    {
        string ID { get; set; }

        string Name { get; set; }

        string Desc { get; set; }

        string Members { get; set; }
    }
}
