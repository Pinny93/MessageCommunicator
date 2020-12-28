using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MessageCommunicator.TestGui.Data
{
    [TypeAlias("MessageRecognizerRawSettings")]
    public class MessageRecognizerRawSettings : IMessageRecognizerAppSettings
    {
        private const string CATEGORY = "Raw Recognizer";

        [EncodingWebName]
        [Category(CATEGORY)]
        public string Encoding { get; set; } = "utf-8";

        /// <inheritdoc />
        public MessageRecognizerSettings CreateLibSettings()
        {
            return new RawMessageRecognizerSettings(
                System.Text.Encoding.GetEncoding(this.Encoding));
        }
    }
}
