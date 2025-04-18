using WindscribeNet.CliRunners;
using WindscribeNet.Commands;
using WindscribeNet.Enums;

namespace WindscribeNet
{
    /// <summary>
    /// Provides high-level static methods to interact with the Windscribe CLI.
    /// </summary>
    public static class Windscribe
    {
        private static readonly ICliRunner cliRunner = CliRunnerProvider.Instance;

        /// <summary>
        /// Gets the current status of Windscribe.
        /// </summary>
        public static async Task<StatusCommandResponse> GetStatusAsync()
        {
            return await cliRunner.RunAsync<StatusCommandResponse>(new StatusCommand());
        }

        /// <summary>
        /// Sets the state of the Windscribe firewall.
        /// </summary>
        /// <param name="state">The desired firewall state (On or Off).</param>
        public static async Task<FirewallCommandResponse> SetFirewallAsync(ActiveState state)
        {
            return await cliRunner.RunAsync<FirewallCommandResponse>(new FirewallCommand(state));
        }

        /// <summary>
        /// Connects to Windscribe using the specified parameters.
        /// </summary>
        /// <param name="location">The location to connect to (optional).</param>
        /// <param name="isStatic">Whether to use a static IP location.</param>
        /// <param name="protocol">The VPN protocol to use (optional).</param>
        /// <param name="nonBlocking">Whether to return immediately without waiting for connection.</param>
        public static async Task<ConnectCommandResponse> ConnectAsync(string? location = null, bool isStatic = false, string? protocol = null, bool nonBlocking = false)
        {
            return await cliRunner.RunAsync<ConnectCommandResponse>(new ConnectCommand(location, isStatic, protocol, nonBlocking));
        }

        /// <summary>
        /// Disconnects from the current Windscribe connection.
        /// </summary>
        public static async Task<DisconnectCommandResponse> DisconnectAsync()
        {
            return await cliRunner.RunAsync<DisconnectCommandResponse>(new DisconnectCommand());
        }

        /// <summary>
        /// Waits for the Windscribe status to satisfy a specified condition.
        /// </summary>
        /// <param name="condition">The condition to evaluate against the status.</param>
        /// <param name="timeout">Optional timeout duration. Defaults to 30 seconds.</param>
        /// <param name="pollIntervalMilliseconds">Polling interval in milliseconds.</param>
        /// <param name="cancellationToken">A token to cancel the wait operation.</param>
        public static async Task<StatusCommandResponse> WaitForStatusAsync(
            Func<StatusCommandResponse, bool> condition,
            TimeSpan? timeout = null,
            int pollIntervalMilliseconds = 250,
            CancellationToken cancellationToken = default)
        {
            DateTime start = DateTime.UtcNow;
            TimeSpan effectiveTimeout = timeout ?? TimeSpan.FromSeconds(30);

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                StatusCommandResponse status = await GetStatusAsync();
                if (condition(status))
                    return status;

                if (DateTime.UtcNow - start > effectiveTimeout)
                    throw new TimeoutException("Timeout while waiting for status condition to be satisfied.");

                await Task.Delay(pollIntervalMilliseconds, cancellationToken);
            }
        }

        /// <summary>
        /// Waits until Windscribe is connected.
        /// </summary>
        /// <param name="timeout">Optional timeout duration. Defaults to 30 seconds.</param>
        /// <param name="pollIntervalMilliseconds">Polling interval in milliseconds.</param>
        /// <param name="cancellationToken">A token to cancel the wait operation.</param>
        public static async Task<StatusCommandResponse> WaitUntilConnectedAsync(
            TimeSpan? timeout = null,
            int pollIntervalMilliseconds = 250,
            CancellationToken cancellationToken = default)
        {
            return await WaitForStatusAsync(
                status => status.ConnectState.State == ConnectStateType.Connected,
                timeout,
                pollIntervalMilliseconds,
                cancellationToken
            );
        }

        /// <summary>
        /// Waits until Windscribe is disconnected.
        /// </summary>
        /// <param name="timeout">Optional timeout duration. Defaults to 30 seconds.</param>
        /// <param name="pollIntervalMilliseconds">Polling interval in milliseconds.</param>
        /// <param name="cancellationToken">A token to cancel the wait operation.</param>
        public static async Task<StatusCommandResponse> WaitUntilDisconnectedAsync(
            TimeSpan? timeout = null,
            int pollIntervalMilliseconds = 250,
            CancellationToken cancellationToken = default)
        {
            return await WaitForStatusAsync(
                status => status.ConnectState.State == ConnectStateType.Disconnected,
                timeout,
                pollIntervalMilliseconds,
                cancellationToken
            );
        }
    }
}
