﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography;

namespace DataLogService
{
    /// <summary>
    /// Datalogger is the main class for logging values from various data feeds.  Data feeds might be generated by specific devices or by data collected online.
    /// Datalog is the main entity object (based on entity framework) that provides access to the database.  The promary table for the data logging function is datalog: id, logtime, apikey, logvalue
    /// TinajaEntities2 is the main object context (and the name of the connection string) based on the model file: DataLogServiceModel.edmx
    /// </summary>
    [WebService(Namespace = "http://tinajalabs.net/tinajadl/", Name = "Tinaja Data Logging", Description = "Web Services supporting the Tinaja Data Logging application.")]
    // [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class datalogger : System.Web.Services.WebService
    {

        [WebMethod(Description = "Insert a data log record - apikey, logvalue")]
        public string insertLog(string apikey, string logval)
        {
            tinajaEntities2 myEnt = new tinajaEntities2();
            // datalog dl = new datalog { apikey = theKey, logvalue = theValue };

            decimal logResult = 0;

            try
            {
                if (string.IsNullOrEmpty(apikey))
                    throw new Exception("Error: apikey cannot be blank.");

                if (!decimal.TryParse(logval, out logResult))
                    throw new Exception("Error: logvalue must be a decimal type with up to 4 decimal places.  (e.g., -999999.9999 to 999999.9999)");
            }
            catch (FormatException exFormat)
            {
                LogIt("Fail, validation FormatException - insertLog: " + exFormat.Message);
                return "Fail, validation FormatException - insertLog: " + exFormat.Message;
            }
            catch (Exception ex)
            {
                LogIt("Fail, validation Exception - insertLog: " + ex.Message);
                return "Fail, validation Exception - insertLog: " + ex.Message;
            }

            decimal logvalue = logResult;

            datalog myDL = new datalog();
            myDL.apikey = apikey;
            myDL.logvalue = logvalue;
            myDL.logtime = DateTime.UtcNow;
            myDL.id = Guid.NewGuid();

            try
            {
                myEnt.AddTodatalog(myDL);
                myEnt.SaveChanges();
            }
            catch (FormatException exFormat)
            {
                LogIt("Fail, SaveChanges FormatException - insertLog: " + exFormat.Message);
                return "Fail, SaveChanges FormatException - insertLog: " + exFormat.Message;
            }
            catch (Exception ex)
            {
                LogIt("Fail, SaveChanges Exception - insertLog: " + ex.Message);
                return "Fail, SaveChanges Exception - insertLog: " + ex.Message;
            }

            return "Inserted";
        }

        /// <summary>
        /// Get all log entries as a collection of datalog objects
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "Get all log entries as a collection of datalog objects")]
        public List<datalog> getAllLogs()
        {
            var myEnt = new tinajaEntities2();
            var logset = from datalog in myEnt.datalog
                         orderby datalog.logtime
                         select datalog;
            return logset.ToList<datalog>();
        }


        /// <summary>
        /// Get all log entries by apikey
        /// </summary>
        /// <param name="apikey">A key to make it possible to retrieve the records for a given log</param>
        /// <returns>All records for a given apikey as a collection of datalog objects</returns>
        [WebMethod(Description = "Get all log entries by apikey as a collection of datalog objects")]
        public List<datalog> getLogsByApikey(string apikey)
        {
            var myEnt = new tinajaEntities2();
            var logset = from datalog in myEnt.datalog
                         where datalog.apikey == apikey
                         orderby datalog.logtime
                         select datalog;
            return logset.ToList<datalog>();
        }


        /// <summary>
        /// Get all log entries by apikey and for the last n number of hours, usually 24.  For charting purposes.
        /// </summary>
        /// <param name="apikey">A key to make it possible to retrieve the records for a given log</param>
        /// <param name="hours">The number of hours to retrieve from the latest record available</param>
        /// <returns></returns>
        [WebMethod(Description = "Get all log entries by apikey and for the last n number of hours, usually 24.  For charting purposes.")]
        public List<datalog> getLogsByApikeyHours(string apikey, int hours)
        {
            // get the negative value of the hours supplied
            int negHours = hours * -1;
            DateTime hoursBackTime = DateTime.UtcNow.AddHours(negHours);
            var myEnt = new tinajaEntities2();
            var logset = from datalog in myEnt.datalog
                         where (datalog.apikey == apikey) && (datalog.logtime >= hoursBackTime)
                         orderby datalog.logtime
                         select datalog;
            return logset.ToList<datalog>();
        }


        /// <summary>
        /// Generate an API key
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "Generate an API key for use with data logging calls")]
        public string genApiKey()
        {
            int maxSize = 8;
            int minSize = 5;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }

        /// <summary>
        /// Gets the version of the current build
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "Gets the version of the current build")]
        public string getServiceVersion()
        {
            string thisVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            return thisVersion;
        }

        /// <summary>
        /// Service logger tracks application errors and status updates
        /// </summary>
        /// <param name="logString">The string of content to save to the file</param>
        public void LogIt(string logString)
        {
            string logFile = ConfigurationManager.AppSettings["logFile"];

            // save the logfile in the root path of the site
            string physicalPath = Server.MapPath(logFile);

            StreamWriter outfile = new StreamWriter(physicalPath, true);
            outfile.WriteLine(DateTime.Now.ToString() + ": " + logString + " [version: " + getServiceVersion() + "]");
            outfile.Close();
        }

    }
}
