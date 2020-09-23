namespace Fusion_XML;
        // class declarations
         class ScheduleResponse;
         class Event12;
         class MainProg;
         class MeetingsArgs;
     class ScheduleResponse 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING RequestID[];
        STRING RoomID[];
        STRING RoomName[];
    };

     class Event12 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING MeetingID[];
        STRING RVMeetingID[];
        STRING Recurring[];
        STRING InstanceID[];
        STRING Subject[];
        STRING Organizer[];
        STRING MeetingPresets[];
        STRING HaveAttendees[];
        STRING HaveResources[];
    };

     class MainProg 
    {
        // class delegates

        // class events
        EventHandler meetingsCallback ( MainProg sender, MeetingsArgs e );

        // class functions
        FUNCTION getMeetings ( STRING respFromFusion );
        FUNCTION checkSchedule ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER meetingNum;

        // class properties
        INTEGER pScheduleOrder;
        INTEGER pDebugging;
    };

     class MeetingsArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER listCount;
        INTEGER meetingNum;
        STRING startTime[];
        STRING endTime[];
        STRING subject[];
        STRING organizer[];
        INTEGER confState;

        // class properties
    };

