using System;
using System.Collections.Generic;
using System.Net.Sockets;
//using RainwayIPC.Models;
using FlatSharp;

namespace LibraryEngine.Network

{
    public static class TcpClientExtensions
    {
		public delegate void MessageHandler(Packet message);


		public static void SendMessage(this TcpClient socket, Packet packet)
        {
			int maxBytesNeeded = FlatBufferSerializer.Default.GetMaxSize(packet);
			byte[] bytes = new byte[maxBytesNeeded];
			int bytesWritten = FlatBufferSerializer.Default.Serialize(packet, bytes);

			var stream = socket.GetStream();
			stream.Write(bytes);

			//packet = new Packet

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
						message = FlatBufferSerializer.Default.Parse<Packet>((Memory<byte>)bytes.ToArray());
						
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
