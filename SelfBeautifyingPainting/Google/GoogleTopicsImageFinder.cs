//#define _USE_GOOGLE_API
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google;
using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;

namespace SelfBeautifyingPainting.Google
{
    class GoogleTopicsImageFinder
    {
        private string api_key = "AIzaSyDoxeH06S1fMBmbY3pEEOJGfTZBzA2bbik";

        private string cx = "006390233743907945299:psnsr5yd-ys";
           
        private RequestBuilder requestBuilder;
        private int width, height;

        public GoogleTopicsImageFinder(int w, int h)
        {
            width = w;
            height = h;
            requestBuilder = new RequestBuilder();
        }

        private Random rnd = new Random();

        private string GetHtmlCode(string topic)
        {
            string url = "https://www.google.com/search?q=" + topic + "&tbm=isch";
            string data = "";

            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return "";
                using (var sr = new StreamReader(dataStream))
                {
                    data = sr.ReadToEnd();
                }
            }
            return data;
        }

        private byte[] GetImage(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            using (Stream dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    byte[] bytes = sr.ReadBytes(100000);

                    return bytes;
                }
            }

            return null;
        }

        private IEnumerable<string> GetUrls(string topicOrHtml)
        {
#if _USE_GOOGLE_API
            var cs = new CustomsearchService(new BaseClientService.Initializer() {ApiKey = api_key, ApplicationName = "SelfBeautifyingPainting" });
            

            var listRequest = cs.Cse.List(topicOrHtml);
            listRequest.Cx = cx;
            //listRequest.SearchType = CseResource.ListRequest.SearchTypeEnum.Image;
            //listRequest.ExactTerms = topic;
            listRequest.Key = api_key;
            listRequest.Googlehost = "https://www.google.com";
            listRequest.ImgColorType=CseResource.ListRequest.ImgColorTypeEnum.Color;
            listRequest.FileType = "jpg";

            var search = listRequest.Execute();
           
            foreach (var result in search.Items)
            {
                yield return result.Image.ContextLink;
            }


#else
             var urls = new List<string>();
             int ndx = topicOrHtml.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
             ndx = topicOrHtml.IndexOf("<img", ndx, StringComparison.Ordinal);

             while (ndx >= 0)
             {
                 ndx = topicOrHtml.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                 ndx = ndx + 5;
                 int ndx2 = topicOrHtml.IndexOf("\"", ndx, StringComparison.Ordinal);
                 string url = topicOrHtml.Substring(ndx, ndx2 - ndx);
                 urls.Add(url);
                 ndx = topicOrHtml.IndexOf("<img", ndx, StringComparison.Ordinal);
             }
             return urls;
#endif
        }




        public Bitmap GetPicture(string topic)
        {
#if _USE_GOOGLE_API
            string topicOrHtml =topic;
#else
            string topicOrHtml = GetHtmlCode(topic);
#endif
            List<string> urls = new List<string>(GetUrls(topicOrHtml));;

            int randomUrl = rnd.Next(0, urls.Count);

            string luckyUrl = urls[randomUrl];

            byte[] image = GetImage(luckyUrl);

            using (var ms = new MemoryStream(image))
            {
                var img = Image.FromStream(ms);
                return new Bitmap(img, width, height);
                //return new Bitmap(img,img.Width,img.Height);
            }
        }
    }
}
