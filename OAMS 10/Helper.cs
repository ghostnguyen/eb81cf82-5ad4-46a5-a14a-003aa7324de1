using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
namespace OAMS
{
    public class Helper
    {
        public static double DistanceBetweenPoints(double? lat1, double? lng1, double? lat2, double? lng2)
        {
            if (lat1.HasValue && lng1.HasValue && lat2.HasValue && lng2.HasValue)
            { }
            else
            {
                return Double.MaxValue;
            }


            double R = 6371; // Radius of the Earth in km
            double dLat = (lat2.Value - lat1.Value) * Math.PI / 180;
            double dLon = (lng2.Value - lng1.Value) * Math.PI / 180;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(lat1.Value * Math.PI / 180) * Math.Cos(lat2.Value * Math.PI / 180) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c;

            return d;
        }
    }

    public class OAMSSetting
    {
        static public Message messageL = new Message();
        static public string GoogleUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["GoogleUsername"];
            }
        }

        static public string GooglePassword
        {
            get
            {
                return ConfigurationManager.AppSettings["GooglePassword"];
            }
        }

        static public bool ByPassLogin
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["ByPassLogin"]);
            }
        }

        public static string Username
        {
            get {
                return HttpContext.Current.User.Identity.Name; 
            }
        }

        public static Guid HCMC_ID
        {
            get
            {
                return Guid.Parse(ConfigurationManager.AppSettings["HCMC_ID"]);
            }
        }


    }

    public class Message
    {
        public string SelectNone { get { return "-- None --"; } }
        public string SelectAll { get { return "-- All --"; } }
    }

    public class MyMethodBase
    {
        int _level;

        public string Name
        {
            get
            {
                StackTrace stackTrace = new StackTrace();

                try
                {
                    MethodBase m = stackTrace.GetFrame(_level).GetMethod();
                    string name = m.DeclaringType.Name + "." + m.Name;

                    return name;
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public MyMethodBase(int level)
        {
            _level = level;
        }

        public static MyMethodBase Current
        {
            get
            {
                return new MyMethodBase(1);
            }
        }

        public MyMethodBase Caller
        {
            get
            {
                return new MyMethodBase(_level + 1);
            }
        }
    }

    //public class Nameof<T>
    //{
    //    public static string Property<TProp>(Expression<Func<T, TProp>> expression)
    //    {
    //        var body = expression.Body as MemberExpression;
    //        if (body == null)
    //            throw new ArgumentException("'expression' should be a member expression");
    //        return body.Member.Name;
    //    }
    //}

    /// <summary>
    /// Gets property name using lambda expressions.
    /// </summary>
    public class PropertyName
    {
        public static string For<T>(
            Expression<Func<T, object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }

        public static string For(
            Expression<Func<object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }

        public static string GetMemberName(
            Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Expression.NodeType ==
                    ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression)
                        + "."
                        + memberExpression.Member.Name;
                }
                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format(
                        "Cannot interpret member from {0}",
                        expression));

                return GetMemberName(unaryExpression.Operand);
            }

            throw new Exception(string.Format(
                "Could not determine member from {0}",
                expression));
        }
    }
}