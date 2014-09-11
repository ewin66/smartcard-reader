using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCSC;
using System.Diagnostics;
using PCSC.Iso7816;

namespace nKidReader
{
    class PCSCSharp
    {
        private static SCardMonitor monitor;
        private static SCardContext context;
        private static MainForm mainForm;

        public static void Ready(MainForm mf)
        {
            try
            {
                Debug.WriteLine("This program will monitor all SmartCard readers and display all status changes.");
                Debug.WriteLine("Press a key to continue.");

                mainForm = mf;

                // Retrieve the names of all installed readers.
                string[] readerNames;
                context = new SCardContext();
                context.Establish(SCardScope.System);
                readerNames = context.GetReaders();
                //context.Release();

                if (readerNames == null || readerNames.Length == 0)
                {
                    Debug.WriteLine("There are currently no readers installed.");
                    SCardError error = new SCardError();
                    PCSCException ex = new PCSCException(error, "Không tìm thấy máy quét nào.");
                    //mainForm.Invoke(mainForm.delegateReaderError, ex);
                    return;
                }

                // Create a monitor object with its own PC/SC context. 
                // The context will be released after monitor.Dispose()
                monitor = new SCardMonitor(new SCardContext(), SCardScope.System);
                // Point the callback function(s) to the anonymous & static defined methods below.
                monitor.CardInserted += (sender, args) => CardInsertedEvent("CardInserted", args);
                monitor.CardRemoved += (sender, args) => DisplayEvent("CardRemoved", args);
                monitor.Initialized += (sender, args) => DisplayEvent("Initialized", args);
                monitor.StatusChanged += StatusChanged;
                monitor.MonitorException += MonitorException;

                foreach (string reader in readerNames)
                {
                    Debug.WriteLine("Start monitoring for reader " + reader + ".");
                }

                monitor.Start(readerNames);

                //// Let the program run until the user presses a key
                //Console.ReadKey();

                //// Stop monitoring
                //monitor.Cancel();
            }
            catch (PCSCException ex)
            {
                mainForm.Invoke(mainForm.delegateReaderError, ex);
            }
            catch (Exception ex)
            {
                mainForm.Invoke(mainForm.delegateReaderError, ex);
            }
        }

        private static string GetUID(string readerName)
        {
            try
            {
                IsoReader isoReader = new IsoReader(context, readerName, SCardShareMode.Shared, SCardProtocol.Any, false);
                var card = new MifareCard(isoReader);
                byte[] UIDBytes = card.GetUID();
                string uniqueID = string.Join("-", UIDBytes);
                return uniqueID;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex: " + ex.ToString());
                return "";
            }
            
        }


        private static void CardInsertedEvent(string eventName, CardStatusEventArgs unknown)
        {
            string uniqueID = GetUID(unknown.ReaderName);
            mainForm.Invoke(mainForm.delegateDetectCardID, uniqueID);
            Debug.WriteLine("UUID = " + uniqueID);
        }

        private static void DisplayEvent(string eventName, CardStatusEventArgs unknown)
        {
            //Debug.WriteLine(">> {0} Event for reader: {1}", eventName, unknown.ReaderName);
            //Debug.WriteLine("ATR: {0}", BitConverter.ToString(unknown.Atr ?? new byte[0]));
            //Debug.WriteLine("State: {0}\n", unknown.State);
        }

        private static void StatusChanged(object sender, StatusChangeEventArgs args)
        {
            //Debug.WriteLine(">> StatusChanged Event for reader: {0}", args.ReaderName);
            //Debug.WriteLine("ATR: {0}", BitConverter.ToString(args.Atr ?? new byte[0]));
            //Debug.WriteLine("Last state: {0}\nNew state: {1}\n", args.LastState, args.NewState);
        }

        private static void MonitorException(object sender, PCSCException ex)
        {
            Debug.WriteLine("Monitor exited due an error:");
            Debug.WriteLine(SCardHelper.StringifyError(ex.SCardError));
            monitor.Cancel();
            mainForm.Invoke(mainForm.delegateReaderError, ex);
        }
    }
}
