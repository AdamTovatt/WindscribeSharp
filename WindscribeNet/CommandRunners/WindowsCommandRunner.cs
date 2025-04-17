using System.Diagnostics;
using WindscribeNet.Commands;
using WindscribeNet.Commands.ResponseParsing;

namespace WindscribeNet.CommandRunners
{
    internal class WindowsCommandRunner : ICommandRunner
    {
        private string _filePath;

        internal WindowsCommandRunner()
        {
            _filePath = DefaultPaths.CliApplicationName;
        }

        public async Task<T> RunAsync<T>(Command command)
            where T : CommandResponse, IRawResponseConvertable<T>
        {
            string? rawResponse = null;

            try
            {
                rawResponse = await RunInternalAsync(command);
            }
            catch (Exception ex)
            {
                _filePath = string.Format(DefaultPaths.WindowsCliApplicationLocation, DefaultPaths.CliApplicationName);
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
