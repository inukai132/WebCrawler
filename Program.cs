using System;
using System.IO;
using System.Net;
using System.Xml.Xsl;

namespace WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            var wc = new WebCrawler(IPAddress.Parse("122.100.0.155"));
            using (var fs = new StreamWriter("./log/list.csv", true))
                fs.WriteLine(wc.GetReport());
        }
    }

    /// <summary>
    /// This class will connect to an IP and get the Domain Name from it.
    /// The class also is also responsible for reporting the IP and DN. 
    /// </summary>
    class WebCrawler
    {
        private readonly IPAddress _server;
        private string _domainName;

        private WebCrawler()
        {
        }

        public WebCrawler(IPAddress server)
        {
            _server = server;
            GetName();
        }

        public void GetName()
        {
#if DEBUG
            Console.WriteLine("Looking Up: {0}",_server.ToString());
#endif
            _domainName = Dns.GetHostEntry(_server).HostName;
#if DEBUG
            Console.WriteLine("IP {0} Resolved to Domain Name {1}",_server.ToString(),_domainName);
#endif
        }

        public string GetReport()
        {
            if (!String.IsNullOrEmpty(_domainName))
                return String.Format("{0},{1}", _server.ToString(), _domainName);
            else
                return String.Format("{0},{1}", _server.ToString(), "Could Not Resolve");
        }

    }
}
