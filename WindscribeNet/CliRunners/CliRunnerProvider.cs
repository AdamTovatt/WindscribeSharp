using System.Runtime.InteropServices;

namespace WindscribeNet.CliRunners
{
    /// <summary>
    /// Provides access to a platform-specific <see cref="ICliRunner"/> implementation.
    /// </summary>
    internal static class CliRunnerProvider
    {
        private static readonly Lazy<ICliRunner> instance = new Lazy<ICliRunner>(CreateRunner);

        /// <summary>
        /// Gets the platform-specific <see cref="ICliRunner"/> instance.
        /// </summary>
        internal static ICliRunner Instance => instance.Value;

        private static ICliRunner CreateRunner()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsCliRunner();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return new LinuxCliRunner();
            }

            throw new PlatformNotSupportedException("Only Windows and Linux are supported.");
        }
    }
}
