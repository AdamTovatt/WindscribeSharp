using System.Diagnostics;
using System.Runtime.InteropServices;
using WindscribeNet.Commands;
using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.CliRunners
{
    internal class WindowsCliRunner : ICliRunner
    {
        private string _filePath;

        internal WindowsCliRunner()
        {
            _filePath = DefaultPaths.CliApplicationName;
        }

        public async Task<T> RunAsync<T>(Command command)
            where T : CommandResponse, IRawResponseConvertable<T>
        {
            string? rawResponse;

            try
            {
                rawResponse = await RunInternalAsync(command);
            }
            catch (ExternalException exception)
            {
                if (exception.Message.Contains("system cannot find the file"))
                {
                    _filePath = string.Format(DefaultPaths.WindowsCliApplicationLocation, DefaultPaths.CliApplicationName);
                    rawResponse = await RunInternalAsync(command);
                }
                else
                {
                    throw;
                }
            }

            return T.FromRawText(rawResponse);
        }

        private async Task<string> RunInternalAsync(Command command)
        {
            string arguments = command.ToString();

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = _filePath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                return output;
            }
        }
    }
}
