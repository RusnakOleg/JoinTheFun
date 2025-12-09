using JoinTheFun.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JoinTheFun.BLL.Services
{
    public class TextCleanerService : ITextCleanerService
    {
        public string Clean(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            text = text.ToLower();

            text = Regex.Replace(text, @"[^a-zа-щґєіїюя0-9!?\*@#%'\-\s]", " ");
            text = Regex.Replace(text, @"\s+", " ").Trim();

            return text;
        }
    }
}
