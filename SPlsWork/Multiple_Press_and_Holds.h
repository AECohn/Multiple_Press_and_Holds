namespace Multiple_Press_and_Holds;
        // class declarations
         class MultipleHold;
     class MultipleHold 
    {
        // class delegates
        delegate FUNCTION ReportIndex ( INTEGER index );

        // class events

        // class functions
        FUNCTION Init ( INTEGER Num_Holds , INTEGER hold_time );
        FUNCTION Press ( INTEGER ArrayIndex );
        FUNCTION Letgo ( INTEGER ArrayIndex );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER Hold_Values[];

        // class properties
        DelegateProperty ReportIndex SendIndex;
    };

