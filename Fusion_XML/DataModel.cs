using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.CrestronXmlLinq;
using Crestron.SimplSharp.CrestronXml;
using Crestron.SimplSharp.CrestronXml.Serialization;

namespace Fusion_XML
{
    public class ScheduleResponse
    {
        public string RequestID { get; set; }
        public string RoomID { get; set; }
        public string RoomName { get; set; }
        public List<Event12> EventList { get; set; }

        public ScheduleResponse()
        {
            EventList = new List<Event12>();
        }

    }

    public class Event12
    {
        public string MeetingID { get; set; }
        public string RVMeetingID { get; set; }
        public string Recurring { get; set; }
        public string InstanceID { get; set; }
        public DateTime dtStart { get; set; }
        public DateTime dtEnd { get; set; }
        public string Subject { get; set; }
        public string Organizer { get; set; }
        public string MeetingPresets { get; set; }
        public string HaveAttendees { get; set; }
        public string HaveResources { get; set; }
    }
}