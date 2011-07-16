//
// Helper.cs
//
// Author:
//   Scott Thomas <lunchtimemama@gmail.com>
//
// Copyright (C) 2008 S&S Black Ltd.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace Mono.Upnp.Internal
{
    static class Helper
    {
        public static T Get<T> (WeakReference weakReference)
            where T : new ()
        {
            if (weakReference.IsAlive) {
                return (T)weakReference.Target;
            } else {
                var t = new T ();
                weakReference.Target = t;
                return t;
            }
        }
        
        public static IList<T> MakeReadOnlyCopy<T> (IEnumerable<T> items)
        {
            var list = items as IList<T>;
            if (list == null) {
                if (items == null) {
                    list = new T[0];
                } else {
                    list = new List<T> (items);
                }
            }
            return new ReadOnlyCollection<T> (list);
        }
        
        public static CollectionMap<TKey, TValue> MakeReadOnlyCopy<TKey, TValue> (IEnumerable<TValue> items)
            where TValue : IMappable<TKey>
        {
            var map = new CollectionMap<TKey, TValue> ();
            if (items != null) {
                foreach (var item in items) {
                    map.Add (item);
                }
            }
            map.MakeReadOnly ();
            return map;
        }

        public static bool ReadToNextElement (XmlReader reader)
        {
            while (reader.Read ()) {
                if (reader.NodeType == XmlNodeType.Element) {
                    return true;
                }
            }
            return false;
        }

        public static HttpWebResponse GetResponse (Uri location)
        {
            return GetResponse ((HttpWebRequest)WebRequest.Create (location));
        }

        public static HttpWebResponse GetResponse (Uri location, int retry)
        {
            return GetResponse ((HttpWebRequest)WebRequest.Create (location), retry);
        }

        public static HttpWebResponse GetResponse (HttpWebRequest request)
        {
            return GetResponse (request, 1);
        }

        public static HttpWebResponse GetResponse (HttpWebRequest request, int retry)
        {
            request.Timeout = 30000;
            while(true) {
                try {
                    var response = (HttpWebResponse)request.GetResponse ();
                    if (response.StatusCode == HttpStatusCode.GatewayTimeout) {
                        throw new WebException ("", WebExceptionStatus.Timeout);
                    }
                    return response;
                } catch (WebException e) {
                    if (e.Status == WebExceptionStatus.Timeout && retry > 0) {
                        retry--;
                    } else if (e.Response != null) {
                        return (HttpWebResponse)e.Response;
                    } else {
                        throw;
                    }
                }
            }
        }

        public static string MakeErrorDescription (string errorDescription, string message)
        {
            if (message == null) {
                return errorDescription;
            } else {
                return string.Concat (errorDescription, ": ", message);
            }
        }
        
        public readonly static Encoding UTF8Unsigned = new UTF8Encoding (false);
    }
}
