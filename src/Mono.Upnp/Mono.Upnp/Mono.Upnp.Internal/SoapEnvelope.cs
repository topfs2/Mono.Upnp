// 
// SoapEnvelope.cs
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

namespace Mono.Upnp.Internal
{
    [XmlType ("Envelope", Protocol.SoapEnvelopeSchema, "s")]
    class SoapEnvelope<T> : SoapEnvelope<T, T>
    {
        protected SoapEnvelope ()
        {
        }
        
        public SoapEnvelope (T body)
            : base (body)
        {
        }
    }
    
    [XmlType ("Envelope", Protocol.SoapEnvelopeSchema, "s")]
    class SoapEnvelope<THeader, TBody>
    {
        readonly SoapHeader<THeader> header;
        readonly TBody body;
        
        protected SoapEnvelope ()
        {
        }
        
        public SoapEnvelope (TBody body)
            : this (null, body)
        {
        }
        
        public SoapEnvelope (SoapHeader<THeader> header, TBody body)
        {
            this.header = header;
            this.body = body;
        }
        
        [XmlAttribute ("encodingStyle", Protocol.SoapEnvelopeSchema)]
        public string EncodingStyle {
            get { return Protocol.SoapEncodingSchema; }
        }
        
        [XmlElement (OmitIfNull = true, Namespace = Protocol.SoapEnvelopeSchema)]
        public SoapHeader<THeader> Header {
            get { return header; }
        }
        
        [XmlElement (Namespace = Protocol.SoapEnvelopeSchema)]
        public TBody Body {
            get { return body; }
        }
    }
}