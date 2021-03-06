﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;

namespace MessageCommunicator.TestGui
{
    public class VectorIconDrawingPresenter : DrawingPresenter, IWeakMessageTarget<MessageThemeChanged>
    {
        public VectorIconDrawingPresenter()
        {
            MessageBus.Current.ListenWeak(this);
        }

        public void UpdateBrushes()
        {
            if (this.Drawing is VectorIconGeometryDrawing vectorIconDrawing)
            {
                vectorIconDrawing.UpdateBrushes();
            }

            this.InvalidateVisual();
        }

        /// <inheritdoc />
        public void OnMessage(MessageThemeChanged message)
        {
            this.UpdateBrushes();
        }
    }
}
