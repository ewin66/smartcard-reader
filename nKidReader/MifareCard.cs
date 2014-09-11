using System;
using System.Diagnostics;
using PCSC;
using PCSC.Iso7816;

namespace nKidReader
{
    public enum KeyStructure : byte
    {
        VolatileMemory = 0x00,
        NonVolatileMemory = 0x20
    }

    public class MifareCard
    {
        private readonly IIsoReader _isoReader;

        public MifareCard(IIsoReader isoReader)
        {
            if (isoReader == null)
            {
                throw new ArgumentNullException("isoReader");
            }
            _isoReader = isoReader;
        }

        public bool LoadKey(KeyStructure keyStructure, int keyNumber, byte[] key)
        {
            unchecked
            {

                var loadKeyCmd = new CommandApdu(IsoCase.Case3Short, SCardProtocol.Any)
                {
                    CLA = 0xFF,
                    Instruction = InstructionCode.ExternalAuthenticate,
                    P1 = (byte)keyStructure,
                    P2 = (byte)keyNumber,
                    Data = key
                };

                Debug.WriteLine(string.Format("Load Authentication Keys: {0}", BitConverter.ToString(loadKeyCmd.ToArray())));
                var response = _isoReader.Transmit(loadKeyCmd);
                Debug.WriteLine(string.Format("SW1 SW2 = {0:X2} {1:X2}", response.SW1, response.SW2));

                return Success(response);
            }
        }

        public byte[] GetUID()
        {
            unchecked
            {
                var loadKeyCmd = new CommandApdu(IsoCase.Case2Short, SCardProtocol.Any)
                {
                    CLA = 0xFF,
                    Instruction = InstructionCode.GetData,
                    P1 = 0x00,
                    P2 = 0x00,
                    Le = 0x00
                };
                Response response = _isoReader.Transmit(loadKeyCmd);
                bool success = Success(response);
                if (success)
                    return response.GetData();
                else
                    return null;
            }
        }

        private static bool Success(Response response)
        {
            unchecked
            {
                return (response.SW1 == (byte)SW1Code.Normal) && (response.SW2 == 0x00);
            }
        }

    }
}