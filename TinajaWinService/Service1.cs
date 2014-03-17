using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
// using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Timers;
using TinajaWinService.myWebService;
using System.Configuration;
using System.Net.Mail;


// notes about building a windows service
// http://msdn.microsoft.com/en-us/library/zt39148a.aspx
// http://msdn.microsoft.com/en-us/magazine/cc163821.aspx#S4

namespace TinajaWinService
{
    public partial class TinajaWinService : ServiceBase
    {

        Timer timer1 = new Timer();

        public TinajaWinService()
        {
            InitializeComponent();

            // creating an event log: http://msdn.microsoft.com/en-us/library/k00ce235.aspx
            // System.Diagnostics.EventLog.DeleteEventSource("TinajaWinService");

            if (!System.Diagnostics.EventLog.SourceExists("TinajaWinServiceSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("TinajaWinServiceSource", "TinajaLog");
                return;
            }
            eventLog1.Source = "TinajaWinServiceSource";
            eventLog1.Log = "TinajaLog";
        }


        protected override void OnStart(string[] args)
        {
            // set up the timer handler, some settings and start the timer
            timer1.Elapsed += new ElapsedEventHandler(timer1_Elapsed);

            timer1.AutoReset = true;

            // interval = thousandths of a second; 1000 = 1 second
            // timer1.Interval = 5 * 60000;
            // Double IntervalMinutes = Convert.ToDouble(ConfigurationSettings.AppSettings["IntervalMinutes"]);
            Double IntervalMinutes = Convert.ToDouble(ConfigurationManager.AppSettings["IntervalMinutes"]);
            timer1.Interval = 60000 * IntervalMinutes;

            timer1.Enabled = true;
            timer1.Start();

            eventLog1.WriteEntry("TinajaWinService started. Timer interval minutes set to: " + IntervalMinutes.ToString(), EventLogEntryType.Information, 703);
        }


        protected override void OnStop()
        {
            timer1.Enabled = false;
            eventLog1.WriteEntry("TinajaWinService stopped.", EventLogEntryType.Information, 704);
        }


        private void timer1_Elapsed(object sender, EventArgs e)
        {
            myWebService.TinajaRemoteMonitoring myService = new myWebService.TinajaRemoteMonitoring();

            // ScheduledMethods is a single call for a collection of methods.
            // for now the method will return an integer, but in the future 
            // the returned integer could be used to set the timer interval
            int RetVal = myService.ScheduledMethods();

            if (RetVal < 0)
            {
                eventLog1.WriteEntry("TinajaRemoteMonitoring:ScheduledMethods error: " + RetVal.ToString(), EventLogEntryType.Error, 705);

                // send an alert email
                string emailServiceAddress = ConfigurationManager.AppSettings["ServiceEmail"];
                string emailRecipient = ConfigurationManager.AppSettings["AlertEmail"];

                try
                {
                    MailMessage message = new MailMessage();

                    message.From = new MailAddress(emailServiceAddress, "Tinaja Service");
                    message.To.Add(new MailAddress(emailRecipient, "Tinaja Manager"));
                    //mail.CC.Add(new MailAddress("donna@yourcompany.com", "Donna Summers"));
                    //mail.Bcc.Add(new MailAddress("bob@yourcompany.com", "Bob Smith"));

                    string thisComputerName = Environment.MachineName;
                    string thisOS = Environment.OSVersion.VersionString;
                    string thisServiceName = this.ServiceName;

                    message.Subject = "Tinaja WinService - Error";
                    string msg = "Windows Service (" + thisServiceName + ":ScheduledMethods) detected an error [" + RetVal.ToString() + "] at: " + DateTime.Now.ToString() + "\n";
                    msg += "Running " + thisOS + " on " + thisComputerName + ".\n";
                    msg += "Contact Tinaja support.\n";
                    message.Body = msg;

                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.Port = 587;
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    eventLog1.WriteEntry("TinajaWinService alert email error: " + ex.Message, EventLogEntryType.Error, 706);
                }
            }
        }
    }
}
