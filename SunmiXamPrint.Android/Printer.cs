using Android.Bluetooth;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunmiXamPrint.Droid
{
    public class Printer
    {
        public async Task PrintQR(string content, BluetoothDevice device)
        {
            try
            {                
                using (BluetoothSocket socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync();
                    await socket.OutputStream.WriteAsync(new byte[] { 0x1B, 0x61, 0x01 }, 0, 3);
                    // Write content
                    byte[] qrBytes = System.Text.Encoding.ASCII.GetBytes(content);
                    int dataLength = qrBytes.Length + 3;
                    byte dataPL = (byte)(dataLength % 256);
                    byte dataPH = (byte)(dataLength / 256);
                    var bytes = new List<byte>();

                    bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x04, 0x00, 0x31, 0x41, 0x32, 0x00 }); // Select model
                    bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x43, 0x08 });  // Set module size (8)
                    bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x45, 0x30 });  // Set error correction
                    bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, dataPL, dataPH, 0x31, 0x50, 0x30 }); // Start store qr data.
                    bytes.AddRange(qrBytes);
                    bytes.AddRange(new byte[] { 0x1D, 0x28, 0x6B, 0x03, 0x00, 0x31, 0x51, 0x30 }); // Print qr data from previous 80 code.
                    await socket.OutputStream.WriteAsync(bytes.ToArray(), 0, bytes.Count); 
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PrintText(byte[]  buffer,BluetoothDevice device)
        {
            try
            {                
                using (BluetoothSocket socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb")))
                {
                    await socket.ConnectAsync();    
                    await socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                    socket.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}