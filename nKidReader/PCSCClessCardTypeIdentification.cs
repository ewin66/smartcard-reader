/*
Copyright (c) 2011, Gerhard H. Schalk (www.smartcard-magic.net)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the 
documentation and/or other materials provided with the distribution.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT 
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT 
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT 
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON 
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.*/
using System;
using GS.Apdu;
using GS.PCSC;
using GS.SCard;
using GS.SCard.Const;
using GS.Util.Hex;
using GS.Util.ByteArray;
using System.Diagnostics;

namespace nKidReader
{
    public class PCSCClessCardTypeIdentification
    {
        static byte[] baAtrMifare1K = new byte[]{ 0x3B,					    // Initial
                    						      0x8F,					    // T0
                							      0x80,					    // TD1
                							      0x01,					    // TD2
                							      0x80,					    // Category Indicator
                						 	      0x4F,					    // Appl. Id. Precence Indicator
                							      0x0C,					    // Tag Length
                						          0xA0,0x00,0x00,0x03,0x06, // AID
                							      0x03,					    // SS
                							      0x00,0x01,				// NN - Mifare 1k
                							      0x00,0x00,0x00,0x00,		// r.f.u
                							      0x6A};					// TCK

        static byte[] baAtrMifare4K = new byte[]{ 0x3B,					 // Initial
             							           0x8F,					 // T0
            							           0x80,					 // TD1
            							           0x01,					 // TD2
                    							   0x80,					 // Category Indicator
                    							   0x4F,					 // Appl. Id. Precence Indicator
                    							   0x0C,					 // Tag Length
                    							   0xA0,0x00,0x00,0x03,0x06, // AID
                    							   0x03,				     // SS
                    							   0x00,0x02,				 // NN - Mifare 4k
                    							   0x00,0x00,0x00,0x00,		 // r.f.u
                    							   0x69};					 // TCK

        static byte[] baAtrMifareUL = new byte[]{ 0x3B,						// Initial
                    							  0x8F,						// T0
                    							  0x80,						// TD1
                    							  0x01,						// TD2
                    							  0x80,						// Category Indicator
                    							  0x4F,						// Appl. Id. Precence Indicator
                    							  0x0C,						// Tag Length
                    							  0xA0,0x00,0x00,0x03,0x06,	// AID
                    							  0x03,						// SS
                    							  0x00,0x03,				// NN - Mifare Ultra Light
                    							  0x00,0x00,0x00,0x00,		// r.f.u
                    							  0x68};					// TCK        





        public static void ready(MainForm script)
        {
            PCSCReader reader = new PCSCReader();

            try
            {
                //Debug.WriteLine("Run reader...");
                reader.Connect();

                reader.ActivateCard();

                    //reader.SCard.WaitForCardPresent();
                    reader.SCard.Reconnect(SCARD_DISCONNECT.Unpower);

                    RespApdu respApdu = reader.Exchange("FF CA 00 00 00");// Get Card UID
                    if (respApdu.SW1SW2 == 0x9000)
                    {
                        string uid = HexFormatting.ToHexString(respApdu.Data, true);
                        //Debug.WriteLine("UID  = 0x" + HexFormatting.ToHexString(respApdu.Data, true));
                        script.Invoke(script.delegateDetectCardID, uid);
                    }
                    else
                    {
                        Debug.WriteLine("Please use a PC/SC2.01 compliant contactless Reader!");
                    }
                    reader.SCard.WaitForCardRemoval();
            }
            catch (WinSCardException ex)
            {
                //Debug.WriteLine(ex.WinSCardFunctionName + " Error 0x" + ex.Status.ToString("X08") + ": " + ex.Message);
                script.Invoke(script.delegateReaderError, ex);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);
            }
            finally
            {
                reader.Disconnect();
            }
        }
    }
}