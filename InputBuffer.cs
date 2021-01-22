using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryEngine
{
    public class InputBuffer
    {
        private static readonly object instanceLock = new object();
        private static InputBuffer instance;
        public static InputBuffer Instance
        {
            get
            {

                if (instance == null)
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new InputBuffer();
                        }

                    }
                }
                return instance;
            }
        }

        public Command[] inputBuffer { get; private set; }
        private int inputIndex;

        private InputBuffer()
        {
            inputBuffer = new Command[60];
            inputIndex = 0;
        }

        public void Add(Command command)
        {
            inputBuffer[inputIndex] = command;
            inputIndex++;
            if (inputIndex >= inputBuffer.Length)
            {
                inputIndex = 0;
            }
        }

        public bool Contains(Command command)
        {
            for (var i = 0; i < inputBuffer.Length; i++)
            {
                if (inputBuffer[i] == command)
                {
                    return true;
                }
            }
            return false;

        }

        public void Purge()
        {
            Array.Clear(inputBuffer, 0, inputBuffer.Length);
        }

        

    }
}
