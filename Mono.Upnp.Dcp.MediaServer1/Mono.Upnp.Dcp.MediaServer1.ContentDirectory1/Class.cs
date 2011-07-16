// 
// Class.cs
//  
// Author:
//       Scott Peterson <lunchtimemama@gmail.com>
// 
// Copyright (c) 2009 Scott Peterson
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

using Mono.Upnp.Xml;

namespace Mono.Upnp.Dcp.MediaServer1.ContentDirectory1
{
    public class Class : IEquatable<Class>
    {
        protected Class ()
        {
        }
        
        public Class (string fullClassName)
            : this (fullClassName, null)
        {
        }
        
        public Class (string fullClassName, string friendlyClassName)
        {
            if (fullClassName == null) {
                throw new ArgumentNullException ("fullClassName");
            }

            FullClassName = fullClassName;
            FriendlyClassName = friendlyClassName;
        }
        
        [XmlAttribute ("name", OmitIfNull = true)]
        public virtual string FriendlyClassName { get; protected set; }
        
        [XmlValue]
        public virtual string FullClassName { get; protected set; }

        public override bool Equals (object obj)
        {
            return Equals (obj as Class);
        }

        public bool Equals (Class @class)
        {
            return @class != null && @class.FullClassName == FullClassName;
        }

        public override int GetHashCode ()
        {
            return FullClassName.GetHashCode ();
        }
    }
}
