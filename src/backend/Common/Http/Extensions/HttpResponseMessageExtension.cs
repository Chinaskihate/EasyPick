using Http.Exceptions;
using System.Net;

namespace Http.Extensions;

public static class HttpResponseMessageExtension
{
    public static void ThrowIfNotSuccess(this HttpResponseMessage message)
    {
        // TODO: add handling for other status codes
        switch (message.StatusCode)
        {
            case HttpStatusCode.NotFound:
                throw new HttpNotFoundException(message.ReasonPhrase, message.RequestMessage.RequestUri);
            case HttpStatusCode.OK:
                return;
            default:
                throw new NotImplementedException($"No handling for status code {message.StatusCode} yet!");
        }
    }
}