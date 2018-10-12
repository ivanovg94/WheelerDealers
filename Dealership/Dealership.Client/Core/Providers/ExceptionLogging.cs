using Dealership.Client.Core.Abstract;
using System;
using System.IO;

public class ExceptionLogging : IExceptionLogging
{
    public void SendErrorToText(Exception ex)
    {
        var line = Environment.NewLine + Environment.NewLine;
        var errorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
        var errormsg = ex.GetType().Name.ToString();
        var extype = ex.GetType().ToString();
        var errorLocation = ex.Message.ToString();
        var source = ex.Source.ToString();
        var trace = ex.StackTrace.ToString();
        try
        {
            string filepath = @"..\..\..\..\Dealership.Data\DataProcessor\ExceptionLogging\";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".log";
            if (!File.Exists(filepath))
            {
                File.Create(filepath).Dispose();
            }
            using (StreamWriter sw = File.AppendText(filepath))
            {
                string error = $"Log Written Date: {DateTime.Now.ToString()} {line} " +
                    $"Error Line No : {errorlineNo} {line} Error Message:  {errormsg} {line}" +
                    $" Exception Type: {extype} { line} Error Location :{errorLocation} {line} " +
                    $" Source : {source}{line}" +
                    $" Stack trace: {trace}{line}";

                sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                sw.WriteLine("-------------------------------------------------------------------------------------");
                sw.WriteLine(line);
                sw.WriteLine(error);
                sw.WriteLine("--------------------------------*End*------------------------------------------");
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();
            }
        }
        catch (Exception e)
        {
            e.ToString();
        }
    }

}