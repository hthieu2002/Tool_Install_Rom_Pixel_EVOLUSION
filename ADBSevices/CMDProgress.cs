using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADBSevices
{
    public class CMDProgress
    {
        public static string ExecuteCommand(string argument, int timeout = 0)
        {
            using (var process = new Process())
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo.Arguments = argument;
                if (timeout == 0)
                {
                    process.Start();
                    StringBuilder result = new StringBuilder();
                    try
                    {
                        while (!process.HasExited)
                        {
                            result.Append(process.StandardOutput.ReadToEnd());
                        }
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    //watch.Stop();

                    //System.Console.WriteLine("Command {0} takes {1}ms", argument, watch.ElapsedMilliseconds);
#if DEBUG
                    Console.WriteLine("{0}. Result: {1}", argument, result.ToString());
#endif
                    return result.ToString();
                }
                else
                {
                    using (var outputWaitHandle = new AutoResetEvent(false))
                    {
                        using (var errorWaitHandle = new AutoResetEvent(false))
                        {
                            return HandleOutput(process, outputWaitHandle, errorWaitHandle, timeout, false);
                        }
                    }
                }
            }
        }

        private static string HandleOutput(Process p, AutoResetEvent outputWaitHandle, AutoResetEvent errorWaitHandle, int timeout, bool forceRegular)
        {
            var output = new StringBuilder();
            var error = new StringBuilder();
            try
            {
                p.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        try
                        {
                            outputWaitHandle.Set();
                        }
                        catch
                        {
                            //ignored
                        }
                    }
                    else
                    {
                        try
                        {
                            output.AppendLine(e.Data);
                        }
                        catch
                        {
                            //ignored
                        }
                    }
                };

                p.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        try
                        {
                            errorWaitHandle.Set();
                        }
                        catch
                        {
                            //ignored
                        }
                    }
                    else
                    {
                        try
                        {
                            error.AppendLine(e.Data);
                        }
                        catch
                        {
                            //ignored
                        }
                    }
                };

                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                if (p.WaitForExit(timeout) && outputWaitHandle.WaitOne(timeout) && errorWaitHandle.WaitOne(timeout))
                {
                    string strReturn;

                    if (error.ToString().Trim().Length.Equals(0) || forceRegular)
                    {
                        strReturn = output.ToString().Trim();
                    }
                    else
                    {
                        strReturn = error.ToString().Trim();
                    }

                    return strReturn;
                }
                // Timed out.
                return "PROCESS TIMEOUT";
            }
            catch (Exception ex)
            {
                //return "PROCESS TIMEOUT";
                return ex.Message;
            }
        }
    }
}
