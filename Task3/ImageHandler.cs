using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Task3
{
    public class ImageHandler : IHttpHandler
    {
        public bool IsReusable => true;

        public void ProcessRequest(HttpContext context)
        {
            Debug.WriteLine("ImageHandler are processing the request!");
            if (Convert.ToBoolean(HttpContext.Current.Items["isFrance"]) == true)
            {
                context.Response.Write("Bounjour my friend. Those kitty images are not for you.");
            }
            else
            {
                context.Response.WriteFile(context.Request.AppRelativeCurrentExecutionFilePath);
                context.Response.ContentType = "image/jpeg";
            }

        }
    }
}