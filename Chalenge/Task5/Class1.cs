using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MathNet.Numerics.Random;
using MathNet.Numerics.Distributions;

namespace Task5
{
    public static class Class1
    {
        //string dsf = "https://www.random.org/integers/?num=10&min=1&max=6&col=1&base=10&format=plain&rnd=new";
        private static Queue<int> _integers;
        private static bool _queueIsFiled = false;
        private static int _range;
        private static bool _error;
        static Class1()
        {
            _range = 10;
            _error = false;
            _integers = new Queue<int>();
        }

        public static void SetRange(int num)
        {
            _range = num;
            _queueIsFiled = false;
        }
        public static int GetRandInteger()
        {
            if (!_queueIsFiled)
            {
                try
                {
                    _integers = GetRandIntegers(_range);
                    _queueIsFiled = true;
                    _error = false;
                    _queueIsFiled = true;
                    int result = _integers.Dequeue();
                    return result;
                }
                catch (Exception e)
                {
                    _error = true;
                    int ret = RandomSeed.Guid() % _range;
                    if (ret < 0) return ret * -1;
                }

                
            }
            else
            {
                if (_integers.Any() && !_error)
                {
                    return _integers.Dequeue();
                }
            }
            int rect = RandomSeed.Guid() % _range;
            if (rect < 0)
            {
                return rect * -1;
            }
            else
            {
                return rect;
            }
        }
        private static async Task<Queue<int>> CallAsync(string endpoint, dynamic request, bool specialQuery = false)
        {
            if (specialQuery)
            {
                return !string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(request)
                    ? await Get(endpoint + request, false)
                    : null;
            }

            return !string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(request)
                ? await Get(string.Format("{0}?q={1}", endpoint, request), true)
                : null;
        }

        private static async Task<Queue<int>> Get(string url, bool isCollection)
        {
            const int timeout = 10;
            const int readWriteTimeout = 50;

            if (!string.IsNullOrEmpty(url))
            {
                var headers = new WebHeaderCollection();

                var hr = (HttpWebRequest) WebRequest.Create(url);
                hr.Timeout = timeout * 1000;
                hr.ReadWriteTimeout = readWriteTimeout * 1000;
                hr.AllowAutoRedirect = true;
                hr.MaximumAutomaticRedirections = 10;
                hr.Headers = headers;

                try
                {
                    var result = await hr.GetResponseAsync();

                    var stream = result?.GetResponseStream();
                    if (stream != null)
                    {
                        using (var sr = new StreamReader(stream))
                        {
                            var response = sr.ReadToEnd();
                            string[] sf = response.Split(new string[] { "\n" }, StringSplitOptions.None);

                            Queue<int> collection = new  Queue<int>(sf.Length - 1);

                            for (int i = 0; i < sf.Length - 1; i++)
                            {
                                collection.Enqueue(int.Parse(sf[i]));
                            }
                            return collection;
                        }
                    }
                }
                catch (WebException e)
                {
                    throw e;
                }
            }

            return null;
        }

        private static Queue<int> GetRandIntegers(int num)
        {
            try
            {
                var response =
                    CallAsync("https://www.random.org/integers/",
                        $"?num=10000&min=0&max={num}&col=1&base=10&format=plain&rnd=new", true).Result;

                return response;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
