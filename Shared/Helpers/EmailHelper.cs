using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public class EmailHelper
    {
        public static string Template(string template, Dictionary<string, string> dictionary)
        {
            string emailTemplatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", template);
            string emailTemplate = File.ReadAllText(emailTemplatePath);
            foreach (var item in dictionary)
                emailTemplate = emailTemplate.Replace(item.Key, item.Value);
            return emailTemplate;
        }

        public static Dictionary<string, string> General(List<string> recipients, string body)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("{body}", body);
            if (recipients.Count() > 1) dictionary.Add("{email}", "");
            else dictionary.Add("{email}", recipients[0]);
            return dictionary;
        }
    }
}
