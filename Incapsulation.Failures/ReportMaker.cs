using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public enum FailureType
    {
        unexpecte_shutdown = 0,
        short_non_responding = 1,
        hardware_failures = 2,
        connection_problems = 3
    }
    public class Device
    {
        public int DeviceID { get; set; }
        public string Name { get; set; }
        public Device(int Id, string name)
        {
            DeviceID = Id;
            Name = name;
        }
    }
    public class Failure
    {
        private FailureType FailureType { get; set; }
        public int DeviceId { get; set; }
        public DateTime Date { get; set; }
        public Failure(FailureType failure, int DeveciID, DateTime date)
        {
            FailureType = failure;
            DeviceId = DeveciID;
            Date = date;
        }
        public bool IsFailureSerious()
        {
            if (FailureType == FailureType.short_non_responding || FailureType == FailureType.connection_problems)
                return false;
            return true;
        }
        public bool Earlier(DateTime date)
        {
            if (Date.Year < date.Year) return true;
            if (Date.Year > date.Year) return false;
            if (Date.Month < date.Month) return true;
            if (Date.Month < date.Month) return false;
            if (Date.Day < date.Day) return true;
            return false;
        }
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="FailureType">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] FailureType, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            Failure[] failure = GetMassFailure(FailureType,deviceId, times).ToArray();
            DateTime date = new DateTime(year: year, month: month, day: day);
            Device[] devicesList = GetMassDevices(devices).ToArray();
            return FindDevicesFailedBeforeDate(date, failure, devicesList);
        }
        public static List<string> FindDevicesFailedBeforeDate(DateTime date, Failure[] FailureType, Device[] devices)
        {
            var problematicDevices = new HashSet<int>();
            int i = 0;
            foreach (var type in FailureType)
            {
                if (type.IsFailureSerious() && type.Earlier(date))
                    problematicDevices.Add(type.DeviceId);
                i++;
            }


            var result = new List<string>();
            foreach (var device in devices)
                if (problematicDevices.Contains(device.DeviceID))
                    result.Add(device.Name);

            return result;
        }
        //возвращаем перечисляемый массив классов failure
        public static IEnumerable<Failure> GetMassFailure(int[] FailureType, int[] deviceId, object[][] times)
        {
            for (int i = 0; i < FailureType.Length; i++)
            {
                yield return new Failure((FailureType)FailureType[i], deviceId[i], new DateTime(year: Convert.ToInt32(times[i][2]),
                                                                                     month: Convert.ToInt32(times[i][1]),
                                                                                      day: Convert.ToInt32(times[i][0])));
            }
            yield break;
        }


        //возвращаем перечисляемый массив классов Device
        public static IEnumerable<Device> GetMassDevices(List<Dictionary<string, object>> devices)
        {
            foreach(var device in devices)
            {
                yield return new Device((int)device["DeviceId"], device["Name"] as string);
            }
            yield break;
        }
    }
}
