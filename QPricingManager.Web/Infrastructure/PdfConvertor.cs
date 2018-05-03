using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace QPricingManager.Web.Infrastructure
{
    public class PdfConvertor
    {
        private static byte[] Html2PdfRocketConvert(string html)
        {
            string apiKey = "1a5eac50-ff02-4f3b-911c-53f17a48b0cd";
            using (var client = new WebClient())
            {
                NameValueCollection options = new NameValueCollection();
                options.Add("apikey", apiKey);
                options.Add("value", html);
                options.Add("UseLandscape", "true");
                options.Add("MarginLeft", "5");
                options.Add("MarginRight", "5");
                options.Add("MarginTop", "2.75");
                options.Add("MarginBottom", "2.75");
                byte[] result = client.UploadValues("http://api.html2pdfrocket.com/pdf", options);
                return result;
            }
        }

        private static string ApplyReportStyles(string html)
        {


            var htmlWithStyles = new StringBuilder();
            htmlWithStyles.Append("<html><head><meta charset=\"UTF-8\"><style type=\"text/css\">");
            htmlWithStyles.Append(System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content/bootstrap.css")));
            htmlWithStyles.Append(System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content/Extended.css")));
            htmlWithStyles.Append("</style></head><body>");
            htmlWithStyles.Append(html);
            htmlWithStyles.Append("</body></html>");

            return htmlWithStyles.ToString();
        }

        public static byte[] ConvertReport(string html, string title = "")
        {
            var rez = GenerateTitle(title) + html;
            rez = ApplyReportStyles(rez);

            return Html2PdfRocketConvert(rez);
        }

        private static string GenerateTitle(string title)
        {
            if (title.EndsWith(".pdf"))
            {
                title = title.Remove(title.Length - 4, 4);
            }
            return string.Format("<h4 style='margin-bottom:5px;'> {0} </h4>", title);
        }
    }
}