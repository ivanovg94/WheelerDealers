using System;
using System.IO;

public static class ExceptionLogging
{
    private static String ErrorlineNo, Errormsg, extype, ErrorLocation, Source, Trace;

    public static void SendErrorToText(Exception ex)
    {
        var line = Environment.NewLine + Environment.NewLine;

        ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
        Errormsg = ex.GetType().Name.ToString();
        extype = ex.GetType().ToString();
        ErrorLocation = ex.Message.ToString();
        Source = ex.Source.ToString();
        Trace = ex.StackTrace.ToString();
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
                    $"Error Line No : {ErrorlineNo} {line} Error Message:  {Errormsg} {line}" +
                    $" Exception Type: {extype} { line} Error Location :{ErrorLocation} {line} " +
                    $" Source : {Source}{line}" +
                    $" Stack trace: {Trace}{line}";

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