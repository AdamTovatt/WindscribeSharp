using WindscribeNet;
using WindscribeNet.Commands;
using WindscribeNet.Enums;

namespace WindscribeNetTestRunner
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            StatusCommandResponse status = await Windscribe.GetStatusAsync();

            if (status.ConnectState.State == ConnectStateType.Connected)
            {
                Console.WriteLine("Was connected, will disconnect");

                await Windscribe.DisconnectAsync();
                await Windscribe.WaitUntilDisconnectedAsync();

                string realIp = await IpAddressHelper.GetCurrentAsync();
                Console.WriteLine($"Diconnected. Real ip: {realIp}");
            }

            await Task.Delay(500);

            Console.WriteLine("Will now connect");

            await Windscribe.ConnectAsync();
            await Windscribe.WaitUntilConnectedAsync();

            string fakeIp = await IpAddressHelper.GetCurrentAsync();
            Console.WriteLine($"Connected! Fake ip: {fakeIp}");
        }
    }
}
