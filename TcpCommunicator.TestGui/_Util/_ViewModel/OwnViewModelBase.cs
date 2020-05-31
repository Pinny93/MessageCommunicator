﻿using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Runtime.CompilerServices;
using System.Text;
using ReactiveUI;

namespace TcpCommunicator.TestGui
{
    public abstract class OwnViewModelBase : ReactiveObject, IActivatableViewModel
    {
        /// <inheritdoc />
        public ViewModelActivator Activator { get; }

        public event EventHandler<ViewServiceRequestEventArgs>? ViewServiceRequest;

        protected OwnViewModelBase()
        {
            this.Activator = new ViewModelActivator();
            this.WhenActivated(this.OnActivated);
        }

        protected virtual void OnActivated(CompositeDisposable disposables)
        {

        }

        /// <summary>
        /// Gets the view service of the given type.
        /// </summary>
        protected T GetViewService<T>()
            where T : class
        {
            var eventArgs = new ViewServiceRequestEventArgs(typeof(T));
            this.ViewServiceRequest?.Invoke(this, eventArgs);

            if (!(eventArgs.ViewServiceType is T result))
            {
                throw new ApplicationException($"Unable to get view service of type {typeof(T).FullName}!");
            }
            return result;
        }
    }
}
