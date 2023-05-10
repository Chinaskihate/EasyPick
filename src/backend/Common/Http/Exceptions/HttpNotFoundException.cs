namespace Http.Exceptions;

public class HttpNotFoundException : Exception
{
    public HttpNotFoundException(string reason, Uri uri) : base(reason)
    {
        Uri = uri;
    }

    public Uri Uri { get; set; }
}