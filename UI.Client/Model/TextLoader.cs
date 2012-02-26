using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;
using Logging;
using GalaSoft.MvvmLight.Messaging;

namespace UI.Client.Model
{
    public class TextLoader
    {
        public TextLoader(ILogger logger)
        {
            _logger = logger;

            LoadAllText();
        }

        ILogger _logger;
        XElement _xText;

        private void LoadAllText()
        {
            TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Factory.StartNew(
                () =>
                {
                    return XElement.Load(@".\tags.xml");
                }
            )
            .ContinueWith(
                task =>
                {
                    if (task.Exception != null)
                    {
                        task.Exception.Handle(LoadExceptionHandler);
                    }
                    else
                    {
                        _xText = task.Result;
                    }
                },
                scheduler
            );
        }

        public string TextFor(string tag)
        {
            var firstMatch = _xText.Descendants(tag).FirstOrDefault();
            return (firstMatch == null) ? null : firstMatch.Value;
        }

        private bool LoadExceptionHandler(Exception e)
        {
            _logger.Log(Level.Error, e.Message);
            return true;
        }
    }
}
