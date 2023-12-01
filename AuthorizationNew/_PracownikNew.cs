using DataWizPro.DataServices;
using DataWizPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataWizPro.AuthorizationNew
{
    public class GenericFilter<T>
    {
        // Method to find an object based on a condition defined by the provided Func
        public T FindFirst(List<T> items, Func<T, bool> condition)
        {
            return items.FirstOrDefault(condition);
        }
    }

    //public class MenedzerL2
    //{
    //    List<MenagerAuthorization> _list;

    //    public MenedzerL2(List<MenagerAuthorization> list)
    //    {
    //        _list = list;
    //    }

    //    public MenagerAuthorization GetMenedzerL2(string pi, string cpk)
    //    {
    //        GenericFilter<MenagerAuthorization> genericFilter = new GenericFilter<MenagerAuthorization>();
    //        return genericFilter.FindFirst(_list, x => x.pi == pi && x.L2 == cpk);
    //    }
    //}

    public class MenedzerL2
    {
        List<MenagerAuthorization> _list;
        AuthorizationService authorizationService;

        public MenedzerL2(string pi)
        {
            authorizationService = new AuthorizationService();
            _list = authorizationService.GetAllRecordsOfManagerWithQuery(pi);
        }

        public MenagerAuthorization GetMenedzerL2(string pi, string cpk)
        {
            GenericFilter<MenagerAuthorization> genericFilter = new GenericFilter<MenagerAuthorization>();
            return genericFilter.FindFirst(_list, x => x.pi == pi && x.L2 == cpk);
        }
    }

    //public static class ReflectionUtils
    //{
    //    public static void AssignPropertiesToFields(object source, object target)
    //    {
    //        foreach (PropertyInfo property in source.GetType().GetProperties())
    //        {
    //            FieldInfo field = target.GetType().GetField(property.Name.ToLower(), BindingFlags.NonPublic | BindingFlags.Instance);
    //            if (field != null)
    //            {
    //                field.SetValue(target, property.GetValue(source));
    //            }
    //        }
    //    }
    //}

    public static class ReflectionUtils
    {
        public static void AssignPropertiesToFields(object source, object target)
        {
            // Get all fields of the target object in lowercase
            var targetFields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                                      .ToDictionary(f => f.Name.ToLower(), f => f);

            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                string lowerCasePropertyName = property.Name.ToLower();

                // Check if there's a matching field in the target object
                if (targetFields.TryGetValue(lowerCasePropertyName, out FieldInfo field))
                {
                    field.SetValue(target, property.GetValue(source));
                }
            }
        }
    }



    public class NewClassMenagerL2
    {
        AuthorizationService authorizationService;
        private string pi = null;
        private string imie = null;
        private string nazwisko = null;
        private string alias = null;
        private string L2 = null;
        private string l3 = null;
        private string L4 = null;

        public NewClassMenagerL2(string pi)
        {
            authorizationService = new AuthorizationService();
            MenagerAuthorization source = authorizationService.GetSpecificMenagerWithQueryPi(pi);
            ReflectionUtils.AssignPropertiesToFields(source, this);
        }

        public NewClassMenagerL2(string pi, string cpk)
        {
            authorizationService = new AuthorizationService();
        }

        public string getPi()
        {
            return pi;
        }
        public string getImie()
        {
            return imie;
        }
        public string getNazwisko()
        {
            return nazwisko;
        }
        public string getAlias()
        {
            return alias;
        }
        public string getL2()
        {
            return L2;
        }
        public string getL3()
        {
            return l3;
        }
        public string getL4()
        {
            return L4;
        }

        
    }
}
