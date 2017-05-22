using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace WpfIATCSharp
{
    class BehaviorAnalysis
    {

        private void SendData(object data)
        {
            try
            {
                NamedPipeClientStream _pipeClient = new NamedPipeClientStream(".", "closePipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
                _pipeClient.Connect();
                StreamWriter sw = new StreamWriter(_pipeClient);
                sw.WriteLine(data);
                sw.Flush();
                Thread.Sleep(1000);
                sw.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void Start(string recResult)
        {
            string tts_text="";
            try
            {
                string operation = getSingleNode(recResult, "operation").ToUpper();
                switch (operation)
                {
                    case "QUERY":
                        {
                            string service = getSingleNode(recResult, "service");
                            switch (service)
                            {
                                case "weather":
                                    string[] city = { "data", "result", "unit", "city" };
                                    string[] weather = { "data", "result", "unit", "weather" };
                                    string[] tempRange = { "data", "result", "unit", "tempRange" };
                                    string[] wind = { "data", "result", "unit", "wind" };
                                    tts_text = getSingleNode(recResult,city)+" "+getSingleNode(recResult,weather) + " " + getSingleNode(recResult,tempRange) + " " + getSingleNode(recResult,wind);
                                    break;
                            }
                        }
                        break;
                    case "ANSWER":
                        {
                            string service = getSingleNode(recResult, "service");
                            switch (service)
                            {
                                case "openQA":
                                case "chat":
                                case "faq":
                                    string[] array = { "answer", "text" };
                                    tts_text = getSingleNode(recResult, array);
                                    break;
                            }
                        }
                        break;
                }

                //TTS tts = new TTS();
                //tts.CreateWAV(tts_text);

                Debug.WriteLine(tts_text);
                Thread pipeThread = new Thread(new ParameterizedThreadStart(SendData));
                pipeThread.IsBackground = true;
                pipeThread.Start(tts_text);

            }
            catch(Exception ex)
            {
                
            }

        }


        private string getSingleNode(string recResult,string attr)
        {
            string nodeText = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(recResult);
                XmlNode root = xmlDoc.SelectSingleNode("//nlp");
                if (root != null)
                {
                    nodeText = root.SelectSingleNode(attr).InnerText;
                }
            }
            catch (Exception e)
            {
                nodeText = null;
            }
            return nodeText;
        }

        private string getSingleNode(string recResult,string[] attrs)
        {
            string nodeText = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(recResult);
                XmlNode node = xmlDoc.SelectSingleNode("//nlp");
                if(node != null)
                {
                    foreach(string attr in attrs)
                    {
                        node = node.SelectSingleNode(attr);
                    }
                    nodeText = node.InnerText;
                }
            }
            catch(Exception)
            {
                return null;
            }
            return nodeText;
        }

        /// <summary>    
        /// 读取xml中的指定节点的值  
        /// </summary>    
        public void ReadXmlNode(string xmlResult)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xmlResult);
                XmlNode root = xmlDoc.SelectSingleNode("//nlp");
                if (root != null)
                {
                    string Operation = (root.SelectSingleNode("operation")).InnerText;
                    string Service = (root.SelectSingleNode("service")).InnerText;
                    string Answer = root.SelectSingleNode("answer").SelectSingleNode("text").InnerText;
                }
                else
                {
                    Debug.WriteLine("the node  is not existed");
                }
            }
            catch (Exception e)
            {
                //显示错误信息  
                Debug.WriteLine(e.Message);
            }
        }
    }
}
