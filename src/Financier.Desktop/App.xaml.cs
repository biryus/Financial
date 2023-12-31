﻿using NLog;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Financier.Desktop
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class App : Application
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject);

            DispatcherUnhandledException += (s, e) =>
            {
                LogUnhandledException(e.Exception);
                e.Handled = true;
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                LogUnhandledException(e.Exception);
                e.SetObserved();
            };
        }

        private static void LogUnhandledException(Exception exception)
        {
            MessageBox.Show(exception.Message);
            _logger.Error(exception);
            _logger.Error(exception.InnerException);
            _logger.Error(exception.StackTrace);
        }
    }
}
