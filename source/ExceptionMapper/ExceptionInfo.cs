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
using System.Text;


namespace AFourTech.ExceptionMapper
{
    /// <summary>
    ///Represents a class that provides general information about exception 
    /// </summary>
    public class ExceptionInfo
    {
        string messege;
        string stackTrace;
        string exceptionType;
        string alternateText;
        string className;
        string statusCode;


        /// <summary>
        /// Gets the type of exception
        /// </summary>
        public string ExceptionType
        {
            get { return exceptionType; }
            internal set { exceptionType = value; }
        }
        /// <summary>
        /// Gets the exception message for exception
        /// </summary>
        public string Message
        {
            get { return messege; }
            internal set { messege = value; }
        }
        /// <summary>
        /// Gets stack trace information about exception 
        /// </summary>
        public string StackTrace
        {
            get { return stackTrace; }
            internal set { stackTrace = value; }
        }
        /// <summary>
        /// Gets alternate text for the exception  
        /// </summary>
        public string AlternateText
        {
            get { return alternateText; }
            internal set { alternateText = value; }
        }
        /// <summary>
        /// Gets base class name where exception occurred.
        /// </summary>
        public string ClassName
        {
            get { return className; }
            internal set { className = value; }
        }
        /// <summary>
        /// Gets status code for the call indicating success or failure
        /// </summary>
        public string StatusCode
        {
            get { return statusCode; }
            internal set { statusCode = value; }
        }

    }
}
