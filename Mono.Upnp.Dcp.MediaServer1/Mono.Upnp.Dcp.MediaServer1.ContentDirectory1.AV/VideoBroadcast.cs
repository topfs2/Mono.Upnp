// 
// VideoBroadcast.cs
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

namespace Mono.Upnp.Dcp.MediaServer1.ContentDirectory1.AV
{
    public class VideoBroadcast : VideoItem
    {
        protected VideoBroadcast ()
        {
        }
        
        public VideoBroadcast (string id, string parentId, VideoBroadcastOptions options)
            : base (id, parentId, options)
        {
            Icon = options.Icon;
            Region = options.Region;
            ChannelNr = options.ChannelNr;
        }

        protected void CopyToOptions (VideoBroadcastOptions options)
        {
            base.CopyToOptions (options);

            options.Icon = Icon;
            options.Region = Region;
            options.ChannelNr = ChannelNr;
        }

        public new VideoBroadcastOptions GetOptions ()
        {
            var options = new VideoBroadcastOptions ();
            CopyToOptions (options);
            return options;
        }
        
        [XmlElement ("icon", Schemas.UpnpSchema, OmitIfNull = true)]
        public virtual Uri Icon { get; protected set; }
        
        [XmlElement ("region", Schemas.UpnpSchema, OmitIfNull = true)]
        public virtual string Region { get; protected set; }
        
        [XmlElement ("channelNr", Schemas.UpnpSchema, OmitIfNull = true)]
        public virtual int? ChannelNr { get; protected set; }
    
        protected override void DeserializeElement (XmlDeserializationContext context)
        {
            context.AutoDeserializeElement (this);
        }

        protected override void SerializeMembers (XmlSerializationContext context)
        {
            AutoSerializeMembers (this, context);
        }
    }
}
