using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes

using Crestron.SimplSharp.CrestronXmlLinq;
using Crestron.SimplSharp.CrestronXml;
using Crestron.SimplSharp.CrestronXml.Serialization;
using Crestron.SimplSharp.CrestronIO;

namespace Fusion_XML
{
    public class MainProg
    {

        /// <summary>
        /// SIMPL+ can only execute the default constructor. If you have variables that require initialization, please
        /// use an Initialize method
        /// </summary>
        public MainProg()
        {
            cTimer = new CTimer(timerCallBack, 0);
            cTimer.Reset(0, 1000);
        }

        private CTimer cTimer;

        public event EventHandler<MeetingsArgs> meetingsCallback = delegate { };
        //public event EventHandler<RoomStatusArgs> roomStatusCallback = delegate { };


        public int meetingNum;


        public List<string> scheduleIdList = new List<string>();
        public List<DateTime> scheduleStartList = new List<DateTime>();
        public List<DateTime> scheduleEndList = new List<DateTime>();
        public List<string> scheduleOrganizerList = new List<string>();
        public List<string> scheduleSubjectList = new List<string>();


        private int scheduleOrder = 0;
        public ushort pScheduleOrder
        { set { scheduleOrder = value; } }

        private bool debugging = false;
        public ushort pDebugging
        { set { debugging = value == 1; } }


        public void getMeetings(string respFromFusion)
        {
            try
            {
                if (!string.IsNullOrEmpty(respFromFusion))
                {
                    respFromFusion = respFromFusion.Replace("Event", "Event12");

                    int loc2 = respFromFusion.IndexOf("</ScheduleResponse>");
                    int loc1 = respFromFusion.IndexOf("<Event12>");

                    respFromFusion = respFromFusion.Insert(loc2, "</EventList>");
                    respFromFusion = respFromFusion.Insert(loc1, "<EventList>");

                    byte[] array = Encoding.UTF8.GetBytes(respFromFusion);
                    MemoryStream stream = new MemoryStream(array);

                    ScheduleResponse eventSchedule = CrestronXMLSerialization.DeSerializeObject<ScheduleResponse>(stream);

                    if (eventSchedule is ScheduleResponse)
                    {
                        List<Event12> eventList = eventSchedule.EventList;

                        meetingNum = eventList.Count;

                        scheduleIdList.Clear();
                        scheduleStartList.Clear();
                        scheduleEndList.Clear();
                        scheduleOrganizerList.Clear();
                        scheduleSubjectList.Clear();

                        if (scheduleOrder == 0)     //按照时间顺序进行排列
                        {
                            for (int i = 0; i <= meetingNum - 1; i++)
                            {
                                scheduleIdList.Add(eventList[i].MeetingID);

                                DateTime startTime;
                                startTime = eventList[i].dtStart;
                                scheduleStartList.Add(startTime);
                                //string startTimeS = string.Format("{0:00}:{1:00}", startTime.Hour, startTime.Minute);

                                DateTime endTime;
                                endTime = eventList[i].dtEnd;
                                scheduleEndList.Add(endTime);
                                //string endTimeS = string.Format("{0:00}:{1:00}", endTime.Hour, endTime.Minute);

                                //int confState = meetingState(startTime, endTime);

                                scheduleOrganizerList.Add(eventList[i].Organizer);

                                scheduleSubjectList.Add(eventList[i].Subject);

                                //MeetingsArgs myArg = new MeetingsArgs(meetingNum, i + 1, startTimeS, endTimeS, eventList[i].Subject, eventList[i].Organizer, confState);
                                //if (meetingsCallback != null)
                                    //meetingsCallback(this, myArg);

                                //if (debugging)
                                    //CrestronConsole.PrintLine("Current meeting number: {0}, start time: {1}, end time: {2}, subject: {3}, organizer: {4}, status: {5}", i + 1, startTimeS, endTimeS, eventList[i].Subject, eventList[i].Organizer, confState == 0 ? "Not started" : confState == 1 ? "On going" : "Ended");
                            }
                        }
                        else if (scheduleOrder == 1)    //按照时间倒序进行排列
                        {
                            for (int i = meetingNum - 1; i >= 0; i--)
                            {
                                scheduleIdList.Add(eventList[i].MeetingID);

                                DateTime startTime;
                                startTime = eventList[i].dtStart;
                                scheduleStartList.Add(startTime);
                                //string startTimeS = string.Format("{0:00}:{1:00}", startTime.Hour, startTime.Minute);

                                DateTime endTime;
                                endTime = eventList[i].dtEnd;
                                scheduleEndList.Add(endTime);
                                //string endTimeS = string.Format("{0:00}:{1:00}", endTime.Hour, endTime.Minute);

                                //int confState = meetingState(startTime, endTime);

                                scheduleOrganizerList.Add(eventList[i].Organizer);

                                scheduleSubjectList.Add(eventList[i].Subject);

                                //MeetingsArgs myArg = new MeetingsArgs(meetingNum, meetingNum - i, startTimeS, endTimeS, eventList[i].Subject, eventList[i].Organizer, confState);
                                //if (meetingsCallback != null)
                                    //meetingsCallback(this, myArg);

                                //if (debugging)
                                    //CrestronConsole.PrintLine("Current meeting number: {0}, start time: {1}, end time: {2}, subject: {3}, organizer: {4}, status: {5}", meetingNum - i, startTimeS, endTimeS, eventList[i].Subject, eventList[i].Organizer, confState == 0 ? "Not started" : confState == 1 ? "On going" : "Ended");
                            }
                        }
                    }
                }
                else
                {
                    if (debugging)
                        CrestronConsole.PrintLine("fail|Unexpected error");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error("--------------Error when get schedule: {0}", ex.Message);
                if (debugging)
                    CrestronConsole.PrintLine("--------------Error when get schedule: {0}", ex.Message);
            }
            finally
            {
                checkSchedule();
            }
        }

        public void checkSchedule()
        {
            try
            {
                for (int i = 0; i < scheduleIdList.Count; i++)
                {
                    int confState = meetingState(scheduleStartList[i], scheduleEndList[i]);
                    string startTimeS = string.Format("{0:00}:{1:00}", scheduleStartList[i].Hour, scheduleStartList[i].Minute);
                    string endTimeS = string.Format("{0:00}:{1:00}", scheduleEndList[i].Hour, scheduleEndList[i].Minute);
                    MeetingsArgs myArg = new MeetingsArgs(meetingNum, i, startTimeS, endTimeS, scheduleSubjectList[i], scheduleOrganizerList[i], confState);
                    if (meetingsCallback != null)
                        meetingsCallback(this, myArg);

                    if (debugging)
                        CrestronConsole.PrintLine("Current meeting number: {0}, start time: {1}, end time: {2}, subject: {3}, organizer: {4}, status: {5}", i, startTimeS, endTimeS, scheduleSubjectList[i], scheduleOrganizerList[i], confState == 0 ? "Not started" : confState == 1 ? "On going" : "Ended");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error("------Error when check schedule: {0}", ex.Message);
            }
        }

        /*
        //变更会议信息UI接口（每分钟更新）
        public void checkSchedule()
        {
            try
            {
                if (meetingNum == 0)
                {
                    if (debugging)
                        CrestronConsole.Print("Available for the day");

                    RoomStatusArgs myArg = new RoomStatusArgs(0, 0, 0, 0);

                    if (roomStatusCallback != null)
                        roomStatusCallback(this, myArg);
                }
                else
                {
                    bool found = false;
                    for (int i = 0; i < meetingNum; i++)
                    {
                        if (scheduleStartList[i] < DateTime.Now && scheduleEndList[i] > DateTime.Now)   //Reserved
                        {
                            if (debugging)
                                CrestronConsole.PrintLine("Reserved");

                            found = true;
                            TimeSpan ts = scheduleEndList[i] - DateTime.Now;

                            RoomStatusArgs myArg = new RoomStatusArgs(2, ts.Hours, ts.Minutes, (int)ts.TotalMinutes);

                            if (roomStatusCallback != null)
                                roomStatusCallback(this, myArg);
                        }
                        else if (scheduleStartList[i] > DateTime.Now)   //Avaliable for the next
                        {
                            if (debugging)
                                CrestronConsole.PrintLine("Avaliable for the next");

                            found = true;
                            TimeSpan ts = scheduleStartList[i] - DateTime.Now;

                            RoomStatusArgs myArg = new RoomStatusArgs(1, ts.Hours, ts.Minutes, (int)ts.TotalMinutes);

                            if (roomStatusCallback != null)
                                roomStatusCallback(this, myArg);
                        }
                    }

                    if (!found)
                    {
                        if (debugging)
                            CrestronConsole.Print("Available for the day");

                        RoomStatusArgs myArg = new RoomStatusArgs(0, 0, 0, 0);

                        if (roomStatusCallback != null)
                            roomStatusCallback(this, myArg);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error("------Error when check schedule: {0}", ex.Message);
            }
        }*/

        private int meetingState(DateTime startTime, DateTime endTime)
        {
            if (endTime < DateTime.Now)    //已结束 2
                return 2;
            else if (startTime < DateTime.Now && endTime > DateTime.Now)   //正进行 1
                return 1;
            else if (startTime > DateTime.Now)   //未开始 0
                return 0;
            else
                return 2;
        }

        private void timerCallBack(object unused)
        {
            if (DateTime.Now.Second == 0)
                checkSchedule();
        }
    }

    public class MeetingsArgs : EventArgs
    {
        public ushort listCount;
        public ushort meetingNum;
        public string startTime;
        public string endTime;
        public string subject;
        public string organizer;
        public ushort confState;   //0:未开始、1：进行中、2：已结束

        public MeetingsArgs() { }

        public MeetingsArgs(int listCount, int meetingNum, string startTime, string endTime, string subject, string organizer, int confState)
        {
            this.listCount = (ushort)listCount;
            this.meetingNum = (ushort)meetingNum;
            this.startTime = startTime;
            this.endTime = endTime;
            this.subject = subject;
            this.organizer = organizer;
            this.confState = (ushort)confState;
        }
    }

    /*
    public class RoomStatusArgs : EventArgs
    {
        public int currentStatus;   //0：Avaliable for the day、1：Avaliable for the next、2：Reserved
        public int dispHour;
        public int dispMinute;
        public int totalMinute;

        public RoomStatusArgs() { }

        public RoomStatusArgs(int currentStatus, int dispHour, int dispMinute, int totalMinute)
        {
            this.currentStatus = currentStatus;
            this.dispHour = dispHour;
            this.dispMinute = dispMinute;
            this.totalMinute = totalMinute;
        }
    }*/
}
