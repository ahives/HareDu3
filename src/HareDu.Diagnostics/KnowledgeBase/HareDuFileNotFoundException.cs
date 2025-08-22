namespace HareDu.Diagnostics.KnowledgeBase;

using System;
using System.IO;
using System.Runtime.Serialization;

public class HareDuFileNotFoundException :
    FileNotFoundException
{
    public HareDuFileNotFoundException()
    {
    }

    protected HareDuFileNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public HareDuFileNotFoundException(string message)
        : base(message)
    {
    }

    public HareDuFileNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public HareDuFileNotFoundException(string message, string fileName)
        : base(message, fileName)
    {
    }

    public HareDuFileNotFoundException(string message, string fileName, Exception innerException)
        : base(message, fileName, innerException)
    {
    }
}