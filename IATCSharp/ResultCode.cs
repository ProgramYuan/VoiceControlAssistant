namespace WpfIATCSharp
{
    public enum ResultCode
    {
        IvwErrID_FAIL = -1,
        IvwErrID_OK = 0,
        IvwErr_InvCal = 1,
        IvwErr_InvArg = 2,
        IvwErr_TellSize = 3,
        IvwErr_OutOfMemory = 4,
        IvwErr_BufferFull = 5,
        IvwErr_BufferEmpty = 6,
        IvwErr_InvRes = 7,
        IvwErr_ReEnter = 8,
        IvwErr_NotSupport = 9,
        IvwErr_NotFound = 10,
        IvwErr_InvSN = 11,
        IvwErr_Limitted = 12,
        IvwErr_TimeOut = 13,
        IvwErr_WakeUp = 14,
        IvwErr_Flush = 15,
        IvwErr_InvResAddress = 16,
        IvwErr_Enroll1_Success = 17,
        IvwErr_Enroll1_Failed = 18,
        IvwErr_Enroll2_Success = 29,
        IvwErr_Enroll2_Failed = 20,
        IvwErr_Enroll3_Success = 21,
        IvwErr_Enroll3_Failed = 22,
        IvwErr_SpeechTooShort = 23,
        IvwErr_SpeechStop = 24,
        IvwErr_MergeRes_Failed = 25,
    }
}
