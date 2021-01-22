using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LibraryEngine.Network;
//using RainwayIPC.Models;
using FlatSharp;

namespace LibraryEngine.Systems
{
    public class NetClient : ISystem
    {

        public string ip;
        public ushort port;
        private TcpClient client;

        public NetClient(string ip, ushort port)
        {
            this.ip = ip;
            this.port = port;

        }

        public void Update(Scene scene, GameTime gameTime)
        {
            // send input buffer over every frame
            Task.Run(() => client.ProcessMessages(HandleMessage));

            var packet = new Packet();
            packet.type = PacketType.Input;
            packet.data = new Data(new InputData()
            {
                // messy conversion that we may need to look at later
                inputBuffer = (uint[])(object)Array.ConvertAll(InputBuffer.Instance.inputBuffer, value => (int)value)
            });


            this.client.SendMessage(packet);
        }

        public void Initialize()
        {
            this.client = Connect();

            
        }

        public void TestSend()
        {
            // send input buffer over every frame
            Task.Run(() => client.ProcessMessages(HandleMessage));

            var packet = new Packet();
            packet.type = PacketType.Input;
            packet.data = new Data(new InputData()
            {
                // messy conversion that we may need to look at later
                inputBuffer = (uint[])(object)Array.ConvertAll(InputBuffer.Instance.inputBuffer, value => (int)value)
            });


            this.client.SendMessage(packet);
        }

        public void FlushInputBuffer()
        {

        }

        public void HandleMessage(Packet packet)
        {
            switch (packet.type)
            {
                case PacketType.TestMessage:
                    Console.WriteLine(packet.data.TestMessageData.msg);
                    break;

                case PacketType.Sync:
                    break;
                default:
                    Console.WriteLine($"Unhandled message type: {packet.type}");

                    break;
            }

        }

        private TcpClient Connect()
        {
            client = new TcpClient();

            client.Connect(ip, port);

            return client;
        }

    }
}
