using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;
using Fusion_XML;

namespace UserModule_FUSION_SCHEDULE_INFO
{
    public class UserModuleClass_FUSION_SCHEDULE_INFO : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput ENABLE_DEBUG;
        Crestron.Logos.SplusObjects.BufferInput SCHEDULERESPONSE__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput MEETING_NUMBER;
        InOutArray<Crestron.Logos.SplusObjects.AnalogOutput> MEETING_STATE;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> START_TIME;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> END_TIME;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> SUBJECT;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> ORGANIZER;
        Fusion_XML.MainProg FUSIONXML;
        UShortParameter BUFFER_INPUT_WAIT_TIME;
        UShortParameter SCHEDULE_ORDER;
        ushort SCHEDULING_BUFFER_INPUT_WAIT_FLAG = 0;
        ushort MEETING_NUMBER_LAST = 0;
        CrestronString FROM_FUSION;
        public void MEETINGSCALLFN ( object __sender__ /*Fusion_XML.MainProg SENDER */, Fusion_XML.MeetingsArgs MEETINGSF ) 
            { 
            MainProg  SENDER  = (MainProg )__sender__;
            ushort MARK = 0;
            
            try
            {
                SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
                
                __context__.SourceCodeLine = 105;
                MARK = (ushort) ( MEETINGSF.meetingNum ) ; 
                __context__.SourceCodeLine = 106;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( MARK <= 30 ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 108;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (MEETINGSF.listCount != MEETING_NUMBER_LAST))  ) ) 
                        { 
                        __context__.SourceCodeLine = 110;
                        MEETING_NUMBER_LAST = (ushort) ( MEETINGSF.listCount ) ; 
                        __context__.SourceCodeLine = 111;
                        MEETING_NUMBER  .Value = (ushort) ( MEETING_NUMBER_LAST ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 113;
                    START_TIME [ MARK]  .UpdateValue ( MEETINGSF . startTime  ) ; 
                    __context__.SourceCodeLine = 114;
                    END_TIME [ MARK]  .UpdateValue ( MEETINGSF . endTime  ) ; 
                    __context__.SourceCodeLine = 115;
                    SUBJECT [ MARK]  .UpdateValue ( MEETINGSF . subject  ) ; 
                    __context__.SourceCodeLine = 116;
                    ORGANIZER [ MARK]  .UpdateValue ( MEETINGSF . organizer  ) ; 
                    __context__.SourceCodeLine = 117;
                    MEETING_STATE [ MARK]  .Value = (ushort) ( MEETINGSF.confState ) ; 
                    } 
                
                
                
            }
            finally { ObjectFinallyHandler(); }
            }
            
        public void ROOMSTATUSCALLFN ( object __sender__ /*Fusion_XML.MainProg SENDER */, Fusion_XML.RoomStatusArgs ROOMF ) 
            { 
            MainProg  SENDER  = (MainProg )__sender__;
            try
            {
                SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
                
                
                
            }
            finally { ObjectFinallyHandler(); }
            }
            
        private void ASSIGN_SCHEDULING_BUFFER_INPUT (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 129;
            CreateWait ( "SCHEDULING_BUFFER_INPUT_WAIT" , BUFFER_INPUT_WAIT_TIME  .Value , SCHEDULING_BUFFER_INPUT_WAIT_Callback ) ;
            
            }
            
        public void SCHEDULING_BUFFER_INPUT_WAIT_CallbackFn( object stateInfo )
        {
        
            try
            {
                Wait __LocalWait__ = (Wait)stateInfo;
                SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
                __LocalWait__.RemoveFromList();
                
            
            __context__.SourceCodeLine = 131;
            SCHEDULING_BUFFER_INPUT_WAIT_FLAG = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 132;
            FROM_FUSION  .UpdateValue ( SCHEDULERESPONSE__DOLLAR__  ) ; 
            __context__.SourceCodeLine = 133;
            Functions.ClearBuffer ( SCHEDULERESPONSE__DOLLAR__ ) ; 
            __context__.SourceCodeLine = 135;
            FUSIONXML . getMeetings ( FROM_FUSION .ToString()) ; 
            
        
        
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            
        }
        
    object ENABLE_DEBUG_OnChange_0 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 142;
            FUSIONXML . pDebugging = (ushort) ( ENABLE_DEBUG  .Value ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object SCHEDULERESPONSE__DOLLAR___OnChange_1 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        ushort BREAK_FLAG = 0;
        ushort I = 0;
        
        
        __context__.SourceCodeLine = 148;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (SCHEDULING_BUFFER_INPUT_WAIT_FLAG == 1))  ) ) 
            {
            __context__.SourceCodeLine = 149;
            CancelWait ( "SCHEDULING_BUFFER_INPUT_WAIT" ) ; 
            }
        
        else 
            {
            __context__.SourceCodeLine = 151;
            SCHEDULING_BUFFER_INPUT_WAIT_FLAG = (ushort) ( 1 ) ; 
            }
        
        __context__.SourceCodeLine = 152;
        ASSIGN_SCHEDULING_BUFFER_INPUT (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 158;
        Functions.Delay (  (int) ( 1000 ) ) ; 
        __context__.SourceCodeLine = 160;
        // RegisterEvent( FUSIONXML , MEETINGSCALLBACK , MEETINGSCALLFN ) 
        try { g_criticalSection.Enter(); FUSIONXML .meetingsCallback  += MEETINGSCALLFN; } finally { g_criticalSection.Leave(); }
        ; 
        __context__.SourceCodeLine = 161;
        // RegisterEvent( FUSIONXML , ROOMSTATUSCALLBACK , ROOMSTATUSCALLFN ) 
        try { g_criticalSection.Enter(); FUSIONXML .roomStatusCallback  += ROOMSTATUSCALLFN; } finally { g_criticalSection.Leave(); }
        ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    FROM_FUSION  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 65534, this );
    
    ENABLE_DEBUG = new Crestron.Logos.SplusObjects.DigitalInput( ENABLE_DEBUG__DigitalInput__, this );
    m_DigitalInputList.Add( ENABLE_DEBUG__DigitalInput__, ENABLE_DEBUG );
    
    MEETING_NUMBER = new Crestron.Logos.SplusObjects.AnalogOutput( MEETING_NUMBER__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( MEETING_NUMBER__AnalogSerialOutput__, MEETING_NUMBER );
    
    MEETING_STATE = new InOutArray<AnalogOutput>( 30, this );
    for( uint i = 0; i < 30; i++ )
    {
        MEETING_STATE[i+1] = new Crestron.Logos.SplusObjects.AnalogOutput( MEETING_STATE__AnalogSerialOutput__ + i, this );
        m_AnalogOutputList.Add( MEETING_STATE__AnalogSerialOutput__ + i, MEETING_STATE[i+1] );
    }
    
    START_TIME = new InOutArray<StringOutput>( 30, this );
    for( uint i = 0; i < 30; i++ )
    {
        START_TIME[i+1] = new Crestron.Logos.SplusObjects.StringOutput( START_TIME__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( START_TIME__AnalogSerialOutput__ + i, START_TIME[i+1] );
    }
    
    END_TIME = new InOutArray<StringOutput>( 30, this );
    for( uint i = 0; i < 30; i++ )
    {
        END_TIME[i+1] = new Crestron.Logos.SplusObjects.StringOutput( END_TIME__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( END_TIME__AnalogSerialOutput__ + i, END_TIME[i+1] );
    }
    
    SUBJECT = new InOutArray<StringOutput>( 30, this );
    for( uint i = 0; i < 30; i++ )
    {
        SUBJECT[i+1] = new Crestron.Logos.SplusObjects.StringOutput( SUBJECT__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( SUBJECT__AnalogSerialOutput__ + i, SUBJECT[i+1] );
    }
    
    ORGANIZER = new InOutArray<StringOutput>( 30, this );
    for( uint i = 0; i < 30; i++ )
    {
        ORGANIZER[i+1] = new Crestron.Logos.SplusObjects.StringOutput( ORGANIZER__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( ORGANIZER__AnalogSerialOutput__ + i, ORGANIZER[i+1] );
    }
    
    SCHEDULERESPONSE__DOLLAR__ = new Crestron.Logos.SplusObjects.BufferInput( SCHEDULERESPONSE__DOLLAR____AnalogSerialInput__, 65534, this );
    m_StringInputList.Add( SCHEDULERESPONSE__DOLLAR____AnalogSerialInput__, SCHEDULERESPONSE__DOLLAR__ );
    
    BUFFER_INPUT_WAIT_TIME = new UShortParameter( BUFFER_INPUT_WAIT_TIME__Parameter__, this );
    m_ParameterList.Add( BUFFER_INPUT_WAIT_TIME__Parameter__, BUFFER_INPUT_WAIT_TIME );
    
    SCHEDULE_ORDER = new UShortParameter( SCHEDULE_ORDER__Parameter__, this );
    m_ParameterList.Add( SCHEDULE_ORDER__Parameter__, SCHEDULE_ORDER );
    
    SCHEDULING_BUFFER_INPUT_WAIT_Callback = new WaitFunction( SCHEDULING_BUFFER_INPUT_WAIT_CallbackFn );
    
    ENABLE_DEBUG.OnDigitalChange.Add( new InputChangeHandlerWrapper( ENABLE_DEBUG_OnChange_0, false ) );
    SCHEDULERESPONSE__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( SCHEDULERESPONSE__DOLLAR___OnChange_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    FUSIONXML  = new Fusion_XML.MainProg();
    
    
}

public UserModuleClass_FUSION_SCHEDULE_INFO ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}


private WaitFunction SCHEDULING_BUFFER_INPUT_WAIT_Callback;


const uint ENABLE_DEBUG__DigitalInput__ = 0;
const uint SCHEDULERESPONSE__DOLLAR____AnalogSerialInput__ = 0;
const uint MEETING_NUMBER__AnalogSerialOutput__ = 0;
const uint MEETING_STATE__AnalogSerialOutput__ = 1;
const uint START_TIME__AnalogSerialOutput__ = 31;
const uint END_TIME__AnalogSerialOutput__ = 61;
const uint SUBJECT__AnalogSerialOutput__ = 91;
const uint ORGANIZER__AnalogSerialOutput__ = 121;
const uint BUFFER_INPUT_WAIT_TIME__Parameter__ = 10;
const uint SCHEDULE_ORDER__Parameter__ = 11;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
