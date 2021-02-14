using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Dal_Api.DO
{
    public class LineTrip
    {
        public int LineKey { get; set; }
        
        [XmlIgnore]
        public TimeSpan StartAt { get; set; }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "Start_at")]
        public string StartAt_String
        {
            get
            {
                return XmlConvert.ToString(StartAt);
            }
            set
            {
                StartAt = string.IsNullOrEmpty(value) ?
                    TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
    }
}
