// 
// UpdateXmlSerializer.cs
//  
// Author:
//       Scott Peterson <lunchtimemama@gmail.com>
// 
// Copyright (c) 2010 Scott Peterson
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
using System.IO;
using System.Text;
using System.Xml;

using Mono.Upnp.Internal;
using Mono.Upnp.Xml;
using Mono.Upnp.Xml.Compilation;

namespace Mono.Upnp.Dcp.MediaServer1.Xml
{
    public class UpdateXmlSerializer
    {
        readonly XmlSerializer<UpdateContext> xml_serializer;
        
        public UpdateXmlSerializer ()
            : this (null)
        {
        }
        
        public UpdateXmlSerializer (SerializationCompilerProvider<UpdateContext> compilerProvider)
        {
            if (compilerProvider == null) {
                compilerProvider = (serializer, type) => new UpdateDelegateSerializationCompiler (serializer, type);
            }

            this.xml_serializer = new XmlSerializer<UpdateContext> (compilerProvider);
        }
        
        public bool Serialize<T> (T obj1, T obj2, Stream stream)
        {
            return Serialize (obj1, obj2, stream, null);
        }
        
        public bool Serialize<T> (T obj1, T obj2, Stream stream, XmlSerializationOptions options)
        {
            if (stream == null) {
                throw new ArgumentNullException ("stream");
            }
            
            var encoding = options != null ? options.Encoding ?? Helper.UTF8Unsigned : Helper.UTF8Unsigned;
            var update_writer = new UpdateTextWriter (new StreamWriter (stream, encoding));
            var context = new UpdateContext (obj2, stream, encoding);
            using (var xml_writer = XmlWriter.Create (update_writer, new XmlWriterSettings {
                Encoding = encoding, OmitXmlDeclaration = true })) {
                xml_serializer.Serialize (obj1, xml_writer, context);
            }
            return context.Delineated;
        }
    }
}
