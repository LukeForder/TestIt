using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Logging;
using Microsoft.Practices.ServiceLocation;

namespace UI.Admin.ViewModels
{
    public abstract class ViewModelExtended : ViewModelBase
    {
        public ViewModelExtended()
        {
            _logger = ServiceLocator.Current.GetInstance<ILogger>();
        }

        protected ILogger _logger;
        private int _isDirtyCount;

        public bool IsDirty
        {
            get
            {
                return _isDirtyCount != 0;
            }
        }

        protected void ClearDirty()
        {
            if (IsDirty)
            {
                _isDirtyCount = 0;
                RaiseIsDirtyChanged();
            }
        }

        protected void IncreaseDirtyLevel()
        {
            _isDirtyCount++;

            if (_isDirtyCount != 0)
                RaiseIsDirtyChanged();
        }

        protected void DecreaseDirtyLevel()
        {
            if (_isDirtyCount == 0)
            {
                _logger.Log(Level.Warning, "Decreasing the dirtiness of clean object.");
                return;
            }

            _isDirtyCount--;

            if (_isDirtyCount == 0)
                RaiseIsDirtyChanged();
        }

        private void RaiseIsDirtyChanged()
        {
            var handler = IsDirtyChangedEvent;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public event EventHandler IsDirtyChangedEvent;

        public override void Cleanup()
        {
            base.Cleanup();

            _logger = null;
        }
    }
}
