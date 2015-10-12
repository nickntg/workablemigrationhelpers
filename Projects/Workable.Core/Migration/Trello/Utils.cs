using System;
using System.Linq;

namespace Workable.Core.Migration.Trello
{
    public class Utils
    {
        public Decision DetermineStage(Card card, Board board)
        {
            var list = board.lists.FirstOrDefault(x => x.id == card.idList);
            if (list == null)
            {
                throw new InvalidOperationException(string.Format("Could not find list {0}", card.idList));
            }

            switch (list.name.ToLower())
            {
                case "developers":
                    return new Decision {IsDisqualified = false, Stage = "applied"};
                case "to test":
                    return new Decision { IsDisqualified = false, Stage = "case study" };
                case "pending test answers":
                    return new Decision { IsDisqualified = false, Stage = "case study" };
                case "pending review":
                    return new Decision { IsDisqualified = false, Stage = "case study" };
                case "interview":
                    return new Decision { IsDisqualified = false, Stage = "interview" };
                case "rejection letters":
                    return new Decision { IsDisqualified = true, Stage = "rejected" };
                case "on trial":
                    return new Decision { IsDisqualified = false, Stage = "trial" };
                case "no hire":
                    return new Decision { IsDisqualified = true, Stage = "rejected" };
                case "hired":
                    return new Decision { IsDisqualified = false, Stage = "hired" };
                case "feedback":
                    return new Decision { IsDisqualified = true, Stage = "rejected" };
                case "?":
                    return new Decision { IsDisqualified = false, Stage = "sourced" };
                default:
                    throw new InvalidOperationException(string.Format("Unknown list {0}", list.name));
            }
        }
    }
}
