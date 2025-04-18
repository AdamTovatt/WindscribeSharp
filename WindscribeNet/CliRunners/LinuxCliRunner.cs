using System.ComponentModel;
using System.Diagnostics;
using WindscribeNet.Commands;
using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.CliRunners
{
    internal class LinuxCliRunner : ICliRunner
    {
        private string _filePath;

        internal LinuxCliRunner()
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
            catch (Exception exception) when (exception is FileNotFoundException or Win32Exception)
            {
                _filePath = string.Format(DefaultPaths.LinuxCliApplicationLocation, DefaultPaths.CliApplicationName);
                rawResponse = await RunInternalAsync(command);
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
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new Process { StartInfo = startInfo };
            process.Start();

            string output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            return output;
        }
    }
}
