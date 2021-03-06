﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MessageCommunicator.TestGui.Data
{
    internal static class MessageRecognizerSettingsFactory
    {
        public static IMessageRecognizerAppSettings CreateSettings(MessageRecognitionMode recognitionMode)
        {
            switch (recognitionMode)
            {
                case MessageRecognitionMode.Default:
                    return new MessageRecognizerDefaultSettings();

                case MessageRecognitionMode.EndSymbol:
                    return new MessageRecognizerEndSymbolSettings();

                case MessageRecognitionMode.FixedLength:
                    return new MessageRecognizerFixedLengthSettings();

                case MessageRecognitionMode.FixedLengthAndEndSymbol:
                    return new MessageRecognizerFixedLengthAndEndSymbolsSettings();

                case MessageRecognitionMode.StartAndEndSymbol:
                    return new MessageRecognizerStartAndEndSymbolSettings();

                case MessageRecognitionMode.Raw:
                    return new MessageRecognizerRawSettings();

                case MessageRecognitionMode.ByUnderlyingPackage:
                    return new MessageRecognizerByUnderlyingPackageSettings();

                default:
                    throw new ApplicationException($"Unknown message recognition mode: {recognitionMode}");
            }
        }
    }
}
