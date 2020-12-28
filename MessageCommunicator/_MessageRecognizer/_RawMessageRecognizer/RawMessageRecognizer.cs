using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.GuardClauses;
using MessageCommunicator.Util;

namespace MessageCommunicator
{
    /// <summary>
    /// This <see cref="MessageRecognizer"/> implements a custom messages style of the MessageCommunicator
    /// project. 
    /// </summary>
    public class RawMessageRecognizer : MessageRecognizer
    {
        private Encoding _encoding;
        private Decoder _decoder;

        /// <summary>
        /// Creates a new <see cref="RawMessageRecognizer"/> instance.
        /// </summary>
        /// <param name="encoding">The <see cref="Encoding"/> to be used when convert characters to/from bytes.</param>
        public RawMessageRecognizer(Encoding encoding)
        {
            encoding.MustNotBeNull(nameof(encoding));

            _encoding = encoding;
            _decoder = _encoding.GetDecoder();
        }

        /// <inheritdoc />
        protected override Task<bool> SendInternalAsync(IByteStreamHandler byteStreamHandler, ReadOnlySpan<char> rawMessage)
        {
            byteStreamHandler.MustNotBeNull(nameof(byteStreamHandler));

            var rawMessageLength = rawMessage.Length;
            var lengthDigitCount = TcpCommunicatorUtil.GetCountOfDigits(rawMessageLength);
            var sendBuffer = StringBuffer.Acquire(rawMessageLength + 3 + lengthDigitCount);

            byte[]? bytes = null;
            try
            {
                if(rawMessage.Length > 0){ sendBuffer.Append(rawMessage); }
                sendBuffer.GetInternalData(out var buffer, out var currentCount);

                var sendMessageByteLength = _encoding.GetByteCount(buffer, 0, currentCount);
                bytes = ByteArrayPool.Take(sendMessageByteLength);

                _encoding.GetBytes(buffer, 0, currentCount, bytes, 0);
                StringBuffer.Release(sendBuffer);
                sendBuffer = null;

                return byteStreamHandler.SendAsync(
                    new ArraySegment<byte>(bytes, 0, sendMessageByteLength));
            }
            finally
            {
                if (bytes != null)
                {
                    ByteArrayPool.Return(bytes);
                }
                if (sendBuffer != null)
                {
                    StringBuffer.Release(sendBuffer);
                }
            }
        }

        /// <inheritdoc />
        public override void OnReceivedBytes(bool isNewConnection, ArraySegment<byte> receivedBytes)
        {
            receivedBytes.MustNotBeDefault(nameof(receivedBytes));

            // Clear receive buffer on new connections
            if (isNewConnection)
            {
                _decoder.Reset();
            }

            // Parse characters
            if (receivedBytes.Count == 0) { return; }

            // Raise found message
            base.NotifyRecognizedMessage(receivedBytes.Select((curByte => (char)curByte)).ToArray());
        }
    }
}
