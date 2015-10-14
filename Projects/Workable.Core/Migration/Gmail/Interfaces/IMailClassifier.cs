using MsgReader.Mime;

namespace Workable.Core.Migration.Gmail.Interfaces
{
    public interface IMailClassifier
    {
        Classification ClassifyMail(Message mail);
    }
}