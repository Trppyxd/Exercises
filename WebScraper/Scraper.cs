using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using CsvHelper;
using HtmlAgilityPack;
using Microsoft.Win32;

namespace WebScraper
{
    class Scraper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();
        private int _id = new int();
        private int _pageAmount;
        //private volatile bool _isRunning;

        //public bool IsRunning
        //{
        //    get { return _isRunning; }
        //    set { _isRunning = value; }
        //}

        public int PageAmount
        {
            get { return  _pageAmount; }
            set {  _pageAmount = value; }
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
                
                int pageNumber = 1;
                _id = _entries.Count + 1;
                var web = new HtmlWeb();
                //_pageAmount = 0;

                if (page.Contains("/page/"))
                {
                    pageNumber = Int32.Parse(Regex.Match(page, @"(?=.+)\d+").ToString());
                }
                var startPageNumber = pageNumber;

                var finalPageNumber = 1;
                if (_pageAmount != 0)
                {
                    finalPageNumber = startPageNumber + _pageAmount - 1;
                }

                bool loop = true;
                while (loop)
                {
                    if (page.Contains("skidrowreloaded.com"))
                    {
                        if (pageNumber == finalPageNumber)
                            loop = false;

                        page = $"https://www.skidrowreloaded.com/page/{pageNumber}/";
                        var doc = web.Load(page);

                        if (doc.DocumentNode.SelectNodes("//*[@class = 'post']").Count == 0)
                            loop = false;

                        var articles = doc.DocumentNode.SelectNodes("//*[@class = 'post']");

                        foreach (var article in articles)
                        {
                            var link = HttpUtility.HtmlDecode(article.SelectSingleNode("h2/a").GetAttributeValue("href", "Link not found."));
                            var header = HttpUtility.HtmlDecode(article.SelectSingleNode("h2").InnerText);
                            if (_entries.Any(x => x.Link == link))
                                continue;

                            var metaDate = HttpUtility.HtmlDecode(article.SelectSingleNode("div[@class = 'meta']").InnerText);
                            var iStart = metaDate.IndexOf("Posted ") + 7;
                            var date = metaDate.Substring(iStart, metaDate.IndexOf(" in") - iStart);
                            
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                            _entries.Add(new EntryModel { Id = _id, Title = header, Date = date, Link = link });
                            });

                            _id = _entries.Count + 1;
                        }
                        pageNumber++;
                        // DEBUG
                        //System.Threading.Thread.Sleep(300); // Delay between HTTP requests.
                    }
                }
            }
            catch { }
        }

        public void Export()
        {
            if (_entries.Count != 0)
            {
                using (TextWriter tw = File.CreateText("SampleData.csv"))
                {
                    using (var cw = new CsvWriter(tw))
                    {
                        cw.WriteHeader<EntryModel>();
                        cw.NextRecord();
                        foreach (var entry in _entries)
                        {
                            cw.WriteRecord(entry);
                            cw.NextRecord();
                        }
                    }
                }
            }
        }

        public void Import(string file)
        {
            if (File.Exists(file))
            {
                using (TextReader tr = File.OpenText(file))
                {
                    using (var cw = new CsvReader(tr))
                    {
                        _entries.Clear();

                        var records = cw.GetRecords<EntryModel>();
                        foreach (var entry in records)
                        {
                            _entries.Add(entry);
                        }
                    }
                }
            }
        }
    }
}
