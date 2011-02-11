using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace OAMS.Models
{
    public class AppSettingRepository : BaseRepository
    {
        public void Reload()
        {
            Type type = typeof(AppSetting);
            List<AppSetting> list = DB.AppSettings.ToList();

            var l = type
                .GetMembers(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty)
                .Where(r => r is PropertyInfo)
                .Select(r => r as PropertyInfo);

            foreach (var item in l)
            {
                var value = list.Where(r => r.Key == item.Name).Select(r => r.Value).FirstOrDefault();
                object oVal = value;
                if (item.PropertyType == typeof(Guid))
                {
                    oVal = value.ToGuid();
                }
                else if (item.PropertyType == typeof(int))
                {
                    oVal = value.ToInt();
                }
                item.SetValue(type, oVal, null);
            }

            GeoRepository geoRepository = new GeoRepository();
            AppSetting.DefaultGeo1Name = geoRepository.GetName(AppSetting.DefaultGeoID);            
        }

        public void InsertOrUpdate(string key, string value)
        {
            var v = DB.AppSettings.Where(r => r.Key == key).FirstOrDefault();
            if (v != null)
            {
                v.Value = value;
            }
            else
            {
                v = new AppSetting() { Key = key, Value = value };
                DB.AppSettings.AddObject(v);
            }

            DB.SaveChanges();
            Reload();
        }
    }
}