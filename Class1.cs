﻿//Programmer: Aviv Cohn

using System;
using System.Text;
using Crestron.SimplSharp;

namespace Multiple_Press_and_Holds
{
    public class MultipleHold
    {

        public delegate void ReportIndex(ushort index);
        public ReportIndex SendIndex { get; set; }

        private ushort Hold_Time;
        public ushort[] Hold_Values;
        private bool[] Status;
        public CTimer[] Holds;

        public void Init(ushort Num_Holds, ushort hold_time)
        {
            Hold_Values = new ushort[Num_Holds];
            Hold_Time = hold_time;
            Status = new bool[Num_Holds];
            Holds = new CTimer[Num_Holds];
        }

        public void Press(ushort ArrayIndex)
        {
            Status[ArrayIndex] = false;
            Holds[ArrayIndex] = new CTimer(Hold_Time_Elapsed, ArrayIndex, Hold_Time);
        }

        public void Letgo(ushort ArrayIndex)
        {
            if (Status[ArrayIndex] == false)
            {
                Holds[ArrayIndex].Dispose();
                Hold_Values[ArrayIndex] = 0;

                SendIndex(ArrayIndex);
            }
        }

        private void Hold_Time_Elapsed(object obj)
        {
            ushort ArrayIndex = (ushort)obj;
            Status[ArrayIndex] = true;
            Hold_Values[ArrayIndex] = 1;

            SendIndex((ushort)obj);
        }
    }
}

