using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;

namespace ActiveDirectoryBroker.Services
{
    partial class BrokerHost : ServiceBase
    {
        ServiceHost _host;

        public BrokerHost()
        {
            InitializeComponent();

            ServiceName = "ActiveDirectoryBroker";
            AutoLog = true;
            CanHandlePowerEvent = false;
            CanHandleSessionChangeEvent = false;
            CanPauseAndContinue = true;
            CanShutdown = true;
            CanStop = true;
        }

        void Log(string messageFormat, params object[] parts)
        {
            var message = string.Format(messageFormat, parts);
            if (Environment.UserInteractive)
                Console.WriteLine(message);
            else
                EventLog.WriteEntry(message, EventLogEntryType.Information);
        }

        internal void StartService()
        {
            StopService();

            var serviceUrl = ConfigurationManager.AppSettings["WEB_SERVICE_URL"];
            if (string.IsNullOrEmpty(serviceUrl))
            {
                Log("Web service URL is missing from configurations.");
                return;

            }

            if (_host == null)
            {
                var baseAddress = new Uri(serviceUrl);
                try
                {
                    _host = new ServiceHost(typeof(Broker), baseAddress);
                    Log("Web service host created at {0}.", serviceUrl);
                }
                catch
                {
                    Log("Failed to create web service host at {0}.", serviceUrl);
                    return;
                }
                
            }

            if (_host.State == CommunicationState.Opened)
            {
                Log("Web service is already running at {0}.", serviceUrl);
                return;
            }

            try
            {
                _host.Open();
                Log("Web service started at {0}.", serviceUrl);
            }
            catch(Exception ex)
            {
                Log("Failed to start web service at {0}. {1}", serviceUrl, ex.Message);
            }
        }

        internal void StopService()
        {
            if (_host == null)
                return;

            if (_host.State == CommunicationState.Opened)
            {
                _host.Close();
                Log("Web service stopped.");
            }
            else
                Log("Web service is not open.");
        }

        #region service methods

        protected override void OnStart(string[] args)
        {
            StartService();
        }

        protected override void OnStop()
        {
            StopService();
        }

        protected override void OnPause()
        {
            StopService();
            base.OnPause();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            StartService();
        }

        protected override void OnShutdown()
        {
            StopService();
            base.OnShutdown();
        }

        #endregion
    }
}
