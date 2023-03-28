using System;
using System.Net.Http;
using HtmlAgilityPack;

class getPrice
    {
    static async System.Threading.Tasks.Task Main(string[] args)
        {
        var url = "https://piercingmania.co.uk/plugs-and-tunnels"; // Replace with the website URL you want to scrape
        var itemNames = new string[] { "Double-flared silicone ear piercing plugs in bright colours", "Item 2", "Item 3" }; // Replace with the names of the items you want to track
        var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(url);
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        foreach (var itemName in itemNames)
            {
            var itemNode = htmlDocument.DocumentNode.SelectSingleNode($"//div[@class='item' and text()='{itemName}']");
            if (itemNode != null)
                {
                var priceNode = itemNode.SelectSingleNode(".//span[@class='price']");
                if (priceNode != null)
                    {
                    var priceText = priceNode.InnerText.Trim();
                    var price = decimal.Parse(priceText.Substring(1));
                    Console.WriteLine($"{itemName}: {price}");
                    }
                }
            }
        }
    }