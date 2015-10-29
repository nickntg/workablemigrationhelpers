namespace Workable.Core.Migration.Trello.Interfaces
{
    public interface ITrelloClassifier
    {
        Classification ClassifyCard(Card card, Board board);
    }
}
