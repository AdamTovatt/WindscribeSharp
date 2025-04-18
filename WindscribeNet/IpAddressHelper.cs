namespace WindscribeNet
{
    /// <summary>
    /// Provides a method to retrieve the current public IP address.
    /// </summary>
    public static class IpAddressHelper
    {
        private static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// Gets the current public IP address of the machine.
        /// </summary>
        /// <returns>A string containing the public IP address.</returns>
        public static async Task<string> GetCurrentAsync()
        {
            return await GetCurrentInternalAsync(0);
        }

        private static async Task<string> GetCurrentInternalAsync(int retryCount)
        {
            try
            {
                return await httpClient.GetStringAsync("https://api.ipify.org");
            }
            catch
            {
                if (retryCount >= 5)
                    throw;

                await Task.Delay(200);

                return await GetCurrentInternalAsync(retryCount + 1);
            }
        }
    }
}
