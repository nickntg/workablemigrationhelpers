using System;
using System.Text.RegularExpressions;
using MsgReader.Mime;
using Workable.Core.Migration.Gmail;
using Workable.Core.Migration.Gmail.Interfaces;

namespace Workable.Gmail.Preprocessor.Etravel
{
    /// <summary>
    /// This classifier is probably unique to e-Travel. You should probably write your own.
    /// </summary>
    public class Classifier : IMailClassifier
    {
        private const string TitleStarter = "Τίτλος Αγγελίας: ";
        private const string TitleRegex = "\\s*([^\\n\\r]*)";
        private const string DetailsStarter = "Λεπτομέρειες Θέσης Εργασίας: ";
        private const string DetailsRegex = "\\s*([^\\n\\r]*)";
        private const string NameStarter = "Όνομα: ";
        private const string NameRegEx = "\\s*([^\\n\\r]*)";
        private const string TelephoneStarter = "Τηλέφωνο: ";
        private const string TelephoneRegex = "\\s*([^\\n\\r]*)";
        private const string EmailStarter = "Email: ";
        private const string EmailRegex = "\\s*([^\\n\\r]*)";
        private const string CoverLetter = "Συνοδευτική Επιστολή(.*)Βιογραφικό";
        private const string NoCoverLetter = "Δεν έχει υποβληθεί συνοδευτική επιστολή";

        public Classification ClassifyMail(Message mail)
        {
            if (mail.TextBody == null || mail.TextBody.Body == null)
                return null;

            var content = System.Text.Encoding.UTF8.GetString(mail.TextBody.Body);

            var title = GetSingleMatch(TitleStarter, TitleRegex, content, false);
            if (string.IsNullOrEmpty(title))
                return null;

            var details = GetSingleMatch(DetailsStarter, DetailsRegex, content, false);
            if (string.IsNullOrEmpty(details))
            {
                Show(content);
                return null;
            }

            var name = GetSingleMatch(NameStarter, NameRegEx, content, true);
            if (string.IsNullOrEmpty(name))
            {
                Show(content);
                return null;
            }

            var elstr = new ElStr();
            name = elstr.ToLatin(name);

            var phone = GetSingleMatch(TelephoneStarter, TelephoneRegex, content, false);

            var email = GetSingleMatch(EmailStarter, EmailRegex, content, true);
            if (string.IsNullOrEmpty(email))
            {
                Show(content);
                return null;
            }

            var cover = GetSingleMatch(string.Empty, CoverLetter, content, false, RegexOptions.Singleline);
            if (string.IsNullOrEmpty(cover) || cover.Contains(NoCoverLetter))
            {
                cover = string.Empty;
            }
            else
            {
                cover =
                    cover.Replace("Συνοδευτική Επιστολή", string.Empty)
                        .Replace("Βιογραφικό", string.Empty)
                        .Replace("------------", string.Empty);
            }

            var classification = new Classification
            {
                AdUrl = details,
                CoverLetter = cover,
                Job = title,
                Name = name,
                Phone = phone,
                Submitted = mail.Headers.DateSent,
                Mail = email
            };

            if (mail.Attachments != null && mail.Attachments.Count > 0)
            {
                classification.AttachmentName = mail.Attachments[0].FileName;
                classification.Attachment = Convert.ToBase64String(mail.Attachments[0].Body);
            }

            return classification;
        }

        private void Show(string content)
        {
            Console.Clear();
            Console.WriteLine(content);
            Console.WriteLine("========PAUSED==========");
            Console.ReadKey();
            Console.Clear();
        }

        private string GetSingleMatch(string starter, string regex, string content, bool acceptMultiple, RegexOptions options = RegexOptions.None)
        {
            var rex = new Regex(starter + regex, options);
            var matches = rex.Matches(content);
            if (matches.Count != 1)
            {
                if (matches.Count == 0 || !acceptMultiple)
                {
                    return string.Empty;                    
                }
            }

            return string.IsNullOrEmpty(starter) ? matches[0].ToString() : matches[0].ToString().Replace(starter, string.Empty);
        }
    }
}