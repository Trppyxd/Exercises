using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using CsvHelper;
using HtmlAgilityPack;

namespace WebScraper
{
    class Scraper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();
        private int _id = new int();

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public void ScrapeData(string page)
        {
            try
            {
                //if (!Regex.Match(page, @"/page/./").Success)
                //    page = "https://www.skidrowreloaded.com/";
                int pageNumber = 1;
                Id = _entries.Count + 1;
                var web = new HtmlWeb();

                bool loop = true;
                while (loop)
                {
                    if (page.StartsWith("https://www.skidrowreloaded.com"))
                    {
                        pageNumber++;
                        if (page != "https://www.skidrowreloaded.com")
                            page = $"https://www.skidrowreloaded.com/page/{pageNumber}/";
                        var doc = web.Load(page);

                        var articles = doc.DocumentNode.SelectNodes("//*[@class = 'post']");
                        if (articles.Count == 0)
                            loop = false;

                        foreach (var article in articles)
                        {
                            var link = HttpUtility.HtmlDecode(article.SelectSingleNode("h2/a").GetAttributeValue("href", "Link not found."));
                            var header = HttpUtility.HtmlDecode(article.SelectSingleNode("h2").InnerText);
                            if (_entries.Any(x => x.Link == link))
                                continue;

                            var metaDate = HttpUtility.HtmlDecode(article.SelectSingleNode("div[@class = 'meta']").InnerText);
                            var iStart = metaDate.IndexOf("Posted ") + 7;
                            var date = metaDate.Substring(iStart, metaDate.IndexOf(" in") - iStart);

                            //Debug.Print($"Id: {Id}\n" +
                            //            $"Title: {header}\n" +
                            //            $"Date: {date}\n" +
                            //            $"Link: {link}");

                            _entries.Add(new EntryModel { Id = Id, Title = header, Date = date, Link = link });

                            Id = _entries.Count + 1;
                        }

                        pageNumber++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void Export()
        {
            using (TextWriter tw = File.CreateText("SampleData.csv"))
            {
                using (var cw = new CsvWriter(tw))
                {
                    foreach (var entry in Entries)
                    {
                        cw.WriteRecord(entry);
                        cw.NextRecord();
                    }
                }
            }
        }
    }
}
