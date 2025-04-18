# WindscribeNet

WindscribeNet is an (unofficial) .NET 8 library for controlling [Windscribe](https://windscribe.com/) via C#.
It provides clean, typed access to VPN operations like getting status, connecting and disconnecting, firewall control, and IP checking.

(IP checking is not really a part of Windscribe and is done with [https://api.ipify.org](https://api.ipify.org))

## ✅ Features

- Connect/disconnect to VPN  
- Toggle firewall on/off  
- Get connection status  
- Wait until connected/disconnected
- Wait until a provided status is obtained  
- Get current public IP  

## ⚙️ Requirements

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download)  
- Windscribe CLI installed and available in `PATH` or a known default location (this is installed when Windscribe is installed, see: [https://windscribe.com/download/](https://windscribe.com/download/)) 
- Tested on **Windows 10**. **Linux support is currently untested** but likely compatible  

## 📦 Installation

Just click install on the nuget package.

## 💻 Usage examples

### Check current status

```csharp
using WindscribeNet;

StatusCommandResponse status = await Windscribe.GetStatusAsync();
Console.WriteLine(status.ConnectState.State); // Connected / Disconnected
```

### Connect and wait

```csharp
await Windscribe.ConnectAsync();
await Windscribe.WaitUntilConnectedAsync();
```

(Note: The ```ConnectAsync()``` only returns when Windscribe does so because it's done connecting but adding the ```WaitUntilConnectedAsync()``` afterwards just makes sure that it definitely is connected by checking the status too.)

### Disconnect and wait

```csharp
await Windscribe.DisconnectAsync();
await Windscribe.WaitUntilDisconnectedAsync();
```

(Note: see the note on the connection example above, the same goes for this.)

### Toggle firewall

```csharp
using WindscribeNet.Enums;

await Windscribe.SetFirewallAsync(ActiveState.Off);
await Windscribe.SetFirewallAsync(ActiveState.On);
```

### Get current public IP

```csharp
string ip = await IpAddressHelper.GetCurrentAsync();
Console.WriteLine($"IP address: {ip}");
```

## 📖 License

MIT (do whatever you want with it, you're welcome to contribute to the repository if you have any improvements.)

## 🙏 Acknowledgements

Thanks to the creators of [Windscribe](https://windscribe.com/) for building a solid VPN product and for making parts of their code available as open source.  

This library would not have been possible without the insights gained from their CLI implementation.
