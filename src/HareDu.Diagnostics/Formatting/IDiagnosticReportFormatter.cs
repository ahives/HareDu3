namespace HareDu.Diagnostics.Formatting;

using Model;

public interface IDiagnosticReportFormatter
{
    /// <summary>
    /// Formats the specified diagnostic scanner result into a string representation, creating a detailed
    /// and structured textual diagnostic report.
    /// </summary>
    /// <param name="report">The diagnostic scanner result to format.</param>
    /// <returns>A formatted string representation of the diagnostic report.</returns>
    string Format(ScannerResult report);
}