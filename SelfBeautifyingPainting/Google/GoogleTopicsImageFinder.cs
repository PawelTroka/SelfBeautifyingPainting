//#define _USE_GOOGLE_API

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using Google.Apis.Requests;

namespace SelfBeautifyingPainting.Google
{
    internal class GoogleTopicsImageFinder
    {
        private readonly int height;

        private readonly Random rnd = new Random();
        private readonly int width;
        private string api_key = "AIzaSyDoxeH06S1fMBmbY3pEEOJGfTZBzA2bbik";

        private string cx = "006390233743907945299:psnsr5yd-ys";

        private RequestBuilder requestBuilder;

        public GoogleTopicsImageFinder(int w, int h)
        {
            width = w;
            height = h;
            requestBuilder = new RequestBuilder();
        }

        private string GetHtmlCode(string topic)
        {
            var url = "https://www.google.com/search?q=" + topic + "&tbm=isch";
            var data = "";

            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();

            using (var dataStream = response.GetResponseStream())
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
            var request = (HttpWebRequest) WebRequest.Create(url);
            var response = (HttpWebResponse) request.GetResponse();

            using (var dataStream = response.GetResponseStream())
            {
                if (dataStream == null)
                    return null;
                using (var sr = new BinaryReader(dataStream))
                {
                    var bytes = sr.ReadBytes(100000);

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
            var ndx = topicOrHtml.IndexOf("class=\"images_table\"", StringComparison.Ordinal);
            ndx = topicOrHtml.IndexOf("<img", ndx, StringComparison.Ordinal);

            while (ndx >= 0)
            {
                ndx = topicOrHtml.IndexOf("src=\"", ndx, StringComparison.Ordinal);
                ndx = ndx + 5;
                var ndx2 = topicOrHtml.IndexOf("\"", ndx, StringComparison.Ordinal);
                var url = topicOrHtml.Substring(ndx, ndx2 - ndx);
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
            var topicOrHtml = GetHtmlCode(topic);
#endif
            var urls = new List<string>(GetUrls(topicOrHtml));
            ;

            var randomUrl = rnd.Next(0, urls.Count);

            var luckyUrl = urls[randomUrl];

            var image = GetImage(luckyUrl);

            using (var ms = new MemoryStream(image))
            {
                var img = Image.FromStream(ms);
                return new Bitmap(img, width, height);
                //return new Bitmap(img,img.Width,img.Height);
            }
        }
    }
}