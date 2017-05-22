using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WpfIATCSharp
{

    class IAT
    {
        private const int BUFFER_SIZE = 4096;

        /// <summary>
        /// 指针转字符串
        /// </summary>
        /// <param name="p">指向非托管代码字符串的指针</param>
        /// <returns>返回指针指向的字符串</returns>
        private static string PtrToStr(IntPtr p)
        {
            List<byte> lb = new List<byte>();
            try
            {
                while (Marshal.ReadByte(p) != 0)
                {
                    lb.Add(Marshal.ReadByte(p));
                    p = p + 1;
                }
            }
            catch (AccessViolationException ex)
            {
                Debug.WriteLine(ex.Message);
                //Debug.WriteLine(ex.Message);
            }
            return Encoding.UTF8.GetString(lb.ToArray());
        }


        public static void RunIAT(List<VoiceData> VoiceBuffer, string session_begin_params)
        {
            IntPtr session_id = IntPtr.Zero;
            string rec_result = string.Empty;
            string hints = "正常结束"; 
            AudioStatus aud_stat = AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE;      
            EpStatus ep_stat = EpStatus.ISR_EP_LOOKING_FOR_SPEECH;        
            RecogStatus rec_stat = RecogStatus.ISR_REC_STATUS_SUCCESS;    
            int errcode = (int)ErrorCode.MSP_SUCCESS;

            session_id = MSCDLL.QISRSessionBegin(null, session_begin_params, ref errcode); 
            if ((int)ErrorCode.MSP_SUCCESS != errcode)
            {
                Debug.WriteLine("\nQISRSessionBegin failed! error code:{0}\n", errcode);
                return;
            }

            for(int i=0;i<VoiceBuffer.Count();i++)
            {
                aud_stat = AudioStatus.ISR_AUDIO_SAMPLE_CONTINUE;
                if (i == 0)
                    aud_stat = AudioStatus.ISR_AUDIO_SAMPLE_FIRST;
                errcode = MSCDLL.QISRAudioWrite(PtrToStr(session_id), VoiceBuffer[i].data, (uint)VoiceBuffer[i].data.Length, aud_stat, ref ep_stat, ref rec_stat);
                if((int)ErrorCode.MSP_SUCCESS != errcode)
                {
                    MSCDLL.QISRSessionEnd(PtrToStr(session_id), null);
                }
            }

            errcode = MSCDLL.QISRAudioWrite(PtrToStr(session_id), null, 0, AudioStatus.ISR_AUDIO_SAMPLE_LAST, ref ep_stat, ref rec_stat);
            if ((int)ErrorCode.MSP_SUCCESS != errcode)
            {
                Debug.WriteLine("\nQISRAudioWrite failed! error code:{0} \n", errcode);
                return;
            }

            while (RecogStatus.ISR_REC_STATUS_SPEECH_COMPLETE != rec_stat)
            {
                IntPtr rslt = MSCDLL.QISRGetResult(PtrToStr(session_id), ref rec_stat, 0, ref errcode);
                if ((int)ErrorCode.MSP_SUCCESS != errcode)
                {
                    Debug.WriteLine("\nQISRGetResult failed, error code: {0}\n", errcode);
                    break;
                }
                if (IntPtr.Zero != rslt)
                {
                    string tempRes = PtrToStr(rslt);

                    rec_result = rec_result + tempRes;
                    if (rec_result.Length >= BUFFER_SIZE)
                    {
                        Debug.WriteLine("\nno enough buffer for rec_result !\n");
                        break;
                    }
                }

            }

            //结果
            Debug.WriteLine(rec_result);
            BehaviorAnalysis behaviorAnalysis = new BehaviorAnalysis();
            behaviorAnalysis.Start(rec_result);

            int errorcode = MSCDLL.QISRSessionEnd(PtrToStr(session_id), hints);
            if ((int)ErrorCode.MSP_SUCCESS == errorcode)
            {
                Debug.WriteLine("\nQISRGetResult successfull, {0}\n", hints);
            }
        }
    }
}
