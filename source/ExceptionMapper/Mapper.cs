/*  
Copyright 2014 AFour Technologies

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License. 
*/

using System;
using System.IO;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AFourTech.ExceptionMapper
{
    /// <summary>
    /// Mapper Class represents a class that returns description of exception in form of ExceptionInfo Object
    /// </summary>
    public class Mapper
    {
        private static String exceptionType;
        private static String alternateText;
        private static String defaultExceptionType;
        private static ExceptionInfo exceptionInfo;

        /// <summary>
        /// Dictionary for checking status of our errors with status code and messages.
        /// </summary>
        private static Dictionary<int, string> statusCodeDictionary;

        /// <summary>
        /// Status code for current error.
        /// </summary>
        static int statusCodeDictionaryFinder = 100;


        private static String xmlFilePath;

        /// <summary>
        /// This Hashtable is used for one time caching of xml file.
        /// </summary>
        private static Hashtable exceptionTypeHashtable;


        /// <summary>
        /// This Hashatble is used to store messages tag from xml file.
        /// </summary>
        private static Hashtable messageHashtable;

        ///<summary>
        /// Specify the file path where ExceptionMapping.xml is located. 
        /// </summary>  
        public static String XmlFilePath
        {
            get { return xmlFilePath; }
            set { xmlFilePath = value; }
        }

        /// <summary>
        /// Enum to find StepCondition type 
        /// </summary>
        private enum StepConditionEnum
        {
            GIVEN, WHEN
        }

        /// <summary>Method to initialize Error codes in dictionary</summary>
        private static void initializeErrorCodeDict()
        {
            statusCodeDictionary = new Dictionary<int, string>();

            statusCodeDictionary.Add(100, "Success");
            statusCodeDictionary.Add(101, "An Error has occurred in our library please contact AFourTech for further Information on debugging this error.");
            statusCodeDictionary.Add(103, "Syntax Error in file. Please follow Proper Syntax");
        }

        /// <summary>Method to cache xml file to Hashtable</summary>
        private static void cacheXMLDocument()
        {
            initializeErrorCodeDict();
            exceptionInfo = new ExceptionInfo();
            exceptionTypeHashtable = new Hashtable();

            if (XmlFilePath == null)
            {
                /// <summary>Get the XML file path from current Directory</summary>
                XmlFilePath = @"..\..\resources\ExceptionMapping.xml";
            }
            try
            {
                statusCodeDictionary.Add(102, "File Could not be found at '"+xmlFilePath+"'");
                
                XDocument xDoc = XDocument.Load(XmlFilePath);        
                XElement rootElement = xDoc.Root;
                IEnumerable<XElement> childNodes = rootElement.Elements();              
                foreach (XElement element in childNodes)
                {
                    string parentFirstAttribute = element.FirstAttribute.Value;
                    String elementName = element.Name.ToString();
                    messageHashtable = new Hashtable();
                    IEnumerable<XElement> subChildNodes = element.Elements();
                    foreach (XElement subSingleChild in subChildNodes)
                    {
                        string alternateTextValue;
                        if (subSingleChild.HasAttributes)
                        {
                            alternateTextValue = subSingleChild.FirstAttribute.Value;
                        }
                        else
                        {
                            alternateTextValue = "";
                        }
                        string exceptionMessage = subSingleChild.Value;
                        messageHashtable.Add(exceptionMessage, alternateTextValue);
                    }
                    exceptionTypeHashtable.Add(parentFirstAttribute, messageHashtable);
                    if ("OtherException".Equals(elementName))// when exception not present in file then set exceptiontype to otherException.
                    {
                        defaultExceptionType = parentFirstAttribute;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                statusCodeDictionaryFinder = 102;
            }
            catch(DirectoryNotFoundException)
            {
                statusCodeDictionaryFinder = 102;
            }
            catch (System.Xml.XmlException)
            {
                statusCodeDictionaryFinder = 103;
            }
        }

        /// <summary>
        /// <c>GetExceptionInfo</c> is a static method in the <c>Mapper</c> class. 
        /// Below sample shows how to call the <see cref="GetExceptionInfo"/> method.
        ///  <code>
        ///  class Program
        ///  {
        ///    static void Main(string[] args)
        ///    {
        ///       var res = Mapper.GetExceptionInfo(new Exception("Arithmetic operation resulted in an overflow."));                  
        ///    }    
        ///  }   
        /// </code>
        /// <code>
        ///  class Program
        ///  {
        ///    static void Main(string[] args)
        ///    {     
        ///       var res = Mapper.GetExceptionInfo(new Exception("Arithmetic operation resulted in an overflow."),"then");           
        ///    }    
        ///  }
        /// </code>
        ///  <code>
        ///  class Program   
        ///  {
        ///    static void Main(string[] args)
        ///    {       
        ///       var res = Mapper.GetExceptionInfo(new Exception("Arithmetic operation resulted in an overflow."),"given");           
        ///    }    
        ///  }
        /// </code>
        /// </summary>  
        /// <param name="exception">Exception object to get Type .</param>
        /// <param name="stepString">Step level condition like 'Given' or 'When'.</param>
        /// <returns>ExceptionInfo</returns>
       public static ExceptionInfo GetExceptionInfo(Exception exception, string stepString = null)
        {
            Console.Write("----------------------" + stepString+"\n");
            if (exceptionTypeHashtable == null)
            {
                cacheXMLDocument();
            }
            if (exceptionTypeHashtable.Keys.Count > 0)
            {
                exceptionInfo.AlternateText = null;
                exceptionType = defaultExceptionType;
                int WhenGivenConditionChecker = 0;
                alternateText = "";

                string x = ((StepConditionEnum)StepConditionEnum.GIVEN).ToString();

                if (stepString != null)
                {
                    stepString = stepString.ToUpper();
                    if ((((StepConditionEnum)StepConditionEnum.GIVEN).ToString() == stepString.ToUpper()) || (((StepConditionEnum)StepConditionEnum.WHEN).ToString() == stepString.ToUpper()))
                    {                       
                        WhenGivenConditionChecker = 1;                       
                    }
                }

                try
                {
                    bool flag = false;

                    //Traverse HashTable to get Exception Data present in xml file.
                    foreach (String exceptionRootElement in exceptionTypeHashtable.Keys)
                    {                     
                        if (flag == false)
                        {
                            Hashtable messageHashtable = (Hashtable)exceptionTypeHashtable[exceptionRootElement]; //get parent node
                            foreach (String exceptionMessage in messageHashtable.Keys) //get child node.
                            {
                                String exceptionAlternateText = (String)messageHashtable[exceptionMessage];
                                if (exception.Message.Equals(exceptionMessage))
                                {
                                    exceptionType = exceptionRootElement;
                                    if ("Functional".Equals(exceptionType) && WhenGivenConditionChecker == 1)
                                    {
                                        alternateText = "";
                                    }
                                    else
                                    {
                                        alternateText = exceptionAlternateText;
                                    }                                       
                                    flag = true;
                                }
                                if (WhenGivenConditionChecker == 1)
                                {
                                    exceptionType = "Environmental";
                                }
                            }
                        }

                    }
                    if (alternateText == null)
                    {
                        alternateText = "";
                    }
                }
                /// <summary>
                /// If file not find then FileNotFoundException exception is throw
                /// </summary>      
                catch (Exception )
                {
                    statusCodeDictionaryFinder = 101; ;
                  
                }

            }

            exceptionInfo.AlternateText = alternateText;
            exceptionInfo.Message = exception.Message;
            exceptionInfo.ClassName = exception.GetType().ToString();
            exceptionInfo.StackTrace = exception.StackTrace;
            exceptionInfo.ExceptionType = exceptionType;
            exceptionInfo.StatusCode = statusCodeDictionaryFinder + " - " + statusCodeDictionary[statusCodeDictionaryFinder];

            /// <summary> Get the Stack trace information and store it into StackLog folder </summary>  
            string time = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss.FFF");
            string FolderName = "StackLog";

            var ParentDirName = Directory.GetParent("..\\..\\ExceptionMapping.xml");
            string subDirectoryName = ParentDirName + "\\" + FolderName;

            if (!Directory.Exists(subDirectoryName))
            {
                Directory.CreateDirectory(subDirectoryName);
            }

            /// <summary> Create the log file & store it into StackLog Folder</summary>
            string logFile = subDirectoryName + "\\StackTrace_" + time + ".log";
            File.WriteAllText(logFile, exception.StackTrace);

            return exceptionInfo;

        }
    }
}
