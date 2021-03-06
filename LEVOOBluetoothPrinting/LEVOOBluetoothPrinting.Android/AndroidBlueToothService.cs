using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Bluetooth;
using Java.Util;
using LEVOOBluetoothPrinting.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidBlueToothService))]

namespace LEVOOBluetoothPrinting.Droid
{
    public class AndroidBlueToothService : IBlueToothService
    {
        BluetoothAdapter bluetoothAdapter = BluetoothAdapter.DefaultAdapter;

        public IList<string> GetDeviceList()
        {

            var btdevice = bluetoothAdapter?.BondedDevices
            .Select(i => i.Name).ToList();
            return btdevice;

        }

        public async Task Print(string deviceName, string text)
        {

            BluetoothDevice device = (from bd in bluetoothAdapter?.BondedDevices
                                      where bd?.Name == deviceName
                                      select bd).FirstOrDefault();
            try
            {
                BluetoothSocket bluetoothSocket = device?.
                    CreateRfcommSocketToServiceRecord(
                    UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));

                bluetoothSocket?.Connect();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                await bluetoothSocket?.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                bluetoothSocket.Close();

            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);

                throw exp;
            }
        }
    }
}