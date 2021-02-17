using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public enum FailureTypes
    {
        unexpecte_shutdown = 0,
        short_non_responding = 1,
        hardware_failures = 2,
        connection_problems = 3
    }
    public class Divece
    {
        public int DiveceID { get; set; }
        public string Name { get; set; }
        public Divece(int Id, string name)
        {
            DiveceID = Id;
            Name = name;
        }
    }
    public class Failure
    {
        private FailureTypes FailureType { get; set; }
        public int DeviceId { get; set; }
        public DateTime Date { get; set; }
        public Failure(FailureTypes failure, int DeveciID, DateTime date)
        {
            FailureType = failure;
            DeviceId = DeveciID;
            Date = date;
        }
        public bool IsFailureSerious()
        {
            if (FailureType == FailureTypes.short_non_responding || FailureType == FailureTypes.connection_problems)
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
        /// <param name="failureTypes">
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
            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            Failure[] failure = GetMassFailure(failureTypes,deviceId, times).ToArray();
            DateTime date = new DateTime(year: year, month: month, day: day);
            Divece[] devicesList = GetMassDiveces(devices).ToArray();
            return FindDevicesFailedBeforeDate(date, failure, devicesList);
        }
        public static List<string> FindDevicesFailedBeforeDate(DateTime date, Failure[] failureTypes, Divece[] devices)
        {
            var problematicDevices = new HashSet<int>();
            int i = 0;
            foreach (var type in failureTypes)
            {
                if (type.IsFailureSerious() && type.Earlier(date))
                    problematicDevices.Add(type.DeviceId);
                i++;
            }


            var result = new List<string>();
            foreach (var device in devices)
                if (problematicDevices.Contains(device.DiveceID))
                    result.Add(device.Name);

            return result;
        }
        //возвращаем перечисляемый массив классов failure
        public static IEnumerable<Failure> GetMassFailure(int[] failureTypes, int[] deviceId, object[][] times)
        {
            for (int i = 0; i < failureTypes.Length; i++)
            {
                yield return new Failure((FailureTypes)failureTypes[i], deviceId[i], new DateTime(year: Convert.ToInt32(times[i][2]),
                                                                                     month: Convert.ToInt32(times[i][1]),
                                                                                      day: Convert.ToInt32(times[i][0])));
            }
            yield break;
        }


        //возвращаем перечисляемый массив классов Divece
        public static IEnumerable<Divece> GetMassDiveces(List<Dictionary<string, object>> devices)
        {
            foreach(var device in devices)
            {
                yield return new Divece((int)device["DeviceId"], device["Name"] as string);
            }
            yield break;
        }
    }
}
