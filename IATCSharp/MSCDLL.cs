using System;
using System.Runtime.InteropServices;

namespace WpfIATCSharp
{
    public enum SynthStatus
    {
        MSP_TTS_FLAG_STILL_HAVE_DATA = 1,
        MSP_TTS_FLAG_DATA_END = 2,
        MSP_TTS_FLAG_CMD_CANCELED = 0
    }
    public class MSCDLL
    {
        public const int GRAMID_LEN = 128;

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSPLogin(string userName, string password, string parameter);

        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr MSPSearch(string paramstr, byte[] text, ref uint dataLen, ref int errorCode);
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int MSPLogout();
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRInit(string configs);
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr QISRSessionBegin(string grammarList, string _params, ref int errorCode);
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRAudioWrite(string sessionID, byte[] waveData, uint waveLen, AudioStatus audioStatus, ref EpStatus epStatus, ref RecogStatus recogStatus);
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr QISRGetResult(string sessionID, ref RecogStatus rsltStatus, int waitTime, ref int errorCode);
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRSessionEnd(string sessionID, string hints);
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int QISRFini();
        [DllImport("msc.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr QISRUploadData(string sessionID, string dataName, byte[] userData, uint lenght, string paramValue, ref int errorCode);


        //语音合成
        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr QTTSSessionBegin(string _params, ref int errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int QTTSTextPut(string sessionID, string textString, uint textLen, string _params);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr QTTSAudioGet(string sessionID, ref uint audioLen, ref SynthStatus synthStatus, ref int errorCode);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern IntPtr QTTSAudioInfo(string sessionID);

        [DllImport("msc.dll", CallingConvention = CallingConvention.Winapi)]
        public static extern int QTTSSessionEnd(string sessionID, string hints);

    }

    #region 错误代号
    public enum ErrorCode
    {
        MSP_SUCCESS = 0,
        MSP_ERROR_FAIL = -1,
        MSP_ERROR_EXCEPTION = -2,

        /* General errors 10100(0x2774) */
        MSP_ERROR_GENERAL = 10100,     /* 0x2774 */
        MSP_ERROR_OUT_OF_MEMORY = 10101,     /* 0x2775 */
        MSP_ERROR_FILE_NOT_FOUND = 10102,     /* 0x2776 */
        MSP_ERROR_NOT_SUPPORT = 10103,     /* 0x2777 */
        MSP_ERROR_NOT_IMPLEMENT = 10104,     /* 0x2778 */
        MSP_ERROR_ACCESS = 10105,     /* 0x2779 */
        MSP_ERROR_INVALID_PARA = 10106,     /* 0x277A */
        MSP_ERROR_INVALID_PARA_VALUE = 10107,     /* 0x277B */
        MSP_ERROR_INVALID_HANDLE = 10108,     /* 0x277C */
        MSP_ERROR_INVALID_DATA = 10109,     /* 0x277D */
        MSP_ERROR_NO_LICENSE = 10110,     /* 0x277E */
        MSP_ERROR_NOT_INIT = 10111,     /* 0x277F */
        MSP_ERROR_NULL_HANDLE = 10112,     /* 0x2780 */
        MSP_ERROR_OVERFLOW = 10113,     /* 0x2781 */
        MSP_ERROR_TIME_OUT = 10114,     /* 0x2782 */
        MSP_ERROR_OPEN_FILE = 10115,     /* 0x2783 */
        MSP_ERROR_NOT_FOUND = 10116,     /* 0x2784 */
        MSP_ERROR_NO_ENOUGH_BUFFER = 10117,     /* 0x2785 */
        MSP_ERROR_NO_DATA = 10118,     /* 0x2786 */
        MSP_ERROR_NO_MORE_DATA = 10119,     /* 0x2787 */
        MSP_ERROR_SKIPPED = 10120,     /* 0x2788 */
        MSP_ERROR_ALREADY_EXIST = 10121,     /* 0x2789 */
        MSP_ERROR_LOAD_MODULE = 10122,     /* 0x278A */
        MSP_ERROR_BUSY = 10123,     /* 0x278B */
        MSP_ERROR_INVALID_CONFIG = 10124,     /* 0x278C */
        MSP_ERROR_VERSION_CHECK = 10125,     /* 0x278D */
        MSP_ERROR_CANCELED = 10126,     /* 0x278E */
        MSP_ERROR_INVALID_MEDIA_TYPE = 10127,     /* 0x278F */
        MSP_ERROR_CONFIG_INITIALIZE = 10128,     /* 0x2790 */
        MSP_ERROR_CREATE_HANDLE = 10129,     /* 0x2791 */
        MSP_ERROR_CODING_LIB_NOT_LOAD = 10130,     /* 0x2792 */

        /* Error codes of network 10200(0x27D8)*/
        MSP_ERROR_NET_GENERAL = 10200,     /* 0x27D8 */
        MSP_ERROR_NET_OPENSOCK = 10201,     /* 0x27D9 */   /* Open socket */
        MSP_ERROR_NET_CONNECTSOCK = 10202,     /* 0x27DA */   /* Connect socket */
        MSP_ERROR_NET_ACCEPTSOCK = 10203,     /* 0x27DB */   /* Accept socket */
        MSP_ERROR_NET_SENDSOCK = 10204,     /* 0x27DC */   /* Send socket data */
        MSP_ERROR_NET_RECVSOCK = 10205,     /* 0x27DD */   /* Recv socket data */
        MSP_ERROR_NET_INVALIDSOCK = 10206,     /* 0x27DE */   /* Invalid socket handle */
        MSP_ERROR_NET_BADADDRESS = 10207,     /* 0x27EF */   /* Bad network address */
        MSP_ERROR_NET_BINDSEQUENCE = 10208,     /* 0x27E0 */   /* Bind after listen/connect */
        MSP_ERROR_NET_NOTOPENSOCK = 10209,     /* 0x27E1 */   /* Socket is not opened */
        MSP_ERROR_NET_NOTBIND = 10210,     /* 0x27E2 */   /* Socket is not bind to an address */
        MSP_ERROR_NET_NOTLISTEN = 10211,     /* 0x27E3 */   /* Socket is not listenning */
        MSP_ERROR_NET_CONNECTCLOSE = 10212,     /* 0x27E4 */   /* The other side of connection is closed */
        MSP_ERROR_NET_NOTDGRAMSOCK = 10213,     /* 0x27E5 */   /* The socket is not datagram type */
        MSP_ERROR_NET_DNS = 10214,
        /* Error codes of mssp message 10300(0x283C) */
        MSP_ERROR_MSG_GENERAL = 10300,     /* 0x283C */
        MSP_ERROR_MSG_PARSE_ERROR = 10301,     /* 0x283D */
        MSP_ERROR_MSG_BUILD_ERROR = 10302,     /* 0x283E */
        MSP_ERROR_MSG_PARAM_ERROR = 10303,     /* 0x283F */
        MSP_ERROR_MSG_CONTENT_EMPTY = 10304,     /* 0x2840 */
        MSP_ERROR_MSG_INVALID_CONTENT_TYPE = 10305,     /* 0x2841 */
        MSP_ERROR_MSG_INVALID_CONTENT_LENGTH = 10306,     /* 0x2842 */
        MSP_ERROR_MSG_INVALID_CONTENT_ENCODE = 10307,     /* 0x2843 */
        MSP_ERROR_MSG_INVALID_KEY = 10308,     /* 0x2844 */
        MSP_ERROR_MSG_KEY_EMPTY = 10309,     /* 0x2845 */
        MSP_ERROR_MSG_SESSION_ID_EMPTY = 10310,     /* 0x2846 */
        MSP_ERROR_MSG_LOGIN_ID_EMPTY = 10311,     /* 0x2847 */
        MSP_ERROR_MSG_SYNC_ID_EMPTY = 10312,     /* 0x2848 */
        MSP_ERROR_MSG_APP_ID_EMPTY = 10313,     /* 0x2849 */
        MSP_ERROR_MSG_EXTERN_ID_EMPTY = 10314,     /* 0x284A */
        MSP_ERROR_MSG_INVALID_CMD = 10315,     /* 0x284B */
        MSP_ERROR_MSG_INVALID_SUBJECT = 10316,     /* 0x284C */
        MSP_ERROR_MSG_INVALID_VERSION = 10317,     /* 0x284D */
        MSP_ERROR_MSG_NO_CMD = 10318,     /* 0x284E */
        MSP_ERROR_MSG_NO_SUBJECT = 10319,     /* 0x284F */
        MSP_ERROR_MSG_NO_VERSION = 10320,     /* 0x2850 */
        MSP_ERROR_MSG_MSSP_EMPTY = 10321,     /* 0x2851 */
        MSP_ERROR_MSG_NEW_RESPONSE = 10322,     /* 0x2852 */
        MSP_ERROR_MSG_NEW_CONTENT = 10323,     /* 0x2853 */
        MSP_ERROR_MSG_INVALID_SESSION_ID = 10324,     /* 0x2854 */

        /* Error codes of DataBase 10400(0x28A0)*/
        MSP_ERROR_DB_GENERAL = 10400,     /* 0x28A0 */
        MSP_ERROR_DB_EXCEPTION = 10401,     /* 0x28A1 */
        MSP_ERROR_DB_NO_RESULT = 10402,     /* 0x28A2 */
        MSP_ERROR_DB_INVALID_USER = 10403,     /* 0x28A3 */
        MSP_ERROR_DB_INVALID_PWD = 10404,     /* 0x28A4 */
        MSP_ERROR_DB_CONNECT = 10405,     /* 0x28A5 */
        MSP_ERROR_DB_INVALID_SQL = 10406,     /* 0x28A6 */
        MSP_ERROR_DB_INVALID_APPID = 10407,    /* 0x28A7 */

        /* Error codes of Resource 10500(0x2904)*/
        MSP_ERROR_RES_GENERAL = 10500,     /* 0x2904 */
        MSP_ERROR_RES_LOAD = 10501,     /* 0x2905 */   /* Load resource */
        MSP_ERROR_RES_FREE = 10502,     /* 0x2906 */   /* Free resource */
        MSP_ERROR_RES_MISSING = 10503,     /* 0x2907 */   /* Resource File Missing */
        MSP_ERROR_RES_INVALID_NAME = 10504,     /* 0x2908 */   /* Invalid resource file name */
        MSP_ERROR_RES_INVALID_ID = 10505,     /* 0x2909 */   /* Invalid resource ID */
        MSP_ERROR_RES_INVALID_IMG = 10506,     /* 0x290A */   /* Invalid resource image pointer */
        MSP_ERROR_RES_WRITE = 10507,     /* 0x290B */   /* Write read-only resource */
        MSP_ERROR_RES_LEAK = 10508,     /* 0x290C */   /* Resource leak out */
        MSP_ERROR_RES_HEAD = 10509,     /* 0x290D */   /* Resource head currupt */
        MSP_ERROR_RES_DATA = 10510,     /* 0x290E */   /* Resource data currupt */
        MSP_ERROR_RES_SKIP = 10511,     /* 0x290F */   /* Resource file skipped */

        /* Error codes of TTS 10600(0x2968)*/
        MSP_ERROR_TTS_GENERAL = 10600,     /* 0x2968 */
        MSP_ERROR_TTS_TEXTEND = 10601,     /* 0x2969 */  /* Meet text end */
        MSP_ERROR_TTS_TEXT_EMPTY = 10602,     /* 0x296A */  /* no synth text */

        /* Error codes of Recognizer 10700(0x29CC) */
        MSP_ERROR_REC_GENERAL = 10700,     /* 0x29CC */
        MSP_ERROR_REC_INACTIVE = 10701,     /* 0x29CD */
        MSP_ERROR_REC_GRAMMAR_ERROR = 10702,     /* 0x29CE */
        MSP_ERROR_REC_NO_ACTIVE_GRAMMARS = 10703,     /* 0x29CF */
        MSP_ERROR_REC_DUPLICATE_GRAMMAR = 10704,     /* 0x29D0 */
        MSP_ERROR_REC_INVALID_MEDIA_TYPE = 10705,     /* 0x29D1 */
        MSP_ERROR_REC_INVALID_LANGUAGE = 10706,     /* 0x29D2 */
        MSP_ERROR_REC_URI_NOT_FOUND = 10707,     /* 0x29D3 */
        MSP_ERROR_REC_URI_TIMEOUT = 10708,     /* 0x29D4 */
        MSP_ERROR_REC_URI_FETCH_ERROR = 10709,     /* 0x29D5 */

        /* Error codes of Speech Detector 10800(0x2A30) */
        MSP_ERROR_EP_GENERAL = 10800,     /* 0x2A30 */
        MSP_ERROR_EP_NO_SESSION_NAME = 10801,     /* 0x2A31 */
        MSP_ERROR_EP_INACTIVE = 10802,     /* 0x2A32 */
        MSP_ERROR_EP_INITIALIZED = 10803,     /* 0x2A33 */

        /* Error codes of TUV */
        MSP_ERROR_TUV_GENERAL = 10900,     /* 0x2A94 */
        MSP_ERROR_TUV_GETHIDPARAM = 10901,     /* 0x2A95 */   /* Get Busin Param huanid*/
        MSP_ERROR_TUV_TOKEN = 10902,     /* 0x2A96 */   /* Get Token */
        MSP_ERROR_TUV_CFGFILE = 10903,     /* 0x2A97 */   /* Open cfg file */
        MSP_ERROR_TUV_RECV_CONTENT = 10904,     /* 0x2A98 */   /* received content is error */
        MSP_ERROR_TUV_VERFAIL = 10905,     /* 0x2A99 */   /* Verify failure */

        /* Error codes of IMTV */
        MSP_ERROR_IMTV_SUCCESS = 11000,     /* 0x2AF8 */   /* 成功 */
        MSP_ERROR_IMTV_NO_LICENSE = 11001,     /* 0x2AF9 */   /* 试用次数结束，用户需要付费 */
        MSP_ERROR_IMTV_SESSIONID_INVALID = 11002,     /* 0x2AFA */   /* SessionId失效，需要重新登录通行证 */
        MSP_ERROR_IMTV_SESSIONID_ERROR = 11003,     /* 0x2AFB */   /* SessionId为空，或者非法 */
        MSP_ERROR_IMTV_UNLOGIN = 11004,     /* 0x2AFC */   /* 未登录通行证 */
        MSP_ERROR_IMTV_SYSTEM_ERROR = 11005,     /* 0x2AFD */   /* 系统错误 */

        /* Error codes of HCR */
        MSP_ERROR_HCR_GENERAL = 11100,
        MSP_ERROR_HCR_RESOURCE_NOT_EXIST = 11101,

        /* Error codes of http 12000(0x2EE0) */
        MSP_ERROR_HTTP_BASE = 12000,    /* 0x2EE0 */

        /*Error codes of ISV */
        MSP_ERROR_ISV_NO_USER = 13000,    /* 32C8 */    /* the user doesn't exist */
    }
    #endregion

    #region ISR枚举常量
    public enum AudioStatus
    {
        ISR_AUDIO_SAMPLE_INIT = 0x00,
        ISR_AUDIO_SAMPLE_FIRST = 0x01,
        ISR_AUDIO_SAMPLE_CONTINUE = 0x02,
        ISR_AUDIO_SAMPLE_LAST = 0x04,
        ISR_AUDIO_SAMPLE_SUPPRESSED = 0x08,
        ISR_AUDIO_SAMPLE_LOST = 0x10,
        ISR_AUDIO_SAMPLE_NEW_CHUNK = 0x20,
        ISR_AUDIO_SAMPLE_END_CHUNK = 0x40,

        ISR_AUDIO_SAMPLE_VALIDBITS = 0x7f /* to validate the value of sample->status */
    }

    public enum EpStatus
    {
        ISR_EP_NULL = -1,
        ISR_EP_LOOKING_FOR_SPEECH = 0,          ///还没有检测到音频的前端点
        ISR_EP_IN_SPEECH = 1,                   ///已经检测到了音频前端点，正在进行正常的音频处理。
        ISR_EP_AFTER_SPEECH = 3,                ///检测到音频的后端点，后继的音频会被MSC忽略。
        ISR_EP_TIMEOUT = 4,                     ///超时
        ISR_EP_ERROR = 5,                       ///出现错误
        ISR_EP_MAX_SPEECH = 6                   ///音频过大
    }

    public enum RecogStatus
    {
        ISR_REC_NULL = -1,
        ISR_REC_STATUS_SUCCESS = 0,             ///识别成功，此时用户可以调用QISRGetResult来获取（部分）结果。
        ISR_REC_STATUS_NO_MATCH = 1,            ///识别结束，没有识别结果
        ISR_REC_STATUS_INCOMPLETE = 2,          ///正在识别中
        ISR_REC_STATUS_NON_SPEECH_DETECTED = 3, ///保留
        ISR_REC_STATUS_SPEECH_DETECTED = 4,     ///发现有效音频
        ISR_REC_STATUS_SPEECH_COMPLETE = 5,     ///识别结束
        ISR_REC_STATUS_MAX_CPU_TIME = 6,        ///保留
        ISR_REC_STATUS_MAX_SPEECH = 7,          ///保留
        ISR_REC_STATUS_STOPPED = 8,             ///保留
        ISR_REC_STATUS_REJECTED = 9,            ///保留
        ISR_REC_STATUS_NO_SPEECH_FOUND = 10     ///没有发现音频
    }
    #endregion
}
