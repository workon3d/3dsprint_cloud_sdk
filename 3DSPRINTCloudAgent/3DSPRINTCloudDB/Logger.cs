﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace _3DSPRINTCloudDB
{
    /// <summary>
    /// A Logging class implementing the Singleton pattern and an internal Queue to be flushed perdiodically
    /// </summary>
    public class Logger
    {
        private static Logger instance;
        private static Queue<Log> logQueue;
        private static string logDir = @"c:\work\cloudagent_log";
        private static string logFile = "debug.log";
        private static int maxLogAge = 1; // secs
#if DEBUG
        private static int queueSize = 0;
#endif
        private static DateTime LastFlushed = DateTime.Now;

        /// <summary>
        /// Private constructor to prevent instance creation
        /// </summary>
        private Logger() { }

        ~Logger()
        {
#if DEBUG
            FlushLog();
#endif
        }
        /// <summary>
        /// An LogWriter instance that exposes a single instance
        /// </summary>
        public static Logger Instance
        {
            get
            {
                // If the instance is null then create one and init the Queue
                if (instance == null)
                {
                    instance = new Logger();
                    logQueue = new Queue<Log>();
                }
                return instance;
            }
        }

        /// <summary>
        /// The single instance method that writes to the log file
        /// </summary>
        /// <param name="message">The message to write to the log</param>
        public void log(string message)
        {
#if DEBUG
            // Lock the queue while writing to prevent contention for the log file
            lock (logQueue)
            {
                // Create the entry and push to the Queue
                Log logEntry = new Log(message);
                logQueue.Enqueue(logEntry);

                // If we have reached the Queue Size then flush the Queue
                if (logQueue.Count >= queueSize || DoPeriodicFlush())
                {
                    FlushLog();
                }
            }
#endif
        }

        public void log(string format, params object[] args)
        {
            log(string.Format(format, args));
        }

        public void error(Exception e)
        {
#if DEBUG
            log("exception: {0}\n stack trace:\n {1}", e.Message, e.StackTrace);
#endif
        }

        public void error(string format, params object[] args)
        {
            log("error: " + string.Format(format, args));
        }

        private bool DoPeriodicFlush()
        {
            TimeSpan logAge = DateTime.Now - LastFlushed;
            if (logAge.TotalSeconds >= maxLogAge)
            {
                LastFlushed = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Flushes the Queue to the physical log file
        /// </summary>
        private void FlushLog()
        {
            while (logQueue.Count > 0)
            {
                Log entry = logQueue.Dequeue();
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);

                string logPath = logDir + "\\" + logFile; 
                // string logPath = logDir + entry.LogDate + "_" + logFile;

        // This could be optimised to prevent opening and closing the file for each write
                try
                {
                    using (FileStream fs = File.Open(logPath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter log = new StreamWriter(fs))
                        {
                            log.WriteLine(string.Format("{0}{1}\t{2}", entry.LogDate, entry.LogTime, entry.Message));
                        }
                    }
                }
                catch
                {
                }
            }            
        }
    }

    /// <summary>
    /// A Log class to store the message and the Date and Time the log entry was created
    /// </summary>
    public class Log
    {
        public string Message { get; set; }
        public string LogTime { get; set; }
        public string LogDate { get; set; }

        public Log(string message)
        {
            Message = message;
            LogDate = DateTime.Now.ToString("yyyy-MM-dd");
            LogTime = DateTime.Now.ToString("hh:mm:ss.fff tt");
        }
    }
}
