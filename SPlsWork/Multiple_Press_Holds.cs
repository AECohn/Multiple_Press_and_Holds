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
using Multiple_Press_and_Holds;

namespace UserModule_MULTIPLE_PRESS_HOLDS
{
    public class UserModuleClass_MULTIPLE_PRESS_HOLDS : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        InOutArray<Crestron.Logos.SplusObjects.DigitalInput> PRESS;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> HOLD_SUCCESFUL;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> HOLD_UNSUCCESFUL;
        UShortParameter HOLD_TIME;
        UShortParameter PULSE_TIME;
        Multiple_Press_and_Holds.MultipleHold MYPRESSHOLD;
        public override object FunctionMain (  object __obj__ ) 
            { 
            try
            {
                SplusExecutionContext __context__ = SplusFunctionMainStartCode();
                
                __context__.SourceCodeLine = 35;
                // RegisterDelegate( MYPRESSHOLD , SENDINDEX , REPORTINDEXHANDLER ) 
                MYPRESSHOLD .SendIndex  = REPORTINDEXHANDLER; ; 
                __context__.SourceCodeLine = 37;
                MYPRESSHOLD . Init ( (ushort)( 2 ), (ushort)( (HOLD_TIME  .Value * 10) )) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            return __obj__;
            }
            
        object PRESS_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 43;
                MYPRESSHOLD . Press ( (ushort)( (Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) - 1) )) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object PRESS_OnRelease_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 48;
            MYPRESSHOLD . Letgo ( (ushort)( (Functions.GetLastModifiedArrayIndex( __SignalEventArg__ ) - 1) )) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
public void REPORTINDEXHANDLER ( ushort ACTIVEINDEX ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusSimplSharpDelegateThreadStartCode();
        
        __context__.SourceCodeLine = 54;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (MYPRESSHOLD.Hold_Values[ ACTIVEINDEX ] == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 56;
            Functions.Pulse ( PULSE_TIME  .Value, HOLD_SUCCESFUL [ (ACTIVEINDEX + 1)] ) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 61;
            Functions.Pulse ( PULSE_TIME  .Value, HOLD_UNSUCCESFUL [ (ACTIVEINDEX + 1)] ) ; 
            } 
        
        
        
    }
    finally { ObjectFinallyHandler(); }
    }
    

public override void LogosSplusInitialize()
{
    SocketInfo __socketinfo__ = new SocketInfo( 1, this );
    InitialParametersClass.ResolveHostName = __socketinfo__.ResolveHostName;
    _SplusNVRAM = new SplusNVRAM( this );
    
    PRESS = new InOutArray<DigitalInput>( 2, this );
    for( uint i = 0; i < 2; i++ )
    {
        PRESS[i+1] = new Crestron.Logos.SplusObjects.DigitalInput( PRESS__DigitalInput__ + i, PRESS__DigitalInput__, this );
        m_DigitalInputList.Add( PRESS__DigitalInput__ + i, PRESS[i+1] );
    }
    
    HOLD_SUCCESFUL = new InOutArray<DigitalOutput>( 2, this );
    for( uint i = 0; i < 2; i++ )
    {
        HOLD_SUCCESFUL[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( HOLD_SUCCESFUL__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( HOLD_SUCCESFUL__DigitalOutput__ + i, HOLD_SUCCESFUL[i+1] );
    }
    
    HOLD_UNSUCCESFUL = new InOutArray<DigitalOutput>( 2, this );
    for( uint i = 0; i < 2; i++ )
    {
        HOLD_UNSUCCESFUL[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( HOLD_UNSUCCESFUL__DigitalOutput__ + i, this );
        m_DigitalOutputList.Add( HOLD_UNSUCCESFUL__DigitalOutput__ + i, HOLD_UNSUCCESFUL[i+1] );
    }
    
    HOLD_TIME = new UShortParameter( HOLD_TIME__Parameter__, this );
    m_ParameterList.Add( HOLD_TIME__Parameter__, HOLD_TIME );
    
    PULSE_TIME = new UShortParameter( PULSE_TIME__Parameter__, this );
    m_ParameterList.Add( PULSE_TIME__Parameter__, PULSE_TIME );
    
    
    for( uint i = 0; i < 2; i++ )
        PRESS[i+1].OnDigitalPush.Add( new InputChangeHandlerWrapper( PRESS_OnPush_0, false ) );
        
    for( uint i = 0; i < 2; i++ )
        PRESS[i+1].OnDigitalRelease.Add( new InputChangeHandlerWrapper( PRESS_OnRelease_1, false ) );
        
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    MYPRESSHOLD  = new Multiple_Press_and_Holds.MultipleHold();
    
    
}

public UserModuleClass_MULTIPLE_PRESS_HOLDS ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint PRESS__DigitalInput__ = 0;
const uint HOLD_SUCCESFUL__DigitalOutput__ = 0;
const uint HOLD_UNSUCCESFUL__DigitalOutput__ = 2;
const uint HOLD_TIME__Parameter__ = 10;
const uint PULSE_TIME__Parameter__ = 11;

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
