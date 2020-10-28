﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageCommunicator.TestGui.ViewServices
{
    public class ExportViewService : IExportViewService
    {
        private DialogHostControl _host;

        public ExportViewService(DialogHostControl host)
        {
            _host = host;
        }

        /// <inheritdoc />
        public async Task ExportAsync<T>(IEnumerable<T> allObjects, IEnumerable<T> objectsToExport, string nameProperty, string dataTypeName)
        {
            var exportDlg = new ExportDialogControl();
            exportDlg.DataContext = new ExportDialogControlViewModel(allObjects, objectsToExport, nameProperty, dataTypeName);

            await exportDlg.ShowControlDialogAsync(_host, "Export");
        }
    }
}
