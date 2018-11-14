using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MojoUnity;

namespace KitsuneEy
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  _  _______ _                          ______      \r\n" +
                              " | |/ /_   _| |                        |  ____|     \r\n" +
                              " |   /  | | | |_ ___ _   _ _ __   ___  | |__  _   _ \r\n" +
                              " |  <   | | | __/ __| | | |  _ \\ / _ \\ |  __|| | | |\r\n" +
                              " |   \\ _| |_| |_\\__ \\ |_| | | | | /__/ | |___| |_| |\r\n" +
                              " |_|\\_\\_____|\\__|___/\\__ _|_| |_|\\___| |______\\__  |\r\n" +
                              "                                               __/ |\r\n" +
                              "                                              |___/ ");

            var mHeaders = RouteEy.GetPage(args[0]).Headers;
            var mKeys = mHeaders.AllKeys.ToList().ConvertAll(x => x.ToLower());
            List<string> baseDict = File.ReadAllLines("base.ey").ToList();

            if (args.ToList().Contains("-i"))
                Console.WriteLine(mHeaders.ToString());
            if (args.ToList().Contains("-noRedirect") || args.ToList().Contains("-nR"))
                mHeaders = RouteEy.GetPage(args[0], false).Headers;

            if (mKeys.Contains("server")|| mKeys.Contains("x-powered-by"))
            {
                Console.WriteLine("——————————————");
                if (mKeys.Contains("server"))
                    Console.WriteLine("Server." + ResponseEy.GetWebServer(mHeaders));
                if (mKeys.Contains("x-powered-by"))
                    Console.WriteLine("Powered." + ResponseEy.GetXPoweredBy(mHeaders));
            }
            Console.WriteLine("——————————————");

            string line = " ";
            int i = 1;
            try
            {
                foreach (var item in baseDict)
                {
                    line = i++ + " / " + baseDict.Count;
                    if (string.IsNullOrEmpty(item) || item.Contains("#"))
                        continue;

                    var jItem = Json.Parse(item);
                    var jFind = jItem.AsObjectGet("find");
                    var jApp = jItem.AsObjectGetString("app");
                    var jType = jItem.AsObjectGetString("type");

                    switch (jType)
                    {
                        //Response

                        case ("Response.Item.Contains"):
                            if (ResponseEy.GetItemContains(mHeaders,jFind.AsObjectGetString("grep").ToLower()))
                                Console.WriteLine("HeadersFound : " + jApp);
                            break;
                        case ("Response.Context.Contains"):
                            if (ResponseEy.GetContextContains(mHeaders, jFind.AsObjectGetString("grep").ToLower()))
                                Console.WriteLine("HeadersFound : " + jApp);
                            break;
                        case ("Response.WebServer.Contains"):
                            if (ResponseEy.GetItemContains(mHeaders, "server"))
                                if (ResponseEy.GetWebServerContains(mHeaders, jFind.AsObjectGetString("grep").ToLower()))
                                    Console.WriteLine("HeadersFound : " + jApp);
                            break;
                        case ("Response.Cookie.Contains"):
                            if (ResponseEy.GetItemContains(mHeaders, "set-cookie"))
                                if (ResponseEy.GetCookieContains(mHeaders, jFind.AsObjectGetString("grep").ToLower()))
                                    Console.WriteLine("HeadersFound : " + jApp);
                            break;
                        case ("Response.Item.Value.Contains"):
                            if (ResponseEy.GetItemContains(mHeaders,jFind.AsObjectGetString("item").ToLower()))
                                if (ResponseEy.GetItemValueContains(mHeaders, jFind.AsObjectGetString("item").ToLower(), jFind.AsObjectGetString("grep").ToLower()))
                                    Console.WriteLine("HeadersFound : " + jApp);
                            break;

                        //Index

                        default:
                            Console.WriteLine($"NotSupport : {jType} | {jApp} ({line})");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(line);
                Console.WriteLine(e);
            }
        }
    }
}
