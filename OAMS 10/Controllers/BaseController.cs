using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAMS.Models;
using System.Web.Mvc;

namespace OAMS.Controllers
{
    public class BaseController<T> : Controller
        where T : new()
    {
        private T _repo = new T();
        public T Repo
        {
            get { return _repo; }
        }
    }
}