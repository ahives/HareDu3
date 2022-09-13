namespace HareDu.Diagnostics.Formatting;

using System.Text;

public class DiagnosticReportTextFormatter :
    IDiagnosticReportFormatter
{
    public string Format(ScannerResult report)
    {
        var builder = new StringBuilder();

        builder.AppendLine($"Report Identifier: {report.Id.ToString()}");
        builder.AppendLine($"Timestamp: {report.Timestamp.ToString()}");
        builder.AppendLine();

        builder.AppendLine("Results");
            
        foreach (var result in report.Results)
            FormatProbeResult(result, builder);

        return builder.ToString();
    }

    void FormatProbeResult(ProbeResult result, StringBuilder builder)
    {
        builder.AppendLine($"\tTimestamp: {result.Timestamp.ToString()}");
        builder.AppendLine($"\tComponent Identifier: {result.ComponentId}");
        builder.AppendLine($"\tComponent Type: {result.ComponentType.ToString()}");
        builder.AppendLine($"\tProbe Identifier: {result.Id}");
        builder.AppendLine($"\tProbe Name: {result.Name}");
        builder.AppendLine($"\tStatus: {result.Status.ToString()}");
        builder.AppendLine("\tProbe Data");
            
        foreach (var data in result.Data)
        {
            if (data is null)
                continue;
                
            builder.AppendLine($"\t\t{data.PropertyName} => {data.PropertyValue}");
        }

        builder.AppendLine($"\tReason: {result.KB?.Reason}");
        builder.AppendLine($"\tRemediation: {result.KB?.Remediation}");
        builder.AppendLine();
    }
}