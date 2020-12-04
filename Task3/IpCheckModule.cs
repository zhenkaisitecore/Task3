using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Task3
{
    public class IpCheckModule : IHttpModule
    {
        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += CheckIP;
        }

        private void CheckIP(object sender, EventArgs args)
        {
            string ipaddr = HttpContext.Current.Request.Headers["X-Forwarded-For"].ToString();
            int france_ip_start = ConvertIPToID("2.0.0.0");
            int france_ip_end = ConvertIPToID("2.16.0.255");
            int yourip = ConvertIPToID(ipaddr);
            string requestExtension = VirtualPathUtility.GetExtension(HttpContext.Current.Request.RawUrl);
            HttpContext.Current.Items.Add("isFrance", IsFrance(yourip));

            //if (IsImage(requestExtension) && IsFrance(yourip))
            //{
            //    Debug.WriteLine("You are from France and you cannot view the picture!");
            //    HttpContext.Current.ApplicationInstance.CompleteRequest();
            //    HttpContext.Current.Response.End();
            //}
        }

        private bool IsFrance(int yourip) => ConvertIPToID("2.0.0.0") <= yourip && yourip <= ConvertIPToID("2.16.0.255");

        private int ConvertIPToID(string ipaddr)
        {
            int id = 0;
            int pow = 3;
            int octectInt = 0;
            foreach (string octectStr in ipaddr.Split('.'))
            {
                if (!int.TryParse(octectStr, out octectInt))
                {
                    throw new ArgumentException("Invalid IP address");
                }

                id += octectInt * Convert.ToInt32(Math.Pow(256, pow));
                pow--;
            }

            return id;
        }

        private bool IsImage(string extension)
        {
            string[] extensions = new string[] 
            {
                ".jpg", ".png", ".jpeg"
            };

            foreach (var ext in extensions)
            {
                if (ext == extension) return true;
            }

            return false;
        }
    }
}