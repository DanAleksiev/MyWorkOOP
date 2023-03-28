using System;
using System.Linq.Expressions;
using System.Net.Http;
using HtmlAgilityPack;

class Program
    {
    static async System.Threading.Tasks.Task Main(string[] args)
        {
        var url = "https://www.amazon.co.uk/gp/product/B0BLNCNCMZ/ref=ox_sc_act_image_1?smid=AXZ3JQ1GVFPIF&psc=1"; // Replace with the website URL you want to scrape
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        var itemNodes = htmlDocument.DocumentNode.SelectNondes("//h2[@class='item-name']");
        if (itemNodes != null)
            {
            foreach (var itemNode in itemNodes)
                {
                var itemName = itemNode.InnerText.Trim();
                Console.WriteLine(itemName);
                }
            }
        }
    }





