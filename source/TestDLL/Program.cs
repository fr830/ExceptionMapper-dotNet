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

using AFourTech.ExceptionMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDLL
{
    class Program
    {
        static void Main(string[] args)
        {

            //XmlFilePath : Optional path, If not set takes resource directory in project folder for ExceptionMapping.xml by default.
            //Mapper.XmlFilePath = "D:/ExceptionMapping.xml";
            try
            {
                int a = 4;
                int b = 0;
                int div = a / b;
                Console.WriteLine("Result  == " + div);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex);
                ShowExceptionResult(ei);
            }
            //---------------------------------------------------------
            try
            {
                int a = 4;
                int b = 0;
                int div = a / b;
                Console.WriteLine("Result  == " + div);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex, "given");
                ShowExceptionResult(ei);
            }
            //---------------------------------------------------------
            try
            {
                int a = 4;
                int b = 0;
                int div = a / b;
                Console.WriteLine("Result  == " + div);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex, "when");
                ShowExceptionResult(ei);
            }
            //---------------------------------------------------------
            try
            {
                int a = 4;
                int b = 0;
                int div = a / b;
                Console.WriteLine("Result  == " + div);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex, "then");
                ShowExceptionResult(ei);
            }
            try
            {
                int a = 4;
                int b = 0;
                int div = a / b;
                Console.WriteLine("Result  == " + div);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex, "sss");
                ShowExceptionResult(ei);
            }
 //---------------------------------------------------------
 //---------------------------------------------------------


            try
            {
                int[] x = { 10, 20, 30 };
                int abc = x[4];
                Console.WriteLine("Result  == " + abc);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex);
                //var ei = Mapper.GetExceptionInfo(new Exception("InvalidArgumentException"));
                ShowExceptionResult(ei);
            }
//---------------------------------------------------------
            try
            {
                int[] x = { 10, 20, 30 };
                int abc = x[4];
                Console.WriteLine("Result  == " + abc);

            }
            catch (Exception ex)
            {
               var ei = Mapper.GetExceptionInfo(ex, "when");
                  //var ei = Mapper.GetExceptionInfo(new Exception("InvalidArgumentException"), "when");
         //       var ei = Mapper.GetExceptionInfo(new Exception("InvaliArgument"), "when");
                ShowExceptionResult(ei);
            }
 //---------------------------------------------------------
            try
            {
                int[] x = { 10, 20, 30 };
                int abc = x[4];
                Console.WriteLine("Result  == " + abc);

            }
            catch (Exception ex)
            {
                var ei = Mapper.GetExceptionInfo(ex, "then");
                //var ei = Mapper.GetExceptionInfo(new Exception("InvalidArgumentException"), "then");
         //       var ei = Mapper.GetExceptionInfo(new Exception("InvaliArgument"), "then");
                ShowExceptionResult(ei);
            }
//---------------------------------------------------------
            try
            {
                int[] x = { 10, 20, 30 };
                int abc = x[4];
                Console.WriteLine("Result  == " + abc);

            }
            catch (Exception ex)
            {
             var ei = Mapper.GetExceptionInfo(ex, "given");
             // var ei = Mapper.GetExceptionInfo(new Exception("InvalidArgumentException"), "given");
          //   var ei = Mapper.GetExceptionInfo(new Exception("InvaliArgument"), "given");
                ShowExceptionResult(ei);
            }
//---------------------------------------------------------

            try
            {
                int[] x = { 10, 20, 30 };
                int abc = x[4];
                Console.WriteLine("Result  == " + abc);

            }
            catch (Exception ex)
            {
           //     var ei = Mapper.GetExceptionInfo(new Exception("InvaliArgument"), "yyyy");
              //  var ei = Mapper.GetExceptionInfo(new Exception("InvalidArgumentException"), "asa");
               var ei = Mapper.GetExceptionInfo(ex, "ggg");
                ShowExceptionResult(ei);
            }
            Console.ReadKey();
            
        }

        public static void ShowExceptionResult(ExceptionInfo exc)
        {
            Console.WriteLine("Status Code    --" + exc.StatusCode + "\n");
            Console.WriteLine("Message        --" + exc.Message);
            Console.WriteLine("Class name     --" + exc.ClassName);
            Console.WriteLine("Exception Type --" + exc.ExceptionType);
            Console.WriteLine("AlternateText  --" + exc.AlternateText);
            Console.WriteLine("StackTrace     --" + exc.StackTrace);
            Console.WriteLine("---------------------------------------------------------------------------------\n");
        }


    }
}
