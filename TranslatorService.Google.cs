﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TrARKSlator
{
    public class TranslatorService_Google : TranslatorService
    {

        public TranslatorService_Google()
        {

            this.Name = "Google";

        }

        override public string DetectLanguage(string text)
        {

            return "auto";

        }

        override public string Translate(string text, ref string from)
        {

            string transText = "";
            string transURL = "https://translate.googleapis.com/translate_a/single?client=gtx&sl=" + from + "&tl=en&dt=t&ie=UTF-8&oe=UTF-8&q=" + HttpUtility.UrlEncode( text );

            try
            {
                using (WebClient client = new WebClient())
                {
                    string transResp = client.DownloadString(transURL);
                    JArray trObject = JArray.Parse(transResp);
                    foreach (JArray line in trObject[0]) transText += line[0].ToString();
                    transText = transText.TrimEnd(Environment.NewLine.ToCharArray());
                }
            }
            catch (Exception e) { transText = text; from = "ERROR"; }

            return transText.Length > 0 ? transText : text;

        }

    }

}
