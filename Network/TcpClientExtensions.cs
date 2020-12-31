using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using RainwayIPC.Models;

namespace TestLibraryEngine.Network

{
    public static class TcpClientExtensions
    {
		public delegate void MessageHandler(Packet message);


		public static void SendMessage(this TcpClient socket, Packet packet)
        {
			var bytes = packet.Encode();

			var stream = socket.GetStream();
			stream.Write(bytes);

        }

		public static void ProcessMessages(this TcpClient socket, MessageHandler processMessage)
		{
			// begin listening for messages from the server forever
			byte[] readBuffer = new byte[8046];
			int bytesRead;
			Packet message = null;
			List<byte> bytes = new List<byte>();

			var stream = socket.GetStream();

			while (true)
			{
				while ((bytesRead = stream.Read(readBuffer, 0, readBuffer.Length)) > 0)
				{
					if (bytes == null)
					{
						bytes = new List<byte>();
					}
					else
					{
						bytes.AddRange(readBuffer[0..bytesRead]);
					}

					try
					{
						message = Packet.Decode(bytes.ToArray());	
					}
					catch (Exception e)
					{
						Console.WriteLine($"Error processing message: {e.Message}");
					}

					processMessage(message);
					
				}
			}
		}
	}
}
