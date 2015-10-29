using System;
using System.IO;
using RestSharp;
using Workable.Core.Migration.Trello.Interfaces;

namespace Workable.Core.Migration.Trello
{
    public class TrelloReader
    {
        public void ProcessTrelloExport(ITrelloClassifier classifier, string jsonFile, string saveDirectory)
        {
            var contents = File.ReadAllText(jsonFile);

            var board = SimpleJson.DeserializeObject<Board>(contents);

            if (board == null || board.cards == null || board.cards.Count == 0)
            {
                return;
            }

            var classified = 0;
            var skipped = 0;

            foreach (var card in board.cards)
            {
                try
                {
                    var classification = classifier.ClassifyCard(card, board);

                    if (classification == null)
                    {
                        skipped++;
                    }
                    else
                    {
                        classified++;
                    }

                    if ((classified + skipped) % 10 == 0)
                    {
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Skipped: {0}", skipped);
                        Console.WriteLine("Classified: {0}", classified);
                        Console.WriteLine("Process percentage: {0}",  (skipped+classified) * 100 / board.cards.Count);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
