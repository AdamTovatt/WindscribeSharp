namespace WindscribeNet
{
    /// <summary>
    /// A static class that holds default paths for the Windscribe cli that is used to control Windscribe.
    /// If some other path than the default is required it can be changed here.
    /// </summary>
    public static class DefaultPaths
    {
        /// <summary>
        /// The file name of the cli application used to control Windscribe. Is the same for all platforms.
        /// </summary>
        public static string CliApplicationName = "windscribe-cli";

        /// <summary>
        /// The cli application location on Windows. Should be the path to the binary then a {0} where the file name would be so that it can be
        /// inserted from the CliApplicationName.
        /// </summary>
        public static string WindowsCliApplicationLocation = "\"C:\\Program Files\\Windscribe\\{0}\"";

        /// <summary>
        /// The cli application location on Linux. Should be the path to the binary then a {0} where the file name would be so that it can be
        /// inserted from the CliApplicationName.
        /// </summary>
        public static string LinuxCliApplicationLocation = "/opt/windscribe/{0}";
    }
}
